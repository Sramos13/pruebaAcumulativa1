using System.Windows.Forms.DataVisualization.Charting;

namespace Estadisticas
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timerActualizar = new System.Windows.Forms.Timer(components);
            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)(chart)).BeginInit();
            SuspendLayout();
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(390, 525);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(94, 29);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // Chart
            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart.Location = new Point(80, 30);
            chart.Size = new Size(750, 400);
            chart.Name = "chart";
            this.Controls.Add(chart);


            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(926, 595);
            Controls.Add(btnCerrar);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer timerActualizar;
        private Button btnCerrar;
    }
}
