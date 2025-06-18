using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Candidato
    {
        public int CandidatoID { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return $"{CandidatoID} - {Nombre}";
        }
    }
}
