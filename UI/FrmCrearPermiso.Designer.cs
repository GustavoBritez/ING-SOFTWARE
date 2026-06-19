namespace UI
{
    partial class FrmCrearPermiso
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
            lblTitulo = new Label();
            panelCentral = new Panel();
            txtNombrePermiso = new TextBox();
            lblNombrePermiso = new Label();
            btnAceptar = new Button();
            btnCancelar = new Button();
            panelCentral.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(46, 94, 67);
            lblTitulo.Location = new Point(12, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(410, 40);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "🛡️ Crear Nuevo Permiso";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelCentral
            // 
            panelCentral.BackColor = Color.FromArgb(180, 180, 180);
            panelCentral.BorderStyle = BorderStyle.FixedSingle;
            panelCentral.Controls.Add(txtNombrePermiso);
            panelCentral.Controls.Add(lblNombrePermiso);
            panelCentral.Location = new Point(34, 73);
            panelCentral.Name = "panelCentral";
            panelCentral.Size = new Size(365, 87);
            panelCentral.TabIndex = 1;
            // 
            // txtNombrePermiso
            // 
            txtNombrePermiso.Font = new Font("Segoe UI", 10F);
            txtNombrePermiso.Location = new Point(23, 40);
            txtNombrePermiso.Name = "txtNombrePermiso";
            txtNombrePermiso.Size = new Size(317, 25);
            txtNombrePermiso.TabIndex = 1;
            // 
            // lblNombrePermiso
            // 
            lblNombrePermiso.AutoSize = true;
            lblNombrePermiso.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNombrePermiso.ForeColor = Color.Black;
            lblNombrePermiso.Location = new Point(19, 15);
            lblNombrePermiso.Name = "lblNombrePermiso";
            lblNombrePermiso.Size = new Size(155, 19);
            lblNombrePermiso.TabIndex = 0;
            lblNombrePermiso.Text = "Nombre de la Acción:";
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.FromArgb(225, 225, 225);
            btnAceptar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAceptar.Location = new Point(271, 178);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(128, 40);
            btnAceptar.TabIndex = 2;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(255, 120, 120);
            btnCancelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancelar.Location = new Point(34, 178);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(128, 40);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FrmCrearPermiso
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(218, 237, 223);
            ClientSize = new Size(434, 241);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(panelCentral);
            Controls.Add(lblTitulo);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCrearPermiso";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestión de Permisos";
            Load += FrmCrearPermiso_Load;
            panelCentral.ResumeLayout(false);
            panelCentral.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.TextBox txtNombrePermiso;
        private System.Windows.Forms.Label lblNombrePermiso;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
