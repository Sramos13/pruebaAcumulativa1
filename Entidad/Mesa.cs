namespace Entidad
{
    public class Mesa
    {
        public int MesaID { get; set; }
        public int Numero { get; set; }
        public int LocalidadID { get; set; }
        public DateTime Fecha { get; set; }
        public bool Cerrada { get; set; }
        public int VotantesAsignados { get; set; }

        public int UsuarioID { get; set; }
    }
}
