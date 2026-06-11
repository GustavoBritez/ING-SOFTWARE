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
                EventoBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string nuevoEstado = usuario._Estado ? "activado" : "desactivado";
                string descripcion = $"Cambio de estado";
                bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "GestionUsuario");
            }
            catch (Exception ex)
            {
                EventoBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"ERROR: Cambio de estado";
                bitacoraBLL.RegistrarEvento(2, descripcion, dniActual, "GestionUsuario");
                throw;
            }
        }

        public void CambiarContraseña(UsuarioBE usuario)
        {
            try
            {
                usuarioDAL.CambiarContraseña(usuario);
                EventoBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"Cambio Contraseña";
                bitacoraBLL.RegistrarEvento(4, descripcion, dniActual, "MenuPrincipal");
            }
            catch( Exception ex)
            {
                Console.WriteLine($"Error en Cambiar contraseña: {ex.Message}");
                EventoBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $"ERROR: Cambio de contraseña";
                bitacoraBLL.RegistrarEvento(4, descripcion, dniActual, "MenuPrincipal");
                throw;
            }
        }

        /// <summary>
        ///  Este DSS hay que rehacerlo denuevo.
        ///  Anteriormente nos daba error siempre al crear la primer cuenta
        /// </summary>
        /// <param name="usuario"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void CrearUsuario(UsuarioBE usuario)
        {
            try
            {
                // 1. Validaciones básicas de nulidad
                if (string.IsNullOrWhiteSpace(usuario._NombreDeUsuario) || string.IsNullOrWhiteSpace(usuario._Contraseña))
                {
                    throw new ArgumentException("El usuario o contraseña no pueden estar vacíos.");
                }
                List<UsuarioBE> todosLosUsuarios = usuarioDAL.ListaUsuarios() ?? new List<UsuarioBE>();
                if (todosLosUsuarios.Any(u => string.Equals(u._NombreDeUsuario, usuario._NombreDeUsuario, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new InvalidOperationException($"El nombre de usuario '{usuario._NombreDeUsuario}' ya existe.");
                }
                if (todosLosUsuarios.Any(u => u._Dni == usuario._Dni))
                {
                    throw new InvalidOperationException($"El DNI '{usuario._Dni}' ya está registrado con otro usuario.");
                }
                usuario._Contraseña = Bcryp.HashearContraseña(usuario._Contraseña);
                usuarioDAL.CrearUsuario(usuario);
                int dniActual;
                try
                {
                    int dniSesion = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                    dniActual = dniSesion <= 0 ? 12345678 : dniSesion;
                }
                catch
                {
                    dniActual = 12345678;
                }
                string descripcion = $"Creacion de Usuario";
                new EventoBLL().RegistrarEvento(1, descripcion, dniActual, "GestionUsuario");
            }
            catch (Exception ex)
            {
                int dniActual;
                try
                {
                    int dniSesion = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                    dniActual = dniSesion <= 0 ? 12345678 : dniSesion;
                }
                catch
                {
                    dniActual = 12345678;
                }
                string descripcion = $"ERROR: Creación de Usuario";
                new EventoBLL().RegistrarEvento(4, descripcion, dniActual, "GestionUsuario");
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
                    if (intentosFallidos.ContainsKey(nombreNormalizado))
                    {
                        intentosFallidos[nombreNormalizado] = 0;
                    }
                    Console.WriteLine($"Login exitoso para usuario '{nombreDeUsuario}'.");

                    ServicesSessionManager.Instancia.Login(usuarioEnBD);
                    int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();

                    EventoBLL bitacoraBLL = new();
                    
                    string descripcion = $" Inicio de Sesion";
                    bitacoraBLL.RegistrarEvento(4, descripcion, dniActual, "GestionUsuario");
                    return true;
                }
                else
                {
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
                        
                        EventoBLL bitacoraBLL = new();
                        int dniActual = this.BuscarUsuario(nombreDeUsuario)._Dni;
                        string descripcion = $" Cuenta Bloqueada";
                        bitacoraBLL.RegistrarEvento(1, descripcion, dniActual, "GestionUsuario");
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
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

                EventoBLL bitacoraBLL = new();

                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();

                string descripcion = $" Cierre de Sesion";

                bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "GestionUsuario");
                ///Services.ServicesSessionManager.Instancia.Logout();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en LogOut: {ex.Message}");
                    EventoBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $" ERROR: Cierre de Sesion";
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

                EventoBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $" Modificar Usuario ";
                
                bitacoraBLL.RegistrarEvento(2, descripcion, dniActual, "GestionUsuario");

                usuarioDAL.ModificarUsuario(usuario);
            }
            catch (Exception ex)
            {

                EventoBLL    bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $" ERROR: Modificar Usuario ";
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
                return null;
            }
        }

        public void Desbloquear(UsuarioBE user )
        {
            try
            {
                usuarioDAL.Desbloquear(user);

                EventoBLL    bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $" Desbloquear";
                bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "GestionUsuario");
            }
            catch (Exception ex)
            {
                EventoBLL bitacoraBLL = new();
                int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
                string descripcion = $" ERROR: Desbloquear";
                bitacoraBLL.RegistrarEvento(1, descripcion, dniActual, "GestionUsuario");
            }
        }
    }
}