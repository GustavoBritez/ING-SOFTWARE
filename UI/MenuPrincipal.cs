using BE;
using BLL;
using Services;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI
{
    public partial class MenuPrincipal : Form, IIdiomaObserver
    {
        private readonly UsuarioBLL usuarioBLL = new UsuarioBLL();
        private readonly EventoBLL bitacoraBLL = new EventoBLL();
        private readonly ServicioBcrypt servicioB = new();

        private IdiomaBLL idiomaBLL = new IdiomaBLL();
        public MenuPrincipal()
        {
            InitializeComponent();

            //this.Load += (s, e) => Form1_Load();
            //this.Shown += (s, e) => Form1_Shown();
            this.VisibleChanged += (s, e) => Form1_VisibleChanged();

            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle= ComboBoxStyle.DropDownList;

            ServicesSessionManager.Instancia.Suscribir(this);
            ActualizarIdioma();
        }
        private void ActualizarDisponibilidadBotones()
        {
            try
            {
                UsuarioBE usuarioActivo = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();
                bool tieneSession = usuarioActivo != null;

                // Deshabilitar todos los botones excepto btnLogin si no hay sesión
                btnTurnos.Enabled = tieneSession;
                btnLogout.Enabled = tieneSession;
                btnChangePass.Enabled = tieneSession;
                btnChangePass.Visible = tieneSession;
                btnReportes.Enabled = tieneSession && usuarioActivo?._Rol == "1";
                btnUsuarios.Enabled = tieneSession && usuarioActivo?._Rol == "1";
            }
            catch
            {
                // Si hay error, asumir que no hay sesión y fue

                btnTurnos.Enabled = false;
                btnLogout.Enabled = false;
                btnChangePass.Enabled = false;
                btnChangePass.Visible = false;
                btnReportes.Enabled = false;
                btnUsuarios.Enabled = false;

            }

            btnLogin.Enabled = true;

        }

        private void Form1_VisibleChanged()
        {
            ActualizarDisponibilidadBotones();
            ActualizarUsuario();
        }

        private void ActualizarUsuario()
        {
            if (ServicesSessionManager.Instancia.ObtenerUsuarioActivo() != null)
            {
                this.label6.Text = $"{ServicesSessionManager.Instancia.ObtenerUsuarioActivo()._NombreDeUsuario} || {ServicesSessionManager.Instancia.ObtenerUsuarioActivo()._Rol}";
            }
            else
            {
                this.label6.Text = $"";
            }
        }
        private void btnTurnos_Click(object sender, EventArgs e)
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
                ActualizarUsuario();
                FormManager.Navegar(this, FormManager.ObtenerLogin());
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



            FormManager.Navegar(this, FormManager.ObtenerGestionUsuario());
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            UsuarioBE usuarioActivo = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();


            FormManager.Navegar(this, FormManager.ObtenerBitacora());
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerLogin());
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
                string actualPass = txtActualPass.Text;

                if (string.IsNullOrEmpty(txtNewPass.Text) || string.IsNullOrEmpty(txtRepPass.Text) || string.IsNullOrEmpty(txtActualPass.Text))
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

                /// Verificar otra forma por que es codigo aldope
                bool boleano = servicioB.ValidarContraseña(actualPass, usuario._Contraseña);

                bool boleano2 = servicioB.ValidarContraseña(nuevaPass, usuario._Contraseña);

                // Validar que no sea igual a la contraseña anterior
                if (boleano && boleano2)
                {
                    MessageBox.Show("Error: Tu contraseña es igual, no se cambio",
                       "Cambiar Contraseña",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    return;
                }

                string hashnuevaPass = servicioB.HashearContraseña(nuevaPass);

                usuario._Contraseña = hashnuevaPass;
                usuarioBLL.CambiarContraseña(usuario);

                // Registrar en bitácora

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
                    bitacoraBLL.RegistrarEvento(3, descripcion, usuario._Dni, "MenuPrincipal");
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
                txtActualPass.Text = "";
                ChangePassPanel.Visible = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNewPass.Text = "";
            txtRepPass.Text = "";
            ChangePassPanel.Visible = false;
        }
        public void ActualizarIdioma()
        {
            if (ServicesSessionManager.Instancia.ObtenerIdioma() != null)
            {
                Traducir(this.Controls);
            }
        }
        private void Traducir(Control.ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (!string.IsNullOrEmpty(control.Name))
                {
                    string traduccion = idiomaBLL.Traducir(control.Name);

                    if (traduccion != control.Name) // evita reemplazar si no existe la clave
                        control.Text = traduccion;
                }

                if (control.HasChildren)
                    Traducir(control.Controls);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Idioma> idiomas = idiomaBLL.ObtenerIdiomas();

            if (comboBox1.SelectedItem.ToString() == "Español")
            {
                Idioma español = idiomas.First(i => i.Codigo == "es");
                ServicesSessionManager.Instancia.CambiarIdioma(español);
            }
            else if (comboBox1.SelectedItem.ToString() == "Ingles")
            {
                Idioma ingles = idiomas.First(i => i.Codigo == "en");
                ServicesSessionManager.Instancia.CambiarIdioma(ingles);
            }
            else if (comboBox1.SelectedItem.ToString() == "Portugues")
            {
                Idioma portugues= idiomas.First(i => i.Codigo == "po");
                ServicesSessionManager.Instancia.CambiarIdioma(portugues);
            }
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }
    }

}
