using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using Entidad;

namespace CapaPres
{
    public partial class FormMesa : Form
    {
        private Usuario usuarioActual;
        private Mesa mesaAsignada;
        private string nombreLocalidad;

        public FormMesa(Usuario usuario, Mesa mesa)
        {

            InitializeComponent();
            usuarioActual = usuario;
            mesaAsignada = mesa;
            // Obtener el nombre de la localidad desde la capa de negocio
            GestorElecciones gestor = new GestorElecciones();
            nombreLocalidad = gestor.ObtenerNombreLocalidad(mesa.LocalidadID);

            // Mostrar en etiquetas del formulario
            lblMesaNum.Text = mesa.Numero.ToString();
            lblLocalidad.Text = nombreLocalidad;
        }

        private void btnRegistroDatos_Click(object sender, EventArgs e)
        {
            int mesaId = mesaAsignada.MesaID;
            int votosCandidato = (int)numVotosCandidato.Value;
            int votosBlancos = (int)numBlancos.Value;
            int votosNulos = (int)numNulos.Value;

            int candidatoId = rdbDaniel.Checked ? 1 : (rdbLuisa.Checked ? 2 : 0);
            if (candidatoId == 0)
            {
                MessageBox.Show("Selecciona un candidato.");
                return;
            }

            // Confirmación antes de enviar
            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de que desea enviar los resultados?\nUna vez enviados, no podrán ser modificados.",
                "Confirmar envío",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );

            if (confirmacion != DialogResult.OK)
                return;

            string mensaje = $"REGISTRODATOS|{mesaId}|{candidatoId}:{votosCandidato}|{votosBlancos}|{votosNulos}";

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
                    rtxControl.AppendText($"Servidor: {respuesta}\n");
                }

                // Limpiar controles después del envío
                numNulos.Value = 0;
                numBlancos.Value = 0;
                numVotosCandidato.Value = 0;
            }
            catch (Exception ex)
            {
                rtxControl.AppendText($"ERROR de conexión: {ex.Message}\n");
            }
        }

        private void btnCloseTable_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show(
                "¿Está seguro de cerrar la mesa? No se podrá volver a editar o registrar los votos.",
                "Confirmar cierre",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmar != DialogResult.Yes)
                return;

            int mesaId = mesaAsignada.MesaID;
            string mensaje = $"CIERREMESA|{mesaId}";

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
                    rtxControl.AppendText($"Servidor: {respuesta}\n");

                    if (respuesta == "OK")
                    {
                        MessageBox.Show("La mesa fue cerrada correctamente.");
                        btnCloseTable.Enabled = false;
                        btnRegistroDatos.Enabled = false;
                    }
                    else if (respuesta == "CERRADA")
                    {
                        MessageBox.Show("La mesa ya estaba cerrada.");
                    }
                }
            }
            catch (Exception ex)
            {
                rtxControl.AppendText($"ERROR de conexión: {ex.Message}\n");
            }
        }

    }
}
