using CapaDat;
using Entidad;

namespace CapaNegocio
{
    public class GestorElecciones
    {
        private DAL dal = new DAL();

        // ASIGNACIONMESA
        public (string estado, Mesa? mesa, List<Candidato>? candidatos) AsignarMesa(DateTime fecha, string localidad)
        {
            try
            {
                int localidadId = ObtenerLocalidadId(localidad);

                // Delegar completamente la asignación a DAL
                var resultado = dal.InsertarMesaVerificada(fecha, localidadId);

                if (!resultado.exito)
                    return (resultado.mensaje, null, null);

                List<Candidato> candidatos = ObtenerCandidatos();

                return ("OK", resultado.mesa, candidatos);
            }
            catch (Exception ex)
            {
                return ($"ERROR: {ex.Message}", null, null);
            }
        }


        // REGISTRODATOS
        public string RegistrarDatos(int numeroMesa, Dictionary<int, int> votos, int blancos, int nulos)
        {
            try
            {
                var mesa = dal.MostrarMesas().FirstOrDefault(m => m.Numero == numeroMesa);
                if (mesa == null)
                    return "ERROR: Mesa no encontrada.";

                int totalVotosEmitidos = votos.Values.Sum() + blancos + nulos;
                if (totalVotosEmitidos > mesa.VotantesAsignados)
                    return "ERROR: Número de votos excede el número asignado de votantes.";

                var resultados = votos.Select(v => new Resultado
                {
                    MesaID = mesa.MesaID,
                    CandidatoID = v.Key,
                    Votos = v.Value
                }).ToList();

                dal.InsertarResultados(resultados);

                // Aquí podrías insertar votos blancos y nulos si tienes la lógica correspondiente

                return "OK";
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }

        // CIERREMESA
        public string CerrarMesaPorId(int mesaId)
        {
            var mesa = dal.MostrarMesas().FirstOrDefault(m => m.MesaID == mesaId);
            if (mesa == null)
                return "ERROR: Mesa no encontrada.";

            if (mesa.Cerrada)
                return "CERRADA";

            dal.CerrarMesaPorId(mesaId);
            return "OK";
        }


        // Método auxiliar para obtener el id de localidad por nombre (debería estar en DAL también)
        public int ObtenerLocalidadId(string nombreLocalidad)
        {
            var localidades = MostrarLocalidades();
            var localidad = localidades.FirstOrDefault(l => l.Nombre.Equals(nombreLocalidad, StringComparison.OrdinalIgnoreCase));
            if (localidad == null)
                throw new Exception("Localidad no encontrada.");
            return localidad.Id;
        }

        // Método auxiliar para obtener candidatos (debería estar implementado en DAL)
        private List<Candidato> ObtenerCandidatos()
        {
            List<Candidato> candidatos = dal.MostrarCandidatos();
            if (candidatos == null || candidatos.Count == 0)
            {
                throw new Exception("No se encontraron candidatos.");
            }
            return candidatos;
        }

        //Mostrar Localidades
        public List<Localidad> MostrarLocalidades()
        {
            DAL dal = new DAL();
            List<Localidad> localidades = dal.MostrarLocalidades();
            if (localidades == null || localidades.Count == 0)
            {
                throw new Exception("No se encontraron localidades.");
            }
            return localidades;

        }

        public Usuario? IniciarSesionUsuario(string cedula, string contraseña)
        {
            return dal.IniciarSesion(cedula, contraseña, false);
        }

        public Usuario? IniciarSesionAdmin(string usuario, string contraseña)
        {
            return dal.IniciarSesion(usuario, contraseña, true);
        }
        public Mesa? ObtenerMesaAsignada(int usuarioId)
        {
            return dal.ObtenerMesaPorUsuarioId(usuarioId);
        }

        public string ObtenerNombreLocalidad(int localidadId)
        {
            return dal.ObtenerNombreLocalidad(localidadId);
        }

        public string RegistrarDatosPorMesaId(int mesaId, Dictionary<int, int> votosPorCandidato, int blancos, int nulos)
        {
            return dal.RegistrarVotacionPorMesaId(mesaId, votosPorCandidato, blancos, nulos);
        }

        public Mesa? ObtenerMesaPorId(int mesaId)
        {
            return dal.MostrarMesas().FirstOrDefault(m => m.MesaID == mesaId);
        }

        private DALStadistics dalStats = new DALStadistics();

        public Dictionary<string, int> ObtenerEstadisticas()
        {
            return dalStats.ObtenerTotalesVotos();
        }

        public string ConsultarMesa(int numeroMesa)
        {
            var mesa = dal.MostrarMesas().FirstOrDefault(m => m.Numero == numeroMesa);
            if (mesa == null)
                return "ERROR_MESA_NO_EXISTE";

            var resultados = dal.ObtenerResultadosPorMesa(mesa.MesaID);
            var votosEspeciales = dal.ObtenerVotosEspecialesPorMesa(mesa.MesaID);

            if (!resultados.Any() && votosEspeciales == null)
                return "SIN_DATOS";

            int votosCandidato1 = resultados.Where(r => r.CandidatoID == 1).Sum(r => r.Votos);
            int votosCandidato2 = resultados.Where(r => r.CandidatoID == 2).Sum(r => r.Votos);
            int blancos = votosEspeciales?.VotosBlancos ?? 0;
            int nulos = votosEspeciales?.VotosNulos ?? 0;

            int totalVotados = votosCandidato1 + votosCandidato2 + blancos + nulos;
            int ausentismo = mesa.VotantesAsignados - totalVotados;
            string estado = mesa.Cerrada ? "CERRADA" : "ABIERTA";

            return $"{mesa.VotantesAsignados} {votosCandidato1} {votosCandidato2} {blancos} {nulos} {ausentismo} {estado}";
        }


    }
}

