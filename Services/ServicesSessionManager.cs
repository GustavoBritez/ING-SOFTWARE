using BE;
using BLL;
using DAL;

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
        private readonly UsuarioBLL usuarioBLL; 

        private UsuarioBE usuarioActivo;

        private ServicesSessionManager()
        {
            usuarioBLL = new UsuarioBLL();
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

        public bool Login(string nombreDeUsuario, string contraseña)
        {
            try
            {
                // Verificar si ya hay una sesión activa
                if (usuarioActivo != null && usuarioActivo._NombreDeUsuario == nombreDeUsuario)
                {
                    return false;
                }


                // Validar credenciales usando BLL
                bool credencialesValidas = usuarioBLL.Login(nombreDeUsuario, contraseña);

                if (credencialesValidas)
                {
                    // Si es válido, obtener el usuario completo y guardarlo en sesión
                    UsuarioDAL usuarioDAL = new UsuarioDAL();
                    usuarioActivo = usuarioDAL.ObtenerUsuario(nombreDeUsuario);
                    
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
