namespace UI
{
    partial class Perfiles
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
            TS_Gestion = new ToolStrip();
            toolStripLabel1 = new ToolStripDropDownButton();
            familiaAlPerfilToolStripMenuItem = new ToolStripMenuItem();
            permisoAlPerfilToolStripMenuItem = new ToolStripMenuItem();
            permisoToolStripMenuItem = new ToolStripMenuItem();
            toolStripLabel2 = new ToolStripDropDownButton();
            familiaAlPerfilToolStripMenuItem1 = new ToolStripMenuItem();
            permisoAlPerfilToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripLabel3 = new ToolStripDropDownButton();
            familiaDelPefilToolStripMenuItem = new ToolStripMenuItem();
            permisoDelPerfilToolStripMenuItem = new ToolStripMenuItem();
            dgvPerfiles = new DataGridView();
            panel1 = new Panel();
            label1 = new Label();
            dgvFamilias = new DataGridView();
            panel2 = new Panel();
            label2 = new Label();
            btnSalir = new Button();
            Vista_Familia = new TreeView();
            sqlCommandBuilder1 = new Microsoft.Data.SqlClient.SqlCommandBuilder();
            dgvPermisos = new DataGridView();
            panel3 = new Panel();
            label4 = new Label();
            TS_Gestion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPerfiles).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFamilias).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPermisos).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // TS_Gestion
            // 
            TS_Gestion.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripLabel2, toolStripLabel3 });
            TS_Gestion.Location = new Point(0, 0);
            TS_Gestion.Name = "TS_Gestion";
            TS_Gestion.Size = new Size(891, 25);
            TS_Gestion.TabIndex = 6;
            TS_Gestion.Text = "TS_Gestion";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripLabel1.DropDownItems.AddRange(new ToolStripItem[] { familiaAlPerfilToolStripMenuItem, permisoAlPerfilToolStripMenuItem, permisoToolStripMenuItem });
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(62, 22);
            toolStripLabel1.Text = "Agregar";
            // 
            // familiaAlPerfilToolStripMenuItem
            // 
            familiaAlPerfilToolStripMenuItem.Name = "familiaAlPerfilToolStripMenuItem";
            familiaAlPerfilToolStripMenuItem.Size = new Size(180, 22);
            familiaAlPerfilToolStripMenuItem.Text = "Familia al perfil";
            familiaAlPerfilToolStripMenuItem.Click += familiaAlPerfilToolStripMenuItem_Click;
            // 
            // permisoAlPerfilToolStripMenuItem
            // 
            permisoAlPerfilToolStripMenuItem.Name = "permisoAlPerfilToolStripMenuItem";
            permisoAlPerfilToolStripMenuItem.Size = new Size(180, 22);
            permisoAlPerfilToolStripMenuItem.Text = "Permiso al perfil";
            permisoAlPerfilToolStripMenuItem.Click += permisoAlPerfilToolStripMenuItem_Click;
            // 
            // permisoToolStripMenuItem
            // 
            permisoToolStripMenuItem.Name = "permisoToolStripMenuItem";
            permisoToolStripMenuItem.Size = new Size(180, 22);
            permisoToolStripMenuItem.Text = "Permiso";
            permisoToolStripMenuItem.Click += permisoToolStripMenuItem_Click;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.DropDownItems.AddRange(new ToolStripItem[] { familiaAlPerfilToolStripMenuItem1, permisoAlPerfilToolStripMenuItem1 });
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(63, 22);
            toolStripLabel2.Text = "Eliminar";
            // 
            // familiaAlPerfilToolStripMenuItem1
            // 
            familiaAlPerfilToolStripMenuItem1.Name = "familiaAlPerfilToolStripMenuItem1";
            familiaAlPerfilToolStripMenuItem1.Size = new Size(159, 22);
            familiaAlPerfilToolStripMenuItem1.Text = "Familia al perfil";
            familiaAlPerfilToolStripMenuItem1.Click += familiaAlPerfilToolStripMenuItem1_Click;
            // 
            // permisoAlPerfilToolStripMenuItem1
            // 
            permisoAlPerfilToolStripMenuItem1.Name = "permisoAlPerfilToolStripMenuItem1";
            permisoAlPerfilToolStripMenuItem1.Size = new Size(159, 22);
            permisoAlPerfilToolStripMenuItem1.Text = "Permiso al perfil";
            permisoAlPerfilToolStripMenuItem1.Click += permisoAlPerfilToolStripMenuItem1_Click;
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.DropDownItems.AddRange(new ToolStripItem[] { familiaDelPefilToolStripMenuItem, permisoDelPerfilToolStripMenuItem });
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(71, 22);
            toolStripLabel3.Text = "Modificar";
            // 
            // familiaDelPefilToolStripMenuItem
            // 
            familiaDelPefilToolStripMenuItem.Name = "familiaDelPefilToolStripMenuItem";
            familiaDelPefilToolStripMenuItem.Size = new Size(180, 22);
            familiaDelPefilToolStripMenuItem.Text = "Familia del pefil";
            // 
            // permisoDelPerfilToolStripMenuItem
            // 
            permisoDelPerfilToolStripMenuItem.Name = "permisoDelPerfilToolStripMenuItem";
            permisoDelPerfilToolStripMenuItem.Size = new Size(180, 22);
            permisoDelPerfilToolStripMenuItem.Text = "Permiso del perfil";
            // 
            // dgvPerfiles
            // 
            dgvPerfiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPerfiles.Location = new Point(205, 72);
            dgvPerfiles.Name = "dgvPerfiles";
            dgvPerfiles.Size = new Size(218, 267);
            dgvPerfiles.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(143, 188, 153);
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(205, 38);
            panel1.Name = "panel1";
            panel1.Size = new Size(218, 28);
            panel1.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(20, -2);
            label1.Name = "label1";
            label1.Size = new Size(184, 28);
            label1.TabIndex = 14;
            label1.Text = "Perfiles Existentes";
            // 
            // dgvFamilias
            // 
            dgvFamilias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFamilias.Location = new Point(444, 72);
            dgvFamilias.Name = "dgvFamilias";
            dgvFamilias.Size = new Size(140, 267);
            dgvFamilias.TabIndex = 9;
            dgvFamilias.SelectionChanged += dgvFamilias_SelectionChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(143, 188, 153);
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label2);
            panel2.Location = new Point(444, 37);
            panel2.Name = "panel2";
            panel2.Size = new Size(435, 28);
            panel2.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(132, -2);
            label2.Name = "label2";
            label2.Size = new Size(190, 28);
            label2.TabIndex = 14;
            label2.Text = "Familias Existentes";
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(255, 120, 120);
            btnSalir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Black;
            btnSalir.Location = new Point(12, 345);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(115, 40);
            btnSalir.TabIndex = 11;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // Vista_Familia
            // 
            Vista_Familia.Location = new Point(590, 72);
            Vista_Familia.Name = "Vista_Familia";
            Vista_Familia.Size = new Size(289, 267);
            Vista_Familia.TabIndex = 12;
            // 
            // dgvPermisos
            // 
            dgvPermisos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPermisos.Location = new Point(12, 72);
            dgvPermisos.Name = "dgvPermisos";
            dgvPermisos.Size = new Size(151, 267);
            dgvPermisos.TabIndex = 13;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(143, 188, 153);
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(label4);
            panel3.Location = new Point(12, 37);
            panel3.Name = "panel3";
            panel3.Size = new Size(151, 28);
            panel3.TabIndex = 16;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(27, -1);
            label4.Name = "label4";
            label4.Size = new Size(87, 28);
            label4.TabIndex = 14;
            label4.Text = "Permiso";
            // 
            // Perfiles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(218, 237, 223);
            ClientSize = new Size(891, 387);
            Controls.Add(panel3);
            Controls.Add(dgvPermisos);
            Controls.Add(Vista_Familia);
            Controls.Add(btnSalir);
            Controls.Add(dgvPerfiles);
            Controls.Add(dgvFamilias);
            Controls.Add(panel2);
            Controls.Add(TS_Gestion);
            Controls.Add(panel1);
            Name = "Perfiles";
            Text = "Perfiles";
            Load += Perfiles_Load;
            TS_Gestion.ResumeLayout(false);
            TS_Gestion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPerfiles).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFamilias).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPermisos).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStrip TS_Gestion;
        private ToolStripDropDownButton toolStripLabel1;
        private ToolStripMenuItem familiaAlPerfilToolStripMenuItem;
        private ToolStripMenuItem permisoAlPerfilToolStripMenuItem;
        private ToolStripDropDownButton toolStripLabel2;
        private ToolStripMenuItem familiaAlPerfilToolStripMenuItem1;
        private ToolStripMenuItem permisoAlPerfilToolStripMenuItem1;
        private ToolStripDropDownButton toolStripLabel3;
        private ToolStripMenuItem familiaDelPefilToolStripMenuItem;
        private ToolStripMenuItem permisoDelPerfilToolStripMenuItem;
        private DataGridView dgvPerfiles;
        private Panel panel2;
        private Label label2;
        private DataGridView dgvFamilias;
        private Panel panel1;
        private Label label1;
        private Button btnSalir;
        private TreeView Vista_Familia;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
        private DataGridView dgvPermisos;
        private Panel panel3;
        private Label label4;
        private ToolStripMenuItem permisoToolStripMenuItem;
    }
}