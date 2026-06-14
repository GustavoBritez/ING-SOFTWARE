using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;
using System.IO;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Services;

namespace UI
{
    public partial class Bitacora : Form,IIdiomaObserver
    {
        EventoBLL _bitacoraBLL = new EventoBLL();
        UsuarioBLL _usuarioBLL = new UsuarioBLL();
        private List<EventoBE>? _bitacoraCompleta;

        private IdiomaBLL idiomaBLL = new IdiomaBLL();

        public Bitacora()
        {
            InitializeComponent();
            this.VisibleChanged += (s, e) => Bitacora_VisibleChanged();
            ServicesSessionManager.Instancia.Suscribir(this);
            ActualizarIdioma();
        }

        private void Bitacora_VisibleChanged()
        {
            // Solo actualizamos si el formulario se volvió a poner visible
            if (this.Visible)
            {
                CargarBitacora(BitacoraInicial());
            }
        }

        private List<EventoBE> BitacoraInicial()
        {
            DateTime desde = DateTime.Today.AddDays(-3);
            DateTime hasta = DateTime.Now;

            var bitacoraFiltrada = _bitacoraCompleta
                .Where(b => b._Fecha >= desde && b._Fecha <= hasta)
                .ToList();
            return bitacoraFiltrada;
        }
        private void btnSalir_Click(object? sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerMenuPrincipal());
        }

        private void Bitacora_Load(object? sender, EventArgs e)
        {
            GestionBitacora_Load(sender, e);
        }

        private void GestionBitacora_Load(object? sender, EventArgs e)
        {
            _bitacoraCompleta = _bitacoraBLL.VerEventos();

            InicializarDateTimePickers();
            InicializarComboBoxCriticidad();
            InicializarComboBoxC();
            InicializarComboBoxEvento();

            CargarBitacora(BitacoraInicial());


            dtpDesde.ValueChanged += DtpFecha_ValueChanged;
            dtpHasta.ValueChanged += DtpFecha_ValueChanged;


            cmbModulo.SelectedIndexChanged += CmbCriticidad_SelectedIndexChanged;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            dgvBitacora.AllowUserToResizeColumns = false;
            dgvBitacora.AllowUserToResizeRows = false;

            dgvBitacora.CellClick += DgvBitacora_CellClick;

            //btnAplicarFiltro.Click += BtnAplicarFiltro_Click;
            btnExportar.Click += BtnExportar_Click;
        }

        private void InicializarDateTimePickers()
        {
            DateTime hoy = DateTime.Today;
            dtpHasta.Value = hoy;
        }

        private void InicializarComboBoxCriticidad()
        {
            cmbModulo.Items.Clear();
            cmbModulo.Items.Add("Todos");
            cmbModulo.Items.Add("Login");
            cmbModulo.Items.Add("GestionUsuario");
            cmbModulo.SelectedIndex = 0; // Seleccionar "Todas" por defecto
        }
        private void InicializarComboBoxC()
        {
            cmbCriticidad.Items.Clear();

            cmbCriticidad.Items.Add("Todas");
            cmbCriticidad.Items.Add("1");
            cmbCriticidad.Items.Add("2");
            cmbCriticidad.Items.Add("3");
            cmbCriticidad.Items.Add("4");
            cmbCriticidad.Items.Add("5");

            cmbCriticidad.SelectedIndex = 0;
        }
        private void InicializarComboBoxEvento()
        {
            cmbEvento.Items.Clear();

            cmbEvento.Items.Add("Todos");
            cmbEvento.Items.Add("Inicio de Sesion");
            cmbEvento.Items.Add("Cierre de Sesion");
            cmbEvento.Items.Add("Error");
            cmbEvento.Items.Add("Creacion de Usuario");
            cmbEvento.Items.Add("Modificar Usuario");
            cmbEvento.Items.Add("Desbloqueo de Usuario");
            cmbEvento.Items.Add("Bloqueo de Cuenta");
            cmbEvento.Items.Add("Cambio de Estado");
            cmbEvento.Items.Add("Cambio de Clave");

            cmbEvento.SelectedIndex = 0;
        }
        private void DtpFecha_ValueChanged(object? sender, EventArgs e)
        {
            DateTime hoy = DateTime.Today;

            if (dtpHasta.Value > hoy)
            {
                MessageBox.Show(
                    "No puede seleccionar una fecha futura.",
                    "Validacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                dtpHasta.Value = hoy;
                return;
            }

            if (dtpDesde.Value > dtpHasta.Value)
            {
                MessageBox.Show(
                   "No puede seleccionar FechaDesde mayor a FechaHasta.",
                   "Validacion",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
                dtpDesde.Value = hoy;
                return;
            }
        }

        private void CmbCriticidad_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string modulo = cmbModulo.SelectedItem?.ToString() ?? "Todas";

            var bitacoraFiltrada = _bitacoraCompleta;
            if (modulo != "Todos")
            {
                bitacoraFiltrada = bitacoraFiltrada
                    .Where(b => b._Modulo.ToString() == modulo)
                    .ToList();
            }

            CargarBitacora(bitacoraFiltrada);
        }

        private void BtnAplicarFiltro_Click(object? sender, EventArgs e)
        {
            AplicarFiltrosCombinados();
        }

        private void AplicarFiltrosCombinados()
        {
            try
            {
                if (_bitacoraCompleta == null) return;

                DateTime fechaDesde = dtpDesde.Value.Date;
                DateTime fechaHasta = dtpHasta.Value.Date;
                string criticidadSeleccionada = cmbModulo.SelectedItem?.ToString() ?? "Todas";


                var bitacoraFiltrada = _bitacoraCompleta
                    .Where(b => b._Fecha.Date >= fechaDesde && b._Fecha.Date <= fechaHasta)
                    .ToList();

                CargarBitacora(bitacoraFiltrada);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al filtrar bitacora: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarBitacora(List<EventoBE> bitacora)
        {
            dgvBitacora.DataSource = null;
            dgvBitacora.DataSource = bitacora;
            dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ConfigurarColumnasGrid();
        }

        // Lo estamos refaccionando como quiere silvestro
        // En mantenimiento
        // Columnas en orden
        // Base de datos cambiada
        // Posible error de ejecucion al cargar los datos en grilla
        private void ConfigurarColumnasGrid()
        {
            if (dgvBitacora.Columns.Contains("_Login"))
            {
                dgvBitacora.Columns["_Login"].HeaderText = "Login";
            }

            if (dgvBitacora.Columns.Contains("_Fecha"))
            {
                dgvBitacora.Columns["_Fecha"].HeaderText = "Fecha";
            }

            if (dgvBitacora.Columns.Contains("_Hora"))
            {
                dgvBitacora.Columns["_Hora"].HeaderText = "Hora";
            }

            if (dgvBitacora.Columns.Contains("_Modulo"))
            {
                dgvBitacora.Columns["_Modulo"].HeaderText = "Módulo";
            }

            if (dgvBitacora.Columns.Contains("_Evento"))
            {
                dgvBitacora.Columns["_Evento"].HeaderText = "Evento";
            }

            if (dgvBitacora.Columns.Contains("_Criticidad"))
            {
                dgvBitacora.Columns["_Criticidad"].HeaderText = "Criticidad";
            }

        }

        private void btnLimpiarFiltros_Click(object? sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        private void LimpiarFiltros()
        {
            try
            {

                DateTime hoy = DateTime.Today;
                dtpDesde.Value = hoy;
                dtpHasta.Value = hoy;


                cmbModulo.SelectedIndex = 0;


                _bitacoraCompleta = _bitacoraBLL.VerEventos();
                CargarBitacora(_bitacoraCompleta);

                MessageBox.Show(
                    "Filtros restablecidos correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al limpiar filtros: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnExportar_Click(object? sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf|Archivos CSV (*.csv)|*.csv";
                saveFileDialog.DefaultExt = "pdf";
                saveFileDialog.FileName = $"Bitacora_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName.EndsWith(".pdf"))
                    {
                        ExportarAPDF(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al exportar: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ExportarAPDF(string rutaArchivo)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont fontTitulo = new XFont("Segoe UI", 18, XFontStyle.Bold);
                XFont fontEncabezado = new XFont("Segoe UI", 10, XFontStyle.Bold);
                XFont fontDatos = new XFont("Segoe UI", 9);
                XFont fontPie = new XFont("Segoe UI", 8, XFontStyle.Italic);

                XColor colorEncabezado = XColor.FromArgb(46, 94, 67); // Verde oscuro
                XColor colorTextoEncabezado = XColor.FromArgb(255, 255, 255); // Blanco
                XColor colorTexto = XColor.FromArgb(0, 0, 0); // Negro

                double margenIzq = 20;
                double margenDer = 20;
                double margenSup = 20;
                double margenInf = 20;

                double anchoUtil = page.Width - margenIzq - margenDer;
                double yPos = margenSup;

                gfx.DrawString("📋 Auditoría y Bitácora del Sistema", fontTitulo, XBrushes.DarkGreen,
                    new XRect(margenIzq, yPos, anchoUtil, 30), XStringFormats.TopCenter);
                yPos += 40;

                string filtroInfo = $"Período: {dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy} | " +
                                    $"Criticidad: {cmbModulo.SelectedItem} | " +
                                    $"Fecha de Exportación: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                gfx.DrawString(filtroInfo, fontDatos, XBrushes.Black,
                    new XRect(margenIzq, yPos, anchoUtil, 15), XStringFormats.TopLeft);
                yPos += 25;

                double[] anchos = { 45, 105, 55, 50, 80, 200 };
                double xPosColumna = margenIzq;
                string[] encabezados = { "ID Evento", "Fecha y Hora", "DNI", "Criticidad", "Módulo", "Descripción" };

                for (int i = 0; i < encabezados.Length; i++)
                {
                    gfx.DrawRectangle(new XSolidBrush(colorEncabezado), xPosColumna, yPos, anchos[i], 15);
                    gfx.DrawString(encabezados[i], fontEncabezado, new XSolidBrush(colorTextoEncabezado),
                        new XRect(xPosColumna, yPos, anchos[i], 15), XStringFormats.CenterLeft);
                    xPosColumna += anchos[i];
                }
                yPos += 20;

                if (dgvBitacora.DataSource is List<EventoBE> bitacoraData)
                {
                    foreach (EventoBE bitacora in bitacoraData)
                    {
                        string descripcionCompleta = bitacora._Descripcion ?? "";
                        List<string> lineasDescripcion = new List<string>();
                        int maxCaracteresPorLinea = 38;

                        if (descripcionCompleta.Length <= maxCaracteresPorLinea)
                        {
                            lineasDescripcion.Add(descripcionCompleta);
                        }
                        else
                        {
                            string temp = descripcionCompleta;
                            while (temp.Length > 0)
                            {
                                if (temp.Length <= maxCaracteresPorLinea)
                                {
                                    lineasDescripcion.Add(temp);
                                    break;
                                }
                                else
                                {
                                    int indiceCorte = temp.LastIndexOf(' ', maxCaracteresPorLinea);
                                    if (indiceCorte <= 0) indiceCorte = maxCaracteresPorLinea; // Si no hay espacios, corta directo

                                    lineasDescripcion.Add(temp.Substring(0, indiceCorte).Trim());
                                    temp = temp.Substring(indiceCorte).Trim();
                                }
                            }
                        }


                        double altoLinea = 15;
                        double altoCelda = lineasDescripcion.Count * altoLinea;
                        if (altoCelda < 15) altoCelda = 15;


                        if (yPos + altoCelda > page.Height - margenInf)
                        {
                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            yPos = margenSup;

                            xPosColumna = margenIzq;
                            for (int i = 0; i < encabezados.Length; i++)
                            {
                                gfx.DrawRectangle(new XSolidBrush(colorEncabezado), xPosColumna, yPos, anchos[i], 15);
                                gfx.DrawString(encabezados[i], fontEncabezado, new XSolidBrush(colorTextoEncabezado),
                                    new XRect(xPosColumna, yPos, anchos[i], 15), XStringFormats.CenterLeft);
                                xPosColumna += anchos[i];
                            }
                            yPos += 20;
                        }
                        string[] datos = {
                    bitacora._Id_Evento.ToString(),
                    bitacora._Fecha.ToString("dd/MM/yyyy HH:mm"),
                    bitacora._Dni.ToString(),
                    bitacora._Criticidad.ToString(),
                    bitacora._Modulo,
                    ""
                };

                        xPosColumna = margenIzq;
                        for (int i = 0; i < datos.Length; i++)
                        {

                            gfx.DrawRectangle(XPens.LightGray, xPosColumna, yPos, anchos[i], altoCelda);

                            if (i < 5) // Columnas normales del 0 al 4
                            {
                                gfx.DrawString(datos[i], fontDatos, new XSolidBrush(colorTexto),
                                    new XRect(xPosColumna + 2, yPos, anchos[i] - 2, altoCelda), XStringFormats.CenterLeft);
                            }
                            else
                            {
                                double yPosInterno = yPos;
                                foreach (string linea in lineasDescripcion)
                                {
                                    gfx.DrawString(linea, fontDatos, new XSolidBrush(colorTexto),
                                        new XRect(xPosColumna + 2, yPosInterno, anchos[i] - 2, altoLinea), XStringFormats.CenterLeft);
                                    yPosInterno += altoLinea;
                                }
                            }

                            xPosColumna += anchos[i];
                        }
                        yPos += altoCelda; // El cursor de la página baja el alto total que usó esta fila
                    }
                }

                yPos = page.Height - margenInf - 10;
                gfx.DrawString($"Exportado el: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | Total de registros: {(dgvBitacora.DataSource is List<EventoBE> list ? list.Count : 0)}",
                    fontPie, XBrushes.Gray, new XRect(margenIzq, yPos, anchoUtil, 10), XStringFormats.BottomLeft);

                // Guardar documento
                document.Save(rutaArchivo);

                MessageBox.Show(
                    $"Archivo PDF exportado correctamente a: {rutaArchivo}",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al exportar a PDF: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void DgvBitacora_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0) return;

            //try
            //{
            //    EventoBE bitacora = (EventoBE)dgvBitacora.Rows[e.RowIndex].DataBoundItem;

            //    if (bitacora != null)
            //    {
            //        MostrarDetallesUsuarioBitacora(bitacora);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(
            //        $"Error al obtener detalles: {ex.Message}",
            //        "Error",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Error
            //    );
            //}
        }

        private void btnAplicarFiltro_Click_1(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string c = cmbCriticidad.SelectedItem?.ToString() ?? "Todas";

            var bitacoraFiltrada = _bitacoraCompleta;
            if (c != "Todas")
            {
                bitacoraFiltrada = bitacoraFiltrada
                    .Where(b => b._Criticidad.ToString() == c)
                    .ToList();
            }

            CargarBitacora(bitacoraFiltrada);
        }

        private void lblHasta_Click(object sender, EventArgs e)
        {

        }

        private void panelLateral_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvBitacora_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBitacora.CurrentRow != null)
            {
                string dni = dgvBitacora.CurrentRow.Cells["_Dni"].Value.ToString();

                List<UsuarioBE> usuarios = _usuarioBLL.ListarUsuarios();
                UsuarioBE? usuario = usuarios.FirstOrDefault(u => u._Dni == Convert.ToInt32(dni));


                if (usuario != null)
                {
                    textBox1.Text = usuario._Nombre;
                    textBox2.Text = usuario._Apellido;
                }
            }
        }

        private void cmbEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string evento = cmbEvento.SelectedItem?.ToString() ?? "Todos";

            var bitacoraFiltrada = _bitacoraCompleta;
            if (evento != "Todos")
            {
                if (evento == "Error")
                {
                    bitacoraFiltrada = bitacoraFiltrada
                        .Where(b => b._Descripcion != null && b._Descripcion.ToString().StartsWith("error", StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                else
                {
                    bitacoraFiltrada = bitacoraFiltrada
                        .Where(b => b._Descripcion.ToString() == evento)
                        .ToList();
                }
            }

            CargarBitacora(bitacoraFiltrada);
        }

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
    }
}
