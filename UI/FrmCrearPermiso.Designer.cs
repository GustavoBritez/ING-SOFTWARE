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
            lblTitulo = new System.Windows.Forms.Label();
            panelCentral = new System.Windows.Forms.Panel();
            txtNombrePermiso = new System.Windows.Forms.TextBox();
            lblNombrePermiso = new System.Windows.Forms.Label();
            btnAceptar = new System.Windows.Forms.Button();
            btnCancelar = new System.Windows.Forms.Button();
            panelCentral.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitulo.ForeColor = System.Drawing.Color.FromArgb(46, 94, 67);
            lblTitulo.Location = new System.Drawing.Point(12, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(410, 40);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "🛡️ Crear Nuevo Permiso";
            lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelCentral
            // 
            panelCentral.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
            panelCentral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelCentral.Controls.Add(txtNombrePermiso);
            panelCentral.Controls.Add(lblNombrePermiso);
            panelCentral.Location = new System.Drawing.Point(34, 73);
            panelCentral.Name = "panelCentral";
            panelCentral.Size = new System.Drawing.Size(365, 87);
            panelCentral.TabIndex = 1;
            // 
            // txtNombrePermiso
            // 
            txtNombrePermiso.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtNombrePermiso.Location = new System.Drawing.Point(23, 40);
            txtNombrePermiso.Name = "txtNombrePermiso";
            txtNombrePermiso.Size = new System.Drawing.Size(317, 25);
            txtNombrePermiso.TabIndex = 1;
            // 
            // lblNombrePermiso
            // 
            lblNombrePermiso.AutoSize = true;
            lblNombrePermiso.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblNombrePermiso.ForeColor = System.Drawing.Color.Black;
            lblNombrePermiso.Location = new System.Drawing.Point(19, 15);
            lblNombrePermiso.Name = "lblNombrePermiso";
            lblNombrePermiso.Size = new System.Drawing.Size(155, 19);
            lblNombrePermiso.TabIndex = 0;
            lblNombrePermiso.Text = "Nombre de la Acción:";
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
            btnAceptar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnAceptar.Location = new System.Drawing.Point(271, 178);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new System.Drawing.Size(128, 40);
            btnAceptar.TabIndex = 2;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = System.Drawing.Color.FromArgb(255, 120, 120);
            btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCancelar.Location = new System.Drawing.Point(34, 178);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new System.Drawing.Size(128, 40);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
            // 
            // FrmCrearPermiso
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(218, 237, 223);
            ClientSize = new System.Drawing.Size(434, 241);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(panelCentral);
            Controls.Add(lblTitulo);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCrearPermiso";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Gestión de Permisos";
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
