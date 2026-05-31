using BE;
using DAL;
using Microsoft.Data.SqlClient;
using Services;
using System.Net;
using Services;

namespace BLL
{
    public class UsuarioBLL
    {
        private UsuarioDAL usuarioDAL;
        private ServicioBcrypt Bcryp;
        // Diccionario estático para guardar intentos fallidos en memoria
        public Dictionary<string, int> intentosFallidos = new Dictionary<string, int>();

        public UsuarioBLL()
        {
            usuarioDAL = new UsuarioDAL();
            Bcryp = new ServicioBcrypt();
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
                BitacoraBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Estado cambiado a {usuario._Estado}";
                bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "GestionUsuario");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CambiarEstado: {ex.Message}");
                BitacoraBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"No se cambio el estado";
                bitacoraBLL.RegistrarEvento(2, descripcion, dniActual, "GestionUsuario");
                throw;
            }
        }

        public void CambiarContraseña(UsuarioBE usuario)
        {
            try
            {
                usuarioDAL.CambiarContraseña(usuario);
                BitacoraBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Contraseña cambiada correctamente";
                bitacoraBLL.RegistrarEvento(4, descripcion, dniActual, "GestionUsuario");
            }
            catch( Exception ex)
            {
                Console.WriteLine($"Error en Cambiar contraseña: {ex.Message}");
                BitacoraBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Error al cambiar la contraseña";
                bitacoraBLL.RegistrarEvento(4, descripcion, dniActual, "GestionUsuario");
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

                usuario._Contraseña = Bcryp.HashearContraseña(usuario._Contraseña);

                usuarioDAL.CrearUsuario(usuario);
                BitacoraBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Creación de nuevo usuario '{usuario._NombreDeUsuario}' (DNI: {usuario._Dni}, Rol: {usuario._Rol})";
                bitacoraBLL.RegistrarEvento(4, descripcion, dniActual, "GestionUsuario");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearUsuario: {ex.Message}");
                BitacoraBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Fallo al crearse el usuario";
                bitacoraBLL.RegistrarEvento(4, descripcion, dniActual, "GestionUsuario");
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

        public bool Login(string nombreDeUsuario, string contraseñaPlana)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreDeUsuario) || string.IsNullOrWhiteSpace(contraseñaPlana))
                {
                    return false;
                }

                
                string nombreNormalizado = nombreDeUsuario.ToLower();

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

                
                bool contraseñaValida = Bcryp.ValidarContraseña(contraseñaPlana, usuarioEnBD._Contraseña);

                if (contraseñaValida)
                {
                    // Login exitoso: reiniciar contador
                    if (intentosFallidos.ContainsKey(nombreNormalizado))
                    {
                        intentosFallidos[nombreNormalizado] = 0;
                    }
                    Console.WriteLine($"Login exitoso para usuario '{nombreDeUsuario}'.");

                    ServicesSessionManager.Instancia.Login(usuarioEnBD);
                    int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();

                    BitacoraBLL bitacoraBLL = new();
                    
                    string descripcion = $" Inicio de Sesion";
                    bitacoraBLL.RegistrarEvento(4, descripcion, dniActual, "GestionUsuario");
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

                   
                    if (intentosActuales >= 3)
                    {
                        usuarioEnBD._Bloqueado = true;
                        
                        ModificarUsuario(usuarioEnBD);
                        Console.WriteLine($"Cuenta de usuario '{nombreDeUsuario}' bloqueada por 3 intentos fallidos.");
                        
                        BitacoraBLL bitacoraBLL = new();
                        int dniActual = this.BuscarUsuario(nombreDeUsuario)._Dni;
                        string descripcion = $" Cuenta Bloqueada";
                        bitacoraBLL.RegistrarEvento(1, descripcion, dniActual, "GestionUsuario");
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
        ///  Falta testear
        ///  Lo que hice fue un Logout del Session Mannager al finalizar ya sea por Exito o Fallo 
        /// </summary>
        /// <param name="usuario"></param>
        public void LogOut(UsuarioBE usuario)
        {
            try
            {
                if (usuario != null)
                {
                    Console.WriteLine($"Usuario '{usuario._NombreDeUsuario}' (DNI: {usuario._Dni}) ha cerrado sesión.");
                }

                intentosFallidos.Clear();

                BitacoraBLL bitacoraBLL = new();

                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();

                string descripcion = $" Cierre de Sesion Exitoso";

                bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Gestion Usuario");
                ///Services.ServicesSessionManager.Instancia.Logout();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en LogOut: {ex.Message}");
                BitacoraBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $" Error en LogOut: {usuario._NombreDeUsuario}";
                bitacoraBLL.RegistrarEvento(2, descripcion, dniActual, "GestionUsuario");
                ///Services.ServicesSessionManager.Instancia.Logout();
                throw;
            }
            finally
            {
                Services.ServicesSessionManager.Instancia.Logout();
            }
        }

        public void ModificarUsuario(UsuarioBE usuario)
        {
            try
            {
                // Evitar hashear nuevamente si la contraseña ya está hasheada en la BD (las hashes de BCrypt empiezan por "$2")
                if (!string.IsNullOrWhiteSpace(usuario._Contraseña) && !usuario._Contraseña.StartsWith("$2"))
                {
                    usuario._Contraseña = Bcryp.HashearContraseña(usuario._Contraseña);
                }

                BitacoraBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $" Modificar Usuario -> {usuario._NombreDeUsuario} Exitoso ";
                
                bitacoraBLL.RegistrarEvento(2, descripcion, dniActual, "GestionUsuario");

                usuarioDAL.ModificarUsuario(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ModificarUsuario: {ex.Message}");

                BitacoraBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $" Error al modificar usuario";
                bitacoraBLL.RegistrarEvento(2, descripcion, dniActual, "GestionUsuario");
                throw;
            }
        }

        public UsuarioBE BuscarUsuario(string nombreDeUsuario)
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

        public void Desbloquear(UsuarioBE user )
        {
            try
            {
                usuarioDAL.Desbloquear(user);

                BitacoraBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $" Desbloquear usuario exitoso -> {user._NombreDeUsuario}";
                bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "GestionUsuario");
            }
            catch (Exception ex)
            {
                BitacoraBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $" Desbloquear usuario Fallido";
                bitacoraBLL.RegistrarEvento(1, descripcion, dniActual, "GestionUsuario");
            }
        }
    }
}