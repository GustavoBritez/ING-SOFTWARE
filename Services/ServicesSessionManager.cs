using BE;
namespace Services
{
    /// <summary>
    /// ServicesSessionManager - Patrón Singleton
    /// Gestiona la SESIÓN actual del usuario (quién está logueado)
    /// NO maneja CRUD - eso lo hace BLL
    /// </summary>
    public class ServicesSessionManager
    {
        private static ServicesSessionManager _instancia;
        private static readonly object _lock = new object();
        
        private UsuarioBE usuarioActivo;

        private ServicesSessionManager()
        {

            usuarioActivo = null;
        }

        public static ServicesSessionManager Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new ServicesSessionManager();
                        }
                    }
                }
                return _instancia;
            }
        }

        public UsuarioBE ObtenerUsuarioActivo()
        {
            return usuarioActivo;
        }

        public bool Login(UsuarioBE newUsuario)
        {
            try
            {

                if ( usuarioActivo is null )
                {
                    usuarioActivo = newUsuario;
                }
                else
                {
                    return false;
                }
                    return true;
            }
            catch (Exception ex)
            {
                usuarioActivo = null;
                return false;
            }
        }

        public void Logout()
        {
            usuarioActivo = null;
        }
    }
}
