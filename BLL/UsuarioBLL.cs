using BE;
using DAL;
using Microsoft.Data.SqlClient;

namespace BLL
{
    public class UsuarioBLL
    {
        private UsuarioDAL usuarioDAL;

        public UsuarioBLL()
        {
            usuarioDAL = new UsuarioDAL();
        }

        public void BuscarUsuario()
        {

        }
        public void CambiarEstado()
        {

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
        /// Login: Valida NombreDeUsuario y Contraseña plana
        /// Flujo:
        /// 1. Busca el usuario por NombreDeUsuario en BD
        /// 2. Si existe y no está bloqueado, compara contraseña plana vs hasheada
        /// 3. Devuelve true si credenciales son válidas
        /// </summary>
        public bool Login(string nombreDeUsuario, string contraseñaPlana)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreDeUsuario) || string.IsNullOrWhiteSpace(contraseñaPlana))
                {
                    return false;
                }

                UsuarioBE usuarioEnBD = usuarioDAL.ObtenerUsuario(nombreDeUsuario);

                if (usuarioEnBD == null)
                {
                    Console.WriteLine($"Error: Contraseña o usuario incorrectos");
                    return false;
                }

                if (usuarioEnBD._Bloqueado)
                {
                    Console.WriteLine($"Error: Usuario '{nombreDeUsuario}' está bloqueado.");
                    return false;
                }
                // Validamos la contraseña no es necesario volver a validar con un metodo
                // 3. Comparar contraseña plana (ingresada) vs contraseña hasheada (en BD)
                // BCrypt.Verify(contraseña_plana, contraseña_hash_bd) devuelve true si coinciden
                bool contraseñaValida = BCrypt.Net.BCrypt.Verify(contraseñaPlana, usuarioEnBD._Contraseña);

                if (contraseñaValida)
                {
                    Console.WriteLine($"Login exitoso para usuario '{nombreDeUsuario}'.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: Contraseña o usuario incorrectos");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Login: {ex.Message}");
                return false;
            }
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
        //Lo hago asi.
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