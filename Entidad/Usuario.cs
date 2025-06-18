using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        
        public string Cedula { get; set; }

        public string Correo { get; set; }

        public string UsuarioNombre { get; set; }

        public string Contrasena { get; set; }

        public bool EsAdministrador { get; set; }

        public Usuario(int usuarioID, string cedula, string correo, string usuarioNombre, string contrasena, bool esAdministrador)
        {
            UsuarioID = usuarioID;
            Cedula = cedula;
            Correo = correo;
            UsuarioNombre = usuarioNombre;
            Contrasena = contrasena;
            EsAdministrador = esAdministrador;
        }
        public Usuario() {
            UsuarioID = 0;
            Cedula = string.Empty;
            Correo = string.Empty;
            UsuarioNombre = string.Empty;
            Contrasena = string.Empty;
            EsAdministrador = false;
        }


    }
}
