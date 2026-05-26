using BE;
using BLL;
using Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI
{
    public partial class Form1 : Form
    {
        private readonly UsuarioBLL usuarioBLL = new UsuarioBLL();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// button1_Click - Crear Usuario
        /// Usa UsuarioService (operación CRUD)
        /// </summary>
        /*private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = textBox1.Text;
                string apellido = textBox2.Text;
                int dni;
                string nombreDeUsuario = textBox3.Text;
                string contraseña = textBox4.Text;
                string rol = textBox5.Text;
                bool bloqueado = checkBox1.Checked;

                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) ||
                    string.IsNullOrWhiteSpace(nombreDeUsuario) || string.IsNullOrWhiteSpace(contraseña) ||
                    string.IsNullOrWhiteSpace(rol))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox2.Text, out dni))
                {
                    MessageBox.Show("El DNI debe ser un número entero válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar DNI con UsuarioService
                if (!UsuarioService.Instancia.ValidarDNI(dni))
                {
                    MessageBox.Show("El DNI debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear la instancia de UsuarioBE con todos los parámetros
                UsuarioBE nuevoUsuario = new UsuarioBE(
                    nombre,
                    apellido,
                    dni,
                    nombreDeUsuario,
                    contraseña,
                    rol,
                    bloqueado
                );

                // Usar UsuarioService para crear (operación CRUD)
                UsuarioService.Instancia.CrearUsuario(nuevoUsuario);

                MessageBox.Show($"Usuario '{nombreDeUsuario}' registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// button2_Click - Login
        /// Usa ServicesSessionManager (gestiona la sesión)
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreDeUsuario = textBox3.Text;
                string contraseña = textBox4.Text;

                if (string.IsNullOrWhiteSpace(nombreDeUsuario) || string.IsNullOrWhiteSpace(contraseña))
                {
                    return;
                }

                bool loginExitoso = ServicesSessionManager.Instancia.Login(nombreDeUsuario, contraseña);

                if (loginExitoso)
                {
                    UsuarioBE usuarioEnSesion = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();
                    MessageBox.Show($"Logeado con exito");
                    LimpiarCampos();
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
        }*/

        private void btnTurnos_Click(object sender, EventArgs e)
        {

        }

        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            checkBox1.Checked = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string nombre = textBox1.Text;
                string apellido = textBox2.Text;
                int dni;
                string nombreDeUsuario = textBox4.Text;
                string contraseña = textBox5.Text;
                string rol = textBox6.Text;
                bool bloqueado = checkBox1.Checked;

                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) ||
                    string.IsNullOrWhiteSpace(nombreDeUsuario) || string.IsNullOrWhiteSpace(contraseña) ||
                    string.IsNullOrWhiteSpace(rol))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox3.Text, out dni))
                {
                    MessageBox.Show("El DNI debe ser un número entero válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar DNI
                if (dni <= 0)
                {
                    MessageBox.Show("El DNI debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear la instancia de UsuarioBE con todos los parámetros
                UsuarioBE nuevoUsuario = new UsuarioBE(
                    nombre,
                    apellido,
                    dni,
                    nombreDeUsuario,
                    contraseña,
                    rol,
                    bloqueado
                );

                // Usar UsuarioBLL directamente para crear el usuario
                usuarioBLL.CrearUsuario(nuevoUsuario);

                MessageBox.Show($"Usuario '{nombreDeUsuario}' registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreDeUsuario = textBox4.Text;
                string contraseña = textBox5.Text;

                if (string.IsNullOrWhiteSpace(nombreDeUsuario) || string.IsNullOrWhiteSpace(contraseña))
                {
                    return;
                }

                bool loginExitoso = ServicesSessionManager.Instancia.Login(nombreDeUsuario, contraseña);

                if (loginExitoso)
                {
                    MessageBox.Show($"Logeado con exito");
                    LimpiarCampos();
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
        }
    }
}
