using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL.Perfiles; // Asegurate de importar tu BLL

namespace UI // Cambiá esto por el namespace de tu proyecto
{
    public partial class FrmSeleccionarFamilia : Form
    {
        // 1. Propiedades para que la pantalla principal pueda leer qué eligió el usuario
        public int IdFamiliaOrigen { get; private set; }
        public int IdFamiliaDestino { get; private set; }
        public string NombreFamiliaOrigen { get; private set; }
        public string NombreFamiliaDestino { get; private set; }
        public bool EsVinculacion { get; private set; }

        private FamiliaBLL _familiaBLL;

        public FrmSeleccionarFamilia()
        {
            InitializeComponent();
            _familiaBLL = new FamiliaBLL();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void FrmSeleccionarFamilia_Load(object sender, EventArgs e)
        {
            try
            {
                var todasLasFamilias = _familiaBLL.ObtenerTodasLasFamilias();

                cmbOrigen.DisplayMember = "Nombre";
                cmbOrigen.ValueMember = "Id";
                cmbOrigen.DataSource = todasLasFamilias.ToList();
                cmbOrigen.SelectedIndex = -1;

                cmbDestino.DisplayMember = "Nombre";
                cmbDestino.ValueMember = "Id";
                cmbDestino.DataSource = todasLasFamilias.ToList();
                cmbDestino.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las familias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVincular_Click(object sender, EventArgs e)
        {
            if (cmbOrigen.SelectedIndex == -1 || cmbDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione la Familia Origen y la Familia Destino.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idOrigen = (int)cmbOrigen.SelectedValue;
            int idDestino = (int)cmbDestino.SelectedValue;

            if (idOrigen == idDestino)
            {
                MessageBox.Show("No puede insertar una familia dentro de sí misma.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IdFamiliaOrigen = idOrigen;
            IdFamiliaDestino = idDestino;
            NombreFamiliaOrigen = cmbOrigen.Text;
            NombreFamiliaDestino = cmbDestino.Text;

            // Le avisamos al formulario principal que apretó "Vincular"
            EsVinculacion = true;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDesvincular_Click(object sender, EventArgs e)
        {
            if (cmbOrigen.SelectedIndex == -1 || cmbDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione la Familia Origen y la Familia Destino.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idOrigen = (int)cmbOrigen.SelectedValue;
            int idDestino = (int)cmbDestino.SelectedValue;

            if (idOrigen == idDestino)
            {
                MessageBox.Show("No tiene sentido desvincular una familia de sí misma.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IdFamiliaOrigen = idOrigen;
            IdFamiliaDestino = idDestino;
            NombreFamiliaOrigen = cmbOrigen.Text;
            NombreFamiliaDestino = cmbDestino.Text;

            // Le avisamos al formulario principal que apretó "Desvincular"
            EsVinculacion = false;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}