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

        public int ObtenerDniUsuarioActual()
        {
            if (usuarioActivo != null)
            {
                return usuarioActivo._Dni;
            }
            return 0; // Retorna 0 si no hay usuario activo
        }

        public bool Login(UsuarioBE newUsuario)
        {
            try
            {
                if (newUsuario is not null)
                {
                    usuarioActivo = newUsuario;
                    return true;
                }
                else
                {
                    usuarioActivo = null;
                    return false;
                }
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
