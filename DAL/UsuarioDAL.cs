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
                string query = $@"INSERT INTO {TABLA_USUARIOS} (DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado, Estado) 
                                  VALUES (@dni, @nombreDeUsuario, @nombre, @apellido, @contraseña, @rol, @bloqueado, @estado)";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@dni", usuario._Dni),
                    new SqlParameter("@nombreDeUsuario", usuario._NombreDeUsuario),
                    new SqlParameter("@nombre", usuario._Nombre),
                    new SqlParameter("@apellido", usuario._Apellido),
                    new SqlParameter("@contraseña", usuario._Contraseña),
                    new SqlParameter("@rol", usuario._Rol),
                    new SqlParameter("@bloqueado", usuario._Bloqueado),
                    new SqlParameter("@estado", usuario._Estado)
                };

                conexion.ExecuteNonQuery(query, parametros);

                // Registrar en bitácora
                BitacoraDAL bitacoraDAL = new();
                bitacoraDAL.GuardarBitacora(new BitacoraBE(
                    criticidad: 1,
                    descripcion: $"Creación de usuario '{usuario._NombreDeUsuario}'",
                    dni: ServicesSessionManager.Instancia.ObtenerUsuarioActivo()._Dni,
                    fecha: DateTime.Now,
                    modulo: Modulo
                ));

                Console.WriteLine($"Usuario {usuario._NombreDeUsuario} registrado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");

                // Registrar el error en bitácora
                try
                {
                    BitacoraDAL bitacoraDAL = new();
                    bitacoraDAL.GuardarBitacora(new BitacoraBE(
                        criticidad: 3, // Criticidad alta por error
                        descripcion: $"Error al crear usuario '{usuario._NombreDeUsuario}': {ex.Message}",
                        dni: ServicesSessionManager.Instancia.ObtenerUsuarioActivo()._Dni,
                        fecha: DateTime.Now,
                        modulo: Modulo
                    ));
                }
                catch { /* Si falla la bitácora, no interrumpimos el flujo de error */ }

                throw;
            }
        }
        public void CambiarContraseña(UsuarioBE usuario)
        {

            try
            {
                string query = $@"UPDATE {TABLA_USUARIOS} 
                          SET Nombre = @nombre, Apellido = @apellido, NombreDeUsuario = @nombredeusuario, Contraseña = @contraseña, 
                              Rol = @rol, Bloqueado = @bloqueado , Estado = @estado
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
                    new SqlParameter("@estado", usuario._Estado)
                };

                conexion.ExecuteNonQuery(query, parametros);

                BitacoraBE bit = new BitacoraBE()
                {
                    _Criticidad = 5,
                    _Dni = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual(),
                    _Descripcion = $"Se cambió contraseña de {ServicesSessionManager.Instancia.ObtenerUsuarioActivo()._Dni}",
                    _Modulo = "Cambiar Contraseña",
                    _Fecha = DateTime.Now
                };

                BitacoraDAL bitacoraDAL = new();

                bitacoraDAL.GuardarBitacora(bit);
            }
            catch (Exception ex)
            {


                BitacoraBE bit = new BitacoraBE()
                {
                    _Criticidad = 2,
                    _Dni = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual(),
                    _Descripcion = $"No cambió contraseña de {ServicesSessionManager.Instancia.ObtenerUsuarioActivo()._Dni}",
                    _Modulo = "Cambiar Contraseña",
                    _Fecha = DateTime.Now
                };

                BitacoraDAL bitacoraDAL = new();

                bitacoraDAL.GuardarBitacora(bit);
                Console.WriteLine("ERROR:  No se cambio la contraseña ");
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
                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado, Estado
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
                    dt.Rows[0]["Apellido"].ToString(),
                    Convert.ToInt32(dt.Rows[0]["DNI"]),
                    dt.Rows[0]["NombreDeUsuario"].ToString(),
                    dt.Rows[0]["Contraseña"].ToString(),
                    dt.Rows[0]["Rol"].ToString(),
                    Convert.ToBoolean(dt.Rows[0]["Bloqueado"]),
                    Convert.ToBoolean(dt.Rows[0]["Estado"])
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
                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado, Estado 
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
                    Convert.ToBoolean(dt.Rows[0]["Estado"])
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
                              Rol = @rol, Bloqueado = @bloqueado , Estado = @estado
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
                    new SqlParameter("@estado", usuario._Estado)
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

                BitacoraDAL bitacoraDAL = new();
                bitacoraDAL.GuardarBitacora(new BitacoraBE(
                    criticidad: usuarioActivo != null ? 3 : 1,
                    descripcion: descripcionParaBitacora,
                    dni: dniParaBitacora, 
                    fecha: DateTime.Now,
                    id_evento: usuario._Bloqueado ? 102 : 101, 
                    modulo: "UsuarioDAL"
                ));

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

                BitacoraDAL bitacoraDAL = new();
                bitacoraDAL.GuardarBitacora(new BitacoraBE(
                    criticidad: 1,
                    descripcion: $"Cambio de estado de '{usuario._NombreDeUsuario}'",
                    dni: 41236101,
                    fecha: DateTime.Now,
                    modulo: Modulo
                ));

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
                string query = $@"SELECT DNI, NombreDeUsuario, Nombre, Apellido, Contraseña, Rol, Bloqueado, Estado
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
                        Convert.ToBoolean(row["Estado"])
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