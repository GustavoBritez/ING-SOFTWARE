using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using Services;

namespace UI
{
    public partial class GestionUsuario : Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        public GestionUsuario()
        {
            InitializeComponent();

            cmbRol.Items.Add("Usuario");
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Recepcionista");
            cmbRol.Items.Add("Medico");
            cmbRol.Items.Add("Nutricionista");
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {

                /*if (!ValidarCampos())
                {
                    return;
                }*/


                string _dni = txtDni.Text;
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string nombreDeUsuario = txtNombreUsuario.Text.Trim();

                if (!int.TryParse(_dni, out int dni))
                {
                    if (dni < 90000000)
                    {
                        MessageBox.Show("Error, el DNI debe ser entero y con 8 digitos");
                    }
                }

                string contraseña = $"{txtNombre.Text}{txtDni.Text}";
                string rol = cmbRol.SelectedItem?.ToString() ?? "Usuario";
                bool bloqueado = rbEstadoInactivo.Checked == false;


                UsuarioBE nuevoUsuario = new UsuarioBE(
                    nombre: nombre,
                    apellido: apellido,
                    dni: dni,
                    nombreDeUsuario: nombreDeUsuario,
                    contraseña: contraseña,
                    rol: rol,
                    bloqueado: bloqueado
                );


                usuarioBLL.CrearUsuario(nuevoUsuario);

                MessageBox.Show(
                    $"Usuario '{nombreDeUsuario}' creado exitosamente.\nContraseña: {contraseña}",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al crear usuario: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                MessageBox.Show("El DNI es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El Nombre es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El Apellido es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
            {
                MessageBox.Show("El Nombre de Usuario es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbRol.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un Rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtNombreUsuario.Clear();
            cmbRol.SelectedIndex = -1;
            rbEstadoActivo.Checked = true;
            rbEstadoInactivo.Checked = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            FormManager.Navegar(this, FormManager.ObtenerForm1());
        }
    }
}
