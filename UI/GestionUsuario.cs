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
using Microsoft.VisualBasic;
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
            cmbRol.SelectedIndex = 0;
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void GestionUsuario_Load(object sender, EventArgs e)
        {
            GestionUsuarios_Load(sender, e);
        }
        public void GestionUsuarios_Load(object sender, EventArgs e)
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuarioBLL.ListarUsuarios();

            // Configurar el DataGridView como read-only y selección de fila completa
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ocultar la columna de Contraseña
            if (dgvUsuarios.Columns.Contains("_Contraseña"))
            {
                dgvUsuarios.Columns["_Contraseña"].Visible = false;
            }

            // Configurar encabezados y propiedades de las columnas
            if (dgvUsuarios.Columns.Contains("_Dni"))
            {
                dgvUsuarios.Columns["_Dni"].HeaderText = "DNI";
            }

            if (dgvUsuarios.Columns.Contains("_Nombre"))
            {
                dgvUsuarios.Columns["_Nombre"].HeaderText = "Nombre";
            }

            if (dgvUsuarios.Columns.Contains("_Apellido"))
            {
                dgvUsuarios.Columns["_Apellido"].HeaderText = "Apellido";
            }

            if (dgvUsuarios.Columns.Contains("_NombreDeUsuario"))
            {
                dgvUsuarios.Columns["_NombreDeUsuario"].HeaderText = "Nombre de Usuario";
            }

            if (dgvUsuarios.Columns.Contains("_Rol"))
            {
                dgvUsuarios.Columns["_Rol"].HeaderText = "Rol";
            }

            if (dgvUsuarios.Columns.Contains("Bloqueado"))
            {
                dgvUsuarios.Columns["_Bloqueado"].HeaderText = "Estado";
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que se haya seleccionado exactamente una fila
                if (dgvUsuarios.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Error: Seleccione una fila para Modificar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UsuarioBE usuarioSeleccionado = dgvUsuarios.SelectedRows[0].DataBoundItem as UsuarioBE;

                if (usuarioSeleccionado is null)
                {
                    MessageBox.Show("Error: No se pudo seleccionar un usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Usted está a punto de modificar los datos del usuario '{usuarioSeleccionado._NombreDeUsuario}'", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Actualizar solo los campos que se hayan completado
                if (!string.IsNullOrWhiteSpace(txtNombre.Text))
                    usuarioSeleccionado._Nombre = txtNombre.Text.Trim();

                if (!string.IsNullOrWhiteSpace(txtApellido.Text))
                    usuarioSeleccionado._Apellido = txtApellido.Text.Trim();

                if (!string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
                    usuarioSeleccionado._NombreDeUsuario = txtNombreUsuario.Text.Trim();

                if (cmbRol.SelectedItem != null)
                    usuarioSeleccionado._Rol = cmbRol.SelectedItem.ToString();

                // Preguntar si desea cambiar contraseña
                DialogResult result = MessageBox.Show("¿Desea cambiar la contraseña?", "Cambiar Contraseña", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string nuevaContraseña = Interaction.InputBox("Ingrese nueva contraseña:", "Nueva Contraseña");
                    if (!string.IsNullOrWhiteSpace(nuevaContraseña))
                        usuarioSeleccionado._Contraseña = nuevaContraseña;
                }

                usuarioSeleccionado._Bloqueado = rbEstadoInactivo.Checked;

                usuarioBLL.ModificarUsuario(usuarioSeleccionado);

                MessageBox.Show("Usuario modificado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: Modificaciones no aplicadas. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GestionUsuarios_Load(sender, e);
            }
        }
    }
}
