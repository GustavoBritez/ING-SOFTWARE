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
                string nombreDeUsuario = txtUsuario.Text.Trim();
                string contraseña = txtPassword.Text;

                if (string.IsNullOrWhiteSpace(nombreDeUsuario) || string.IsNullOrWhiteSpace(contraseña))
                {
                    MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nombreNormalizado = nombreDeUsuario.ToLower();


                UsuarioBE usuarioVerificacion = usuarioBLL.ObtenerUsuario(nombreNormalizado);
                if (usuarioVerificacion != null && usuarioVerificacion._Bloqueado)
                {
                    MessageBox.Show($"La cuenta del usuario '{nombreDeUsuario}' se encuentra bloqueada.\nContacte al administrador.",
                        "Cuenta Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                bool loginExitoso = usuarioBLL.Login(nombreNormalizado, contraseña);

                if (loginExitoso)
                {
                    MessageBox.Show($"¡Logeado con éxito!", "Login Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    UsuarioBE usuarioLogueado = usuarioBLL.ObtenerUsuario(nombreNormalizado);
                    ServicesSessionManager.Instancia.Login(usuarioLogueado);

                    usuarioBLL.intentosFallidos.Clear();

                    FormManager.Navegar(this, FormManager.ObtenerForm1());
                }
                else
                {

                    if (usuarioVerificacion != null) 
                    {
                        int intentosFallidos = usuarioBLL.ObtenerIntentosFallidos(nombreNormalizado);
                        int intentosRestantes = 3 - intentosFallidos;

                        if (intentosRestantes <= 0)
                        {
                            usuarioVerificacion._Bloqueado = true;
                            usuarioBLL.ModificarUsuario(usuarioVerificacion);

                            MessageBox.Show("Su cuenta ha sido bloqueada por superar los 3 intentos fallidos.",
                                "Cuenta Bloqueada por Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show($"Usuario o contraseña incorrectos.\nIntentos restantes: {intentosRestantes}/3",
                                "Login Fallido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {

                        MessageBox.Show("Usuario o contraseña incorrectos.", "Login Fallido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
