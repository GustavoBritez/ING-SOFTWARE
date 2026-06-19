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
    }
}
