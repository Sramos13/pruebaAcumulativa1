using System.Net.Sockets;
using System.Text;
using CapaNegocio;

namespace CapaPres
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GestorElecciones gestor = new GestorElecciones();
            // Cargar localidades en el ComboBox
            gestor.MostrarLocalidades().ForEach(localidad =>
            {
                cmbLocalidad.Items.Add(localidad);
            });

        }

        private void btnAsignarMesa_Click(object sender, EventArgs e)
        {
            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");
            string localidad = cmbLocalidad.SelectedItem.ToString();

            string comando = $"ASIGNACIONMESA|{fecha}|{localidad}";
            EnviarComando(comando);
        }

        private void EnviarComando(string comando)
        {
            try
            {
                TcpClient cliente = new TcpClient("127.0.0.1", 8000);
                NetworkStream stream = cliente.GetStream();

                byte[] datos = Encoding.UTF8.GetBytes(comando);
                stream.Write(datos, 0, datos.Length);

                byte[] buffer = new byte[2048];
                int leidos = stream.Read(buffer, 0, buffer.Length);
                string respuesta = Encoding.UTF8.GetString(buffer, 0, leidos);

                richTextRespuestas.AppendText(respuesta + Environment.NewLine);
            }
            catch (Exception ex)
            {
                richTextRespuestas.AppendText($"Error: {ex.Message}\n");
            }
        }
    }
}
