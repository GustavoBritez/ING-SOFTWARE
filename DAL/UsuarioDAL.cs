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
                string query = $@"INSERT INTO {TABLA_USUARIOS} (DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado, Estado, Idioma) 
                                  VALUES (@dni, @nombreDeUsuario, @nombre, @apellido, @contraseña, @rol, @bloqueado, @estado, @idioma)";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@dni", usuario._Dni),
                    new SqlParameter("@nombreDeUsuario", usuario._NombreDeUsuario),
                    new SqlParameter("@nombre", usuario._Nombre),
                    new SqlParameter("@apellido", usuario._Apellido),
                    new SqlParameter("@contraseña", usuario._Contraseña),
                    new SqlParameter("@rol", usuario._Rol),
                    new SqlParameter("@bloqueado", usuario._Bloqueado),
                    new SqlParameter("@estado", usuario._Estado),
                    new SqlParameter("@idioma",usuario._Idioma)
                };

                conexion.ExecuteNonQuery(query, parametros);

                Console.WriteLine($"Usuario {usuario._NombreDeUsuario} registrado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
            }
        }

        public void CambiarContraseña(UsuarioBE usuario)
        {

            try
            {
                string query = $@"UPDATE {TABLA_USUARIOS} 
                          SET Nombre = @nombre, Apellido = @apellido, NombreDeUsuario = @nombredeusuario, Contraseña = @contraseña, 
                              Rol = @rol, Bloqueado = @bloqueado , Estado = @estado, Idioma=@idioma
                          WHERE DNI = @dni";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@nombre", usuario._Nombre),
                    new SqlParameter("@apellido", usuario._Apellido),
                    new SqlParameter("@contraseña", usuario._Contraseña),
                    new SqlParameter("@rol", usuario._Rol),
                    new SqlParameter("@nombredeusuario", usuario._NombreDeUsuario),
                    new SqlParameter("@bloqueado", usuario._Bloqueado),
                    new SqlParameter("@dni", usuario._Dni),
                    new SqlParameter("@estado", usuario._Estado),
                    new SqlParameter("@idioma",usuario._Idioma)
                };

                conexion.ExecuteNonQuery(query, parametros);

                EventoBE bit = new EventoBE()
                {
                    _Criticidad = 5,
                    _Dni = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual(),
                    _Descripcion = $"Se cambió contraseña de {ServicesSessionManager.Instancia.ObtenerUsuarioActivo()._Dni}",
                    _Modulo = "MenuPrincipal",
                    _Fecha = DateTime.Now
                };

                EventoDAL bitacoraDAL = new();

                bitacoraDAL.GuardarBitacora(bit);
            }
            catch (Exception ex)
            {


                EventoBE bit = new EventoBE()
                {
                    _Criticidad = 2,
                    _Dni = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual(),
                    _Descripcion = $"No cambió contraseña de {ServicesSessionManager.Instancia.ObtenerUsuarioActivo()._Dni}",
                    _Modulo = "MenuPrincipal",
                    _Fecha = DateTime.Now
                };

                EventoDAL bitacoraDAL = new();

                bitacoraDAL.GuardarBitacora(bit);
                Console.WriteLine("ERROR:  No se cambio la contraseña ");
            }
            
        }

        public UsuarioBE ObtenerUsuario(string nombreDeUsuario)
        {
            try
            {
                //Seleccionamos todos estas columnas de la fila donde el NombreDeUsuario sea igual al que pasamos
                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado, Estado, Idioma
                                  FROM {TABLA_USUARIOS} 
                                  WHERE NombreDeUsuario = @nombreDeUsuario";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@nombreDeUsuario", nombreDeUsuario)
                };

                // Aqui obtenemos los datos del usuario
                DataTable dt = conexion.ExecuteReader(query, parametros);

                // Si no se encuentra el usuario , devolvemos null
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                UsuarioBE usuarioEncontrado = new UsuarioBE(
                    dt.Rows[0]["Nombre"].ToString(),
                    dt.Rows[0]["Apellido"].ToString(),
                    Convert.ToInt32(dt.Rows[0]["DNI"]),
                    dt.Rows[0]["NombreDeUsuario"].ToString(),
                    dt.Rows[0]["Contraseña"].ToString(),
                    dt.Rows[0]["Rol"].ToString(),
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
                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado, Estado, Idioma
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
                string query = $@"UPDATE {TABLA_USUARIOS} 
                          SET Nombre = @nombre, Apellido = @apellido, NombreDeUsuario = @nombredeusuario, Contraseña = @contraseña, 
                              Rol = @rol, Bloqueado = @bloqueado , Estado = @estado, Idioma=@idioma
                          WHERE DNI = @dni";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@nombre", usuario._Nombre),
                    new SqlParameter("@apellido", usuario._Apellido),
                    new SqlParameter("@contraseña", usuario._Contraseña),
                    new SqlParameter("@rol", usuario._Rol),
                    new SqlParameter("@nombredeusuario", usuario._NombreDeUsuario),
                    new SqlParameter("@bloqueado", usuario._Bloqueado),
                    new SqlParameter("@dni", usuario._Dni),
                    new SqlParameter("@estado", usuario._Estado),
                    new SqlParameter("@idioma", usuario._Idioma)
                };

                conexion.ExecuteNonQuery(query, parametros);

                var usuarioActivo = ServicesSessionManager.Instancia.ObtenerUsuarioActivo();

                int dniParaBitacora;
                string descripcionParaBitacora;

                if (usuarioActivo != null)
                {
                    dniParaBitacora = usuarioActivo._Dni;
                    descripcionParaBitacora = $"Modificación de usuario '{usuario._NombreDeUsuario}' por el administrador.";
                }
                else
                {
                    dniParaBitacora = usuario._Dni;


                    if (usuario._Bloqueado)
                    {
                        descripcionParaBitacora = $"Login fallido: El usuario '{usuario._NombreDeUsuario}' superó los intentos permitidos y bloqueó la cuenta.";
                    }
                    else
                    {
                        descripcionParaBitacora = $"Modificación automática del sistema sobre el usuario '{usuario._NombreDeUsuario}'.";
                    }
                }

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
                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado, Estado, Idioma
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
                              Rol = @rol, Bloqueado = @bloqueado , Estado = @estado, Idioma = @idioma
                          WHERE DNI = @dni";

            SqlParameter[] parametros = new SqlParameter[]
            {
                    new SqlParameter("@nombre", usuario._Nombre),
                    new SqlParameter("@apellido", usuario._Apellido),
                    new SqlParameter("@contraseña", usuario._Contraseña),
                    new SqlParameter("@rol", usuario._Rol),
                    new SqlParameter("@nombredeusuario", usuario._NombreDeUsuario),
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