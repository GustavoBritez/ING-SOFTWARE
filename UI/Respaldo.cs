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

namespace UI
{
    public partial class Respaldo : Form, IIdiomaObserver
    {
        private IdiomaBLL idiomaBLL = new IdiomaBLL();
        private readonly DigitoVerificadorBLL digitoVerificadorBLL = new DigitoVerificadorBLL();
        public Respaldo()
        {
            InitializeComponent();
            ServicesSessionManager.Instancia.Suscribir(this);
            ActualizarIdioma();
            ActualizarVisibilidadDV();
        }

        private void btnRealizarBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaBackup.Text))
            {
                MessageBox.Show("Seleccione una ruta para guardar el Backup.",
                                "Atención",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BackupBLL backup = new BackupBLL();

                // 1. Esto hace el backup físico y registra el evento en la Bitácora
                backup.RealizarBackup(txtRutaBackup.Text);

                // =========================================================
                // 2. ACTUALIZACIÓN DEL DÍGITO VERIFICADOR (Absorbe el evento)
                // =========================================================
                DigitoVerificadorBLL dvBll = new DigitoVerificadorBLL();
                dvBll.RecalcularYPersistir();
                // =========================================================

                MessageBox.Show("Backup realizado correctamente.",
                                "Éxito",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnBuscarBackup_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Guardar Backup";
            saveFileDialog1.Filter = "Backup (*.bak)|*.bak";
            saveFileDialog1.DefaultExt = "bak";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.FileName = $"BackupING_{DateTime.Now:yyyyMMdd_HHmmss}.bak";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRutaBackup.Text = saveFileDialog1.FileName;
            }
        }

        private void btnBuscarRestore_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Seleccionar Backup";
            openFileDialog1.Filter = "Backup (*.bak)|*.bak";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRutaRestore.Text = openFileDialog1.FileName;
            }
        }

        private void btnRealizarRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaRestore.Text))
            {
                MessageBox.Show("Seleccione un archivo .bak.",
                                "Atención",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "La base de datos será restaurada.\n\n¿Desea continuar?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                BackupBLL backup = new BackupBLL();

                backup.RealizarRestore(txtRutaRestore.Text);

                MessageBox.Show("Restore realizado correctamente.",
                                "Éxito",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnVerificarDV_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DigitoVerificadorBLL digitoVerificadorBLL = new DigitoVerificadorBLL();

                bool consistente = digitoVerificadorBLL.VerificarBaseDatos();

                if (consistente)
                {
                    MessageBox.Show(
                        "Los DV de la base de datos son consistentes. El sistema se encuentra íntegro.",
                        "Verificación DV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    // Traemos el detalle de los usuarios hackeados
                    List<string> corruptos = digitoVerificadorBLL.ObtenerUsuariosCorruptos();
                    string detalleCorruptos = corruptos.Count > 0
                        ? $"\n\nRegistros alterados detectados en la tabla Usuarios:\n- {string.Join("\n- ", corruptos)}"
                        : "\n\nSe detectaron alteraciones en otras tablas del sistema.";

                    MessageBox.Show(
                        $"Se detectaron inconsistencias en la base de datos.{detalleCorruptos}\n\nPor favor, utilice la opción 'Recalcular DV' para restaurar el sistema.",
                        "Alerta de Seguridad",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnRecalcularDV_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "ATENCIÓN: Se recalcularán y persistirán todos los DV (Individuales y Globales) de la base de datos. ¿Desea continuar?",
                "Confirmación de Recálculo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                DigitoVerificadorBLL digitoVerificadorBLL = new DigitoVerificadorBLL();

                // 1. Recalculamos el DV individual de cada usuario
                digitoVerificadorBLL.ActualizarDVIndividualesUsuarios();

                // 2. Recalculamos los totales globales en la tabla DV
                digitoVerificadorBLL.RecalcularYPersistir();

                MessageBox.Show(
                    "Los DV fueron recalculados correctamente. La integridad ha sido restaurada.",
                    "Recalcular DV",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Al finalizar, cerramos la sesión y volvemos al login por seguridad
                ServicesSessionManager.Instancia.Logout();
                FormManager.Navegar(this, FormManager.ObtenerLogin());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ActualizarVisibilidadDV()
        {
            var usuarioActivo = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();
            gbDV.Visible = usuarioActivo != null && usuarioActivo._IdPerfil == 1;
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

        private void btnSalirR_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerMenuPrincipal());
        }
    }

}
