using System.Drawing;
using System.Windows.Forms;

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
            rbMostrarTodos = new RadioButton();
            rbMostrarInactivos = new RadioButton();
            rbMostrarActivos = new RadioButton();
            gbDetalles = new GroupBox();
            btnCancelar = new Button();
            btnAceptar = new Button();
            CKB_Desactivar = new CheckBox();
            CKB_Activar = new CheckBox();
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
            button1 = new Button();
            panelLateral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            gbFiltrar.SuspendLayout();
            gbDetalles.SuspendLayout();
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
            panelLateral.Size = new Size(160, 245);
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
            btnActDesact.Click += btnActDesact_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(225, 225, 225);
            btnEliminar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminar.Location = new Point(15, 125);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(128, 40);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "Desbloquear";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnDesbloquear_Click;
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
            btnModificar.Click += btnModificar_Click;
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
            gbFiltrar.Controls.Add(rbMostrarTodos);
            gbFiltrar.Controls.Add(rbMostrarInactivos);
            gbFiltrar.Controls.Add(rbMostrarActivos);
            gbFiltrar.FlatStyle = FlatStyle.Flat;
            gbFiltrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbFiltrar.Location = new Point(245, 410);
            gbFiltrar.Name = "gbFiltrar";
            gbFiltrar.Size = new Size(200, 114);
            gbFiltrar.TabIndex = 2;
            gbFiltrar.TabStop = false;
            gbFiltrar.Text = "Filtrar Usuarios";
            // 
            // rbMostrarTodos
            // 
            rbMostrarTodos.ForeColor = Color.Red;
            rbMostrarTodos.Location = new Point(15, 75);
            rbMostrarTodos.Name = "rbMostrarTodos";
            rbMostrarTodos.Size = new Size(150, 20);
            rbMostrarTodos.TabIndex = 5;
            rbMostrarTodos.Text = "Mostrar Todos";
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
            gbDetalles.Controls.Add(btnCancelar);
            gbDetalles.Controls.Add(btnAceptar);
            gbDetalles.Controls.Add(CKB_Desactivar);
            gbDetalles.Controls.Add(CKB_Activar);
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
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(200, 100, 100);
            btnCancelar.Enabled = false;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.Location = new Point(333, 102);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(80, 25);
            btnCancelar.TabIndex = 14;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Visible = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.FromArgb(100, 200, 100);
            btnAceptar.Enabled = false;
            btnAceptar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAceptar.Location = new Point(245, 102);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(80, 25);
            btnAceptar.TabIndex = 13;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Visible = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // CKB_Desactivar
            // 
            CKB_Desactivar.AutoSize = true;
            CKB_Desactivar.Location = new Point(245, 75);
            CKB_Desactivar.Name = "CKB_Desactivar";
            CKB_Desactivar.Size = new Size(80, 19);
            CKB_Desactivar.TabIndex = 12;
            CKB_Desactivar.Text = "Desactivar";
            CKB_Desactivar.UseVisualStyleBackColor = true;
            // 
            // CKB_Activar
            // 
            CKB_Activar.AutoSize = true;
            CKB_Activar.Location = new Point(333, 75);
            CKB_Activar.Name = "CKB_Activar";
            CKB_Activar.Size = new Size(63, 19);
            CKB_Activar.TabIndex = 11;
            CKB_Activar.Text = "Activar";
            CKB_Activar.UseVisualStyleBackColor = true;
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
            // button1
            // 
            button1.BackColor = Color.FromArgb(225, 225, 225);
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button1.Location = new Point(28, 324);
            button1.Name = "button1";
            button1.Size = new Size(128, 40);
            button1.TabIndex = 4;
            button1.Text = "Actualizar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // GestionUsuario
            // 
            BackColor = Color.FromArgb(218, 237, 223);
            ClientSize = new Size(950, 560);
            Controls.Add(button1);
            Controls.Add(btnSalir);
            Controls.Add(gbDetalles);
            Controls.Add(gbFiltrar);
            Controls.Add(panelLateral);
            Controls.Add(lblTitulo);
            Controls.Add(dgvUsuarios);
            Font = new Font("Segoe UI", 9F);
            Name = "GestionUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Usuarios";
            Load += GestionUsuario_Load;
            panelLateral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            gbFiltrar.ResumeLayout(false);
            gbDetalles.ResumeLayout(false);
            gbDetalles.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

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
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.CheckBox CKB_Desactivar;
        private System.Windows.Forms.CheckBox CKB_Activar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private RadioButton rbMostrarTodos;
        private Button button1;
    }
}