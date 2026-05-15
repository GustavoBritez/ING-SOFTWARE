namespace WinFormsApp10
{
    public partial class Form1 : Form
    {
        private Documento documento;
        private Historial historial;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /// Supongo que cargo el documento vacio y el historial vacio la primera vez
            documento = new Documento("");
            historial = new Historial();
        }

        private void BTN_GUARDAR_Click(object sender, EventArgs e)
        {
            documento.State = TEXTBOX.Text;

            IMemento memento = documento.CreateMemento();

            historial.addMemento(memento);
            ActualizarHistorial();

            MessageBox.Show("Version nueva guardada");
        }

        private void BTN_RESTAURAR_Click(object sender, EventArgs e)
        {
            if (ListVersiones.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona una version para retaurar");
                return;
            }

            IMemento seleccion = historial.getMemento(ListVersiones.SelectedIndex);

            if (seleccion != null)
            {
                documento.SetMemento(seleccion);
                MessageBox.Show($"{documento.State}");
            }
        }

        private void ActualizarHistorial()
        {
            ListVersiones.Items.Clear();
            for (int i = 0; i < historial.getCount(); i++)
            {
                ListVersiones.Items.Add($"Version {i + 1}");
            }
        }
    }
}
