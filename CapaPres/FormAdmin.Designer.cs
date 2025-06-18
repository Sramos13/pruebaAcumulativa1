namespace CapaPres
{
    partial class FormAdmin
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
            dtpFecha = new DateTimePicker();
            cmbLocalidad = new ComboBox();
            btnAsignarMesa = new Button();
            lblFecha = new Label();
            lblLocalidad = new Label();
            richTextRespuestas = new RichTextBox();
            SuspendLayout();
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(219, 37);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(250, 27);
            dtpFecha.TabIndex = 0;
            // 
            // cmbLocalidad
            // 
            cmbLocalidad.FormattingEnabled = true;
            cmbLocalidad.Location = new Point(219, 86);
            cmbLocalidad.Name = "cmbLocalidad";
            cmbLocalidad.Size = new Size(151, 28);
            cmbLocalidad.TabIndex = 1;
            // 
            // btnAsignarMesa
            // 
            btnAsignarMesa.Location = new Point(63, 141);
            btnAsignarMesa.Name = "btnAsignarMesa";
            btnAsignarMesa.Size = new Size(186, 29);
            btnAsignarMesa.TabIndex = 2;
            btnAsignarMesa.Text = "Asignar Mesa";
            btnAsignarMesa.UseVisualStyleBackColor = true;
            btnAsignarMesa.Click += btnAsignarMesa_Click;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(63, 42);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(47, 20);
            lblFecha.TabIndex = 4;
            lblFecha.Text = "Fecha";
            // 
            // lblLocalidad
            // 
            lblLocalidad.AutoSize = true;
            lblLocalidad.Location = new Point(63, 89);
            lblLocalidad.Name = "lblLocalidad";
            lblLocalidad.Size = new Size(74, 20);
            lblLocalidad.TabIndex = 5;
            lblLocalidad.Text = "Localidad";
            // 
            // richTextRespuestas
            // 
            richTextRespuestas.Location = new Point(32, 215);
            richTextRespuestas.Name = "richTextRespuestas";
            richTextRespuestas.ReadOnly = true;
            richTextRespuestas.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextRespuestas.Size = new Size(754, 131);
            richTextRespuestas.TabIndex = 16;
            richTextRespuestas.Text = "";
            // 
            // FormAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(808, 394);
            Controls.Add(richTextRespuestas);
            Controls.Add(lblLocalidad);
            Controls.Add(lblFecha);
            Controls.Add(btnAsignarMesa);
            Controls.Add(cmbLocalidad);
            Controls.Add(dtpFecha);
            Name = "FormAdmin";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpFecha;
        private ComboBox cmbLocalidad;
        private Button btnAsignarMesa;
        private Label lblFecha;
        private Label lblLocalidad;
        private RadioButton radioButton1;
        private RichTextBox richTextRespuestas;
    }
}
