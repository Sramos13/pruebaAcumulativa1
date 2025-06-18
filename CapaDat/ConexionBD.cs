using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CapaDat
{
    public class ConexionBD
    {
        //Se crea la conexión a la base de datos
        private SqlConnection Conexion = new SqlConnection("Data Source=XAVIER;Initial Catalog=DBElecciones;Integrated Security=True;TrustServerCertificate=True");

        //Se crea el método para abrir la conexión
        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
            {
                Conexion.Open();
            }
            return Conexion;
        }

        //Se crea el método para cerrar la conexión
        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
            {
                Conexion.Close();
            }
            return Conexion;
        }
    }
}
