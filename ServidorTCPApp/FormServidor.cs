using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace ServidorTCPApp
{
    public partial class FormServidor : Form
    {
        private TcpListener servidor;
        private GestorElecciones gestor = new GestorElecciones();
        public FormServidor()
        {
            InitializeComponent();
        }

        private void btnIniciarServidor_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                servidor = new TcpListener(IPAddress.Any, 8000);
                servidor.Start();
                MostrarLog("Servidor TCP iniciado en puerto 8000");

                while (true)
                {
                    TcpClient cliente = servidor.AcceptTcpClient();
                    Task.Run(() => ManejarCliente(cliente));
                }
            });
        }

        private void ManejarCliente(TcpClient cliente)
        {
            using (NetworkStream stream = cliente.GetStream())
            {
                byte[] buffer = new byte[2048];
                int bytesLeidos = stream.Read(buffer, 0, buffer.Length);
                string mensaje = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);
                MostrarLog($"Recibido: {mensaje}");

                string respuesta = ProcesarComando(mensaje);
                MostrarLog($"Respuesta: {respuesta}");

                byte[] respuestaBytes = Encoding.UTF8.GetBytes(respuesta);
                stream.Write(respuestaBytes, 0, respuestaBytes.Length);
            }

            cliente.Close();
        }

        private string ProcesarComando(string mensaje)
        {
            try
            {
                string[] partes = mensaje.Split('|');
                string comando = partes[0];

                if (comando == "ASIGNACIONMESA")
                {
                    DateTime fecha = DateTime.Parse(partes[1]);
                    string localidad = partes[2];

                    var resultado = gestor.AsignarMesa(fecha, localidad);
                    if (resultado.estado.StartsWith("ERROR"))
                        return resultado.estado;

                    string candidatos = string.Join(",", resultado.candidatos!.Select(c => c.Nombre));
                    return $"OK|Mesa:{resultado.mesa!.Numero}|Votantes:{resultado.mesa.VotantesAsignados}|Candidatos:{candidatos}";
                }

                else if (comando == "CONSULTAR_MESA")
                {
                    int numeroMesa = int.Parse(partes[1]);
                    return gestor.ConsultarMesa(numeroMesa);
                }

                else if (comando == "REGISTRODATOS")
                {
                    int mesaId = int.Parse(partes[1]);

                    // Verificar si la mesa está cerrada
                    var mesa = gestor.ObtenerMesaPorId(mesaId);
                    if (mesa == null)
                        return "ERROR: Mesa no encontrada";
                    if (mesa.Cerrada)
                        return "CERRADA";

                    // Votos por candidatos
                    Dictionary<int, int> votosPorCandidato = new();
                    for (int i = 2; i < partes.Length - 2; i++)
                    {
                        string[] voto = partes[i].Split(':');
                        int candidatoId = int.Parse(voto[0]);
                        int votos = int.Parse(voto[1]);
                        votosPorCandidato[candidatoId] = votos;
                    }

                    int blancos = int.Parse(partes[^2]);
                    int nulos = int.Parse(partes[^1]);

                    string resultado = gestor.RegistrarDatosPorMesaId(mesaId, votosPorCandidato, blancos, nulos);
                    return resultado;
                }


                else if (comando == "CIERREMESA")
                {
                    int mesaId = int.Parse(partes[1]);
                    return gestor.CerrarMesaPorId(mesaId);
                }

                // Puedes añadir aquí REGISTRODATOS y CIERREMESA
                return "ERROR: Comando no reconocido";
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }

        private void MostrarLog(string texto)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => rtbLog.AppendText(texto + Environment.NewLine)));
            }
            else
            {
                rtbLog.AppendText(texto + Environment.NewLine);
            }
        }
    }
}
