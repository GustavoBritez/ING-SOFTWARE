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
            panelMenu.SuspendLayout();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(76, 124, 89);
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
            btnReportes.Text = "📊 Reportes y Gráficos";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.UseVisualStyleBackColor = false;
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
            panelContenedor.Dock = DockStyle.Fill;
            panelContenedor.Location = new Point(240, 60);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(960, 640);
            panelContenedor.TabIndex = 2;
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
    }
}
