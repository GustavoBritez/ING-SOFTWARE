using Microsoft.Data.SqlClient;
using Services.Perfiles;
using System.Data;


namespace DAL.Perfiles
{
    public class FamiliaDAL
    {
        private readonly Conexion _conexion = new();

        private readonly string TABLA_COMPONENTES = "Componentes";
        private readonly string TABLA_PERFIL = "Perfil";

        public FamiliaServices ObtenerArbolFamiliar(int idFamiliaRaiz)
        {
            // 1. Buscar el nombre de la Familia Padre
            string queryPadre = "SELECT Nombre FROM Familia WHERE ID_Familia = @id";
            SqlParameter[] paramPadre = new SqlParameter[] { new SqlParameter("@id", idFamiliaRaiz) };

            DataTable dtPadre = _conexion.ExecuteReader(queryPadre, paramPadre);
            if (dtPadre.Rows.Count == 0) return null;

            string nombreFamilia = dtPadre.Rows[0]["Nombre"].ToString();
            FamiliaServices familiaArmada = new FamiliaServices(nombreFamilia) { Id = idFamiliaRaiz };

            // 2. Buscar Familias Hijas (Recursividad cruzando con Familia_Familia)
            string queryFamHijas = @"
        SELECT f.ID_Familia, f.Nombre 
        FROM Familia_Familia ff
        INNER JOIN Familia f ON ff.ID_FamiliaHija = f.ID_Familia
        WHERE ff.ID_FamiliaPadre = @idPadre";

            SqlParameter[] paramFam = new SqlParameter[] { new SqlParameter("@idPadre", idFamiliaRaiz) };
            DataTable dtFamHijas = _conexion.ExecuteReader(queryFamHijas, paramFam);

            foreach (DataRow fila in dtFamHijas.Rows)
            {
                int idHijo = Convert.ToInt32(fila["ID_Familia"]);

                // ¡Magia recursiva! Llamamos al mismo método para que arme las ramas de adentro
                FamiliaServices subFamilia = ObtenerArbolFamiliar(idHijo);

                if (subFamilia is not null)
                    familiaArmada.Agregar(subFamilia);
            }

            // 3. Buscar Permisos Hijos (Hojas cruzando con Permiso_Familia)
            string queryPermisos = @"
        SELECT p.ID_Permiso, p.Nombre 
        FROM Permiso_Familia pf
        INNER JOIN Permiso p ON pf.ID_Permiso = p.ID_Permiso
        WHERE pf.ID_Familia = @idPadre";

            SqlParameter[] paramPerm = new SqlParameter[] { new SqlParameter("@idPadre", idFamiliaRaiz) };
            DataTable dtPermisos = _conexion.ExecuteReader(queryPermisos, paramPerm);

            foreach (DataRow fila in dtPermisos.Rows)
            {
                int idHijo = Convert.ToInt32(fila["ID_Permiso"]);
                string nombreHijo = fila["Nombre"].ToString();

                PatenteServices permisoHoja = new PatenteServices(nombreHijo) { Id = idHijo };
                familiaArmada.Agregar(permisoHoja);
            }

            return familiaArmada;
        }

        public void InsertarFamiliaNueva(string nombreFamilia)
        {
            string query = "INSERT INTO Familia (Nombre) VALUES (@nombre)";
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@nombre", nombreFamilia)
            };

            _conexion.ExecuteNonQuery(query, parametros);
        }

        public List<string> ObtenerPerfilesDeFamilia(int idFamilia)
        {
            List<string> nombresPerfiles = new List<string>();

            string query = @"
                SELECT p.Nombre 
                FROM Familia_Perfil fp
                INNER JOIN Perfil p ON fp.ID_Perfil = p.ID_Perfil
                WHERE fp.ID_Familia = @idFamilia";

            SqlParameter[] parametros = {
                     new SqlParameter("@idFamilia", idFamilia)
            };

            DataTable dt = _conexion.ExecuteReader(query, parametros);

            foreach (DataRow fila in dt.Rows)
            {
                nombresPerfiles.Add(fila["Nombre"].ToString());
            }

            return nombresPerfiles;
        }

        public bool ExisteRelacionPermisoFamilia(int idFamilia, int idPermiso)
        {
            string query = "SELECT COUNT(1) FROM Permiso_Familia WHERE ID_Familia = @idFam AND ID_Permiso = @idPerm";
            SqlParameter[] param = {
                new SqlParameter("@idFam", idFamilia),
                new SqlParameter("@idPerm", idPermiso)
            };
            DataTable dt = _conexion.ExecuteReader(query, param);
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        public bool ExisteFamiliaPorNombre(string nombreFamilia)
        {
            // Buscamos si hay coincidencias exactas en la tabla Familia
            string query = "SELECT COUNT(1) FROM Familia WHERE Nombre = @nombre";

            SqlParameter[] param = {
                new SqlParameter("@nombre", nombreFamilia)
            };

            DataTable dt = _conexion.ExecuteReader(query, param);

            // Validación defensiva para evitar el error de posición 0
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]) > 0;
            }

            return false;
        }

        public void InsertarPermisoFamilia(int idFamilia, int idPermiso)
        {
            string query = "INSERT INTO Permiso_Familia (ID_Familia, ID_Permiso) VALUES (@idFam, @idPerm)";
            SqlParameter[] param = {
                new SqlParameter("@idFam", idFamilia),
                new SqlParameter("@idPerm", idPermiso)
            };
            _conexion.ExecuteNonQuery(query, param);
        }

        public void EliminarPermisoFamilia(int idFamilia, int idPermiso)
        {
            // Borramos de la tabla puente específica de las Familias
            string query = "DELETE FROM Permiso_Familia WHERE ID_Familia = @idFam AND ID_Permiso = @idPerm";

            SqlParameter[] param = {
                new SqlParameter("@idFam", idFamilia),
                new SqlParameter("@idPerm", idPermiso)
            };

            _conexion.ExecuteNonQuery(query, param);
        }

        public void EliminarFamilia(int idFamilia)
        {
            string queryPerfiles = "DELETE FROM Familia_Perfil WHERE ID_Familia = @id";
            SqlParameter[] paramPerfiles = { new SqlParameter("@id", idFamilia) };
            _conexion.ExecuteNonQuery(queryPerfiles, paramPerfiles);

            string queryPermisos = "DELETE FROM Permiso_Familia WHERE ID_Familia = @id";
            SqlParameter[] paramPermisos = { new SqlParameter("@id", idFamilia) };
            _conexion.ExecuteNonQuery(queryPermisos, paramPermisos);

            string queryFamilia = "DELETE FROM Familia WHERE ID_Familia = @id";
            SqlParameter[] paramFamilia = { new SqlParameter("@id", idFamilia) };
            _conexion.ExecuteNonQuery(queryFamilia, paramFamilia);
        }
    }
}
