namespace UI
{
    partial class Presentacion
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
#region Código generado por el Diseñador

        private void InitializeComponent()
        {
            panelIzquierdo = new Panel();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            panelLogin = new Panel();
            btnLogin = new Button();
            txtPassword = new TextBox();
            txtUsuario = new TextBox();
            lblPassword = new Label();
            lblUsuario = new Label();
            lblLogin = new Label();
            panelIzquierdo.SuspendLayout();
            panelLogin.SuspendLayout();
            SuspendLayout();
            // 
            // panelIzquierdo
            // 
            panelIzquierdo.BackColor = Color.FromArgb(78, 122, 84);
            panelIzquierdo.Controls.Add(lblSubtitulo);
            panelIzquierdo.Controls.Add(lblTitulo);
            panelIzquierdo.Dock = DockStyle.Left;
            panelIzquierdo.Location = new Point(0, 0);
            panelIzquierdo.Margin = new Padding(2, 2, 2, 2);
            panelIzquierdo.Name = "panelIzquierdo";
            panelIzquierdo.Size = new Size(233, 375);
            panelIzquierdo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 10F);
            lblSubtitulo.ForeColor = Color.WhiteSmoke;
            lblSubtitulo.Location = new Point(35, 142);
            lblSubtitulo.Margin = new Padding(2, 0, 2, 0);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(126, 19);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Sistema Nutricional";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(31, 90);
            lblTitulo.Margin = new Padding(2, 0, 2, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(184, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "NutriEvolve";
            // 
            // panelLogin
            // 
            panelLogin.BackColor = Color.FromArgb(226, 234, 226);
            panelLogin.Controls.Add(btnLogin);
            panelLogin.Controls.Add(txtPassword);
            panelLogin.Controls.Add(txtUsuario);
            panelLogin.Controls.Add(lblPassword);
            panelLogin.Controls.Add(lblUsuario);
            panelLogin.Controls.Add(lblLogin);
            panelLogin.Dock = DockStyle.Fill;
            panelLogin.Location = new Point(233, 0);
            panelLogin.Margin = new Padding(2, 2, 2, 2);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(389, 375);
            panelLogin.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(78, 122, 84);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(109, 289);
            btnLogin.Margin = new Padding(2, 2, 2, 2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(140, 38);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(58, 221);
            txtPassword.Margin = new Padding(2, 2, 2, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(257, 27);
            txtPassword.TabIndex = 4;
            // 
            // txtUsuario
            // 
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.Location = new Point(58, 154);
            txtUsuario.Margin = new Padding(2, 2, 2, 2);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(257, 27);
            txtUsuario.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F);
            lblPassword.ForeColor = Color.Black;
            lblPassword.Location = new Point(54, 195);
            lblPassword.Margin = new Padding(2, 0, 2, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(79, 19);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Contraseña";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 10F);
            lblUsuario.ForeColor = Color.Black;
            lblUsuario.Location = new Point(54, 128);
            lblUsuario.Margin = new Padding(2, 0, 2, 0);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(56, 19);
            lblUsuario.TabIndex = 1;
            lblUsuario.Text = "Usuario";
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblLogin.ForeColor = Color.FromArgb(78, 122, 84);
            lblLogin.Location = new Point(117, 52);
            lblLogin.Margin = new Padding(2, 0, 2, 0);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(99, 37);
            lblLogin.TabIndex = 0;
            lblLogin.Text = "LOGIN";
            // 
            // Presentacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 375);
            Controls.Add(panelLogin);
            Controls.Add(panelIzquierdo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2, 2, 2, 2);
            MaximizeBox = false;
            Name = "Presentacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema NutriEvolve";
            panelIzquierdo.ResumeLayout(false);
            panelIzquierdo.PerformLayout();
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
    }
}
#endregion