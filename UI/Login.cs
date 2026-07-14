using BE;
using BLL;
using BLL.Perfiles;
using Services;
using Services.Perfiles;
using System.Data;


namespace UI
{
    public partial class Login : Form, IIdiomaObserver
    {
        UsuarioBLL usuarioBLL = new();
        private IdiomaBLL idiomaBLL = new IdiomaBLL();
        public Login()
        {
            InitializeComponent();
            cmbIdioma.DropDownStyle = ComboBoxStyle.DropDownList;
            ServicesSessionManager.Instancia.Suscribir(this);
            ActualizarIdioma();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            FormManager.Navegar(this, FormManager.ObtenerMenuPrincipal());
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {

                if (ServicesSessionManager.Instancia.ObtenerUsuarioActivo() != null)
                {
                    idiomaBLL.MostrarMensaje("msg_sesion_activa", "titulo_sesion_activa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormManager.Navegar(this, FormManager.ObtenerMenuPrincipal());
                    return;
                }

                string nombre = txtUsuario.Text?.Trim();
                string contraseña = txtPassword.Text ?? string.Empty;

                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(contraseña))
                {  
                    idiomaBLL.MostrarMensaje("msg_falta_uscon", "titulo_falta_uscon", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Obtener usuario desde la BLL
                UsuarioBE usuario = usuarioBLL.BuscarUsuario(nombre);

                if (usuario == null)
                {
                    idiomaBLL.MostrarMensaje("msg_inexistente_usuario", "titulo_no_usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (usuario._Bloqueado)
                {
                    idiomaBLL.MostrarMensaje("msg_cuentabloqueada", "titulo_cuentabloqueada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                bool loginOK = usuarioBLL.Login(nombre, contraseña);
                if (loginOK)
                {
                    PatenteBLL patenteBLL = new PatenteBLL();
                    List<PatenteServices> listaPatentes = patenteBLL.ObtenerPermisosDePerfil(usuario._IdPerfil);

                    List<string> nombresPermisos = listaPatentes.Select(p => p.Nombre).ToList();

                    ServicesSessionManager.Instancia.CargarPermisosDelUsuario(nombresPermisos);

                    ServicesSessionManager.Instancia.Login(usuario);

                    List<Idioma> idiomas = idiomaBLL.ObtenerIdiomas();
                    Idioma idioma = idiomas.Find(i => i.Nombre == usuario._Idioma.ToString());
                    ServicesSessionManager.Instancia.CambiarIdioma(idioma);

                    //MessageBox.Show("Inicio de sesión exitoso.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    idiomaBLL.MostrarMensaje("msg_inicio_sesion","titulo_inicio_sesion",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    FormManager.Navegar(this, FormManager.ObtenerMenuPrincipal());


                }
                else
                {
                    UsuarioBE usuarioDespues = usuarioBLL.BuscarUsuario(nombre);
                    if (usuarioDespues != null && usuarioDespues._Bloqueado)
                    {
                        idiomaBLL.MostrarMensaje("msg_bloquear_cuenta", "titulo_bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        int intentos = usuarioBLL.ObtenerIntentosFallidos(nombre);
                        int intentosRestantes = Math.Max(0, 3 - intentos);

                        idiomaBLL.MostrarMensaje("msg_intentos_incorrectos","titulo_intento_fallido",MessageBoxButtons.OK, MessageBoxIcon.Warning, intentos, intentosRestantes);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                idiomaBLL.MostrarMensaje("msg_login_error", "titulo_login_error", MessageBoxButtons.OK, MessageBoxIcon.Error, ex);
             
            }
        }

        #region
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
        #endregion

        // Solo para logearme ma rapido
        private void Login_Load(object sender, EventArgs e)
        {
            cmbIdioma.SelectedIndex = 0;
        }

        private void cmbIdioma_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
