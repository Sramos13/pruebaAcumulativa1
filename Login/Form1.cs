
using System.Net.Sockets;
using CapaNegocio;
using CapaPres;

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string cedula = txtUsuario.Text;
            string password = mktContra.Text;

            GestorElecciones gestor = new GestorElecciones();
            var usuario = gestor.IniciarSesionUsuario(cedula, password);

            if (usuario != null)
            {
                var mesa = gestor.ObtenerMesaAsignada(usuario.UsuarioID);

                if (mesa != null)
                {
                    MessageBox.Show($"Bienvenido, {usuario.UsuarioNombre}");

                    FormMesa formMesa = new FormMesa(usuario, mesa);
                    formMesa.Show();
                    this.Hide(); // opcional
                    formMesa.FormClosed += (s, args) => this.Show(); // Muestra el formulario de login al cerrar la mesa
                }
                else
                {
                    MessageBox.Show("Este usuario no tiene una mesa asignada.");
                }
            }
            else
            {
                MessageBox.Show("Cédula o contraseña incorrectos.");
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            string usuarioText = txtUsuario.Text;
            string password = mktContra.Text;

            GestorElecciones gestor = new GestorElecciones();
            var admin = gestor.IniciarSesionAdmin(usuarioText, password);

            if (admin != null)
            {
                MessageBox.Show("Bienvenido administrador.");
                FormAdmin formAdmin = new FormAdmin();
                formAdmin.Show();
                this.Hide(); // Oculta el formulario de login
                formAdmin.FormClosed += (s, args) => this.Show(); // Muestra el formulario de login al cerrar el admin
            }
            else
            {
                MessageBox.Show("Usuario o contraseña de administrador incorrectos.");
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            Estadisticas.Form1 formEstadisticas = new Estadisticas.Form1();
            formEstadisticas.Show();
            this.Hide(); // Oculta el formulario de login
            formEstadisticas.FormClosed += (s, args) => this.Show(); // Muestra el formulario de login al cerrar las estadísticas
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text.Trim(), out int numeroMesa))
            {
                MessageBox.Show("Por favor, ingrese un número de mesa válido.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensaje = $"CONSULTAR_MESA|{numeroMesa}";

            try
            {
                using (TcpClient tcp = new TcpClient("127.0.0.1", 8000))
                using (NetworkStream stream = tcp.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                using (StreamReader reader = new StreamReader(stream))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(mensaje);

                    string respuesta = reader.ReadLine();
                    MessageBox.Show(respuesta, "Resultado de Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        /*private void BtnConsultarMesa_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text.Trim(), out int numeroMesa))
            {
                MessageBox.Show("Por favor, ingrese un número de mesa válido.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensaje = $"CONSULTAR_MESA|{numeroMesa}";

            try
            {
                using (TcpClient tcp = new TcpClient("127.0.0.1", 8000))
                using (NetworkStream stream = tcp.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                using (StreamReader reader = new StreamReader(stream))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(mensaje);

                    string respuesta = reader.ReadLine();
                    MessageBox.Show(respuesta, "Resultado de Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/


    }
}
