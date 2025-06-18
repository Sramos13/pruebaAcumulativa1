using System.Numerics;
using CapaNegocio;
using System.Windows.Forms.DataVisualization.Charting;

namespace Estadisticas
{
    public partial class Form1 : Form
    {
        private Chart chart;
        private GestorElecciones gestorElecciones = new GestorElecciones();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();

            // Área del gráfico
            ChartArea chartArea = new ChartArea("MainArea");
            chart.ChartAreas.Add(chartArea);

            // Leyenda
            Legend legend = new Legend("MainLegend");
            legend.Docking = Docking.Right;
            legend.Font = new Font("Segoe UI", 10);
            chart.Legends.Add(legend);

            // Serie de datos
            Series series = new Series
            {
                Name = "Estadísticas",
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                Label = "#VALX: #VAL votos",
                Legend = "MainLegend"
            };

            // Puntos de datos
            series.Points.Add(new DataPoint(0, 100)
            {
                AxisLabel = "Daniel Noboa",
                LegendText = "Daniel Noboa",
                Color = Color.RoyalBlue
            });

            series.Points.Add(new DataPoint(0, 130)
            {
                AxisLabel = "Luisa Gonzales",
                LegendText = "Luisa Gonzales",
                Color = Color.Orange
            });

            series.Points.Add(new DataPoint(0, 35)
            {
                AxisLabel = "Votos Blancos",
                LegendText = "Votos Blancos",
                Color = Color.LightGray
            });

            series.Points.Add(new DataPoint(0, 35)
            {
                AxisLabel = "Votos Nulos",
                LegendText = "Votos Nulos",
                Color = Color.IndianRed
            });

            chart.Series.Add(series);

            // Timer para actualizar
            timerActualizar.Interval = 10000; // 10 segundos
            timerActualizar.Tick += (s, ev) => ActualizarEstadisticas();
            timerActualizar.Start();

            ActualizarEstadisticas(); // Primera carga
        }

        private void ActualizarEstadisticas()
        {
            try
            {
                // Obtener datos desde el gestor de negocio
                var datos = new CapaNegocio.GestorElecciones().ObtenerEstadisticas();

                // Verificar si ya existe la serie
                Series serie;
                if (chart.Series.IndexOf("Estadísticas") >= 0)
                {
                    serie = chart.Series["Estadísticas"];
                    serie.Points.Clear(); // Limpiar los datos anteriores
                }
                else
                {
                    // Crear nueva serie si no existe
                    serie = new Series("Estadísticas")
                    {
                        ChartType = SeriesChartType.Pie,
                        IsValueShownAsLabel = true,
                        Label = "#VALX: #VAL votos",
                        Legend = "MainLegend"
                    };
                    chart.Series.Add(serie);
                }

                // Añadir puntos actualizados
                foreach (var kv in datos)
                {
                    var punto = new DataPoint(0, kv.Value)
                    {
                        AxisLabel = kv.Key,
                        LegendText = kv.Key
                    };

                    switch (kv.Key)
                    {
                        case "Daniel Noboa":
                            punto.Color = Color.RoyalBlue;
                            break;
                        case "Luisa Gonzales":
                            punto.Color = Color.Orange;
                            break;
                        case "Votos Blancos":
                            punto.Color = Color.LightGray;
                            break;
                        case "Votos Nulos":
                            punto.Color = Color.IndianRed;
                            break;
                        case "Ausentismo":
                            punto.Color = Color.DarkGray; // o el que prefieras
                            break;
                    }


                    serie.Points.Add(punto);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar estadísticas: {ex.Message}");
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            timerActualizar.Stop();
            this.Close();
        }
    }
}
