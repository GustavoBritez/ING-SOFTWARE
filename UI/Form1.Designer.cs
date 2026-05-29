namespace UI
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
            panelMenu = new Panel();
            btnChangePass = new Button();
            btnLogin = new Button();
            btnLogout = new Button();
            btnAyuda = new Button();
            btnUsuarios = new Button();
            btnReportes = new Button();
            btnSeguimiento = new Button();
            btnTurnos = new Button();
            lblModulo = new Label();
            panelTop = new Panel();
            lblTitulo = new Label();
            panelContenedor = new Panel();
            ChangePassPanel = new Panel();
            btnCancelar = new Button();
            btnAceptar = new Button();
            txtRepPass = new TextBox();
            label3 = new Label();
            label2 = new Label();
            txtNewPass = new TextBox();
            panelMenu.SuspendLayout();
            panelTop.SuspendLayout();
            panelContenedor.SuspendLayout();
            ChangePassPanel.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(76, 124, 89);
            panelMenu.Controls.Add(btnChangePass);
            panelMenu.Controls.Add(btnLogin);
            panelMenu.Controls.Add(btnLogout);
            panelMenu.Controls.Add(btnAyuda);
            panelMenu.Controls.Add(btnUsuarios);
            panelMenu.Controls.Add(btnReportes);
            panelMenu.Controls.Add(btnSeguimiento);
            panelMenu.Controls.Add(btnTurnos);
            panelMenu.Controls.Add(lblModulo);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(240, 700);
            panelMenu.TabIndex = 0;
            // 
            // btnChangePass
            // 
            btnChangePass.BackColor = Color.FromArgb(78, 122, 84);
            btnChangePass.FlatStyle = FlatStyle.Flat;
            btnChangePass.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnChangePass.ForeColor = Color.Transparent;
            btnChangePass.Location = new Point(0, 564);
            btnChangePass.Margin = new Padding(2);
            btnChangePass.Name = "btnChangePass";
            btnChangePass.Size = new Size(240, 38);
            btnChangePass.TabIndex = 8;
            btnChangePass.Text = "Cambiar Contraseña";
            btnChangePass.UseVisualStyleBackColor = false;
            btnChangePass.Click += btnChangePass_Click;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(78, 122, 84);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(0, 606);
            btnLogin.Margin = new Padding(2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(240, 38);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(76, 124, 89);
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(12, 640);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(215, 45);
            btnLogout.TabIndex = 6;
            btnLogout.Text = "🚪 Cerrar Sesión";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnAyuda
            // 
            btnAyuda.BackColor = Color.FromArgb(76, 124, 89);
            btnAyuda.FlatAppearance.BorderSize = 0;
            btnAyuda.FlatStyle = FlatStyle.Flat;
            btnAyuda.Font = new Font("Segoe UI", 10F);
            btnAyuda.ForeColor = Color.White;
            btnAyuda.Location = new Point(12, 300);
            btnAyuda.Name = "btnAyuda";
            btnAyuda.Size = new Size(215, 45);
            btnAyuda.TabIndex = 5;
            btnAyuda.Text = "❓ Ayuda";
            btnAyuda.TextAlign = ContentAlignment.MiddleLeft;
            btnAyuda.UseVisualStyleBackColor = false;
            // 
            // btnUsuarios
            // 
            btnUsuarios.BackColor = Color.FromArgb(76, 124, 89);
            btnUsuarios.FlatAppearance.BorderSize = 0;
            btnUsuarios.FlatStyle = FlatStyle.Flat;
            btnUsuarios.Font = new Font("Segoe UI", 10F);
            btnUsuarios.ForeColor = Color.White;
            btnUsuarios.Location = new Point(12, 245);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(215, 45);
            btnUsuarios.TabIndex = 4;
            btnUsuarios.Text = "👤 Gestión de Usuarios";
            btnUsuarios.TextAlign = ContentAlignment.MiddleLeft;
            btnUsuarios.UseVisualStyleBackColor = false;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.FromArgb(76, 124, 89);
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 10F);
            btnReportes.ForeColor = Color.White;
            btnReportes.Location = new Point(12, 190);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(215, 45);
            btnReportes.TabIndex = 3;
            btnReportes.Text = "📊 Bitacora";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnSeguimiento
            // 
            btnSeguimiento.BackColor = Color.FromArgb(76, 124, 89);
            btnSeguimiento.FlatAppearance.BorderSize = 0;
            btnSeguimiento.FlatStyle = FlatStyle.Flat;
            btnSeguimiento.Font = new Font("Segoe UI", 10F);
            btnSeguimiento.ForeColor = Color.White;
            btnSeguimiento.Location = new Point(12, 131);
            btnSeguimiento.Name = "btnSeguimiento";
            btnSeguimiento.Size = new Size(215, 45);
            btnSeguimiento.TabIndex = 2;
            btnSeguimiento.Text = "\U0001f957 Seguimiento Nutricional";
            btnSeguimiento.TextAlign = ContentAlignment.MiddleLeft;
            btnSeguimiento.UseVisualStyleBackColor = false;
            // 
            // btnTurnos
            // 
            btnTurnos.BackColor = Color.FromArgb(76, 124, 89);
            btnTurnos.FlatAppearance.BorderSize = 0;
            btnTurnos.FlatStyle = FlatStyle.Flat;
            btnTurnos.Font = new Font("Segoe UI", 10F);
            btnTurnos.ForeColor = Color.White;
            btnTurnos.Location = new Point(12, 80);
            btnTurnos.Name = "btnTurnos";
            btnTurnos.Size = new Size(215, 45);
            btnTurnos.TabIndex = 1;
            btnTurnos.Text = "📅 Gestión de Turnos";
            btnTurnos.TextAlign = ContentAlignment.MiddleLeft;
            btnTurnos.UseVisualStyleBackColor = false;
            btnTurnos.Click += btnTurnos_Click;
            // 
            // lblModulo
            // 
            lblModulo.AutoSize = true;
            lblModulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblModulo.ForeColor = Color.White;
            lblModulo.Location = new Point(24, 25);
            lblModulo.Name = "lblModulo";
            lblModulo.Size = new Size(86, 25);
            lblModulo.TabIndex = 0;
            lblModulo.Text = "Módulos";
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(92, 145, 104);
            panelTop.Controls.Add(lblTitulo);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(240, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(960, 60);
            panelTop.TabIndex = 1;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(25, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(124, 28);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "NutriEvolve";
            // 
            // panelContenedor
            // 
            panelContenedor.BackColor = Color.FromArgb(225, 240, 228);
            panelContenedor.Controls.Add(ChangePassPanel);
            panelContenedor.Dock = DockStyle.Fill;
            panelContenedor.Location = new Point(240, 60);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(960, 640);
            panelContenedor.TabIndex = 2;
            // 
            // ChangePassPanel
            // 
            ChangePassPanel.BackColor = Color.FromArgb(76, 124, 89);
            ChangePassPanel.Controls.Add(btnCancelar);
            ChangePassPanel.Controls.Add(btnAceptar);
            ChangePassPanel.Controls.Add(txtRepPass);
            ChangePassPanel.Controls.Add(label3);
            ChangePassPanel.Controls.Add(label2);
            ChangePassPanel.Controls.Add(txtNewPass);
            ChangePassPanel.Location = new Point(35, 240);
            ChangePassPanel.Name = "ChangePassPanel";
            ChangePassPanel.Size = new Size(388, 193);
            ChangePassPanel.TabIndex = 0;
            ChangePassPanel.Visible = false;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Red;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Transparent;
            btnCancelar.Location = new Point(197, 127);
            btnCancelar.Margin = new Padding(2);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(123, 38);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.FromArgb(76, 124, 99);
            btnAceptar.FlatStyle = FlatStyle.Flat;
            btnAceptar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAceptar.ForeColor = Color.Transparent;
            btnAceptar.Location = new Point(39, 127);
            btnAceptar.Margin = new Padding(2);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(123, 38);
            btnAceptar.TabIndex = 9;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // txtRepPass
            // 
            txtRepPass.Location = new Point(39, 99);
            txtRepPass.Name = "txtRepPass";
            txtRepPass.Size = new Size(100, 23);
            txtRepPass.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(157, 91);
            label3.Name = "label3";
            label3.Size = new Size(193, 28);
            label3.TabIndex = 4;
            label3.Text = "Repetir Contraseña";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(157, 41);
            label2.Name = "label2";
            label2.Size = new Size(185, 28);
            label2.TabIndex = 3;
            label2.Text = "Nueva Contraseña";
            // 
            // txtNewPass
            // 
            txtNewPass.Location = new Point(39, 41);
            txtNewPass.Name = "txtNewPass";
            txtNewPass.Size = new Size(100, 23);
            txtNewPass.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(225, 240, 228);
            ClientSize = new Size(1200, 700);
            Controls.Add(panelContenedor);
            Controls.Add(panelTop);
            Controls.Add(panelMenu);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema NutriEvolve";
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelContenedor.ResumeLayout(false);
            ChangePassPanel.ResumeLayout(false);
            ChangePassPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelContenedor;

        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.Label lblTitulo;

        private System.Windows.Forms.Button btnTurnos;
        private System.Windows.Forms.Button btnSeguimiento;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnAyuda;
        private System.Windows.Forms.Button btnLogout;
        private Button btnChangePass;
        private Button btnLogin;
        private Panel ChangePassPanel;
        private Button btnCancelar;
        private Button btnAceptar;
        private TextBox txtRepPass;
        private Label label3;
        private Label label2;
        private TextBox txtNewPass;
    }
}
