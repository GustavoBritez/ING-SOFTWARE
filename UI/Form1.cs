using BE;
using BLL;
namespace UI
{
    public partial class Form1 : Form
    {

        private readonly UsuarioBLL userBLL = new UsuarioBLL();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pass = textBox2.Text;

            UsuarioBE userBE = new UsuarioBE()
            {
                Email = user,
                Password = pass
            };

            userBLL.RegistrarUsuario(userBE);

        }
    }
}
