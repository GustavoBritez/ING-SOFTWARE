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
using BE;

namespace UI
{
    public partial class Bitacora : Form
    {
        BitacoraBLL _bitacoraBLL = new BitacoraBLL();
        private List<BitacoraBE> _bitacoraCompleta; 

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
            _bitacoraCompleta = _bitacoraBLL.VerEventos();
            
            InicializarDateTimePickers();
            
            CargarBitacora(_bitacoraCompleta);
            
            dtpDesde.ValueChanged += DtpFecha_ValueChanged;
            dtpHasta.ValueChanged += DtpFecha_ValueChanged;
        }

        private void InicializarDateTimePickers()
        {
            DateTime hoy = DateTime.Today;
            
            dtpDesde.Value = hoy;
            dtpDesde.Enabled = false; 
        
            dtpHasta.Value = hoy;
            dtpHasta.MinDate = hoy.AddDays(-3);
            dtpHasta.MaxDate = hoy;
        }

        private void DtpFecha_ValueChanged(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Today;
            DateTime minimoPermitido = hoy.AddDays(-3);
            
            if (dtpHasta.Value < minimoPermitido)
            {
                MessageBox.Show(
                    "No puede seleccionar una fecha anterior a 3 días atras.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                dtpHasta.Value = minimoPermitido;
                return;
            }
            
            if (dtpHasta.Value > hoy)
            {
                MessageBox.Show(
                    "No puede seleccionar una fecha futura.",
                    "Validacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                dtpHasta.Value = hoy;
                return;
            }
            
            // Aplicar filtro de fechas
            AplicarFiltroFechas();
        }

        private void AplicarFiltroFechas()
        {
            try
            {
                DateTime fechaDesde = dtpDesde.Value.Date;
                DateTime fechaHasta = dtpHasta.Value.Date;
                
                var bitacoraFiltrada = _bitacoraCompleta
                    .Where(b => b._Fecha.Date >= fechaDesde && b._Fecha.Date <= fechaHasta)
                    .ToList();
                
                CargarBitacora(bitacoraFiltrada);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al filtrar bitacora: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarBitacora(List<BitacoraBE> bitacora)
        {
            dgvBitacora.DataSource = null;
            dgvBitacora.DataSource = bitacora;
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
