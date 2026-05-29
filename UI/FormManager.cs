using System;
using System.Windows.Forms;

namespace UI
{
    public static class FormManager
    {
        private static Presentacion _presentacion;
        private static Form1 _form1;
        private static GestionUsuario _gestionUsuario;
        private static Bitacora _bitacora;

        public static Presentacion ObtenerPresentacion()
        {
            if (_presentacion == null || _presentacion.IsDisposed)
            {
                _presentacion = new Presentacion();
            }
            return _presentacion;
        }

        public static Form1 ObtenerForm1()
        {
            if (_form1 == null || _form1.IsDisposed)
            {
                _form1 = new Form1();
            }
            
            return _form1;
        }

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
            if ( _bitacora == null || _bitacora.IsDisposed)
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
            if (_presentacion != null && !_presentacion.IsDisposed)
            {
                _presentacion.Dispose();
            }
            if (_form1 != null && !_form1.IsDisposed)
            {
                _form1.Dispose();
            }
            if (_gestionUsuario != null && !_gestionUsuario.IsDisposed)
            {
                _gestionUsuario.Dispose();
            }

            _presentacion = null;
            _form1 = null;
            _gestionUsuario = null;
        }
    }
}
