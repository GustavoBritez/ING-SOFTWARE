using BE;
using BLL;
using Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI
{
    public partial class Form1 : Form
    {
        private readonly UsuarioBLL usuarioBLL = new UsuarioBLL();
        private readonly ServicioBcrypt servicioB = new();
        public Form1()
        {
            InitializeComponent();
            this.Load += (s, e) => Form1_Load();
        }

        private void Form1_Load()
        {
            ActualizarDisponibilidadBotones();
        }

        private void ActualizarDisponibilidadBotones()
        {
            try
            {
                UsuarioBE usuarioActivo = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();
                bool tieneSession = usuarioActivo != null;

                // Deshabilitar todos los botones excepto btnLogin si no hay sesión
                btnLogin.Enabled = !tieneSession;
                btnTurnos.Enabled = tieneSession;
                btnLogout.Enabled = tieneSession;
                btnChangePass.Enabled = tieneSession;
                btnChangePass.Visible = tieneSession;
                btnReportes.Enabled = tieneSession && usuarioActivo?._Rol == "Administrador";
                btnUsuarios.Enabled = tieneSession && usuarioActivo?._Rol == "Administrador";
            }
            catch
            {
                // Si hay error, asumir que no hay sesión
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
            UsuarioBE usuarioActual = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();

            ServicesSessionManager.Instancia.Logout();

            MessageBox.Show("Cerrar sesión exitoso");

            ActualizarDisponibilidadBotones();
            FormManager.Navegar(this, FormManager.ObtenerPresentacion());
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
                string repPass = txtNewPass.Text;

                if ( string.IsNullOrEmpty(txtNewPass.Text) || string.IsNullOrEmpty(txtRepPass.Text))
                {
                    MessageBox.Show("Los campos estan vacios",
                    "Cambiar Contraseña",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }

                // Si son completamente iguales
                if (string.CompareOrdinal(nuevaPass, repPass) != 1)
                {
                    UsuarioBE Usuario = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();

                    string hashnuevaPass = servicioB.HashearContraseña(nuevaPass);
                    // Las contraseñas no son iguales entramos al if
                    if (string.CompareOrdinal(Usuario._Contraseña, hashnuevaPass) != 0)
                    {


                        Usuario._Contraseña = hashnuevaPass;
                        usuarioBLL.CambiarContraseña(Usuario);
                        MessageBox.Show("Contraseña cambiada exitosamente",
                        "Cambiar Contraseña",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error : Tu contraseña es igual, no se cambio",
                           "Cambiar Contraseña",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                    }
                }
            }
            finally
            {
                txtNewPass.Text = "";
                txtRepPass.Text = "";
                ChangePassPanel.Visible = !ChangePassPanel.Visible;
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
