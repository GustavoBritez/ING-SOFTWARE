using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UI
{
    public static class FormManager
    {
        private static Login _Login;
        private static MenuPrincipal _MenuPrincipal;
        private static GestionUsuario _gestionUsuario;
        private static Bitacora _bitacora;
        private static Perfiles _perfiles;


        public static Login ObtenerLogin()
        {
            if (_Login == null || _Login.IsDisposed)
            {
                _Login = new Login();
            }
            return _Login;
        }

        public static Perfiles ObtenerPerfiles()
        {
            if (_perfiles == null || _perfiles.IsDisposed)
            {
                _perfiles = new Perfiles();
            }
            return _perfiles;
        }

        public static MenuPrincipal ObtenerMenuPrincipal()
        {
            if (_MenuPrincipal == null || _MenuPrincipal.IsDisposed)
            {
                _MenuPrincipal = new MenuPrincipal();
            }

            return _MenuPrincipal;
        }

        // Para el composite son dos casos de uso, gestionar roles y gestionar familias
        // Un loop para los permisos tmb ? no entendi que dijo o que pidio si alguno lo entiende que me explique y lo hacemo

        public static GestionUsuario ObtenerGestionUsuario()
        {
            if (_gestionUsuario == null || _gestionUsuario.IsDisposed)
            {
                _gestionUsuario = new GestionUsuario();
            }
            return _gestionUsuario;
        }

        public static Bitacora ObtenerBitacora()
        {
            if (_bitacora == null || _bitacora.IsDisposed)
            {
                _bitacora = new Bitacora();
            }
            return _bitacora;
        }

        public static void Navegar(Form formularioActual, Form formularioDestino)
        {
            try
            {
                if (formularioActual != null && !formularioActual.IsDisposed)
                {
                    formularioActual.Hide();
                }

                if (formularioDestino != null && !formularioDestino.IsDisposed)
                {

                    formularioDestino.Show();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al navegar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void LimpiarInstancias()
        {
            if (_Login != null && !_Login.IsDisposed)
            {
                _Login.Dispose();
            }
            if (_MenuPrincipal != null && !_MenuPrincipal.IsDisposed)
            {
                _MenuPrincipal.Dispose();
            }
            if (_gestionUsuario != null && !_gestionUsuario.IsDisposed)
            {
                _gestionUsuario.Dispose();
            }

            _Login = null;
            _MenuPrincipal = null;
            _gestionUsuario = null;
        }

        #region "Graficos Botones"

        public class ButtonActive : Button
        {
            private Color _colorFondo = Color.FromArgb(0, 191, 143);
            private Color _colorTexto = Color.Black;

            public ButtonActive()
            {
                this.FlatStyle = FlatStyle.Flat;
                this.FlatAppearance.BorderSize = 0;
                this.Size = new Size(150, 45);
                this.BackColor = _colorFondo;
                this.ForeColor = _colorTexto;
                this.Cursor = Cursors.Hand; // Cambia el cursor a la manito al pasar por encima
                this.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            }

            private GraphicsPath GetCapsulePath(RectangleF rect, float radius)
            {
                GraphicsPath path = new GraphicsPath();
                float diameter = radius * 2;

                path.StartFigure();
                // Arco superior izquierdo
                path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
                // Arco superior derecho
                path.AddArc(rect.Width - diameter + rect.X, rect.Y, diameter, diameter, 270, 90);
                // Arco inferior derecho
                path.AddArc(rect.Width - diameter + rect.X, rect.Height - diameter + rect.Y, diameter, diameter, 0, 90);
                // Arco inferior izquierdo
                path.AddArc(rect.X, rect.Height - diameter + rect.Y, diameter, diameter, 90, 90);
                path.CloseFigure();

                return path;
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                base.OnPaint(pevent);

                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                RectangleF rectSuperficie = new RectangleF(0, 0, this.Width, this.Height);

                float raddioBorde = this.Height / 2F;

                using (GraphicsPath pathSuperficie = GetCapsulePath(rectSuperficie, raddioBorde))
                using (Brush brushFondo = new SolidBrush(this.BackColor))
                {
                    this.Region = new Region(pathSuperficie);

                    pevent.Graphics.FillPath(brushFondo, pathSuperficie);
                }

                TextRenderer.DrawText(
                    pevent.Graphics,
                    this.Text,
                    this.Font,
                    this.ClientRectangle,
                    this.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                this.Invalidate();
            }
        }
        #endregion


    }
}
