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

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            this.Close();
            FormManager.Navegar(this, FormManager.ObtenerForm1());
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtUsuario.Text?.Trim();
                string contraseña = txtPassword.Text ?? string.Empty;

                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(contraseña))
                {
                    MessageBox.Show("Debe ingresar usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener usuario desde la BLL
                UsuarioBE usuario = usuarioBLL.ObtenerUsuario(nombre);

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

                // Intentar login
                bool loginOK = usuarioBLL.Login(nombre, contraseña);

                if (loginOK)
                {
                    // Obtener usuario actualizado desde BD y establecer sesión
                    UsuarioBE usuarioActivo = usuarioBLL.ObtenerUsuario(nombre);
                    ServicesSessionManager.Instancia.Login(usuarioActivo);

                    // Limpiar intentos fallidos en memoria (requisito)
                    usuarioBLL.intentosFallidos.Clear();

                    MessageBox.Show("Inicio de sesión exitoso.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Navegar al formulario principal
                    FormManager.Navegar(this, FormManager.ObtenerForm1());
                }
                else
                {
                    // Si el login falló, comprobar si la cuenta fue bloqueada después del intento
                    UsuarioBE usuarioDespues = usuarioBLL.ObtenerUsuario(nombre);
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
    }
}
