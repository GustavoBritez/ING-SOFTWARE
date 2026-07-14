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
        private readonly ServicioBcrypt servicioB = new();

        private IdiomaBLL idiomaBLL = new IdiomaBLL();

        public MenuPrincipal()
        {
            InitializeComponent();

            //this.Load += (s, e) => Form1_Load();
            //this.Shown += (s, e) => Form1_Shown();
            this.VisibleChanged += (s, e) => Form1_VisibleChanged();

            cmbIdioma.DropDownStyle = ComboBoxStyle.DropDownList;

            ServicesSessionManager.Instancia.Suscribir(this);
            ActualizarIdioma();
        }

        // Ver
        //private void ActualizarDisponibilidadBotones()
        //{
        //    try
        //    {
        //        UsuarioBE usuarioActivo = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();
        //        bool tieneSession = usuarioActivo != null;

        //        if (tieneSession)
        //        {

        //            btnLogout.Visible = true;
        //        }
        //        else
        //        {
        //            // Si no hay sesión, apagamos todo por las dudas
        //            btnTurnos.Visible = false;
        //            btnCambiarContrasena.Visible = false;
        //            btnReportes.Visible = false;
        //            btnUsuarios.Visible = false;
        //            btnLogout.Visible = false;
        //        }

        //        btnLogin.Visible = true;
        //        btnLogin.Enabled = true;
        //    }
        //    catch
        //    {
        //        // En caso de error, cerramos todo menos el Login
        //        btnTurnos.Visible = false;
        //        btnCambiarContrasena.Visible = false;
        //        btnReportes.Visible = false;
        //        btnUsuarios.Visible = false;
        //        btnLogout.Visible = false;

        //        btnLogin.Visible = true;
        //        btnLogin.Enabled = true;
        //    }
        //}

        private void Form1_VisibleChanged()
        {
            ApuntarComboBox();
            //ActualizarDisponibilidadBotones();
            ActualizarUsuario();
        }
        private void ApuntarComboBox()
        {
            string idioma = ServicesSessionManager.Instancia.ObtenerIdioma().Nombre;

            if (idioma == "Español")
            {
                cmbIdioma.SelectedIndex = 0;
            }
            else if (idioma == "English")
            {
                cmbIdioma.SelectedIndex = 1;
            }
            else if (idioma == "Portugues")
            {
                cmbIdioma.SelectedIndex = 2;
            }
        }

        private void ActualizarUsuario()
        {
            // Guardamos el usuario en una variable para no llamar a la Instancia tantas veces
            var usuarioActivo = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();

            if (usuarioActivo != null)
            {
                // Traducimos el ID numérico a un texto legible para la interfaz
                string nombrePerfil = "";
                switch (usuarioActivo._IdPerfil)
                {
                    case 1:
                        nombrePerfil = "Administrador";
                        break;
                    case 2:
                        nombrePerfil = "Usuario";
                        break;
                    case 3:
                        nombrePerfil = "Médico";
                        break;
                    default:
                        nombrePerfil = $"Perfil {usuarioActivo._IdPerfil}";
                        break;
                }

                this.label6.Text = $"{usuarioActivo._NombreDeUsuario} || {nombrePerfil}";
            }
            else
            {
                this.label6.Text = ""; // O string.Empty
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioBE usuarioActual = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();

                Idioma id = ServicesSessionManager.Instancia.ObtenerIdioma();
                usuarioActual._Idioma = id.Nombre;
                usuarioBLL.CambioDeIdiomaUser(usuarioActual);


                usuarioBLL.LogOut(usuarioActual);
                idiomaBLL.MostrarMensaje("msg_cerrar_sesion", "titulo_cerrar_sesion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ActualizarDisponibilidadBotones();
                ActualizarUsuario();

                List<Idioma> idiomas = idiomaBLL.ObtenerIdiomas();
                Idioma español = idiomas.First(i => i.Codigo == "es");
                ServicesSessionManager.Instancia.CambiarIdioma(español);

                FormManager.Navegar(this, FormManager.ObtenerLogin());
            }
            catch (Exception ex)
            {
                idiomaBLL.MostrarMensaje("msg_error_cerrar_sesion", "titulo_error_cerrar_sesion", MessageBoxButtons.OK, MessageBoxIcon.Information, ex.Message);
            }

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            UsuarioBE usuarioActivo = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();
            if (usuarioActivo == null)
            {
                idiomaBLL.MostrarMensaje("msg_error_nosesion", "titulo_error_nosesion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FormManager.Navegar(this, FormManager.ObtenerGestionUsuario());
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerBitacora());
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerLogin());
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
                    
                    idiomaBLL.MostrarMensaje("msg_campos_vacios", "titulo_campos_vacios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // Validar que las contraseñas coincidan
                if (nuevaPass != repPass)
                {
                   
                    idiomaBLL.MostrarMensaje("msg_contra_distinta", "titulo_contra_distinta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                UsuarioBE usuario = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();

                bool boleano = servicioB.ValidarContraseña(actualPass, usuario._Contraseña);

                bool boleano2 = servicioB.ValidarContraseña(nuevaPass, usuario._Contraseña);

                // Validar que no sea igual a la contraseña anterior
                if (boleano && boleano2)
                {
                    
                    idiomaBLL.MostrarMensaje("msg_samecontra", "titulo_samecontra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string hashnuevaPass = servicioB.HashearContraseña(nuevaPass);

                usuario._Contraseña = hashnuevaPass;
                usuarioBLL.CambiarContraseña(usuario);

                idiomaBLL.MostrarMensaje("msg_contra_cambiada", "titulo_contra_cambiada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                idiomaBLL.MostrarMensaje("msg_error_cambio", "titulo_error_cambio", MessageBoxButtons.OK, MessageBoxIcon.Information,ex.Message);
            }
            finally
            {
                txtNewPass.Text = "";
                txtRepPass.Text = "";
                txtActualPass.Text = "";
                ChangePassPanel.Visible = false;
            }
        }
        private void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            ChangePassPanel.Visible = !ChangePassPanel.Visible;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNewPass.Text = "";
            txtRepPass.Text = "";
            ChangePassPanel.Visible = false;
        }

        #region Idioma No tocar
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
        private void cmdIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Idioma> idiomas = idiomaBLL.ObtenerIdiomas();

            if (cmbIdioma.SelectedItem.ToString() == "Español")
            {
                Idioma español = idiomas.First(i => i.Codigo == "es");
                ServicesSessionManager.Instancia.CambiarIdioma(español);
            }
            else if (cmbIdioma.SelectedItem.ToString() == "English")
            {
                Idioma ingles = idiomas.First(i => i.Codigo == "en");
                ServicesSessionManager.Instancia.CambiarIdioma(ingles);
            }
            else if (cmbIdioma.SelectedItem.ToString() == "Portugues")
            {
                Idioma portugues = idiomas.First(i => i.Codigo == "po");
                ServicesSessionManager.Instancia.CambiarIdioma(portugues);
            }
        }
        #endregion

        private void btnRespaldo_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerRespaldo());
        }

        private void btnGestionarPerfiles_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, new Perfiles());
        }
    }

}
