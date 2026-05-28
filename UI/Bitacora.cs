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

namespace UI
{
    public partial class Bitacora : Form
    {
        BitacoraBLL _bitacoraBLL = new BitacoraBLL();
        UsuarioBLL _usuarioBLL = new UsuarioBLL();
        private List<BitacoraBE>? _bitacoraCompleta;

        public Bitacora()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object? sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerForm1());
        }

        private void Bitacora_Load(object? sender, EventArgs e)
        {
            GestionBitacora_Load(sender, e);

        }

        private void GestionBitacora_Load(object? sender, EventArgs e)
        {

            dgvBitacora.DataSource = null;
            dgvBitacora.DataSource = _bitacoraBLL.VerEventos();
            _bitacoraCompleta = _bitacoraBLL.VerEventos();

            InicializarDateTimePickers();
            InicializarComboBoxCriticidad();

            CargarBitacora(_bitacoraCompleta);


            dtpDesde.ValueChanged += DtpFecha_ValueChanged;
            dtpHasta.ValueChanged += DtpFecha_ValueChanged;


            cmbCriticidad.SelectedIndexChanged += CmbCriticidad_SelectedIndexChanged;


            dgvBitacora.CellClick += DgvBitacora_CellClick;

            btnAplicarFiltro.Click += BtnAplicarFiltro_Click;
            btnExportar.Click += BtnExportar_Click;
        }

        private void InicializarDateTimePickers()
        {
            DateTime hoy = DateTime.Today;

            dtpDesde.Value = hoy;
            dtpDesde.Enabled = false;

            dtpHasta.Value = hoy;
            dtpHasta.MinDate = hoy.AddDays(-3);
            dtpHasta.MaxDate = hoy;
        }

        private void InicializarComboBoxCriticidad()
        {
            cmbCriticidad.Items.Clear();
            cmbCriticidad.Items.Add("Todas");
            cmbCriticidad.Items.Add("1");
            cmbCriticidad.Items.Add("2");
            cmbCriticidad.Items.Add("3");
            cmbCriticidad.SelectedIndex = 0; // Seleccionar "Todas" por defecto
        }

        private void DtpFecha_ValueChanged(object? sender, EventArgs e)
        {
            DateTime hoy = DateTime.Today;
            DateTime minimoPermitido = hoy.AddDays(-3);

            if (dtpHasta.Value < minimoPermitido)
            {
                MessageBox.Show(
                    "No puede seleccionar una fecha anterior a 3 días atras.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                dtpHasta.Value = minimoPermitido;
                return;
            }

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
        }

        private void CmbCriticidad_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AplicarFiltrosCombinados();
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
                string criticidadSeleccionada = cmbCriticidad.SelectedItem?.ToString() ?? "Todas";

                var bitacoraFiltrada = _bitacoraCompleta
                    .Where(b => b._Fecha.Date >= fechaDesde && b._Fecha.Date <= fechaHasta)
                    .ToList();

                if (criticidadSeleccionada != "Todas")
                {
                    bitacoraFiltrada = bitacoraFiltrada
                        .Where(b => b._Criticidad.ToString() == criticidadSeleccionada)
                        .ToList();
                }

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

        private void CargarBitacora(List<BitacoraBE> bitacora)
        {
            dgvBitacora.DataSource = null;
            dgvBitacora.DataSource = bitacora;
            dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ConfigurarColumnasGrid();
        }

        private void ConfigurarColumnasGrid()
        {
            if (dgvBitacora.Columns.Contains("_Id_Evento"))
            {
                dgvBitacora.Columns["_Id_Evento"].HeaderText = "ID Evento";
            }

            if (dgvBitacora.Columns.Contains("_Fecha"))
            {
                dgvBitacora.Columns["_Fecha"].HeaderText = "Fecha y Hora";
            }

            if (dgvBitacora.Columns.Contains("_Dni"))
            {
                dgvBitacora.Columns["_Dni"].HeaderText = "DNI Usuario";
            }

            if (dgvBitacora.Columns.Contains("_Criticidad"))
            {
                dgvBitacora.Columns["_Criticidad"].HeaderText = "Criticidad";
            }

            if (dgvBitacora.Columns.Contains("_Modulo"))
            {
                dgvBitacora.Columns["_Modulo"].HeaderText = "Módulo";
            }

            if (dgvBitacora.Columns.Contains("_Descripcion"))
            {
                dgvBitacora.Columns["_Descripcion"].HeaderText = "Descripción";
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


                cmbCriticidad.SelectedIndex = 0;


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

                // Título
                gfx.DrawString("📋 Auditoría y Bitácora del Sistema", fontTitulo, XBrushes.DarkGreen,
                    new XRect(margenIzq, yPos, anchoUtil, 30), XStringFormats.TopCenter);
                yPos += 40;

                string filtroInfo = $"Período: {dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy} | " +
                                   $"Criticidad: {cmbCriticidad.SelectedItem} | " +
                                   $"Fecha de Exportación: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                gfx.DrawString(filtroInfo, fontDatos, XBrushes.Black,
                    new XRect(margenIzq, yPos, anchoUtil, 15), XStringFormats.TopLeft);
                yPos += 25;

                double[] anchos = { 50, 110, 50, 50, 80, 150 }; // Ancho de cada columna
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

                if (dgvBitacora.DataSource is List<BitacoraBE> bitacoraData)
                {
                    foreach (BitacoraBE bitacora in bitacoraData)
                    {
                        if (yPos + 15 > page.Height - margenInf)
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

                        // Datos de las columnas
                        string[] datos = {
                            bitacora._Id_Evento.ToString(),
                            bitacora._Fecha.ToString("dd/MM/yyyy HH:mm"),
                            bitacora._Dni.ToString(),
                            bitacora._Criticidad.ToString(),
                            bitacora._Modulo,
                            bitacora._Descripcion
                        };

                        xPosColumna = margenIzq;
                        for (int i = 0; i < datos.Length; i++)
                        {
                            // Línea separadora
                            gfx.DrawRectangle(XPens.LightGray, xPosColumna, yPos, anchos[i], 15);
                            
                            // Texto de datos
                            gfx.DrawString(datos[i], fontDatos, new XSolidBrush(colorTexto),
                                new XRect(xPosColumna + 2, yPos, anchos[i] - 2, 15), XStringFormats.CenterLeft);
                            
                            xPosColumna += anchos[i];
                        }
                        yPos += 15;
                    }
                }

                yPos = page.Height - margenInf - 10;
                gfx.DrawString($"Exportado el: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | Total de registros: {(dgvBitacora.DataSource is List<BitacoraBE> list ? list.Count : 0)}", 
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
            if (e.RowIndex < 0) return; // Ignore header clicks

            try
            {
                BitacoraBE bitacora = (BitacoraBE)dgvBitacora.Rows[e.RowIndex].DataBoundItem;

                if (bitacora != null)
                {
                    MostrarDetallesUsuarioBitacora(bitacora);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al obtener detalles: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void MostrarDetallesUsuarioBitacora(BitacoraBE bitacora)
        {
            try
            {
                List<UsuarioBE> usuarios = _usuarioBLL.ListarUsuarios();
                UsuarioBE? usuario = usuarios.FirstOrDefault(u => u._Dni == bitacora._Dni);

                // POPUP
                Form popup = new Form();
                popup.Text = "Detalles del Evento y Usuario";
                popup.Size = new Size(500, 400);
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.BackColor = Color.FromArgb(218, 237, 223);
                popup.FormBorderStyle = FormBorderStyle.FixedDialog;
                popup.MaximizeBox = false;
                popup.MinimizeBox = false;

                Panel panel = new Panel();
                panel.Dock = DockStyle.Fill;
                panel.BackColor = Color.FromArgb(218, 237, 223);
                panel.Padding = new Padding(15);

                Label lblTituloEvento = new Label();
                lblTituloEvento.Text = "📌 Información del Evento";
                lblTituloEvento.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblTituloEvento.ForeColor = Color.FromArgb(46, 94, 67);
                lblTituloEvento.AutoSize = true;
                panel.Controls.Add(lblTituloEvento);

                int yPos = 40;
                string eventoInfo = $"ID Evento: {bitacora._Id_Evento}\n" +
                                   $"Fecha: {bitacora._Fecha:yyyy-MM-dd HH:mm:ss}\n" +
                                   $"Criticidad: {bitacora._Criticidad}\n" +
                                   $"Módulo: {bitacora._Modulo}\n" +
                                   $"Descripción: {bitacora._Descripcion}";

                Label lblEvento = new Label();
                lblEvento.Text = eventoInfo;
                lblEvento.Font = new Font("Segoe UI", 10);
                lblEvento.Location = new Point(15, yPos);
                lblEvento.Size = new Size(450, 120);
                lblEvento.AutoSize = false;
                panel.Controls.Add(lblEvento);

                Label lblTituloUsuario = new Label();
                lblTituloUsuario.Text = "👤 Información del Usuario";
                lblTituloUsuario.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblTituloUsuario.ForeColor = Color.FromArgb(46, 94, 67);
                lblTituloUsuario.Location = new Point(15, 170);
                lblTituloUsuario.AutoSize = true;
                panel.Controls.Add(lblTituloUsuario);

                Label lblUsuario = new Label();
                if (usuario != null)
                {
                    string usuarioInfo = $"Nombre: {usuario._Nombre}\n" +
                                        $"Apellido: {usuario._Apellido}\n" +
                                        $"DNI: {usuario._Dni}\n" +
                                        $"Usuario: {usuario._NombreDeUsuario}\n" +
                                        $"Estado: {(usuario._Estado ? "Activo" : "Inactivo")}\n" +
                                        $"Bloqueado: {(usuario._Bloqueado ? "Sí" : "No")}";
                    lblUsuario.Text = usuarioInfo;
                }
                else
                {
                    lblUsuario.Text = $"No se encontró información del usuario con DNI: {bitacora._Dni}";
                }

                lblUsuario.Font = new Font("Segoe UI", 10);
                lblUsuario.Location = new Point(15, 200);
                lblUsuario.Size = new Size(450, 120);
                lblUsuario.AutoSize = false;
                panel.Controls.Add(lblUsuario);

                Button btnCerrar = new Button();
                btnCerrar.Text = "Cerrar";
                btnCerrar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btnCerrar.BackColor = Color.FromArgb(225, 225, 225);
                btnCerrar.ForeColor = Color.Black;
                btnCerrar.Location = new Point(200, 330);
                btnCerrar.Size = new Size(100, 30);
                btnCerrar.Click += (s, e) => popup.Close();
                panel.Controls.Add(btnCerrar);

                popup.Controls.Add(panel);
                popup.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al mostrar detalles: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
