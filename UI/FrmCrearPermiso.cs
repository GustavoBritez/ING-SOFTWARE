using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmCrearPermiso : Form
    {
        // Esta propiedad pública es la que va a leer tu pantalla principal
        public string NombrePermiso { get; private set; }

        public FrmCrearPermiso()
        {
            InitializeComponent();

            // Opcional: Centrar el form en la pantalla
            this.StartPosition = FormStartPosition.CenterParent;
            // Opcional: Evitar que lo maximicen
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Validamos acá mismo en la vista
            if (string.IsNullOrWhiteSpace(txtNombrePermiso.Text))
            {
                MessageBox.Show("El nombre del permiso no puede estar vacío.", "Atención");
                return;
            }

            // Guardamos el texto en nuestra propiedad pública
            NombrePermiso = txtNombrePermiso.Text.Trim();

            // Le decimos a Windows Forms que todo salió OK y cerramos
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Le decimos a Windows Forms que el usuario canceló
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
