using BE;
using DAL;
using Microsoft.Data.SqlClient;

namespace BLL
{
    public class UsuarioBLL
    {
        private UsuarioDAL usuarioDAL;

        // Diccionario estático para guardar intentos fallidos en memoria
        public Dictionary<string, int> intentosFallidos = new Dictionary<string, int>();

        public UsuarioBLL()
        {
            usuarioDAL = new UsuarioDAL();
        }

        public void BuscarUsuario()
        {

        }
        /// <summary>
        /// Este metodo lo usaremos para cambiar el estado de un usuario si esta Activo o Inactivo
        /// </summary>
        public void CambiarEstado(UsuarioBE usuario)
        {
            try
            {
                usuario._Estado = !usuario._Estado;
                usuarioDAL.CambioEstado(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CambiarEstado: {ex.Message}");
                throw;
            }
        }
        public void CrearUsuario(UsuarioBE usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario._NombreDeUsuario) ||
                    string.IsNullOrWhiteSpace(usuario._Contraseña))
                {
                    throw new ArgumentException("El usuario y la contraseña no pueden estar vacíos.");
                }

                usuario._Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario._Contraseña);

                usuarioDAL.CrearUsuario(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearUsuario: {ex.Message}");
                throw;
            }
        }
        public List<UsuarioBE> ListarUsuarios()
        {
            try
            {
                return usuarioDAL.ListaUsuarios();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ListarUsuarios: {ex.Message}");
                return new List<UsuarioBE>();
            }
        }
        /// <summary>
        /// <summary>
        /// Login: Valida NombreDeUsuario y Contraseña plana
        /// Gestiona intentos fallidos en memoria:
        /// - 1-2 intentos fallidos: Rechaza login
        /// - 3 intentos fallidos: Bloquea la cuenta automáticamente
        /// - Login exitoso: Reinicia el contador
        /// </summary>
        public bool Login(string nombreDeUsuario, string contraseñaPlana)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreDeUsuario) || string.IsNullOrWhiteSpace(contraseñaPlana))
                {
                    return false;
                }

                // Normalizar el nombre de usuario a minusculas para evitar problemas
                string nombreNormalizado = nombreDeUsuario.ToLower();

                // Usar el nombre normalizado para obtener el usuario de la BD
                // SQL normaliza todo
                UsuarioBE usuarioEnBD = usuarioDAL.ObtenerUsuario(nombreNormalizado);

                if (usuarioEnBD == null)
                {
                    Console.WriteLine($"Error: Usuario '{nombreDeUsuario}' no existe");
                    return false;
                }

                if (usuarioEnBD._Bloqueado)
                {
                    Console.WriteLine($"Error: Usuario '{nombreDeUsuario}' está bloqueado.");
                    return false;
                }

                // Validar contraseña
                bool contraseñaValida = BCrypt.Net.BCrypt.Verify(contraseñaPlana, usuarioEnBD._Contraseña);

                if (contraseñaValida)
                {
                    // Login exitoso: reiniciar contador
                    if (intentosFallidos.ContainsKey(nombreNormalizado))
                    {
                        intentosFallidos[nombreNormalizado] = 0;
                    }
                    Console.WriteLine($"Login exitoso para usuario '{nombreDeUsuario}'.");
                    return true;
                }
                else
                {
                    // Contraseña incorrecta: incrementar intentos
                    if (!intentosFallidos.ContainsKey(nombreNormalizado))
                    {
                        intentosFallidos[nombreNormalizado] = 0;
                    }

                    intentosFallidos[nombreNormalizado]++;
                    int intentosActuales = intentosFallidos[nombreNormalizado];

                    Console.WriteLine($"Error: Contraseña incorrecta para usuario '{nombreDeUsuario}'. Intentos: {intentosActuales}/3");

                    // Si llega a 3 intentos, bloquear la cuenta
                    if (intentosActuales >= 3)
                    {
                        usuarioEnBD._Bloqueado = true;
                        usuarioDAL.CambioEstado(usuarioEnBD);
                        Console.WriteLine($"Cuenta de usuario '{nombreDeUsuario}' bloqueada por 3 intentos fallidos.");
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Login: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Obtiene el numero de intentos fallidos de un usuario
        /// </summary>
        public int ObtenerIntentosFallidos(string nombreDeUsuario)
        {
            // Normalizar el nombre a minúsculas para consistencia
            string nombreNormalizado = nombreDeUsuario.ToLower();

            if (intentosFallidos.ContainsKey(nombreNormalizado))
            {
                return intentosFallidos[nombreNormalizado];
            }
            return 0;
        }
        /// <summary>
        /// LogOut: Cierra la sesión del usuario actual
        /// Integra con el SessionManager para limpiar la sesión
        /// </summary>
        public void LogOut(UsuarioBE usuario)
        {
            try
            {
                if (usuario != null)
                {

                    Console.WriteLine($"Usuario '{usuario._NombreDeUsuario}' (DNI: {usuario._Dni}) ha cerrado sesión.");
                    
                    Services.ServicesSessionManager.Instancia.Logout();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en LogOut: {ex.Message}");
                throw;
            }
        }
        // ModificarUsuario -> Entra un UsuarioBE con todos los cambios necesarios, incluido el DNI (que no se puede modificar)
        //Hasheamos la contraseña y llamamos a la DAL para subir estos cambios
        public void ModificarUsuario(UsuarioBE usuario)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(usuario._Contraseña))
                {
                    usuario._Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario._Contraseña);
                }

                usuarioDAL.ModificarUsuario(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ModificarUsuario: {ex.Message}");
                throw;
            }
        }
        public List<UsuarioBE> usuariosActivos()
        {
            List<UsuarioBE> test = new();
            return test;
        }
        public UsuarioBE ObtenerUsuario(string nombreDeUsuario)
        {
            try
            {
                return usuarioDAL.ObtenerUsuario(Convert.ToString(nombreDeUsuario));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerUsuario: {ex.Message}");
                return null;
            }
        }

    }
}