using BE;
using Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class UsuarioDAL
    {
        private readonly Conexion conexion = new();
        private const string TABLA_USUARIOS = "Usuarios"; /// Nombre de la TABLA usuarios en la BD - SQL Server 2019 NO PROBE EN 2020
        private readonly string Modulo = "UsuarioDAL";
        public void CrearUsuario(UsuarioBE usuario)
        {
            try
            {
                // 1. Cambiamos 'Rol' por 'ID_Perfil' en las columnas y en los valores
                string query = $@"INSERT INTO {TABLA_USUARIOS} (DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, ID_Perfil, Bloqueado, Estado, Idioma) 
                          VALUES (@dni, @nombreDeUsuario, @nombre, @apellido, @contraseña, @idPerfil, @bloqueado, @estado, @idioma)";

                SqlParameter[] parametros = new SqlParameter[]
                {
            new SqlParameter("@dni", usuario._Dni),
            new SqlParameter("@nombreDeUsuario", usuario._NombreDeUsuario),
            new SqlParameter("@nombre", usuario._Nombre),
            new SqlParameter("@apellido", usuario._Apellido),
            new SqlParameter("@contraseña", usuario._Contraseña),
            new SqlParameter("@idPerfil", usuario._IdPerfil), // 2. ¡Agregamos el parámetro que faltaba!
            new SqlParameter("@bloqueado", usuario._Bloqueado),
            new SqlParameter("@estado", usuario._Estado),
            new SqlParameter("@idioma", usuario._Idioma)
                };

                conexion.ExecuteNonQuery(query, parametros);

                Console.WriteLine($"Usuario {usuario._NombreDeUsuario} registrado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                // Es buena práctica relanzar la excepción si querés que la interfaz gráfica (UI) se entere del error
                throw;
            }
        }

        public void CambiarContraseña(UsuarioBE usuario)
        {
            try
            {
                // 1. Cambiamos 'Rol = @rol' por 'ID_Perfil = @idPerfil'
                string query = $@"UPDATE {TABLA_USUARIOS} 
                          SET Nombre = @nombre, Apellido = @apellido, NombreDeUsuario = @nombredeusuario, Contraseña = @contraseña, 
                              ID_Perfil = @idPerfil, Bloqueado = @bloqueado , Estado = @estado, Idioma=@idioma
                          WHERE DNI = @dni";

                SqlParameter[] parametros = new SqlParameter[]
                {
            new SqlParameter("@nombre", usuario._Nombre),
            new SqlParameter("@apellido", usuario._Apellido),
            new SqlParameter("@contraseña", usuario._Contraseña),
            new SqlParameter("@nombredeusuario", usuario._NombreDeUsuario),
            new SqlParameter("@idPerfil", usuario._IdPerfil), // 2. ¡Acá agregamos el parámetro faltante!
            new SqlParameter("@bloqueado", usuario._Bloqueado),
            new SqlParameter("@dni", usuario._Dni),
            new SqlParameter("@estado", usuario._Estado),
            new SqlParameter("@idioma", usuario._Idioma)
                };

                conexion.ExecuteNonQuery(query, parametros);
            }
            catch (Exception ex)
            {
              
                Console.WriteLine("ERROR: No se cambio la contraseña ");

                // Es muy importante relanzar la excepción para que el formulario (UI) sepa que falló 
                // y no le muestre un cartel de "Éxito" al usuario.
                throw;
            }
        }

        public UsuarioBE ObtenerUsuario(string nombreDeUsuario)
        {
            try
            {
                // 1. Cambiamos 'Rol' por 'ID_Perfil' en el SELECT
                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, ID_Perfil, Bloqueado, Estado, Idioma
                          FROM {TABLA_USUARIOS} 
                          WHERE NombreDeUsuario = @nombreDeUsuario";

                SqlParameter[] parametros = new SqlParameter[]
                {
            new SqlParameter("@nombreDeUsuario", nombreDeUsuario)
                };

                DataTable dt = conexion.ExecuteReader(query, parametros);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // 2. Le pasamos los 9 parámetros correctos, convirtiendo el ID_Perfil a entero
                UsuarioBE usuarioEncontrado = new UsuarioBE(
                    dt.Rows[0]["Nombre"].ToString(),
                    dt.Rows[0]["Apellido"].ToString(),
                    Convert.ToInt32(dt.Rows[0]["DNI"]),
                    dt.Rows[0]["NombreDeUsuario"].ToString(),
                    dt.Rows[0]["Contraseña"].ToString(),
                    Convert.ToInt32(dt.Rows[0]["ID_Perfil"]), // ¡ACÁ ESTÁ LA CORRECCIÓN CLAVE!
                    Convert.ToBoolean(dt.Rows[0]["Bloqueado"]),
                    Convert.ToBoolean(dt.Rows[0]["Estado"]),
                    dt.Rows[0]["Idioma"].ToString()
                );

                return usuarioEncontrado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuario por nombre: {ex.Message}");
                return null;
            }
        }
        public UsuarioBE BuscarUsuario(int dni)
        {
            try
            {
                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, ID_Perfil, Bloqueado, Estado, Idioma
                          FROM {TABLA_USUARIOS} 
                          WHERE DNI = @dni";

                SqlParameter[] parametros = new SqlParameter[] { new SqlParameter("@dni", dni) };
                DataTable dt = conexion.ExecuteReader(query, parametros);

                if (dt.Rows.Count == 0) return null;

                UsuarioBE usuario = new UsuarioBE(
                    dt.Rows[0]["Nombre"].ToString(),
                    dt.Rows[0]["Apellido"].ToString(),
                    Convert.ToInt32(dt.Rows[0]["DNI"]),
                    dt.Rows[0]["NombreDeUsuario"].ToString(),
                    dt.Rows[0]["Contraseña"].ToString(),
                    Convert.ToInt32(dt.Rows[0]["ID_Perfil"]), // FALTABA ESTO
                    Convert.ToBoolean(dt.Rows[0]["Bloqueado"]),
                    Convert.ToBoolean(dt.Rows[0]["Estado"]),
                    dt.Rows[0]["Idioma"].ToString()
                );

                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuario: {ex.Message}");
                return null;
            }
        }

        public void ModificarUsuario(UsuarioBE usuario)
        {
            try
            {
                // Cambiamos "Rol = @rol" por "ID_Perfil = @idPerfil" para que coincida con tu base de datos
                string query = $@"UPDATE {TABLA_USUARIOS} 
                          SET Nombre = @nombre, Apellido = @apellido, NombreDeUsuario = @nombredeusuario, 
                              Contraseña = @contraseña, ID_Perfil = @idPerfil, Bloqueado = @bloqueado, 
                              Estado = @estado, Idioma=@idioma
                          WHERE DNI = @dni";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@nombre", usuario._Nombre),
                    new SqlParameter("@apellido", usuario._Apellido),
                    new SqlParameter("@contraseña", usuario._Contraseña),
                    new SqlParameter("@idPerfil", usuario._IdPerfil), // Ahora sí existe
                    new SqlParameter("@nombredeusuario", usuario._NombreDeUsuario),
                    new SqlParameter("@bloqueado", usuario._Bloqueado),
                    new SqlParameter("@dni", usuario._Dni),
                    new SqlParameter("@estado", usuario._Estado),
                    new SqlParameter("@idioma", usuario._Idioma)
                };

                conexion.ExecuteNonQuery(query, parametros);

                // ... el resto de tu código de la bitácora queda igual ...
                Console.WriteLine($"Usuario {usuario._NombreDeUsuario} modificado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar usuario: {ex.Message}");
                throw;
            }
        }

        public void CambioEstado(UsuarioBE usuario)
        {
            try
            {
                string query = $@"UPDATE {TABLA_USUARIOS} 
                                  SET Estado = @estado
                                  WHERE DNI = @dni";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@estado", usuario._Estado),
                    new SqlParameter("@dni", usuario._Dni)
                };

                conexion.ExecuteNonQuery(query, parametros);

                Console.WriteLine($"Usuario {usuario._NombreDeUsuario} Cambio de estado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar de estado, usuario: {ex.Message}");
                throw;
            }
        }

        public List<UsuarioBE> ListaUsuarios()
        {
            List<UsuarioBE> usuarios = new List<UsuarioBE>();

            try
            {

                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, ID_Perfil, Bloqueado, Estado, Idioma
                  FROM {TABLA_USUARIOS} 
                  ORDER BY NombreDeUsuario";

                DataTable dt = conexion.ExecuteReader(query);

                foreach (DataRow row in dt.Rows)
                {
                    // Pasamos los 9 parámetros
                    UsuarioBE usuario = new UsuarioBE(
                        row["Nombre"].ToString(),
                        row["Apellido"].ToString(),
                        Convert.ToInt32(row["DNI"]),
                        row["NombreDeUsuario"].ToString(),
                        row["Contraseña"].ToString(),
                        Convert.ToInt32(row["ID_Perfil"]), // FALTABA ESTO
                        Convert.ToBoolean(row["Bloqueado"]),
                        Convert.ToBoolean(row["Estado"]),
                        row["Idioma"].ToString()
                    );
                    usuarios.Add(usuario);
                }

                Console.WriteLine($"Se obtuvieron {usuarios.Count} usuarios.");
                return usuarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar usuarios: {ex.Message}");
                return usuarios;
            }
        }

        public void Desbloquear(UsuarioBE usuario)
        {
            string query = $@"UPDATE {TABLA_USUARIOS} 
                      SET Nombre = @nombre, Apellido = @apellido, NombreDeUsuario = @nombredeusuario, Contraseña = @contraseña, 
                          ID_Perfil = @idPerfil, Bloqueado = @bloqueado , Estado = @estado, Idioma = @idioma
                      WHERE DNI = @dni";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", usuario._Nombre),
                new SqlParameter("@apellido", usuario._Apellido),
                new SqlParameter("@contraseña", usuario._Contraseña),
                new SqlParameter("@nombredeusuario", usuario._NombreDeUsuario),
                new SqlParameter("@idPerfil", usuario._IdPerfil), 
                new SqlParameter("@bloqueado", usuario._Bloqueado),
                new SqlParameter("@dni", usuario._Dni),
                new SqlParameter("@estado", usuario._Estado),
                new SqlParameter("@idioma", usuario._Idioma)
            };

            conexion.ExecuteNonQuery(query, parametros);
        }
        public void CambiarIdiomaUsuario(UsuarioBE usuario)
        {
            try
            {
                string query = $@"UPDATE {TABLA_USUARIOS}
                          SET Idioma = @idioma
                          WHERE DNI = @dni";

                SqlParameter[] parametros = new SqlParameter[]
                {
            new SqlParameter("@idioma", usuario._Idioma),
            new SqlParameter("@dni", usuario._Dni)
                };

                conexion.ExecuteNonQuery(query, parametros);

                Console.WriteLine($"Idioma del usuario {usuario._NombreDeUsuario} actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar idioma del usuario: {ex.Message}");
                throw;
            }
        }
    }
}