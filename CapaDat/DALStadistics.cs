using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CapaDat
{
    public class DALStadistics
    {
        private ConexionBD conexion = new ConexionBD();

        /*public Dictionary<string, int> ObtenerTotalesVotos()
        {
            var resultados = new Dictionary<string, int>();
            int totalAsignados = 0;
            int totalVotosEmitidos = 0;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = @"
            SELECT c.Nombre, SUM(r.Votos) 
            FROM Resultado r
            INNER JOIN Candidato c ON c.CandidatoID = r.CandidatoID
            GROUP BY c.Nombre";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var nombre = reader.GetString(0);
                        var votos = reader.GetInt32(1);
                        resultados[nombre] = votos;
                        totalVotosEmitidos += votos;
                    }
                }

                conexion.CerrarConexion();
            }

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = @"SELECT VotosBlancos, VotosNulos FROM VotosEspeciales";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int blancos = 0, nulos = 0;
                    while (reader.Read())
                    {
                        blancos += reader.GetInt32(0);
                        nulos += reader.GetInt32(1);
                    }

                    resultados["Votos Blancos"] = blancos;
                    resultados["Votos Nulos"] = nulos;
                    totalVotosEmitidos += blancos + nulos;
                }

                conexion.CerrarConexion();
            }

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = @"SELECT SUM(TotalVotantes) FROM Mesa";

                object result = cmd.ExecuteScalar();
                totalAsignados = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                conexion.CerrarConexion();
            }

            int ausentismo = totalAsignados - totalVotosEmitidos;
            if (ausentismo < 0) ausentismo = 10;

            resultados["Ausentismo"] = ausentismo;

            return resultados;
        }*/
        public Dictionary<string, int> ObtenerTotalesVotos()
        {
            var resultados = new Dictionary<string, int>();
            int totalVotosEmitidos = 0;

            // 1. Obtener votos por candidato
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = @"
                SELECT c.Nombre, SUM(r.Votos) 
                FROM Resultado r
                INNER JOIN Candidato c ON c.CandidatoID = r.CandidatoID
                GROUP BY c.Nombre";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var nombre = reader.GetString(0);
                        var votos = reader.GetInt32(1);
                        resultados[nombre] = votos;
                        totalVotosEmitidos += votos;
                    }
                }

                conexion.CerrarConexion();
            }

            // 2. Obtener votos blancos y nulos
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = @"SELECT VotosBlancos, VotosNulos FROM VotosEspeciales";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int blancos = 0, nulos = 0;
                    while (reader.Read())
                    {
                        blancos += reader.GetInt32(0);
                        nulos += reader.GetInt32(1);
                    }

                    resultados["Votos Blancos"] = blancos;
                    resultados["Votos Nulos"] = nulos;
                    totalVotosEmitidos += blancos + nulos;
                }

                conexion.CerrarConexion();
            }

            // 3. Obtener ausentismo por mesa y calcular total
            int ausentismoTotal = 0;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = @"
                SELECT 
                    m.MesaID,
                    m.TotalVotantes,
                    ISNULL(SUM(r.Votos), 0) AS VotosEmitidos,
                    ISNULL(ve.VotosBlancos, 0) AS Blancos,
                    ISNULL(ve.VotosNulos, 0) AS Nulos
                FROM Mesa m
                LEFT JOIN Resultado r ON m.MesaID = r.MesaID
                LEFT JOIN VotosEspeciales ve ON m.MesaID = ve.MesaID
                GROUP BY m.MesaID, m.TotalVotantes, ve.VotosBlancos, ve.VotosNulos";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int mesaId = reader.GetInt32(0);
                        int totalVotantes = reader.GetInt32(1);
                        int votos = reader.GetInt32(2);
                        int blancos = reader.GetInt32(3);
                        int nulos = reader.GetInt32(4);

                        int ausentismoMesa = totalVotantes - (votos + blancos + nulos);
                        if (ausentismoMesa < 0) ausentismoMesa = 0;

                        ausentismoTotal += ausentismoMesa;
                    }
                }

                conexion.CerrarConexion();
            }

            resultados["Ausentismo Total"] = ausentismoTotal;

            return resultados;
        }


    }

}
