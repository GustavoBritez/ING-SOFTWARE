namespace UI
{
    partial class GestionUsuario
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
            panelLateral = new Panel();
            btnActDesact = new Button();
            btnEliminar = new Button();
            btnModificar = new Button();
            btnCrear = new Button();
            dgvUsuarios = new DataGridView();
            lblTitulo = new Label();
            gbFiltrar = new GroupBox();
            rbMostrarInactivos = new RadioButton();
            rbMostrarActivos = new RadioButton();
            gbDetalles = new GroupBox();
            gbEstadoInterno = new GroupBox();
            rbEstadoInactivo = new RadioButton();
            rbEstadoActivo = new RadioButton();
            txtNombreUsuario = new TextBox();
            lblNombreUsuario = new Label();
            cmbRol = new ComboBox();
            lblRol = new Label();
            txtApellido = new TextBox();
            lblApellido = new Label();
            txtNombre = new TextBox();
            lblNombre = new Label();
            txtDni = new TextBox();
            lblDni = new Label();
            btnSalir = new Button();
            panelLateral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            gbFiltrar.SuspendLayout();
            gbDetalles.SuspendLayout();
            gbEstadoInterno.SuspendLayout();
            SuspendLayout();
            // 
            // panelLateral
            // 
            panelLateral.BackColor = Color.FromArgb(143, 188, 153);
            panelLateral.BorderStyle = BorderStyle.FixedSingle;
            panelLateral.Controls.Add(btnActDesact);
            panelLateral.Controls.Add(btnEliminar);
            panelLateral.Controls.Add(btnModificar);
            panelLateral.Controls.Add(btnCrear);
            panelLateral.Location = new Point(12, 60);
            panelLateral.Name = "panelLateral";
            panelLateral.Size = new Size(160, 240);
            panelLateral.TabIndex = 4;
            // 
            // btnActDesact
            // 
            btnActDesact.BackColor = Color.FromArgb(225, 225, 225);
            btnActDesact.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnActDesact.Location = new Point(15, 180);
            btnActDesact.Name = "btnActDesact";
            btnActDesact.Size = new Size(128, 40);
            btnActDesact.TabIndex = 0;
            btnActDesact.Text = "Act/Desact";
            btnActDesact.UseVisualStyleBackColor = false;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(225, 225, 225);
            btnEliminar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminar.Location = new Point(15, 125);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(128, 40);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.FromArgb(225, 225, 225);
            btnModificar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnModificar.Location = new Point(15, 70);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(128, 40);
            btnModificar.TabIndex = 2;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            // 
            // btnCrear
            // 
            btnCrear.BackColor = Color.FromArgb(225, 225, 225);
            btnCrear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCrear.Location = new Point(15, 15);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(128, 40);
            btnCrear.TabIndex = 3;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = false;
            btnCrear.Click += btnCrear_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.BackgroundColor = Color.White;
            dgvUsuarios.BorderStyle = BorderStyle.None;
            dgvUsuarios.Location = new Point(185, 60);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(753, 335);
            dgvUsuarios.TabIndex = 3;
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(46, 94, 67);
            lblTitulo.Location = new Point(12, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(926, 40);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "👤 Gestión de Usuarios";
            lblTitulo.TextAlign = ContentAlignment.TopCenter;
            // 
            // gbFiltrar
            // 
            gbFiltrar.Controls.Add(rbMostrarInactivos);
            gbFiltrar.Controls.Add(rbMostrarActivos);
            gbFiltrar.FlatStyle = FlatStyle.Flat;
            gbFiltrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbFiltrar.Location = new Point(245, 410);
            gbFiltrar.Name = "gbFiltrar";
            gbFiltrar.Size = new Size(200, 85);
            gbFiltrar.TabIndex = 2;
            gbFiltrar.TabStop = false;
            gbFiltrar.Text = "Filtrar Usuarios";
            // 
            // rbMostrarInactivos
            // 
            rbMostrarInactivos.Location = new Point(15, 50);
            rbMostrarInactivos.Name = "rbMostrarInactivos";
            rbMostrarInactivos.Size = new Size(150, 20);
            rbMostrarInactivos.TabIndex = 0;
            rbMostrarInactivos.Text = "Mostrar Inactivos";
            // 
            // rbMostrarActivos
            // 
            rbMostrarActivos.Checked = true;
            rbMostrarActivos.Location = new Point(15, 25);
            rbMostrarActivos.Name = "rbMostrarActivos";
            rbMostrarActivos.Size = new Size(150, 20);
            rbMostrarActivos.TabIndex = 1;
            rbMostrarActivos.TabStop = true;
            rbMostrarActivos.Text = "Mostrar Activos";
            // 
            // gbDetalles
            // 
            gbDetalles.BackColor = Color.FromArgb(180, 180, 180);
            gbDetalles.Controls.Add(gbEstadoInterno);
            gbDetalles.Controls.Add(txtNombreUsuario);
            gbDetalles.Controls.Add(lblNombreUsuario);
            gbDetalles.Controls.Add(cmbRol);
            gbDetalles.Controls.Add(lblRol);
            gbDetalles.Controls.Add(txtApellido);
            gbDetalles.Controls.Add(lblApellido);
            gbDetalles.Controls.Add(txtNombre);
            gbDetalles.Controls.Add(lblNombre);
            gbDetalles.Controls.Add(txtDni);
            gbDetalles.Controls.Add(lblDni);
            gbDetalles.FlatStyle = FlatStyle.Flat;
            gbDetalles.Location = new Point(495, 410);
            gbDetalles.Name = "gbDetalles";
            gbDetalles.Size = new Size(443, 135);
            gbDetalles.TabIndex = 1;
            gbDetalles.TabStop = false;
            // 
            // gbEstadoInterno
            // 
            gbEstadoInterno.Controls.Add(rbEstadoInactivo);
            gbEstadoInterno.Controls.Add(rbEstadoActivo);
            gbEstadoInterno.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbEstadoInterno.Location = new Point(240, 70);
            gbEstadoInterno.Name = "gbEstadoInterno";
            gbEstadoInterno.Size = new Size(190, 55);
            gbEstadoInterno.TabIndex = 0;
            gbEstadoInterno.TabStop = false;
            gbEstadoInterno.Text = "Estado";
            // 
            // rbEstadoInactivo
            // 
            rbEstadoInactivo.Location = new Point(95, 22);
            rbEstadoInactivo.Name = "rbEstadoInactivo";
            rbEstadoInactivo.Size = new Size(80, 20);
            rbEstadoInactivo.TabIndex = 0;
            rbEstadoInactivo.Text = "Inactivo";
            // 
            // rbEstadoActivo
            // 
            rbEstadoActivo.Checked = true;
            rbEstadoActivo.Location = new Point(15, 22);
            rbEstadoActivo.Name = "rbEstadoActivo";
            rbEstadoActivo.Size = new Size(70, 20);
            rbEstadoActivo.TabIndex = 1;
            rbEstadoActivo.TabStop = true;
            rbEstadoActivo.Text = "Activo";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new Point(315, 42);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(115, 23);
            txtNombreUsuario.TabIndex = 1;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombreUsuario.Location = new Point(210, 45);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(100, 20);
            lblNombreUsuario.TabIndex = 2;
            lblNombreUsuario.Text = "NombreUsuario";
            lblNombreUsuario.TextAlign = ContentAlignment.TopRight;
            // 
            // cmbRol
            // 
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.Location = new Point(315, 12);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(115, 23);
            cmbRol.TabIndex = 3;
            // 
            // lblRol
            // 
            lblRol.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRol.Location = new Point(220, 15);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(90, 20);
            lblRol.TabIndex = 4;
            lblRol.Text = "Rol";
            lblRol.TextAlign = ContentAlignment.TopRight;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(75, 72);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(130, 23);
            txtApellido.TabIndex = 5;
            // 
            // lblApellido
            // 
            lblApellido.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblApellido.Location = new Point(10, 75);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(60, 20);
            lblApellido.TabIndex = 6;
            lblApellido.Text = "Apellido";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(75, 42);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(130, 23);
            txtNombre.TabIndex = 7;
            // 
            // lblNombre
            // 
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombre.Location = new Point(10, 45);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(60, 20);
            lblNombre.TabIndex = 8;
            lblNombre.Text = "Nombre";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(75, 12);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(130, 23);
            txtDni.TabIndex = 9;
            // 
            // lblDni
            // 
            lblDni.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDni.Location = new Point(10, 15);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(60, 20);
            lblDni.TabIndex = 10;
            lblDni.Text = "DNI";
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(255, 120, 120);
            btnSalir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Black;
            btnSalir.Location = new Point(12, 505);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(115, 40);
            btnSalir.TabIndex = 0;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // GestionUsuario
            // 
            BackColor = Color.FromArgb(218, 237, 223);
            ClientSize = new Size(950, 560);
            Controls.Add(btnSalir);
            Controls.Add(gbDetalles);
            Controls.Add(gbFiltrar);
            Controls.Add(dgvUsuarios);
            Controls.Add(panelLateral);
            Controls.Add(lblTitulo);
            Font = new Font("Segoe UI", 9F);
            Name = "GestionUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Usuarios";
            panelLateral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            gbFiltrar.ResumeLayout(false);
            gbDetalles.ResumeLayout(false);
            gbDetalles.PerformLayout();
            gbEstadoInterno.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion
        // Declaración de variables de control necesarias en la clase del Form
        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActDesact;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox gbFiltrar;
        private System.Windows.Forms.RadioButton rbMostrarActivos;
        private System.Windows.Forms.RadioButton rbMostrarInactivos;
        private System.Windows.Forms.GroupBox gbDetalles;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.GroupBox gbEstadoInterno;
        private System.Windows.Forms.RadioButton rbEstadoActivo;
        private System.Windows.Forms.RadioButton rbEstadoInactivo;
        private System.Windows.Forms.Button btnSalir;
    }
}