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
            Perfil_A_Familia = new ToolStripMenuItem();
            permisoAlPerfilToolStripMenuItem = new ToolStripMenuItem();
            permisoToolStripMenuItem = new ToolStripMenuItem();
            perfilToolStripMenuItem = new ToolStripMenuItem();
            familiaToolStripMenuItem = new ToolStripMenuItem();
            toolStripLabel2 = new ToolStripDropDownButton();
            familiaAlPerfilToolStripMenuItem1 = new ToolStripMenuItem();
            permisoAlPerfilToolStripMenuItem1 = new ToolStripMenuItem();
            permisoToolStripMenuItem1 = new ToolStripMenuItem();
            perfilToolStripMenuItem1 = new ToolStripMenuItem();
            familiaToolStripMenuItem1 = new ToolStripMenuItem();
            dgvPerfiles = new DataGridView();
            panel1 = new Panel();
            labelPerfil = new Label();
            dgvFamilias = new DataGridView();
            panel2 = new Panel();
            labelFamilia = new Label();
            btnSalir = new Button();
            Vista_Familia = new TreeView();
            dgvPermisos = new DataGridView();
            panel3 = new Panel();
            labelPermiso = new Label();
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
            TS_Gestion.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripLabel2 });
            TS_Gestion.Location = new Point(0, 0);
            TS_Gestion.Name = "TS_Gestion";
            TS_Gestion.Size = new Size(1199, 25);
            TS_Gestion.TabIndex = 6;
            TS_Gestion.Text = "TS_Gestion";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripLabel1.DropDownItems.AddRange(new ToolStripItem[] { Perfil_A_Familia, permisoAlPerfilToolStripMenuItem, permisoToolStripMenuItem, perfilToolStripMenuItem, familiaToolStripMenuItem });
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(62, 22);
            toolStripLabel1.Text = "Agregar";
            // 
            // Perfil_A_Familia
            // 
            Perfil_A_Familia.Name = "Perfil_A_Familia";
            Perfil_A_Familia.Size = new Size(159, 22);
            Perfil_A_Familia.Text = "Perfil a Familia";
            Perfil_A_Familia.Click += Agregar_Perfil_A_Familia;
            // 
            // permisoAlPerfilToolStripMenuItem
            // 
            permisoAlPerfilToolStripMenuItem.Name = "permisoAlPerfilToolStripMenuItem";
            permisoAlPerfilToolStripMenuItem.Size = new Size(159, 22);
            permisoAlPerfilToolStripMenuItem.Text = "Permiso al perfil";
            permisoAlPerfilToolStripMenuItem.Click += Agregar_permisoAlPerfil;
            // 
            // permisoToolStripMenuItem
            // 
            permisoToolStripMenuItem.Name = "permisoToolStripMenuItem";
            permisoToolStripMenuItem.Size = new Size(159, 22);
            permisoToolStripMenuItem.Text = "Permiso";
            permisoToolStripMenuItem.Click += Agregar_Permiso;
            // 
            // perfilToolStripMenuItem
            // 
            perfilToolStripMenuItem.Name = "perfilToolStripMenuItem";
            perfilToolStripMenuItem.Size = new Size(159, 22);
            perfilToolStripMenuItem.Text = "Perfil";
            perfilToolStripMenuItem.Click += Agregar_Perfil;
            // 
            // familiaToolStripMenuItem
            // 
            familiaToolStripMenuItem.Name = "familiaToolStripMenuItem";
            familiaToolStripMenuItem.Size = new Size(159, 22);
            familiaToolStripMenuItem.Text = "Familia";
            familiaToolStripMenuItem.Click += Agregar_Familia;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.DropDownItems.AddRange(new ToolStripItem[] { familiaAlPerfilToolStripMenuItem1, permisoAlPerfilToolStripMenuItem1, permisoToolStripMenuItem1, perfilToolStripMenuItem1, familiaToolStripMenuItem1 });
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(63, 22);
            toolStripLabel2.Text = "Eliminar";
            // 
            // familiaAlPerfilToolStripMenuItem1
            // 
            familiaAlPerfilToolStripMenuItem1.Name = "familiaAlPerfilToolStripMenuItem1";
            familiaAlPerfilToolStripMenuItem1.Size = new Size(159, 22);
            familiaAlPerfilToolStripMenuItem1.Text = "Perfil al Familia";
            familiaAlPerfilToolStripMenuItem1.Click += Eliminar_PerfilAFamilia_Click;
            // 
            // permisoAlPerfilToolStripMenuItem1
            // 
            permisoAlPerfilToolStripMenuItem1.Name = "permisoAlPerfilToolStripMenuItem1";
            permisoAlPerfilToolStripMenuItem1.Size = new Size(159, 22);
            permisoAlPerfilToolStripMenuItem1.Text = "Permiso al perfil";
            permisoAlPerfilToolStripMenuItem1.Click += Eliminar_permisoAlPerfil;
            // 
            // permisoToolStripMenuItem1
            // 
            permisoToolStripMenuItem1.Name = "permisoToolStripMenuItem1";
            permisoToolStripMenuItem1.Size = new Size(159, 22);
            permisoToolStripMenuItem1.Text = "Permiso";
            permisoToolStripMenuItem1.Click += Eliminar_Permiso_Click;
            // 
            // perfilToolStripMenuItem1
            // 
            perfilToolStripMenuItem1.Name = "perfilToolStripMenuItem1";
            perfilToolStripMenuItem1.Size = new Size(159, 22);
            perfilToolStripMenuItem1.Text = "Perfil";
            perfilToolStripMenuItem1.Click += Eliminar_Perfil_Click;
            // 
            // familiaToolStripMenuItem1
            // 
            familiaToolStripMenuItem1.Name = "familiaToolStripMenuItem1";
            familiaToolStripMenuItem1.Size = new Size(159, 22);
            familiaToolStripMenuItem1.Text = "Familia";
            familiaToolStripMenuItem1.Click += Eliminar_Familia_Click;
            // 
            // dgvPerfiles
            // 
            dgvPerfiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPerfiles.Location = new Point(292, 76);
            dgvPerfiles.Name = "dgvPerfiles";
            dgvPerfiles.Size = new Size(293, 267);
            dgvPerfiles.TabIndex = 7;
            dgvPerfiles.SelectionChanged += dgvPerfiles_SelectionChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(143, 188, 153);
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(labelPerfil);
            panel1.Location = new Point(292, 42);
            panel1.Name = "panel1";
            panel1.Size = new Size(293, 28);
            panel1.TabIndex = 8;
            // 
            // labelPerfil
            // 
            labelPerfil.AutoSize = true;
            labelPerfil.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            labelPerfil.ForeColor = Color.White;
            labelPerfil.Location = new Point(48, -2);
            labelPerfil.Name = "labelPerfil";
            labelPerfil.Size = new Size(184, 28);
            labelPerfil.TabIndex = 14;
            labelPerfil.Text = "Perfiles Existentes";
            // 
            // dgvFamilias
            // 
            dgvFamilias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFamilias.Location = new Point(633, 76);
            dgvFamilias.Name = "dgvFamilias";
            dgvFamilias.Size = new Size(184, 271);
            dgvFamilias.TabIndex = 9;
            dgvFamilias.SelectionChanged += dgvFamilias_SelectionChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(143, 188, 153);
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(labelFamilia);
            panel2.Location = new Point(633, 42);
            panel2.Name = "panel2";
            panel2.Size = new Size(479, 28);
            panel2.TabIndex = 10;
            // 
            // labelFamilia
            // 
            labelFamilia.AutoSize = true;
            labelFamilia.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            labelFamilia.ForeColor = Color.White;
            labelFamilia.Location = new Point(132, -2);
            labelFamilia.Name = "labelFamilia";
            labelFamilia.Size = new Size(190, 28);
            labelFamilia.TabIndex = 14;
            labelFamilia.Text = "Familias Existentes";
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
            Vista_Familia.Location = new Point(823, 72);
            Vista_Familia.Name = "Vista_Familia";
            Vista_Familia.Size = new Size(289, 271);
            Vista_Familia.TabIndex = 12;
            // 
            // dgvPermisos
            // 
            dgvPermisos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPermisos.Location = new Point(12, 76);
            dgvPermisos.Name = "dgvPermisos";
            dgvPermisos.Size = new Size(227, 267);
            dgvPermisos.TabIndex = 13;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(143, 188, 153);
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(labelPermiso);
            panel3.Location = new Point(12, 41);
            panel3.Name = "panel3";
            panel3.Size = new Size(227, 28);
            panel3.TabIndex = 16;
            // 
            // labelPermiso
            // 
            labelPermiso.AutoSize = true;
            labelPermiso.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            labelPermiso.ForeColor = Color.White;
            labelPermiso.Location = new Point(66, -2);
            labelPermiso.Name = "labelPermiso";
            labelPermiso.Size = new Size(87, 28);
            labelPermiso.TabIndex = 14;
            labelPermiso.Text = "Permiso";
            // 
            // Perfiles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(218, 237, 223);
            ClientSize = new Size(1199, 396);
            Controls.Add(panel3);
            Controls.Add(dgvPermisos);
            Controls.Add(Vista_Familia);
            Controls.Add(btnSalir);
            Controls.Add(dgvPerfiles);
            Controls.Add(panel2);
            Controls.Add(TS_Gestion);
            Controls.Add(panel1);
            Controls.Add(dgvFamilias);
            Name = "Perfiles";
            StartPosition = FormStartPosition.CenterScreen;
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
        private ToolStripMenuItem Perfil_A_Familia;
        private ToolStripMenuItem permisoAlPerfilToolStripMenuItem;
        private ToolStripDropDownButton toolStripLabel2;
        private ToolStripMenuItem familiaAlPerfilToolStripMenuItem1;
        private ToolStripMenuItem permisoAlPerfilToolStripMenuItem1;
        private DataGridView dgvPerfiles;
        private Panel panel2;
        private Label labelFamilia;
        private DataGridView dgvFamilias;
        private Panel panel1;
        private Label labelPerfil;
        private Button btnSalir;
        private TreeView Vista_Familia;
        private DataGridView dgvPermisos;
        private Panel panel3;
        private Label labelPermiso;
        private ToolStripMenuItem permisoToolStripMenuItem;
        private ToolStripMenuItem perfilToolStripMenuItem;
        private ToolStripMenuItem familiaToolStripMenuItem;
        private ToolStripMenuItem permisoToolStripMenuItem1;
        private ToolStripMenuItem perfilToolStripMenuItem1;
        private ToolStripMenuItem familiaToolStripMenuItem1;
    }
}