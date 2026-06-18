using Microsoft.Data.SqlClient;
using Services.Perfiles;
using System.Data;

namespace DAL.Perfiles
{
    public class PerfilDAL
    {
        private readonly Conexion _conexion;
        private readonly string TABLA_PERFIL = "Perfil";

        public PerfilDAL()
        {
            this._conexion = new();
        }

        public void InsertarPerfilNuevo(string nombrePerfil)
        {
            string query = "INSERT INTO Perfil (Nombre) VALUES (@nombre)";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", nombrePerfil)
            };

            _conexion.ExecuteNonQuery(query, parametros);
        }

        public void InsertarPermisoPerfil(int idPerfil, int idPermiso)
        {
            string query = $"INSERT INTO {TABLA_PERFIL} (IdPerfil, IdPermiso) " +
                            "VALUES (@idPerfil, @idPermiso)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idPerfil", idPerfil),
                new SqlParameter("@idPermiso", idPermiso)
            };

            _conexion.ExecuteNonQuery(query, parametros);
        }

        public void EliminarPermisoPerfil(int idPerfil, int idPermiso)
        {
            string query = $"DELETE FROM {TABLA_PERFIL} WHERE IdPerfil = @idPerfil AND IdPermiso = @idPermiso";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idPerfil", idPerfil),
                new SqlParameter("@idPermiso", idPermiso)
            };
            _conexion.ExecuteNonQuery(query, parametros);
        }

        public void EliminarFamiliaPerfil(int idPerfil, int idFamilia)
        {
            string query = $"DELETE FROM {TABLA_PERFIL} WHERE IdPerfil = @idPerfil AND IdPermiso = @idFamilia";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idPerfil", idPerfil),
                new SqlParameter("@idFamilia", idFamilia)
            };
            _conexion.ExecuteNonQuery(query, parametros);
        }

        public List<Componente> ObtenerComponentesTotales()
        {
            List<Componente> _perfil = new();

            string queryFamilias = "SELECT ID_Familia as Id, Nombre FROM Familia";
            DataTable dtFamilias = _conexion.ExecuteReader(queryFamilias, null);

            foreach (DataRow fila in dtFamilias.Rows)
            {
                int id = Convert.ToInt32(fila["Id"]);
                string nombre = fila["Nombre"].ToString();
                _perfil.Add(new FamiliaServices(nombre) { Id = id });
            }

            string queryPermisos = "SELECT ID_Permiso as Id, Nombre FROM Permiso";
            DataTable dtPermisos = _conexion.ExecuteReader(queryPermisos, null);

            foreach (DataRow fila in dtPermisos.Rows)
            {
                int id = Convert.ToInt32(fila["Id"]);
                string nombre = fila["Nombre"].ToString();
                _perfil.Add(new PatenteServices(nombre) { Id = id });
            }

            return _perfil;
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

        public List<Componente> ObtenerPerfiles()
        {
            List<Componente> lista = new List<Componente>();
            string query = "SELECT ID_Perfil, Nombre FROM Perfil";
            DataTable dt = _conexion.ExecuteReader(query, null);

            foreach (DataRow fila in dt.Rows)
            {
                int id = Convert.ToInt32(fila["ID_Perfil"]);
                string nombre = fila["Nombre"].ToString();

                lista.Add(new FamiliaServices(nombre) { Id = id });
            }
            return lista;
        }

        public FamiliaServices ObtenerArbolPerfil(int idPerfil)
        {
            string queryPadre = "SELECT Nombre FROM Perfil WHERE ID_Perfil = @id";
            SqlParameter[] paramPadre = { new SqlParameter("@id", idPerfil) };
            DataTable dtPadre = _conexion.ExecuteReader(queryPadre, paramPadre);
            if (dtPadre.Rows.Count == 0) return null;

            string nombrePerfil = dtPadre.Rows[0]["Nombre"].ToString();
            FamiliaServices perfilArmado = new FamiliaServices(nombrePerfil) { Id = idPerfil };

            string queryFamilias = @"
                    SELECT f.ID_Familia, f.Nombre 
                    FROM Familia_Perfil fp
                    INNER JOIN Familia f ON fp.ID_Familia = f.ID_Familia
                    WHERE fp.ID_Perfil = @idPerfil";

            SqlParameter[] paramFam = { new SqlParameter("@idPerfil", idPerfil) };
            DataTable dtFam = _conexion.ExecuteReader(queryFamilias, paramFam);

            FamiliaDAL familiaDAL = new FamiliaDAL();

            foreach (DataRow fila in dtFam.Rows)
            {
                int idFamilia = Convert.ToInt32(fila["ID_Familia"]);
                FamiliaServices subFamilia = familiaDAL.ObtenerArbolFamiliar(idFamilia);
                if (subFamilia != null)
                    perfilArmado.Agregar(subFamilia);
            }

            // 3. Buscar Permisos directos de este Perfil (via Perfil_Permiso)
            string queryPermisos = @"
                SELECT p.ID_Permiso, p.Nombre 
                FROM Perfil_Permiso pp
                INNER JOIN Permiso p ON pp.ID_Permiso = p.ID_Permiso
                WHERE pp.ID_Perfil = @idPerfil";

            // ⚠️ LA SOLUCIÓN: Creamos un parámetro NUEVO y limpio, en vez de reusar el anterior
            SqlParameter[] paramPerm = { new SqlParameter("@idPerfil", idPerfil) };

            // Le pasamos el parámetro nuevo a la ejecución
            DataTable dtPerm = _conexion.ExecuteReader(queryPermisos, paramPerm);

            foreach (DataRow fila in dtPerm.Rows)
            {
                int idPermiso = Convert.ToInt32(fila["ID_Permiso"]);
                string nombrePermiso = fila["Nombre"].ToString();
                perfilArmado.Agregar(new PatenteServices(nombrePermiso) { Id = idPermiso });
            }

            return perfilArmado;
        }
    }
}
