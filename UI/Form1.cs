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


            FormManager.Navegar(this, FormManager.ObtenerPresentacion());
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {

            FormManager.Navegar(this, FormManager.ObtenerGestionUsuario());
        }
    }
}
