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
            this.panelLateral = new System.Windows.Forms.Panel();
            this.btnCambiarContrasena = new System.Windows.Forms.Button();
            this.btnActDesact = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gbFiltrar = new System.Windows.Forms.GroupBox();
            this.rbMostrarInactivos = new System.Windows.Forms.RadioButton();
            this.rbMostrarActivos = new System.Windows.Forms.RadioButton();
            this.gbDetalles = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.CKB_Desactivar = new System.Windows.Forms.CheckBox();
            this.CKB_Activar = new System.Windows.Forms.CheckBox();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.lblRol = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.lblDni = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();

            this.panelLateral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.gbFiltrar.SuspendLayout();
            this.gbDetalles.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelLateral
            // 
            this.panelLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(188)))), ((int)(((byte)(153)))));
            this.panelLateral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLateral.Controls.Add(this.btnCambiarContrasena);
            this.panelLateral.Controls.Add(this.btnActDesact);
            this.panelLateral.Controls.Add(this.btnEliminar);
            this.panelLateral.Controls.Add(this.btnModificar);
            this.panelLateral.Controls.Add(this.btnCrear);
            this.panelLateral.Location = new System.Drawing.Point(12, 60);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.Size = new System.Drawing.Size(160, 302);
            this.panelLateral.TabIndex = 4;

            // 
            // btnCambiarContrasena
            // 
            this.btnCambiarContrasena.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnCambiarContrasena.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCambiarContrasena.Location = new System.Drawing.Point(15, 236);
            this.btnCambiarContrasena.Name = "btnCambiarContrasena";
            this.btnCambiarContrasena.Size = new System.Drawing.Size(128, 46);
            this.btnCambiarContrasena.TabIndex = 4;
            this.btnCambiarContrasena.Text = "Cambiar Contraseña";
            this.btnCambiarContrasena.UseVisualStyleBackColor = false;
            this.btnCambiarContrasena.Click += new System.EventHandler(this.btnCambiarContrasena_Click);

            // 
            // btnActDesact
            // 
            this.btnActDesact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnActDesact.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnActDesact.Location = new System.Drawing.Point(15, 180);
            this.btnActDesact.Name = "btnActDesact";
            this.btnActDesact.Size = new System.Drawing.Size(128, 40);
            this.btnActDesact.TabIndex = 0;
            this.btnActDesact.Text = "Act/Desact";
            this.btnActDesact.UseVisualStyleBackColor = false;
            this.btnActDesact.Click += new System.EventHandler(this.btnActDesact_Click);

            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.Location = new System.Drawing.Point(15, 125);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(128, 40);
            this.btnEliminar.TabIndex = 1;
            this.btnEliminar.Text = "Desbloquear";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnDesbloquear_Click);

            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnModificar.Location = new System.Drawing.Point(15, 70);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(128, 40);
            this.btnModificar.TabIndex = 2;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);

            // 
            // btnCrear
            // 
            this.btnCrear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnCrear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCrear.Location = new System.Drawing.Point(15, 15);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(128, 40);
            this.btnCrear.TabIndex = 3;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);

            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsuarios.Location = new System.Drawing.Point(185, 60);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.Size = new System.Drawing.Size(753, 335);
            this.dgvUsuarios.TabIndex = 3;

            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(94)))), ((int)(((byte)(67)))));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(926, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "👤 Gestión de Usuarios";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;

            // 
            // gbFiltrar
            // 
            this.gbFiltrar.Controls.Add(this.rbMostrarInactivos);
            this.gbFiltrar.Controls.Add(this.rbMostrarActivos);
            this.gbFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbFiltrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gbFiltrar.Location = new System.Drawing.Point(245, 410);
            this.gbFiltrar.Name = "gbFiltrar";
            this.gbFiltrar.Size = new System.Drawing.Size(200, 85);
            this.gbFiltrar.TabIndex = 2;
            this.gbFiltrar.TabStop = false;
            this.gbFiltrar.Text = "Filtrar Usuarios";

            // 
            // rbMostrarInactivos
            // 
            this.rbMostrarInactivos.Location = new System.Drawing.Point(15, 50);
            this.rbMostrarInactivos.Name = "rbMostrarInactivos";
            this.rbMostrarInactivos.Size = new System.Drawing.Size(150, 20);
            this.rbMostrarInactivos.TabIndex = 0;
            this.rbMostrarInactivos.Text = "Mostrar Inactivos";

            // 
            // rbMostrarActivos
            // 
            this.rbMostrarActivos.Checked = true;
            this.rbMostrarActivos.Location = new System.Drawing.Point(15, 25);
            this.rbMostrarActivos.Name = "rbMostrarActivos";
            this.rbMostrarActivos.Size = new System.Drawing.Size(150, 20);
            this.rbMostrarActivos.TabIndex = 1;
            this.rbMostrarActivos.TabStop = true;
            this.rbMostrarActivos.Text = "Mostrar Activos";

            // 
            // gbDetalles
            // 
            this.gbDetalles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.gbDetalles.Controls.Add(this.btnCancelar);
            this.gbDetalles.Controls.Add(this.btnAceptar);
            this.gbDetalles.Controls.Add(this.CKB_Desactivar);
            this.gbDetalles.Controls.Add(this.CKB_Activar);
            this.gbDetalles.Controls.Add(this.txtNombreUsuario);
            this.gbDetalles.Controls.Add(this.lblNombreUsuario);
            this.gbDetalles.Controls.Add(this.cmbRol);
            this.gbDetalles.Controls.Add(this.lblRol);
            this.gbDetalles.Controls.Add(this.txtApellido);
            this.gbDetalles.Controls.Add(this.lblApellido);
            this.gbDetalles.Controls.Add(this.txtNombre);
            this.gbDetalles.Controls.Add(this.lblNombre);
            this.gbDetalles.Controls.Add(this.txtDni);
            this.gbDetalles.Controls.Add(this.lblDni);
            this.gbDetalles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbDetalles.Location = new System.Drawing.Point(495, 410);
            this.gbDetalles.Name = "gbDetalles";
            this.gbDetalles.Size = new System.Drawing.Size(443, 135);
            this.gbDetalles.TabIndex = 1;
            this.gbDetalles.TabStop = false;

            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Location = new System.Drawing.Point(333, 102);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(80, 25);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(100)))));
            this.btnAceptar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAceptar.Location = new System.Drawing.Point(245, 102);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(80, 25);
            this.btnAceptar.TabIndex = 13;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Enabled = false;
            this.btnAceptar.Visible = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);

            // 
            // CKB_Desactivar
            // 
            this.CKB_Desactivar.AutoSize = true;
            this.CKB_Desactivar.Location = new System.Drawing.Point(245, 75);
            this.CKB_Desactivar.Name = "CKB_Desactivar";
            this.CKB_Desactivar.Size = new System.Drawing.Size(80, 19);
            this.CKB_Desactivar.TabIndex = 12;
            this.CKB_Desactivar.Text = "Desactivar";
            this.CKB_Desactivar.UseVisualStyleBackColor = true;

            // 
            // CKB_Activar
            // 
            this.CKB_Activar.AutoSize = true;
            this.CKB_Activar.Location = new System.Drawing.Point(333, 75);
            this.CKB_Activar.Name = "CKB_Activar";
            this.CKB_Activar.Size = new System.Drawing.Size(63, 19);
            this.CKB_Activar.TabIndex = 11;
            this.CKB_Activar.Text = "Activar";
            this.CKB_Activar.UseVisualStyleBackColor = true;

            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Location = new System.Drawing.Point(315, 42);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(115, 23);
            this.txtNombreUsuario.TabIndex = 1;

            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombreUsuario.Location = new System.Drawing.Point(210, 45);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(100, 20);
            this.lblNombreUsuario.TabIndex = 2;
            this.lblNombreUsuario.Text = "NombreUsuario";
            this.lblNombreUsuario.TextAlign = System.Drawing.ContentAlignment.TopRight;

            // 
            // cmbRol
            // 
            this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRol.Location = new System.Drawing.Point(315, 12);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(115, 23);
            this.cmbRol.TabIndex = 3;

            // 
            // lblRol
            // 
            this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRol.Location = new System.Drawing.Point(220, 15);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(90, 20);
            this.lblRol.TabIndex = 4;
            this.lblRol.Text = "Rol";
            this.lblRol.TextAlign = System.Drawing.ContentAlignment.TopRight;

            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(75, 72);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(130, 23);
            this.txtApellido.TabIndex = 5;

            // 
            // lblApellido
            // 
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblApellido.Location = new System.Drawing.Point(10, 75);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(60, 20);
            this.lblApellido.TabIndex = 6;
            this.lblApellido.Text = "Apellido";

            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(75, 42);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(130, 23);
            this.txtNombre.TabIndex = 7;

            // 
            // lblNombre
            // 
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.Location = new System.Drawing.Point(10, 45);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(60, 20);
            this.lblNombre.TabIndex = 8;
            this.lblNombre.Text = "Nombre";

            // 
            // txtDni
            // 
            this.txtDni.Location = new System.Drawing.Point(75, 12);
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(130, 23);
            this.txtDni.TabIndex = 9;

            // 
            // lblDni
            // 
            this.lblDni.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDni.Location = new System.Drawing.Point(10, 15);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(60, 20);
            this.lblDni.TabIndex = 10;
            this.lblDni.Text = "DNI";

            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalir.ForeColor = System.Drawing.Color.Black;
            this.btnSalir.Location = new System.Drawing.Point(12, 505);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(115, 40);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);

            // 
            // GestionUsuario
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(237)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(950, 560);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.gbDetalles);
            this.Controls.Add(this.gbFiltrar);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.panelLateral);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "GestionUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Usuarios";
            this.Load += new System.EventHandler(this.GestionUsuario_Load);
            this.panelLateral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.gbFiltrar.ResumeLayout(false);
            this.gbDetalles.ResumeLayout(false);
            this.gbDetalles.PerformLayout();
            this.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnCambiarContrasena;
        private System.Windows.Forms.CheckBox CKB_Desactivar;
        private System.Windows.Forms.CheckBox CKB_Activar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
    }
}