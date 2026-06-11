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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
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
            label4 = new Label();
            cmbEvento = new ComboBox();
            cmbCriticidad = new ComboBox();
            label1 = new Label();
            cmbModulo = new ComboBox();
            label2 = new Label();
            lbApellido = new Label();
            lbNombre = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            panelLateral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            gbFiltrosFecha.SuspendLayout();
            groupBox1.SuspendLayout();
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
            panelLateral.Paint += panelLateral_Paint;
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(46, 94, 67);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(46, 94, 67);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvBitacora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvBitacora.ColumnHeadersHeight = 30;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(180, 210, 190);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvBitacora.DefaultCellStyle = dataGridViewCellStyle2;
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.GridColor = Color.FromArgb(200, 220, 205);
            dgvBitacora.Location = new Point(185, 60);
            dgvBitacora.Name = "dgvBitacora";
            dgvBitacora.ReadOnly = true;
            dgvBitacora.RowHeadersVisible = false;
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBitacora.Size = new Size(1037, 335);
            dgvBitacora.TabIndex = 3;
            dgvBitacora.SelectionChanged += dgvBitacora_SelectionChanged;
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
            gbFiltrosFecha.Location = new Point(185, 401);
            gbFiltrosFecha.Name = "gbFiltrosFecha";
            gbFiltrosFecha.Size = new Size(469, 63);
            gbFiltrosFecha.TabIndex = 1;
            gbFiltrosFecha.TabStop = false;
            gbFiltrosFecha.Text = "Filtrar por Rango de Fechas";
            // 
            // dtpHasta
            // 
            dtpHasta.Font = new Font("Segoe UI", 9F);
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(297, 19);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(150, 23);
            dtpHasta.TabIndex = 1;
            // 
            // lblHasta
            // 
            lblHasta.Location = new Point(227, 27);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(64, 20);
            lblHasta.TabIndex = 2;
            lblHasta.Text = "Hasta:";
            lblHasta.TextAlign = ContentAlignment.TopRight;
            lblHasta.Click += lblHasta_Click;
            // 
            // dtpDesde
            // 
            dtpDesde.Font = new Font("Segoe UI", 9F);
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(74, 22);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(150, 23);
            dtpDesde.TabIndex = 3;
            // 
            // lblDesde
            // 
            lblDesde.Location = new Point(4, 25);
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
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(cmbEvento);
            groupBox1.Controls.Add(cmbCriticidad);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cmbModulo);
            groupBox1.Controls.Add(label2);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(185, 470);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(613, 75);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filtrar ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(423, 29);
            label4.Name = "label4";
            label4.Size = new Size(46, 15);
            label4.TabIndex = 12;
            label4.Text = "Evento";
            // 
            // cmbEvento
            // 
            cmbEvento.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEvento.Location = new Point(475, 26);
            cmbEvento.Name = "cmbEvento";
            cmbEvento.Size = new Size(115, 23);
            cmbEvento.TabIndex = 11;
            // 
            // cmbCriticidad
            // 
            cmbCriticidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCriticidad.Location = new Point(297, 26);
            cmbCriticidad.Name = "cmbCriticidad";
            cmbCriticidad.Size = new Size(115, 23);
            cmbCriticidad.TabIndex = 7;
            cmbCriticidad.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Location = new Point(227, 29);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 6;
            label1.Text = "Criticidad";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // cmbModulo
            // 
            cmbModulo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModulo.Location = new Point(93, 26);
            cmbModulo.Name = "cmbModulo";
            cmbModulo.Size = new Size(115, 23);
            cmbModulo.TabIndex = 5;
            // 
            // label2
            // 
            label2.Location = new Point(23, 29);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 4;
            label2.Text = "Módulo";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // lbApellido
            // 
            lbApellido.AutoSize = true;
            lbApellido.Font = new Font("Segoe UI Historic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbApellido.Location = new Point(660, 433);
            lbApellido.Name = "lbApellido";
            lbApellido.Size = new Size(84, 21);
            lbApellido.TabIndex = 10;
            lbApellido.Text = "Apellido :";
            // 
            // lbNombre
            // 
            lbNombre.AutoSize = true;
            lbNombre.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbNombre.Location = new Point(660, 401);
            lbNombre.Name = "lbNombre";
            lbNombre.Size = new Size(88, 21);
            lbNombre.TabIndex = 9;
            lbNombre.Text = "Nombre : ";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(754, 403);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(145, 23);
            textBox1.TabIndex = 13;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(754, 435);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(145, 23);
            textBox2.TabIndex = 14;
            // 
            // Bitacora
            // 
            BackColor = Color.FromArgb(218, 237, 223);
            ClientSize = new Size(1240, 614);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(groupBox1);
            Controls.Add(btnSalir);
            Controls.Add(gbFiltrosFecha);
            Controls.Add(lbApellido);
            Controls.Add(lbNombre);
            Controls.Add(panelLateral);
            Controls.Add(lblTitulo);
            Controls.Add(dgvBitacora);
            Font = new Font("Segoe UI", 9F);
            Name = "Bitacora";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Consulta de Bitácora";
            Load += Bitacora_Load;
            panelLateral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            gbFiltrosFecha.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private ComboBox cmbModulo;
        private ComboBox cmbCriticidad;
        private Label label1;
        private Label lbApellido;
        private Label lbNombre;
        private Label label4;
        private ComboBox cmbEvento;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}