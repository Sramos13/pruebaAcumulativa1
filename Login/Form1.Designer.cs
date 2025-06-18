namespace Login
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
            lblUser = new Label();
            lblPass = new Label();
            txtUsuario = new TextBox();
            btnIniciarSesion = new Button();
            btnAdmin = new Button();
            mktContra = new MaskedTextBox();
            btnStatus = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(237, 220);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(59, 20);
            lblUser.TabIndex = 0;
            lblUser.Text = "Usuario";
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(237, 281);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(83, 20);
            lblPass.TabIndex = 1;
            lblPass.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(363, 217);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(125, 27);
            txtUsuario.TabIndex = 2;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.Location = new Point(337, 346);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(167, 29);
            btnIniciarSesion.TabIndex = 4;
            btnIniciarSesion.Text = "Iniciar sesión";
            btnIniciarSesion.UseVisualStyleBackColor = true;
            btnIniciarSesion.Click += btnIniciarSesion_Click;
            // 
            // btnAdmin
            // 
            btnAdmin.Location = new Point(295, 452);
            btnAdmin.Name = "btnAdmin";
            btnAdmin.Size = new Size(249, 29);
            btnAdmin.TabIndex = 5;
            btnAdmin.Text = "Iniciar sesión como Administrador";
            btnAdmin.UseVisualStyleBackColor = true;
            btnAdmin.Click += btnAdmin_Click;
            // 
            // mktContra
            // 
            mktContra.Location = new Point(363, 281);
            mktContra.Name = "mktContra";
            mktContra.Size = new Size(125, 27);
            mktContra.TabIndex = 6;
            mktContra.UseSystemPasswordChar = true;
            // 
            // btnStatus
            // 
            btnStatus.Location = new Point(337, 404);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(165, 29);
            btnStatus.TabIndex = 7;
            btnStatus.Text = "Ver Estadísticas";
            btnStatus.UseVisualStyleBackColor = true;
            btnStatus.Click += btnStatus_Click;
            // 
            // button1
            // 
            button1.Location = new Point(363, 512);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 8;
            button1.Text = "Consultar Mesa";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(524, 512);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(908, 590);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(btnStatus);
            Controls.Add(mktContra);
            Controls.Add(btnAdmin);
            Controls.Add(btnIniciarSesion);
            Controls.Add(txtUsuario);
            Controls.Add(lblPass);
            Controls.Add(lblUser);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUser;
        private Label lblPass;
        private TextBox txtUsuario;
        private Button btnIniciarSesion;
        private Button btnAdmin;
        private MaskedTextBox mktContra;
        private Button btnStatus;
        private Button button1;
        private TextBox textBox1;
    }
}
