using Entidad;
using Microsoft.Data.SqlClient;

namespace CapaDat
{
    public class DAL
    {
        private ConexionBD conexion = new ConexionBD();

        // Método Mostrar todas las mesas
        public List<Mesa> MostrarMesas()
        {
            List<Mesa> mesas = new List<Mesa>();

            using (SqlCommand comando = new SqlCommand())
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "SELECT * FROM Mesa";

                using (SqlDataReader leer = comando.ExecuteReader())
                {
                    while (leer.Read())
                    {
                        mesas.Add(new Mesa
                        {
                            MesaID = leer.GetInt32(0),
                            Numero = leer.GetInt32(1),
                            LocalidadID = leer.GetInt32(2),
                            Fecha = leer.GetDateTime(3),
                            Cerrada = leer.GetBoolean(4),
                            VotantesAsignados = leer.GetInt32(5)
                        });
                    }
                }
                conexion.CerrarConexion();
            }
            return mesas;
        }
        //Método para Obtener los candidatos
        public List<Candidato> MostrarCandidatos()
        {
            List<Candidato> candidatos = new List<Candidato>();
            using (SqlCommand comando = new SqlCommand())
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "SELECT * FROM Candidato";
                using (SqlDataReader leer = comando.ExecuteReader())
                {
                    while (leer.Read())
                    {
                        candidatos.Add(new Candidato
                        {
                            CandidatoID = leer.GetInt32(0),
                            Nombre = leer.GetString(1)
                        });
                    }
                }
                conexion.CerrarConexion();
            }
            return candidatos;
        }

        // Método Insertar mesa nueva
        public (bool exito, Mesa? mesa, string mensaje) InsertarMesaVerificada(DateTime fecha, int localidadId)
        {
            try
            {
                string codigoProvincia = ObtenerCodigoProvinciaPorLocalidadId(localidadId);

                // Paso 1: Obtener un Usuario no asignado cuya cédula comience con el código de provincia
                int? usuarioId = null;

                using (SqlCommand buscar = new SqlCommand())
                {
                    buscar.Connection = conexion.AbrirConexion();
                    buscar.CommandText = @"
                    SELECT TOP 1 u.UsuarioID
                    FROM Usuario u
                    LEFT JOIN Mesa m ON u.UsuarioID = m.UsuarioID
                    WHERE m.UsuarioID IS NULL
                      AND LEFT(u.Cedula, 2) = @codigoProvincia
                      AND u.EsAdmin = 0";

                    buscar.Parameters.AddWithValue("@codigoProvincia", codigoProvincia);
                    object resultado = buscar.ExecuteScalar();
                    conexion.CerrarConexion();

                    if (resultado == null)
                        return (false, null, "ERROR: No hay usuarios disponibles para esta localidad.");

                    usuarioId = Convert.ToInt32(resultado);
                }

                // Paso 2: Verificar cuántas mesas hay ya
                int cantidadMesas = 0;
                int? maxNumero = null;

                using (SqlCommand verificar = new SqlCommand())
                {
                    verificar.Connection = conexion.AbrirConexion();
                    verificar.CommandText = @"
                    SELECT COUNT(*), MAX(Numero)
                    FROM Mesa
                    WHERE LocalidadID = @LocalidadID AND Fecha = @Fecha";

                    verificar.Parameters.AddWithValue("@LocalidadID", localidadId);
                    verificar.Parameters.AddWithValue("@Fecha", fecha);

                    using (SqlDataReader reader = verificar.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cantidadMesas = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                maxNumero = reader.GetInt32(1);
                        }
                    }
                    conexion.CerrarConexion();
                }

                if (cantidadMesas >= 3)
                    return (false, null, "ERROR: Ya existen 3 mesas asignadas para esta localidad.");

                int nuevoNumero = (maxNumero ?? 0) + 1;

                using (SqlCommand insertar = new SqlCommand())
                {
                    insertar.Connection = conexion.AbrirConexion();
                    insertar.CommandText = @"
                    INSERT INTO Mesa (Numero, LocalidadID, Fecha, Cerrada, VotantesAsignados, UsuarioID)
                    VALUES (@Numero, @LocalidadID, @Fecha, 0, 100, @UsuarioID)";

                    insertar.Parameters.AddWithValue("@Numero", nuevoNumero);
                    insertar.Parameters.AddWithValue("@LocalidadID", localidadId);
                    insertar.Parameters.AddWithValue("@Fecha", fecha);
                    insertar.Parameters.AddWithValue("@UsuarioID", usuarioId);

                    insertar.ExecuteNonQuery();
                    conexion.CerrarConexion();
                }

                Mesa nuevaMesa = new Mesa
                {
                    Numero = nuevoNumero,
                    LocalidadID = localidadId,
                    Fecha = fecha,
                    Cerrada = false,
                    VotantesAsignados = 100,
                    UsuarioID = usuarioId.Value
                };

                return (true, nuevaMesa, "Mesa insertada y usuario asignado correctamente.");
            }
            catch (Exception ex)
            {
                conexion.CerrarConexion();
                return (false, null, $"ERROR: {ex.Message}");
            }
        }

        // Método auxiliar para traducir localidadId a código de provincia para la cédula
        private string ObtenerCodigoProvinciaPorLocalidadId(int localidadId)
        {
            return localidadId switch
            {
                1 => "17", // Quito - Pichincha
                2 => "09", // Guayaquil - Guayas
                3 => "01", // Cuenca - Azuay
                _ => throw new Exception("Localidad no válida para asignación automática.")
            };
        }



        // Método para cerrar una mesa
        public void CerrarMesaPorId(int mesaId)
        {
            using (SqlCommand comando = new SqlCommand())
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "UPDATE Mesa SET Cerrada = 1 WHERE MesaID = @MesaID";
                comando.Parameters.AddWithValue("@MesaID", mesaId);
                comando.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
        }


        // Método Insertar resultados de votación
        public void InsertarResultados(List<Resultado> resultados)
        {
            using (SqlCommand comando = new SqlCommand())
            {
                comando.Connection = conexion.AbrirConexion();

                foreach (var res in resultados)
                {
                    comando.Parameters.Clear();  // Limpiar parámetros antes de cada inserción
                    comando.CommandText = "INSERT INTO Resultado (MesaID, CandidatoID, Votos) " +
                                          "VALUES (@MesaID, @CandidatoID, @Votos)";

                    comando.Parameters.AddWithValue("@MesaID", res.MesaID);
                    comando.Parameters.AddWithValue("@CandidatoID", res.CandidatoID);
                    comando.Parameters.AddWithValue("@Votos", res.Votos);

                    comando.ExecuteNonQuery();
                }

                conexion.CerrarConexion();
            }
        }

        //Método para Mostrar las localidades
        public List<Localidad> MostrarLocalidades()
        {
            List<Localidad> localidades = new List<Localidad>();
            using (SqlCommand comando = new SqlCommand())
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "SELECT * FROM Localidad";
                using (SqlDataReader leer = comando.ExecuteReader())
                {
                    while (leer.Read())
                    {
                        localidades.Add(new Localidad
                        {
                            Id = leer.GetInt32(0),
                            Nombre = leer.GetString(1)
                        });
                    }
                }
                conexion.CerrarConexion();
            }
            return localidades;
        }

        public Usuario? IniciarSesion(string identificador, string contraseña, bool esAdmin)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion.AbrirConexion();

                if (esAdmin)
                {
                    cmd.CommandText = @"
                SELECT * FROM Usuario
                WHERE Usuario = @identificador AND Contraseña = @contraseña AND EsAdmin = 1";
                }
                else
                {
                    cmd.CommandText = @"
                SELECT * FROM Usuario
                WHERE Cedula = @identificador AND Contraseña = @contraseña AND EsAdmin = 0";
                }

                cmd.Parameters.AddWithValue("@identificador", identificador);
                cmd.Parameters.AddWithValue("@contraseña", contraseña);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Usuario user = new Usuario
                        {
                            UsuarioID = reader.GetInt32(0),
                            Cedula = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            Correo = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            UsuarioNombre = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Contrasena = reader.GetString(4),
                            EsAdministrador = reader.GetBoolean(5)
                        };
                        return user;
                    }
                }

                conexion.CerrarConexion();
                return null;
            }
        }

        public Mesa? ObtenerMesaPorUsuarioId(int usuarioId)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = @"SELECT * FROM Mesa WHERE UsuarioID = @UsuarioID";
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Mesa
                        {
                            MesaID = reader.GetInt32(0),
                            Numero = reader.GetInt32(1),
                            LocalidadID = reader.GetInt32(2),
                            Fecha = reader.GetDateTime(3),
                            Cerrada = reader.GetBoolean(4),
                            VotantesAsignados = reader.GetInt32(5)
                        };
                    }
                }
                return null;
            }
        }

        public string ObtenerNombreLocalidad(int localidadId)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "SELECT Nombre FROM Localidad WHERE LocalidadID = @id";
                cmd.Parameters.AddWithValue("@id", localidadId);

                object? resultado = cmd.ExecuteScalar();
                conexion.CerrarConexion();

                return resultado != null ? resultado.ToString()! : "Desconocida";
            }
        }

        public string RegistrarVotacionPorMesaId(int mesaId, Dictionary<int, int> nuevosVotosPorCandidato, int nuevosBlancos, int nuevosNulos)
        {
            try
            {
                int votantesAsignados;

                // Obtener total de votantes asignados a la mesa
                using (SqlCommand cmd = new SqlCommand("SELECT VotantesAsignados FROM Mesa WHERE MesaID = @mesaId", conexion.AbrirConexion()))
                {
                    cmd.Parameters.AddWithValue("@mesaId", mesaId);
                    object result = cmd.ExecuteScalar();
                    conexion.CerrarConexion();

                    if (result == null)
                        return "ERROR: Mesa no encontrada.";

                    votantesAsignados = Convert.ToInt32(result);
                }

                // Obtener suma actual de votos por candidatos ya registrados
                int votosCandidatosExistentes = 0;
                using (SqlCommand cmd = new SqlCommand("SELECT SUM(Votos) FROM Resultado WHERE MesaID = @mesaId", conexion.AbrirConexion()))
                {
                    cmd.Parameters.AddWithValue("@mesaId", mesaId);
                    object result = cmd.ExecuteScalar();
                    votosCandidatosExistentes = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    conexion.CerrarConexion();
                }

                // Obtener votos blancos y nulos ya registrados (si existen)
                int blancosExistentes = 0, nulosExistentes = 0;
                using (SqlCommand cmd = new SqlCommand("SELECT VotosBlancos, VotosNulos FROM VotosEspeciales WHERE MesaID = @mesaId", conexion.AbrirConexion()))
                {
                    cmd.Parameters.AddWithValue("@mesaId", mesaId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            blancosExistentes = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            nulosExistentes = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                        }
                    }
                    conexion.CerrarConexion();
                }

                // Calcular total actual
                int votosExistentesTotales = votosCandidatosExistentes + blancosExistentes + nulosExistentes;

                // Calcular total que se desea añadir
                int nuevosVotosTotales = nuevosVotosPorCandidato.Values.Sum() + nuevosBlancos + nuevosNulos;

                // Validación final
                if (votosExistentesTotales + nuevosVotosTotales > votantesAsignados)
                {
                    return "ERROR: Los votos totales exceden el número asignado de votantes para esta mesa.";
                }

                // Insertar votos por candidato
                foreach (var kvp in nuevosVotosPorCandidato)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Resultado (MesaID, CandidatoID, Votos) VALUES (@mesaID, @candidatoID, @votos)", conexion.AbrirConexion()))
                    {
                        cmd.Parameters.AddWithValue("@mesaID", mesaId);
                        cmd.Parameters.AddWithValue("@candidatoID", kvp.Key);
                        cmd.Parameters.AddWithValue("@votos", kvp.Value);
                        cmd.ExecuteNonQuery();
                        conexion.CerrarConexion();
                    }
                }

                // Insertar o actualizar votos especiales
                using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM VotosEspeciales WHERE MesaID = @mesaID", conexion.AbrirConexion()))
                {
                    checkCmd.Parameters.AddWithValue("@mesaID", mesaId);
                    int existe = (int)checkCmd.ExecuteScalar();
                    conexion.CerrarConexion();

                    if (existe > 0)
                    {
                        // Actualizar sumando a lo existente
                        using (SqlCommand updateCmd = new SqlCommand(@"
                    UPDATE VotosEspeciales
                    SET VotosBlancos = VotosBlancos + @blancos,
                        VotosNulos = VotosNulos + @nulos
                    WHERE MesaID = @mesaID", conexion.AbrirConexion()))
                        {
                            updateCmd.Parameters.AddWithValue("@mesaID", mesaId);
                            updateCmd.Parameters.AddWithValue("@blancos", nuevosBlancos);
                            updateCmd.Parameters.AddWithValue("@nulos", nuevosNulos);
                            updateCmd.ExecuteNonQuery();
                            conexion.CerrarConexion();
                        }
                    }
                    else
                    {
                        using (SqlCommand insertCmd = new SqlCommand("INSERT INTO VotosEspeciales (MesaID, VotosBlancos, VotosNulos) VALUES (@mesaID, @blancos, @nulos)", conexion.AbrirConexion()))
                        {
                            insertCmd.Parameters.AddWithValue("@mesaID", mesaId);
                            insertCmd.Parameters.AddWithValue("@blancos", nuevosBlancos);
                            insertCmd.Parameters.AddWithValue("@nulos", nuevosNulos);
                            insertCmd.ExecuteNonQuery();
                            conexion.CerrarConexion();
                        }
                    }
                }

                return "OK";
            }
            catch (Exception ex)
            {
                conexion.CerrarConexion();
                return $"ERROR: {ex.Message}";
            }
        }

        public List<Resultado> ObtenerResultadosPorMesa(int mesaId)
        {
            List<Resultado> lista = new();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Resultado WHERE MesaID = @mesaId", conexion.AbrirConexion()))
            {
                cmd.Parameters.AddWithValue("@mesaId", mesaId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Resultado
                        {
                            MesaID = (int)reader["MesaID"],
                            CandidatoID = (int)reader["CandidatoID"],
                            Votos = (int)reader["Votos"]
                        });
                    }
                }
                conexion.CerrarConexion();
            }
            return lista;
        }

        public VotosEspeciales? ObtenerVotosEspecialesPorMesa(int mesaId)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM VotosEspeciales WHERE MesaID = @mesaId", conexion.AbrirConexion()))
            {
                cmd.Parameters.AddWithValue("@mesaId", mesaId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new VotosEspeciales
                        {
                            MesaID = (int)reader["MesaID"],
                            VotosBlancos = (int)reader["VotosBlancos"],
                            VotosNulos = (int)reader["VotosNulos"]
                        };
                    }
                }
                conexion.CerrarConexion();
            }
            return null;
        }


    }
}
