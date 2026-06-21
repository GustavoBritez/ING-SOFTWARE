namespace UI
{
    partial class FrmSeleccionarFamilia
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
            btnCancelar = new Button();
            btnVincular = new Button();
            cmbOrigen = new ComboBox();
            cmbDestino = new ComboBox();
            lblTituloBitacora = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnDesvincular = new Button();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(225, 225, 225);
            btnCancelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancelar.Location = new Point(12, 127);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(82, 32);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnVincular
            // 
            btnVincular.BackColor = Color.FromArgb(225, 225, 225);
            btnVincular.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnVincular.Location = new Point(292, 127);
            btnVincular.Name = "btnVincular";
            btnVincular.Size = new Size(97, 32);
            btnVincular.TabIndex = 3;
            btnVincular.Text = "Vincular";
            btnVincular.UseVisualStyleBackColor = false;
            btnVincular.Click += btnVincular_Click;
            // 
            // cmbOrigen
            // 
            cmbOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrigen.Location = new Point(128, 45);
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Size = new Size(158, 23);
            cmbOrigen.TabIndex = 8;
            // 
            // cmbDestino
            // 
            cmbDestino.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDestino.Location = new Point(124, 98);
            cmbDestino.Name = "cmbDestino";
            cmbDestino.Size = new Size(162, 23);
            cmbDestino.TabIndex = 9;
            // 
            // lblTituloBitacora
            // 
            lblTituloBitacora.BackColor = Color.FromArgb(143, 188, 153);
            lblTituloBitacora.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTituloBitacora.ForeColor = Color.FromArgb(46, 94, 67);
            lblTituloBitacora.Location = new Point(-23, 2);
            lblTituloBitacora.Name = "lblTituloBitacora";
            lblTituloBitacora.Size = new Size(448, 40);
            lblTituloBitacora.TabIndex = 10;
            lblTituloBitacora.Text = "Relacion Familia a Familia";
            lblTituloBitacora.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(218, 237, 223);
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(46, 94, 67);
            label1.Location = new Point(76, 46);
            label1.Name = "label1";
            label1.Size = new Size(55, 24);
            label1.TabIndex = 11;
            label1.Text = "Origen";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(218, 237, 223);
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(46, 94, 67);
            label2.Location = new Point(67, 100);
            label2.Name = "label2";
            label2.Size = new Size(64, 24);
            label2.TabIndex = 12;
            label2.Text = "Destino";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(218, 237, 223);
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(46, 94, 67);
            label3.Location = new Point(178, 71);
            label3.Name = "label3";
            label3.Size = new Size(47, 48);
            label3.TabIndex = 13;
            label3.Text = "🡫";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // btnDesvincular
            // 
            btnDesvincular.BackColor = Color.FromArgb(225, 225, 225);
            btnDesvincular.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDesvincular.Location = new Point(178, 127);
            btnDesvincular.Name = "btnDesvincular";
            btnDesvincular.Size = new Size(108, 32);
            btnDesvincular.TabIndex = 14;
            btnDesvincular.Text = "Desvincular";
            btnDesvincular.UseVisualStyleBackColor = false;
            btnDesvincular.Click += btnDesvincular_Click;
            // 
            // FrmSeleccionarFamilia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(218, 237, 223);
            ClientSize = new Size(401, 166);
            Controls.Add(btnDesvincular);
            Controls.Add(lblTituloBitacora);
            Controls.Add(cmbDestino);
            Controls.Add(cmbOrigen);
            Controls.Add(btnVincular);
            Controls.Add(btnCancelar);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmSeleccionarFamilia";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Insertar Familia a Familia";
            Load += FrmSeleccionarFamilia_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnCancelar;
        private Button btnAceptar;
        private ComboBox cmbOrigen;
        private ComboBox cmbDestino;
        private Label lblTituloBitacora;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnVincular;
        private Button btnDesvincular;
    }
}