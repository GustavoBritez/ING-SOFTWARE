using BLL;
using BE;

namespace Services
{
    /// <summary>
    /// Gestiona operaciones CRUD de usuarios (Crear, Leer, Actualizar, Eliminar)
    /// NO maneja sesiones - eso lo hace ServicesSessionManager
    /// </summary>
    public class UsuarioService
    {
        private static UsuarioService _instancia;
        private static readonly object _lock = new object();
        private readonly UsuarioBLL usuarioBLL;

        private UsuarioService()
        {
            usuarioBLL = new UsuarioBLL();
        }

        /// <summary>
        /// Propiedad para obtener la instancia única (Singleton)
        /// </summary>
        public static UsuarioService Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new UsuarioService();
                        }
                    }
                }
                return _instancia;
            }
        }

        public void CrearUsuario(UsuarioBE usuario)
        {
            usuarioBLL.CrearUsuario(usuario);
            // Llamar la bitacora
        }

        public UsuarioBE ObtenerUsuario(int dni)
        {
            return usuarioBLL.ObtenerUsuario(dni);
        }

        public void ModificarUsuario(UsuarioBE usuario)
        {
            usuarioBLL.ModificarUsuario(usuario);
        }

        public List<UsuarioBE> ListarUsuarios()
        {
            return usuarioBLL.ListarUsuarios();
        }

        public bool ValidarContraseña(string contraseñaIngresada, string contraseñaHasheada)
        {
            return BCrypt.Net.BCrypt.Verify(contraseñaIngresada, contraseñaHasheada);
        }

        public bool ValidarDNI(int dni)
        {
            return dni > 0;
        }
    }
}
