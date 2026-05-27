using BE;
using BLL;
using Services;
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
    public partial class Presentacion : Form
    {
        UsuarioBLL usuarioBLL = new();
        public Presentacion()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreDeUsuario = txtUsuario.Text;
                string contraseña = txtPassword.Text;

                if (string.IsNullOrWhiteSpace(nombreDeUsuario) || string.IsNullOrWhiteSpace(contraseña))
                {
                    MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar si el usuario existe y está bloqueado antes de intentar login
                UsuarioBE usuarioVerificacion = usuarioBLL.ObtenerUsuario(nombreDeUsuario);
                bool usuarioExisteYEstaBloqueado = usuarioVerificacion != null && usuarioVerificacion._Bloqueado;

                bool loginExitoso = usuarioBLL.Login(nombreDeUsuario, contraseña);

                if (loginExitoso)
                {
                    MessageBox.Show($"¡Logeado con éxito!", "Login Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UsuarioBE usuario = usuarioBLL.ObtenerUsuario(nombreDeUsuario);
                    ServicesSessionManager.Instancia.Login(usuario);
                    FormManager.Navegar(this, FormManager.ObtenerForm1());
                }
                else
                {
                    // Si el usuario existe y está bloqueado, mostrar mensaje diferenciado
                    if (usuarioExisteYEstaBloqueado)
                    {
                        int intentosFallidos = usuarioBLL.ObtenerIntentosFallidos(nombreDeUsuario);

                        if (intentosFallidos >= 3)
                        {
                            MessageBox.Show($"Su cuenta ha sido bloqueada por 3 intentos fallidos de inicio de sesión.\nContacte al administrador para desbloquearla.",
                                "Cuenta Bloqueada por Intentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show($"La cuenta del usuario '{nombreDeUsuario}' ha sido bloqueada.\nContacte al administrador para desbloquearla.",
                                "Cuenta Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Contraseña incorrecta
                        int intentosFallidos = usuarioBLL.ObtenerIntentosFallidos(nombreDeUsuario);
                        int intentosRestantes = 3 - intentosFallidos;

                        if (intentosRestantes > 0)
                        {
                            MessageBox.Show($"Usuario o contraseña incorrectos.\nIntentos restantes: {intentosRestantes}/3",
                                "Login Fallido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Su cuenta ha sido bloqueada por 3 intentos fallidos.\nContacte al administrador para desbloquearla.",
                                "Cuenta Bloqueada por Intentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                LimpiarCampos();
            }
        }
        private void LimpiarCampos()
        {
            txtPassword.Text = "";
            txtUsuario.Text = "";
        }

    }
}
