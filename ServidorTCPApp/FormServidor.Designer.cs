namespace ServidorTCPApp
{
    partial class FormServidor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rtbLog = new RichTextBox();
            btnIniciarServidor = new Button();
            SuspendLayout();
            // 
            // rtbLog
            // 
            rtbLog.Location = new Point(41, 50);
            rtbLog.Name = "rtbLog";
            rtbLog.Size = new Size(711, 278);
            rtbLog.TabIndex = 0;
            rtbLog.Text = "";
            // 
            // btnIniciarServidor
            // 
            btnIniciarServidor.Location = new Point(301, 356);
            btnIniciarServidor.Name = "btnIniciarServidor";
            btnIniciarServidor.Size = new Size(157, 29);
            btnIniciarServidor.TabIndex = 1;
            btnIniciarServidor.Text = "Iniciar Servidor";
            btnIniciarServidor.UseVisualStyleBackColor = true;
            btnIniciarServidor.Click += btnIniciarServidor_Click;
            // 
            // FormServidor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnIniciarServidor);
            Controls.Add(rtbLog);
            Name = "FormServidor";
            Text = "FormServidor";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rtbLog;
        private Button btnIniciarServidor;
    }
}
