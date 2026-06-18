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

        public List<Perfil> ObtenerComponentesTotales()
        {
            List<Perfil> lista = new();
            lista.AddRange(ObtenerFamiliasPerfil());
            lista.AddRange(ObtenerPermisosPerfil());
            return lista;
        }

        // =======================================================
        // 3. CREAR NUEVO PERMISO (HOJA) DESDE EL MENÚ
        // =======================================================

        public void InsertarPatenteNueva(string nombrePermiso)
        {
            // Insertamos directo en la tabla maestra de Permisos
            string query = "INSERT INTO Permiso (Nombre) VALUES (@nombre)";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", nombrePermiso)
            };
            _conexion.ExecuteNonQuery(query, parametros);
        }
    }
}