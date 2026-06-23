using Microsoft.Data.SqlClient;
using Services.Perfiles;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL.Perfiles
{
    public class PatenteDAL
    {
        private readonly Conexion _conexion = new();

        public PatenteDAL() { }

        public void InsertarPermisoPerfil(int idFamilia, int idPermiso)
        {
            string query = "INSERT INTO Permiso_Familia (ID_Familia, ID_Permiso) VALUES (@idFamilia, @idPermiso)";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idFamilia", idFamilia),
                new SqlParameter("@idPermiso", idPermiso)
            };
            _conexion.ExecuteNonQuery(query, parametros);
        }

        public void EliminarPermisoPerfil(int idFamilia, int idPermiso)
        {
            string query = "DELETE FROM Permiso_Familia WHERE ID_Familia = @idFamilia AND ID_Permiso = @idPermiso";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idFamilia", idFamilia),
                new SqlParameter("@idPermiso", idPermiso)
            };
            _conexion.ExecuteNonQuery(query, parametros);
        }

        public void InsertarFamiliaPerfil(int idFamiliaPadre, int idFamiliaHija)
        {
            string query = "INSERT INTO Familia_Familia (ID_FamiliaPadre, ID_FamiliaHija) VALUES (@idFamiliaPadre, @idFamiliaHija)";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idFamiliaPadre", idFamiliaPadre),
                new SqlParameter("@idFamiliaHija", idFamiliaHija)
            };
            _conexion.ExecuteNonQuery(query, parametros);
        }

        public void EliminarFamiliaPerfil(int idFamiliaPadre, int idFamiliaHija)
        {
            string query = "DELETE FROM Familia_Familia WHERE ID_FamiliaPadre = @idFamiliaPadre AND ID_FamiliaHija = @idFamiliaHija";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idFamiliaPadre", idFamiliaPadre),
                new SqlParameter("@idFamiliaHija", idFamiliaHija)
            };
            _conexion.ExecuteNonQuery(query, parametros);
        }

        // Método para eliminar la relación
        public void EliminarPermisoAPerfil(int idPerfil, int idPermiso)
        {
            string query = "DELETE FROM [ING].[dbo].[Perfil_Permiso] WHERE ID_Perfil = @idPerfil AND ID_Permiso = @idPermiso";

            SqlParameter[] param = {
                new SqlParameter("@idPerfil", idPerfil),
                new SqlParameter("@idPermiso", idPermiso)
            };

            _conexion.ExecuteNonQuery(query, param);
        }
        public Dictionary<string, string> ObtenerControlesRestringidos(string nombreFormulario)
        {
            Dictionary<string, string> restricciones = new Dictionary<string, string>();

            string query = @"SELECT C.NombreControl, P.Nombre 
                     FROM Permiso_Control C
                     INNER JOIN Permiso P ON C.ID_Permiso = P.ID_Permiso
                     WHERE C.NombreFormulario = @nombreForm";

            SqlParameter[] param = {
                new SqlParameter("@nombreForm", nombreFormulario)
            };

            DataTable dt = _conexion.ExecuteReader(query, param);

            if (dt != null)
            {
                foreach (DataRow fila in dt.Rows)
                {
                    string nombreControl = fila["NombreControl"].ToString();
                    string nombrePermiso = fila["Nombre"].ToString();

                    if (!restricciones.ContainsKey(nombreControl))
                    {
                        restricciones.Add(nombreControl, nombrePermiso);
                    }
                }
            }

            return restricciones;
        }
        public bool ExistePermisoPorNombre(string nombrePermiso)
        {
            string query = "SELECT COUNT(1) FROM Permiso WHERE Nombre = @nombre";

            SqlParameter[] param = {
                        new  SqlParameter("@nombre", nombrePermiso)
                        };

            DataTable dt = _conexion.ExecuteReader(query, param);

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]) > 0;
            }

            return false;
        }

        public List<Perfil> ObtenerFamiliasPerfil()
        {
            List<Perfil> lista = new();
            string query = "SELECT ID_Familia, Nombre FROM Familia";
            DataTable dt = _conexion.ExecuteReader(query, null);

            foreach (DataRow fila in dt.Rows)
            {
                int id = Convert.ToInt32(fila["ID_Familia"]);
                string nombre = fila["Nombre"].ToString();

                lista.Add(new FamiliaServices(nombre) { Id = id });
            }
            return lista;
        }

        public List<Perfil> ObtenerPermisosPerfil()
        {
            List<Perfil> lista = new();
            string query = "SELECT ID_Permiso, Nombre FROM Permiso";
            DataTable dt = _conexion.ExecuteReader(query, null);

            foreach (DataRow fila in dt.Rows)
            {
                int id = Convert.ToInt32(fila["ID_Permiso"]);
                string nombre = fila["Nombre"].ToString();

                lista.Add(new PatenteServices(nombre) { Id = id });
            }
            return lista;
        }
        public List<PatenteServices> ObtenerPermisosDePerfil(int idPerfil)
        {
            string query = @"SELECT P.ID_Permiso, P.Nombre 
                     FROM Permiso P
                     INNER JOIN Perfil_Permiso PP ON P.ID_Permiso = PP.ID_Permiso
                     WHERE PP.ID_Perfil = @idPerfil";

            SqlParameter[] parametros = new SqlParameter[] { new SqlParameter("@idPerfil", idPerfil) };

            DataTable dt = _conexion.ExecuteReader(query, parametros);

            List<PatenteServices> lista = new();
            foreach (DataRow fila in dt.Rows)
            {
                lista.Add(new PatenteServices(fila["Nombre"].ToString()) { Id = Convert.ToInt32(fila["ID_Permiso"]) });
            }
            return lista;
        }
        public List<Perfil> ObtenerComponentesTotales()
        {
            List<Perfil> lista = new();
            lista.AddRange(ObtenerFamiliasPerfil());
            lista.AddRange(ObtenerPermisosPerfil());
            return lista;
        }

        public void VincularPermisoABoton(string nombreFormulario, string nombreBoton, string nombrePatente)
        {
            try
            {
                string query = @"INSERT INTO Permiso_Boton (NombreFormulario, NombreControl, NombrePatente) 
                         VALUES (@nombreFormulario, @nombreBoton, @nombrePatente)";

                SqlParameter[] parametros = new SqlParameter[]
                {
            new SqlParameter("@nombreFormulario", nombreFormulario),
            new SqlParameter("@nombreBoton", nombreBoton),
            new SqlParameter("@nombrePatente", nombrePatente)
                };

                // Uso "_conexion" o "conexion", ajustalo al nombre de tu objeto en la DAL
                _conexion.ExecuteNonQuery(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la DAL al vincular el permiso con el botón: " + ex.Message);
            }
        }

        public void InsertarPatenteNueva(string nombrePermiso)
        {
            string query = "INSERT INTO Permiso (Nombre) VALUES (@nombre)";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", nombrePermiso)
            };
            _conexion.ExecuteNonQuery(query, parametros);
        }

        public void EliminarPermisoDefinitivo(int idPermiso)
        {
            string queryPerfil = "DELETE FROM Perfil_Permiso WHERE ID_Permiso = @id";
            SqlParameter[] paramPerfil = { new SqlParameter("@id", idPermiso) };
            _conexion.ExecuteNonQuery(queryPerfil, paramPerfil);

            string queryFamilia = "DELETE FROM Permiso_Familia WHERE ID_Permiso = @id";
            SqlParameter[] paramFamilia = { new SqlParameter("@id", idPermiso) };
            _conexion.ExecuteNonQuery(queryFamilia, paramFamilia);

            string queryPermiso = "DELETE FROM Permiso WHERE ID_Permiso = @id";
            SqlParameter[] paramPermiso = { new SqlParameter("@id", idPermiso) };
            _conexion.ExecuteNonQuery(queryPermiso, paramPermiso);
        }
    }
}