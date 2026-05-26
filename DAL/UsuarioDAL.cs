using DAL;
using BE;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class UsuarioDAL
    {
        private readonly Conexion conexion = new();
        private const string TABLA_USUARIOS = "Usuarios"; /// Nombre de la TABLA usuarios en la BD - SQL Server 2019 NO PROBE EN 2020

        public void CrearUsuario(UsuarioBE usuario)
        {
            try
            {
                string query = $@"INSERT INTO {TABLA_USUARIOS} (DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado) 
                                  VALUES (@dni, @nombreDeUsuario, @nombre, @apellido, @contraseña, @rol, @bloqueado)";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@dni", usuario._Dni),
                    new SqlParameter("@nombreDeUsuario", usuario._NombreDeUsuario),
                    new SqlParameter("@nombre", usuario._Nombre),
                    new SqlParameter("@apellido", usuario._Apellido),
                    new SqlParameter("@contraseña", usuario._Contraseña),
                    new SqlParameter("@rol", usuario._Rol),
                    new SqlParameter("@bloqueado", usuario._Bloqueado)
                };

                conexion.ExecuteNonQuery(query, parametros);
                Console.WriteLine($"Usuario {usuario._NombreDeUsuario} registrado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un usuario por su NombreDeUsuario (usado para login)
        /// Devuelve el usuario con su contraseña HASHEADA de la BD
        /// </summary>
        public UsuarioBE ObtenerUsuario(string nombreDeUsuario)
        {
            try
            {
                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado 
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

                // En modo conectado como ven tenemos los datos del  UN usuario en un datatable y se lo ponemos a un UsuarioBE para devolverlo a la BLL
                UsuarioBE usuarioEncontrado = new UsuarioBE(
                    dt.Rows[0]["Nombre"].ToString(),
                    dt.Rows[0]["Apellido"].ToString(),// Ven aqui usamos dt.rows[0] por que siempre se devuelve UN usuario.
                    Convert.ToInt32(dt.Rows[0]["DNI"]),
                    dt.Rows[0]["NombreDeUsuario"].ToString(),
                    dt.Rows[0]["Contraseña"].ToString(),
                    dt.Rows[0]["Rol"].ToString(),
                    Convert.ToInt32(dt.Rows[0]["Bloqueado"]) == 1 // 1 = Bloqueado, 0 = Desbloqueado
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
                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado 
                                  FROM {TABLA_USUARIOS} 
                                  WHERE DNI = @dni";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@dni", dni)
                };

                DataTable dt = conexion.ExecuteReader(query, parametros);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                UsuarioBE usuario = new UsuarioBE(
                    dt.Rows[0]["Nombre"].ToString(),
                    dt.Rows[0]["Apellido"].ToString(),
                    Convert.ToInt32(dt.Rows[0]["DNI"]),
                    dt.Rows[0]["NombreDeUsuario"].ToString(),
                    dt.Rows[0]["Contraseña"].ToString(),
                    dt.Rows[0]["Rol"].ToString(),
                    Convert.ToInt32(dt.Rows[0]["Bloqueado"]) == 1 // 1 = Bloqueado, 0 = Desbloqueado
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
                string query = $@"UPDATE {TABLA_USUARIOS} 
                                  SET Nombre = @nombre, Apellido = @apellido, Contraseña = @contraseña, 
                                      Rol = @rol, Bloqueado = @bloqueado 
                                  WHERE DNI = @dni";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@nombre", usuario._Nombre),
                    new SqlParameter("@apellido", usuario._Apellido),
                    new SqlParameter("@contraseña", usuario._Contraseña),
                    new SqlParameter("@rol", usuario._Rol),
                    new SqlParameter("@bloqueado", usuario._Bloqueado),
                    new SqlParameter("@dni", usuario._Dni)
                };

                conexion.ExecuteNonQuery(query, parametros);
                Console.WriteLine($"Usuario {usuario._NombreDeUsuario} modificado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar usuario: {ex.Message}");
                throw;
            }
        }

        public List<UsuarioBE> ListaUsuarios()
        {
            List<UsuarioBE> usuarios = new List<UsuarioBE>();

            try
            {
                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado 
                                  FROM {TABLA_USUARIOS} 
                                  ORDER BY NombreDeUsuario";

                DataTable dt = conexion.ExecuteReader(query);

                foreach (DataRow row in dt.Rows)
                {
                    UsuarioBE usuario = new UsuarioBE(
                        row["Nombre"].ToString(),
                        row["Apellido"].ToString(),
                        Convert.ToInt32(row["DNI"]),
                        row["NombreDeUsuario"].ToString(),
                        row["Contraseña"].ToString(),
                        row["Rol"].ToString(),
                        Convert.ToInt32(row["Bloqueado"]) == 1 // 1 = Bloqueado, 0 = Desbloqueado
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
    }
}