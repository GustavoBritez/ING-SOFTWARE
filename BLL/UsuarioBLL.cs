using BE;
using DAL;
using Microsoft.Data.SqlClient;

namespace BLL
{
    public class UsuarioBLL
    {
        private UsuarioDAL usuarioDAL;

        // Diccionario estático para guardar intentos fallidos en memoria
        private static Dictionary<string, int> intentosFallidos = new Dictionary<string, int>();

        public UsuarioBLL()
        {
            usuarioDAL = new UsuarioDAL();
        }

        public void BuscarUsuario()
        {

        }
        /// <summary>
        /// Este metodo lo usaremos para cambiar el estado de un usuario si esta Desbloqueado a Bloqueado
        /// </summary>
        public void CambiarEstado(UsuarioBE usuario)
        {
            try
            {
                // Invertir el estado actual
                usuario._Bloqueado = !usuario._Bloqueado;
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

                // DAL se encarga de registrar la bitácora directamente
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

                // Normalizar el nombre de usuario a minúsculas para evitar problemas de case-sensitivity
                string nombreNormalizado = nombreDeUsuario.ToLower();

                UsuarioBE usuarioEnBD = usuarioDAL.ObtenerUsuario(nombreDeUsuario);

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
        /// Obtiene el número de intentos fallidos de un usuario
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
        public void LogOut(UsuarioBE usuario)
        {

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
        //No comprendo el UsuariosActivos, ya que no tenemos un campo en UsuarioBE que diaga "Activo" 
        //Pero capaz con Activo nos referimos a un usuario Activado diferente de uno Desactivado
        //Lo hago asi, igualmente es un cagada por que si esta bloqueado que diferencia hay de un desactivado ?
        // sera que el bloqueo es para un intento de 3 veces faillidos al logear ?
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

        //Eliminen el ValidarDNI no es necesario
        //Eliminen el Validar contraseaña no es necesario ya validamos en Login();



    }
}