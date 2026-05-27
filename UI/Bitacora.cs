using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class Bitacora : Form
    {
        BitacoraBLL _bitacoraBLL = new BitacoraBLL();
        public Bitacora()
        {
            InitializeComponent();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerForm1());
        }

        private void Bitacora_Load(object sender, EventArgs e)
        {
            GestionBitacora_Load(sender, e);
        }
        private void GestionBitacora_Load(object sender, EventArgs e)
        {
            dgvBitacora.DataSource = _bitacoraBLL.VerEventos();
            dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvBitacora.Columns.Contains("Id_Evento"))
            {
                dgvBitacora.Columns["Id_Evento"].HeaderText = "ID Evento";
            }

            if (dgvBitacora.Columns.Contains("Fecha"))
            {
                dgvBitacora.Columns["Fecha"].HeaderText = "Fecha y Hora";
            }

            if (dgvBitacora.Columns.Contains("Dni"))
            {
                dgvBitacora.Columns["Dni"].HeaderText = "DNI Usuario";
            }

            if (dgvBitacora.Columns.Contains("Criticidad"))
            {
                dgvBitacora.Columns["Criticidad"].HeaderText = "Criticidad";
            }

            if (dgvBitacora.Columns.Contains("Modulo"))
            {
                dgvBitacora.Columns["Modulo"].HeaderText = "Módulo";
            }

            if (dgvBitacora.Columns.Contains("Descripcion"))
            {
                dgvBitacora.Columns["Descripcion"].HeaderText = "Descripción del Evento";
            }
        }
    }
}
