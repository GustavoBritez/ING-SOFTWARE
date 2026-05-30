namespace UI
{
    partial class Bitacora
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panelLateral = new Panel();
            btnExportar = new Button();
            btnLimpiarFiltros = new Button();
            dgvBitacora = new DataGridView();
            lblTitulo = new Label();
            gbFiltrosFecha = new GroupBox();
            dtpHasta = new DateTimePicker();
            lblHasta = new Label();
            dtpDesde = new DateTimePicker();
            lblDesde = new Label();
            btnSalir = new Button();
            groupBox1 = new GroupBox();
            cmbCriticidad = new ComboBox();
            label2 = new Label();
            groupBox2 = new GroupBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            panelLateral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            gbFiltrosFecha.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // panelLateral
            // 
            panelLateral.BackColor = Color.FromArgb(143, 188, 153);
            panelLateral.BorderStyle = BorderStyle.FixedSingle;
            panelLateral.Controls.Add(btnExportar);
            panelLateral.Controls.Add(btnLimpiarFiltros);
            panelLateral.Location = new Point(12, 60);
            panelLateral.Name = "panelLateral";
            panelLateral.Size = new Size(160, 131);
            panelLateral.TabIndex = 4;
            // 
            // btnExportar
            // 
            btnExportar.BackColor = Color.FromArgb(225, 225, 225);
            btnExportar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExportar.Location = new Point(15, 74);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(128, 40);
            btnExportar.TabIndex = 1;
            btnExportar.Text = "Exportar";
            btnExportar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiarFiltros
            // 
            btnLimpiarFiltros.BackColor = Color.FromArgb(225, 225, 225);
            btnLimpiarFiltros.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLimpiarFiltros.Location = new Point(15, 16);
            btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            btnLimpiarFiltros.Size = new Size(128, 40);
            btnLimpiarFiltros.TabIndex = 2;
            btnLimpiarFiltros.Text = "Limpiar";
            btnLimpiarFiltros.UseVisualStyleBackColor = false;
            btnLimpiarFiltros.Click += btnLimpiarFiltros_Click;
            // 
            // dgvBitacora
            // 
            dgvBitacora.BackgroundColor = Color.White;
            dgvBitacora.BorderStyle = BorderStyle.None;
            dgvBitacora.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(46, 94, 67);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(46, 94, 67);
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvBitacora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvBitacora.ColumnHeadersHeight = 30;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(180, 210, 190);
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvBitacora.DefaultCellStyle = dataGridViewCellStyle6;
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.GridColor = Color.FromArgb(200, 220, 205);
            dgvBitacora.Location = new Point(185, 60);
            dgvBitacora.Name = "dgvBitacora";
            dgvBitacora.ReadOnly = true;
            dgvBitacora.RowHeadersVisible = false;
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBitacora.Size = new Size(753, 335);
            dgvBitacora.TabIndex = 3;
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(46, 94, 67);
            lblTitulo.Location = new Point(12, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(926, 40);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "📋 Auditoría y Bitácora del Sistema";
            lblTitulo.TextAlign = ContentAlignment.TopCenter;
            // 
            // gbFiltrosFecha
            // 
            gbFiltrosFecha.BackColor = Color.FromArgb(180, 180, 180);
            gbFiltrosFecha.Controls.Add(dtpHasta);
            gbFiltrosFecha.Controls.Add(lblHasta);
            gbFiltrosFecha.Controls.Add(dtpDesde);
            gbFiltrosFecha.Controls.Add(lblDesde);
            gbFiltrosFecha.FlatStyle = FlatStyle.Flat;
            gbFiltrosFecha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbFiltrosFecha.Location = new Point(185, 410);
            gbFiltrosFecha.Name = "gbFiltrosFecha";
            gbFiltrosFecha.Size = new Size(753, 63);
            gbFiltrosFecha.TabIndex = 1;
            gbFiltrosFecha.TabStop = false;
            gbFiltrosFecha.Text = "Filtrar por Rango de Fechas";
            // 
            // dtpHasta
            // 
            dtpHasta.Font = new Font("Segoe UI", 9F);
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(440, 35);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(150, 23);
            dtpHasta.TabIndex = 1;
            // 
            // lblHasta
            // 
            lblHasta.Location = new Point(370, 38);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(64, 20);
            lblHasta.TabIndex = 2;
            lblHasta.Text = "Hasta:";
            lblHasta.TextAlign = ContentAlignment.TopRight;
            // 
            // dtpDesde
            // 
            dtpDesde.Font = new Font("Segoe UI", 9F);
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(180, 35);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(150, 23);
            dtpDesde.TabIndex = 3;
            // 
            // lblDesde
            // 
            lblDesde.Location = new Point(110, 38);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(64, 20);
            lblDesde.TabIndex = 4;
            lblDesde.Text = "Desde:";
            lblDesde.TextAlign = ContentAlignment.TopRight;
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
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(180, 180, 180);
            groupBox1.Controls.Add(cmbCriticidad);
            groupBox1.Controls.Add(label2);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(185, 479);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(314, 69);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filtrar por Criticidad";
            // 
            // cmbCriticidad
            // 
            cmbCriticidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCriticidad.Location = new Point(180, 35);
            cmbCriticidad.Name = "cmbCriticidad";
            cmbCriticidad.Size = new Size(115, 23);
            cmbCriticidad.TabIndex = 5;
            // 
            // label2
            // 
            label2.Location = new Point(110, 38);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 4;
            label2.Text = "Criticidad";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.FromArgb(180, 180, 180);
            groupBox2.Controls.Add(comboBox1);
            groupBox2.Controls.Add(label1);
            groupBox2.FlatStyle = FlatStyle.Flat;
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox2.Location = new Point(624, 479);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(314, 69);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Filtrar por Modulo";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Location = new Point(180, 35);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(115, 23);
            comboBox1.TabIndex = 5;
            // 
            // label1
            // 
            label1.Location = new Point(110, 38);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 4;
            label1.Text = "Modulo";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // Bitacora
            // 
            BackColor = Color.FromArgb(218, 237, 223);
            ClientSize = new Size(1072, 560);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btnSalir);
            Controls.Add(gbFiltrosFecha);
            Controls.Add(dgvBitacora);
            Controls.Add(panelLateral);
            Controls.Add(lblTitulo);
            Font = new Font("Segoe UI", 9F);
            Name = "Bitacora";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Consulta de Bitácora";
            Load += Bitacora_Load;
            panelLateral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            gbFiltrosFecha.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.DataGridView dgvBitacora;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox gbFiltrosFecha;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnSalir;
        private GroupBox groupBox1;
        private Label label2;
        private ComboBox cmbCriticidad;
        private GroupBox groupBox2;
        private ComboBox comboBox1;
        private Label label1;
    }
}