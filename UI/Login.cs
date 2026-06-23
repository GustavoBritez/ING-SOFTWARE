using BE;
using BLL;
using BLL.Perfiles;
using Services;
using Services.Perfiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace UI
{
    public partial class Login : Form, IIdiomaObserver
    {
        UsuarioBLL usuarioBLL = new();
        private IdiomaBLL idiomaBLL = new IdiomaBLL();
        public Login()
        {
            InitializeComponent();
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
                    MessageBox.Show("Ya hay una sesion iniciada", "Sesion activa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    FormManager.Navegar(this, FormManager.ObtenerMenuPrincipal());
                    return;
                }

                string nombre = txtUsuario.Text?.Trim();
                string contraseña = txtPassword.Text ?? string.Empty;

                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(contraseña))
                {
                    MessageBox.Show("Debe ingresar usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener usuario desde la BLL
                UsuarioBE usuario = usuarioBLL.BuscarUsuario(nombre);

                if (usuario == null)
                {
                    MessageBox.Show("Usuario no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (usuario._Bloqueado)
                {
                    MessageBox.Show("Cuenta bloqueada. Contacte al administrador.", "Cuenta bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

                    MessageBox.Show("Inicio de sesión exitoso.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormManager.Navegar(this, FormManager.ObtenerMenuPrincipal());

                }
                else
                {
                    UsuarioBE usuarioDespues = usuarioBLL.BuscarUsuario(nombre);
                    if (usuarioDespues != null && usuarioDespues._Bloqueado)
                    {
                        MessageBox.Show("Cuenta bloqueada por 3 intentos fallidos.", "Cuenta bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        int intentos = usuarioBLL.ObtenerIntentosFallidos(nombre);
                        int intentosRestantes = Math.Max(0, 3 - intentos);
                        MessageBox.Show($"Contraseña inválida. Intentos: {intentos}/3. Intentos restantes: {intentosRestantes}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el proceso de login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtUsuario.Text = "admin";
            txtPassword.Text = "1234";
        }
    }
}
