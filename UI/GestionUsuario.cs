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
        private EventoBLL bitacoraBLL = new EventoBLL();
        private string _modoActual = "";
        private UsuarioBE _usuarioEnModificacion = null;

        public GestionUsuario()
        {
            InitializeComponent();

            cmbRol.Items.Add("Usuario");
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Recepcionista");
            cmbRol.Items.Add("Medico");
            cmbRol.Items.Add("Nutricionista");
            cmbRol.SelectedIndex = 0;

            GestionUsuarios_Load(null, null);
            dgvUsuarios.SelectionChanged += DgvUsuarios_SelectionChanged;

            // Agregar event handlers para los RadioButtons de filtro
            rbMostrarActivos.CheckedChanged += RbMostrar_CheckedChanged;
            rbMostrarInactivos.CheckedChanged += RbMostrar_CheckedChanged;
        }

        private void RbMostrar_CheckedChanged(object sender, EventArgs e)
        {
            AplicarFiltroEstado();
        }

        private void AplicarFiltroEstado()
        {
            try
            {
                List<UsuarioBE> todosusuarios = usuarioBLL.ListarUsuarios();
                List<UsuarioBE> usuariosFiltrados;

                if (rbMostrarActivos.Checked)
                {
                    usuariosFiltrados = todosusuarios.Where(u => u._Estado == true).ToList();
                }
                else if (rbMostrarInactivos.Checked)
                {
                    usuariosFiltrados = todosusuarios.Where(u => u._Estado == false).ToList();
                }
                else
                {
                    usuariosFiltrados = todosusuarios;
                }

                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = usuariosFiltrados;

                ConfigurarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 1)
            {
                UsuarioBE usuarioSeleccionado = dgvUsuarios.SelectedRows[0].DataBoundItem as UsuarioBE;
                if (usuarioSeleccionado != null)
                {
                    CargarCamposDelUsuario(usuarioSeleccionado); // Cheken que atualizo los txt


                    if (_modoActual == "CambiarContrasena")
                    {
                        CambiarContrasenaDelUsuario(usuarioSeleccionado);
                    }
                }
            }
        }

        private void CargarCamposDelUsuario(UsuarioBE usuario)
        {
            txtDni.Text = usuario._Dni.ToString();
            txtNombre.Text = usuario._Nombre;
            txtApellido.Text = usuario._Apellido;
            cmbRol.SelectedItem = usuario._Rol;
            txtNombreUsuario.Text = usuario._NombreDeUsuario;
            CKB_Desactivar.Checked = !usuario._Estado;
            CKB_Activar.Checked = usuario._Estado;
        }

        private void CambiarContrasenaDelUsuario(UsuarioBE usuario)
        {
            try
            {
                string nuevaContraseña = Interaction.InputBox(
                    "Ingrese la nueva contraseña (mínimo 3 caracteres):",
                    "Cambiar Contraseña"
                );

                if (string.IsNullOrWhiteSpace(nuevaContraseña))
                {
                    MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RestablecerModoCambiarContrasena();
                    return;
                }

                if (nuevaContraseña.Length < 3)
                {
                    MessageBox.Show("La contraseña debe tener al menos 3 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RestablecerModoCambiarContrasena();
                    return;
                }

                usuario._Contraseña = nuevaContraseña;
                usuarioBLL.ModificarUsuario(usuario);

                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Cambio de contraseña para usuario '{usuario._NombreDeUsuario}' (DNI: {usuario._Dni})";
                bitacoraBLL.RegistrarEvento(5, descripcion, dniActual, "GestionUsuario");

                MessageBox.Show($"Contraseña del usuario '{usuario._NombreDeUsuario}' cambiada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RestablecerModoCambiarContrasena();
                GestionUsuarios_Load(null, null);
            }
            catch (Exception ex)
            {
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Error al cambiar contraseña del usuario '{usuario._NombreDeUsuario}': {ex.Message}";
                bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "GestionUsuario");

                MessageBox.Show($"Error al cambiar contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RestablecerModoCambiarContrasena();
            }
        }

        private void RestablecerModoCambiarContrasena()
        {
            _modoActual = "";
            dgvUsuarios.ClearSelection();
            LimpiarCampos();


            btnCrear.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnActDesact.Enabled = true;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            _modoActual = "Crear";
            HabilitarModoCrear();
        }

        private void HabilitarModoCrear()
        {

            dgvUsuarios.ClearSelection();


            LimpiarCampos();


            txtDni.Enabled = true;
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            cmbRol.Enabled = true;
            txtNombreUsuario.Enabled = true;


            CKB_Desactivar.Enabled = false;
            CKB_Activar.Enabled = false;


            btnAceptar.Visible = true;
            btnAceptar.Enabled = true;
            btnCancelar.Visible = true;
            btnCancelar.Enabled = true;

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnActDesact.Enabled = false;

            txtDni.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (_modoActual == "Crear")
            {
                CrearUsuario();
            }
            else if (_modoActual == "Modificar")
            {
                ModificarUsuario();
            }
        }

        private void CrearUsuario()
        {
            try
            {
                if (!ValidarCamposCrear())
                {
                    return;
                }

                string _dni = txtDni.Text;
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string nombreDeUsuario = txtNombreUsuario.Text.Trim();

                if (!int.TryParse(_dni, out int dni))
                {
                    MessageBox.Show("Error, el DNI debe ser entero y con 8 dígitos");
                    return;
                }

                if (dni < 10000000)
                {
                    MessageBox.Show("Error, el DNI debe tener 8 dígitos");
                    return;
                }
                // Aseguramos el nombre + dni 
                string contraseña = $"{nombre}{_dni}";
                string rol = cmbRol.SelectedItem?.ToString() ?? "Usuario";

                UsuarioBE nuevoUsuario = new UsuarioBE(
                    nombre: nombre,
                    apellido: apellido,
                    dni: dni,
                    nombreDeUsuario: nombreDeUsuario,
                    contraseña: contraseña,
                    rol: rol,
                    bloqueado: true,
                    estado: true
                );
                usuarioBLL.CrearUsuario(nuevoUsuario);

                MessageBox.Show(
                    $"Usuario '{nombreDeUsuario}' creado exitosamente.\nContraseña: {contraseña}",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                CancelarOperacion();
                GestionUsuarios_Load(null, null);
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

        private bool ValidarCamposCrear()
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarOperacion();
        }

        private void CancelarOperacion()
        {
            _modoActual = "";
            _usuarioEnModificacion = null;


            LimpiarCampos();


            btnAceptar.Visible = false;
            btnAceptar.Enabled = false;
            btnCancelar.Visible = false;
            btnCancelar.Enabled = false;

            btnCrear.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnActDesact.Enabled = true;
            //btnCambiarContrasena.Enabled = true;


            txtDni.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            cmbRol.Enabled = false;
            txtNombreUsuario.Enabled = false;
            CKB_Desactivar.Enabled = false;
            CKB_Activar.Enabled = false;
        }

        private void LimpiarCampos()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtNombreUsuario.Clear();
            cmbRol.SelectedIndex = -1;
            CKB_Desactivar.Checked = false;
            CKB_Activar.Checked = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerMenuPrincipal());
        }

        private void GestionUsuario_Load(object sender, EventArgs e)
        {

            txtDni.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            cmbRol.Enabled = false;
            txtNombreUsuario.Enabled = false;
            CKB_Desactivar.Enabled = false;
            CKB_Activar.Enabled = false;


            btnAceptar.Visible = false;
            btnAceptar.Enabled = false;
            btnCancelar.Visible = false;
            btnCancelar.Enabled = false;

            GestionUsuarios_Load(sender, e);
        }

        public void GestionUsuarios_Load(object sender, EventArgs e)
        {
            AplicarFiltroEstado();
        }

        private void ConfigurarColumnasDataGridView()
        {
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvUsuarios.Columns.Contains("_Contraseña"))
            {
                dgvUsuarios.Columns["_Contraseña"].Visible = false;
            }

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

            if (dgvUsuarios.Columns.Contains("_Bloqueado"))
            {
                dgvUsuarios.Columns["_Bloqueado"].HeaderText = "Bloqueado";
            }

            if (dgvUsuarios.Columns.Contains("_Estado"))
            {
                dgvUsuarios.Columns["_Estado"].HeaderText = "Estado";
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
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

                _modoActual = "Modificar";
                _usuarioEnModificacion = usuarioSeleccionado;
                HabilitarModoModificar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HabilitarModoModificar()
        {
            txtDni.Enabled = false; // DNI no se puede editar
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            cmbRol.Enabled = true;
            txtNombreUsuario.Enabled = true;


            CKB_Desactivar.Enabled = false;
            CKB_Activar.Enabled = false;


            btnAceptar.Visible = true;
            btnAceptar.Enabled = true;
            btnCancelar.Visible = true;
            btnCancelar.Enabled = true;


            btnCrear.Enabled = false;
            btnEliminar.Enabled = false;
            btnActDesact.Enabled = false;
        }

        private void ModificarUsuario()
        {
            try
            {
                if (_usuarioEnModificacion is null)
                {
                    MessageBox.Show("Error: No hay usuario en modificación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string cambios = "";
                if (!string.IsNullOrWhiteSpace(txtNombre.Text) && _usuarioEnModificacion._Nombre != txtNombre.Text.Trim())
                {
                    cambios += $"Nombre: {_usuarioEnModificacion._Nombre} -> {txtNombre.Text.Trim()}; ";
                    _usuarioEnModificacion._Nombre = txtNombre.Text.Trim();
                }

                if (!string.IsNullOrWhiteSpace(txtApellido.Text) && _usuarioEnModificacion._Apellido != txtApellido.Text.Trim())
                {
                    cambios += $"Apellido: {_usuarioEnModificacion._Apellido} -> {txtApellido.Text.Trim()}; ";
                    _usuarioEnModificacion._Apellido = txtApellido.Text.Trim();
                }

                if (!string.IsNullOrWhiteSpace(txtNombreUsuario.Text) && _usuarioEnModificacion._NombreDeUsuario != txtNombreUsuario.Text.Trim())
                {
                    cambios += $"NombreUsuario: {_usuarioEnModificacion._NombreDeUsuario} -> {txtNombreUsuario.Text.Trim()}; ";
                    _usuarioEnModificacion._NombreDeUsuario = txtNombreUsuario.Text.Trim();
                }

                if (cmbRol.SelectedItem != null && _usuarioEnModificacion._Rol != cmbRol.SelectedItem.ToString())
                {
                    cambios += $"Rol: {_usuarioEnModificacion._Rol} -> {cmbRol.SelectedItem.ToString()}; ";
                    _usuarioEnModificacion._Rol = cmbRol.SelectedItem.ToString();
                }

                var u = usuarioBLL.ListarUsuarios().Find(x => x._NombreDeUsuario == txtNombreUsuario.Text.ToString());
                if (u != null)
                {
                    throw new Exception($"Ya existe un usuario con el nombre de Usuario: {txtNombreUsuario.Text.ToString()}");
                }
                usuarioBLL.ModificarUsuario(_usuarioEnModificacion);

                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Modificación de usuario '{_usuarioEnModificacion._NombreDeUsuario}' (DNI: {_usuarioEnModificacion._Dni}). Cambios: {(string.IsNullOrEmpty(cambios) ? "Sin cambios" : cambios)}";
                bitacoraBLL.RegistrarEvento(2, descripcion, dniActual, "GestionUsuario");

                MessageBox.Show("Usuario modificado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CancelarOperacion();
                GestionUsuarios_Load(null, null);
            }
            catch (Exception ex)
            {

                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Error al modificar usuario: {ex.Message}";
                bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "GestionUsuario");

                MessageBox.Show($"Error: Modificaciones no aplicadas. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Error: Seleccione una fila para desbloquear/bloquear", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UsuarioBE usuarioSeleccionado = dgvUsuarios.SelectedRows[0].DataBoundItem as UsuarioBE;

                if (usuarioSeleccionado is null)
                {
                    MessageBox.Show("Error: No se pudo seleccionar un usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //bool estabaBloqueado = usuarioSeleccionado._Bloqueado;

                if (usuarioSeleccionado._Bloqueado == false)
                {
                    MessageBox.Show("Error: El Usuario no esta bloqueado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                usuarioSeleccionado._Bloqueado = !usuarioSeleccionado._Bloqueado;
                usuarioBLL.Desbloquear(usuarioSeleccionado);

                string bloqueado = usuarioSeleccionado._Bloqueado ? "bloqueado" : "desbloqueado";


                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Usuario '{usuarioSeleccionado._NombreDeUsuario}' (DNI: {usuarioSeleccionado._Dni}) {bloqueado}";
                bitacoraBLL.RegistrarEvento(2, descripcion, dniActual, "GestionUsuario");

                MessageBox.Show($"Usuario '{usuarioSeleccionado._NombreDeUsuario}' {bloqueado} correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GestionUsuarios_Load(null, null);
            }
            catch (Exception ex)
            {

                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Error al cambiar bloqueo de usuario: {ex.Message}";
                bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "GestionUsuario");

                MessageBox.Show($"Error: No se pudo cambiar el estado del usuario. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActDesact_Click(object sender, EventArgs e)
        {
            try
            {
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

                bool estabaActivo = usuarioSeleccionado._Estado;
                usuarioBLL.CambiarEstado(usuarioSeleccionado);
                MessageBox.Show($"Usuario '{usuarioSeleccionado._NombreDeUsuario}' cambio correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: No se pudo cambiar el estado del usuario. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GestionUsuarios_Load(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AplicarFiltroEstado();
        }
    }
}
