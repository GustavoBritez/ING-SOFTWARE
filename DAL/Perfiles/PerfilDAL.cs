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
    }
}
