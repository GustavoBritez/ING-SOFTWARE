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
                ///Admin
                ///41236101
                if (string.IsNullOrWhiteSpace(nombreDeUsuario) || string.IsNullOrWhiteSpace(contraseña))
                {
                    return;
                }

                bool loginExitoso = usuarioBLL.Login(nombreDeUsuario, contraseña);

                if (loginExitoso)
                {
                    MessageBox.Show($"Logeado con exito");
                    UsuarioBLL _UsuarioBLL = new();

                    UsuarioBE usar = _UsuarioBLL.ObtenerUsuario(nombreDeUsuario);

                    ServicesSessionManager.Instancia.Login(usar);
                    FormManager.Navegar(this, FormManager.ObtenerForm1());
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Login Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
