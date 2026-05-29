using BE;
using BLL;
using Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI
{
    public partial class Form1 : Form
    {
        private readonly UsuarioBLL usuarioBLL = new UsuarioBLL();
        private readonly BitacoraBLL bitacoraBLL = new BitacoraBLL();
        private readonly ServicioBcrypt servicioB = new();
        public Form1()
        {
            InitializeComponent();
            this.Load += (s, e) => Form1_Load();
            this.Shown += (s, e) => Form1_Shown();
        }

        private void Form1_Load()
        {
            ActualizarDisponibilidadBotones();
        }

        private void Form1_Shown()
        {
            ActualizarDisponibilidadBotones();
        }
        private void ActualizarDisponibilidadBotones()
        {
            try
            {
                UsuarioBE usuarioActivo = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();
                bool tieneSession = usuarioActivo != null;

                if ( usuarioActivo is not null )
                {
                    btnLogin.Enabled = true;

                    btnTurnos.Enabled = !tieneSession;
                    btnLogout.Enabled = !tieneSession;
                    btnChangePass.Enabled = !tieneSession;
                    btnChangePass.Visible = !tieneSession; 
                    btnLogout.Enabled = tieneSession;

                    btnReportes.Enabled = false;
                    btnUsuarios.Enabled = false;
                    bool esAdmin = usuarioActivo._Rol == "Administrador";
                   
                }
                else
                {
                    bool esAdmin = false;

                    btnLogin.Enabled = true;

                    btnTurnos.Enabled = !tieneSession;
                    btnLogout.Enabled = !tieneSession;
                    btnChangePass.Enabled = !tieneSession;
                    btnChangePass.Visible = !tieneSession;
                    btnLogout.Enabled = tieneSession;

                    btnReportes.Enabled = esAdmin;
                    btnUsuarios.Enabled = esAdmin;
                }

            }
            catch
            {
                btnLogin.Enabled = true;
                btnTurnos.Enabled = false;
                btnLogout.Enabled = false;
                btnChangePass.Enabled = false;
                btnChangePass.Visible = false;
                btnReportes.Enabled = false;
                btnUsuarios.Enabled = false;
            }
        }

        private void btnTurnos_Click(object sender, EventArgs e)
        {

        }

        private void LimpiarCampos()
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioBE usuarioActual = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();
                usuarioBLL.LogOut(usuarioActual);
                MessageBox.Show("Cerrar sesión exitoso", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarDisponibilidadBotones();
                FormManager.Navegar(this, FormManager.ObtenerPresentacion());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            UsuarioBE usuarioActivo = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();
            if (usuarioActivo == null)
            {
                MessageBox.Show("Debe iniciar sesión para acceder a Gestión de Usuarios.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (usuarioActivo._Rol != "Administrador")
            {
                MessageBox.Show("Solo los administradores pueden acceder a Gestión de Usuarios.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormManager.Navegar(this, FormManager.ObtenerGestionUsuario());
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            UsuarioBE usuarioActivo = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();
            if (usuarioActivo == null)
            {
                MessageBox.Show("Debe iniciar sesión para acceder a Reportes.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (usuarioActivo._Rol != "Administrador")
            {
                MessageBox.Show("Solo los administradores pueden acceder a Reportes.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormManager.Navegar(this, FormManager.ObtenerBitacora());
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerPresentacion());
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            ChangePassPanel.Visible = !ChangePassPanel.Visible;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevaPass = txtNewPass.Text;
                string repPass = txtRepPass.Text;

                if (string.IsNullOrEmpty(txtNewPass.Text) || string.IsNullOrEmpty(txtRepPass.Text))
                {
                    MessageBox.Show("Los campos estan vacios",
                    "Cambiar Contraseña",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }
                // Validar que las contraseñas coincidan
                if (nuevaPass != repPass)
                {
                    MessageBox.Show("Las contraseñas no coinciden",
                       "Cambiar Contraseña",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    return;
                }

                UsuarioBE usuario = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();

                string hashnuevaPass = servicioB.HashearContraseña(nuevaPass);
                
                // Validar que no sea igual a la contraseña anterior
                if (string.CompareOrdinal(usuario._Contraseña, hashnuevaPass) == 0)
                {
                    MessageBox.Show("Error: Tu contraseña es igual, no se cambio",
                       "Cambiar Contraseña",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    return;
                }

                usuario._Contraseña = hashnuevaPass;
                usuarioBLL.CambiarContraseña(usuario);
                
                // Registrar en bitácora
                string descripcion = $"Cambio de contraseña realizado por el usuario '{usuario._NombreDeUsuario}'";
                bitacoraBLL.RegistrarEvento(2, descripcion, usuario._Dni, "Form1");

                MessageBox.Show("Contraseña cambiada exitosamente",
                "Cambiar Contraseña",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Registrar error en bitácora
                UsuarioBE usuario = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();
                if (usuario != null)
                {
                    string descripcion = $"Error al cambiar contraseña: {ex.Message}";
                    bitacoraBLL.RegistrarEvento(3, descripcion, usuario._Dni, "Form1");
                }

                MessageBox.Show($"Error: {ex.Message}",
                   "Cambiar Contraseña",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }
            finally
            {
                txtNewPass.Text = "";
                txtRepPass.Text = "";
                ChangePassPanel.Visible = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNewPass.Text = "";
            txtRepPass.Text = "";
            ChangePassPanel.Visible = false;
        }
    }
}
