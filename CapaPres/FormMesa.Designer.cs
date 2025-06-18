namespace CapaPres
{
    partial class FormMesa
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
            numNulos = new NumericUpDown();
            numBlancos = new NumericUpDown();
            numVotosCandidato = new NumericUpDown();
            label3 = new Label();
            label2 = new Label();
            lblVotObt = new Label();
            label1 = new Label();
            rdbLuisa = new RadioButton();
            rdbDaniel = new RadioButton();
            lblNumMes = new Label();
            lblMesaNum = new Label();
            rtxControl = new RichTextBox();
            lblLocalidad = new Label();
            lblLoc = new Label();
            btnRegistroDatos = new Button();
            btnCloseTable = new Button();
            ((System.ComponentModel.ISupportInitialize)numNulos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBlancos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVotosCandidato).BeginInit();
            SuspendLayout();
            // 
            // numNulos
            // 
            numNulos.Location = new Point(375, 307);
            numNulos.Name = "numNulos";
            numNulos.Size = new Size(150, 27);
            numNulos.TabIndex = 26;
            // 
            // numBlancos
            // 
            numBlancos.Location = new Point(375, 274);
            numBlancos.Name = "numBlancos";
            numBlancos.Size = new Size(150, 27);
            numBlancos.TabIndex = 25;
            // 
            // numVotosCandidato
            // 
            numVotosCandidato.Location = new Point(375, 241);
            numVotosCandidato.Name = "numVotosCandidato";
            numVotosCandidato.Size = new Size(150, 27);
            numVotosCandidato.TabIndex = 24;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(92, 302);
            label3.Name = "label3";
            label3.Size = new Size(91, 20);
            label3.TabIndex = 23;
            label3.Text = "Votos Nulos:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(92, 273);
            label2.Name = "label2";
            label2.Size = new Size(104, 20);
            label2.TabIndex = 22;
            label2.Text = "Votos Blancos:";
            // 
            // lblVotObt
            // 
            lblVotObt.AutoSize = true;
            lblVotObt.Location = new Point(92, 243);
            lblVotObt.Name = "lblVotObt";
            lblVotObt.Size = new Size(220, 20);
            lblVotObt.TabIndex = 21;
            lblVotObt.Text = "Votos Obtenidos del Candidato:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(92, 121);
            label1.Name = "label1";
            label1.Size = new Size(172, 20);
            label1.TabIndex = 20;
            label1.Text = "Selecciona el Candidato:";
            // 
            // rdbLuisa
            // 
            rdbLuisa.AutoSize = true;
            rdbLuisa.Location = new Point(92, 174);
            rdbLuisa.Name = "rdbLuisa";
            rdbLuisa.Size = new Size(127, 24);
            rdbLuisa.TabIndex = 19;
            rdbLuisa.TabStop = true;
            rdbLuisa.Text = "Luisa Gonzales";
            rdbLuisa.UseVisualStyleBackColor = true;
            // 
            // rdbDaniel
            // 
            rdbDaniel.AutoSize = true;
            rdbDaniel.Location = new Point(92, 144);
            rdbDaniel.Name = "rdbDaniel";
            rdbDaniel.Size = new Size(123, 24);
            rdbDaniel.TabIndex = 18;
            rdbDaniel.TabStop = true;
            rdbDaniel.Text = "Daniel Noboa";
            rdbDaniel.UseVisualStyleBackColor = true;
            // 
            // lblNumMes
            // 
            lblNumMes.AutoSize = true;
            lblNumMes.Location = new Point(92, 65);
            lblNumMes.Name = "lblNumMes";
            lblNumMes.Size = new Size(123, 20);
            lblNumMes.TabIndex = 17;
            lblNumMes.Text = "Número de Mesa";
            // 
            // lblMesaNum
            // 
            lblMesaNum.AutoSize = true;
            lblMesaNum.Location = new Point(348, 65);
            lblMesaNum.Name = "lblMesaNum";
            lblMesaNum.Size = new Size(44, 20);
            lblMesaNum.TabIndex = 27;
            lblMesaNum.Text = "Mesa";
            // 
            // rtxControl
            // 
            rtxControl.Location = new Point(76, 402);
            rtxControl.Name = "rtxControl";
            rtxControl.Size = new Size(697, 143);
            rtxControl.TabIndex = 28;
            rtxControl.Text = "";
            // 
            // lblLocalidad
            // 
            lblLocalidad.AutoSize = true;
            lblLocalidad.Location = new Point(348, 96);
            lblLocalidad.Name = "lblLocalidad";
            lblLocalidad.Size = new Size(74, 20);
            lblLocalidad.TabIndex = 29;
            lblLocalidad.Text = "Localidad";
            // 
            // lblLoc
            // 
            lblLoc.AutoSize = true;
            lblLoc.Location = new Point(92, 96);
            lblLoc.Name = "lblLoc";
            lblLoc.Size = new Size(120, 20);
            lblLoc.TabIndex = 30;
            lblLoc.Text = "Localidad Actual";
            // 
            // btnRegistroDatos
            // 
            btnRegistroDatos.Location = new Point(102, 352);
            btnRegistroDatos.Name = "btnRegistroDatos";
            btnRegistroDatos.Size = new Size(162, 29);
            btnRegistroDatos.TabIndex = 31;
            btnRegistroDatos.Text = "Registro de Datos";
            btnRegistroDatos.UseVisualStyleBackColor = true;
            btnRegistroDatos.Click += btnRegistroDatos_Click;
            // 
            // btnCloseTable
            // 
            btnCloseTable.Location = new Point(319, 567);
            btnCloseTable.Name = "btnCloseTable";
            btnCloseTable.Size = new Size(181, 29);
            btnCloseTable.TabIndex = 32;
            btnCloseTable.Text = "Cerrar Mesa";
            btnCloseTable.UseVisualStyleBackColor = true;
            btnCloseTable.Click += btnCloseTable_Click;
            // 
            // FormMesa
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(894, 647);
            Controls.Add(btnCloseTable);
            Controls.Add(btnRegistroDatos);
            Controls.Add(lblLoc);
            Controls.Add(lblLocalidad);
            Controls.Add(rtxControl);
            Controls.Add(lblMesaNum);
            Controls.Add(numNulos);
            Controls.Add(numBlancos);
            Controls.Add(numVotosCandidato);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblVotObt);
            Controls.Add(label1);
            Controls.Add(rdbLuisa);
            Controls.Add(rdbDaniel);
            Controls.Add(lblNumMes);
            Name = "FormMesa";
            Text = "FormMesa";
            ((System.ComponentModel.ISupportInitialize)numNulos).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBlancos).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVotosCandidato).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numNulos;
        private NumericUpDown numBlancos;
        private NumericUpDown numVotosCandidato;
        private Label label3;
        private Label label2;
        private Label lblVotObt;
        private Label label1;
        private RadioButton rdbLuisa;
        private RadioButton rdbDaniel;
        private Label lblNumMes;
        private Label lblMesaNum;
        private RichTextBox rtxControl;
        private Label lblLocalidad;
        private Label lblLoc;
        private Button btnRegistroDatos;
        private Button btnCloseTable;
    }
}