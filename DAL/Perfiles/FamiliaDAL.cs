using Microsoft.Data.SqlClient;
using Services.Perfiles;
using System.Data;


namespace DAL.Perfiles
{
    public class FamiliaDAL
    {
        private readonly Conexion _conexion = new();

        // Nombres de tus tablas para no errarle
        private readonly string TABLA_COMPONENTES = "Componentes";
        private readonly string TABLA_PERFIL = "Perfil";

        public FamiliaServices ObtenerArbolFamiliar(int idFamiliaRaiz)
        {
            // 1. Buscamos el nombre del rol en la tabla maestra para solucionar el error del constructor
            string queryPadre = $"SELECT Nombre FROM {TABLA_COMPONENTES} WHERE IdComponente = @id";
            SqlParameter[] paramPadre = new SqlParameter[] { new SqlParameter("@id", idFamiliaRaiz) };

            DataTable dtPadre = _conexion.ExecuteReader(queryPadre, paramPadre);

            // Si no encuentra nada en la BD, devolvemos null de forma segura
            if (dtPadre.Rows.Count == 0) return null;

            string nombreFamilia = dtPadre.Rows[0]["Nombre"].ToString();

            // ¡Acá le pasamos el argumento "nombre" requerido para que no tire más error!
            FamiliaServices familiaArmada = new FamiliaServices(nombreFamilia) { Id = idFamiliaRaiz };

            // 2. Buscamos quiénes son los hijos directos de este padre en la tabla relacional (Perfil)
            string queryHijos = $@"
                SELECT c.IdComponente, c.Nombre, c.EsFamilia 
                FROM {TABLA_PERFIL} pc
                INNER JOIN {TABLA_COMPONENTES} c ON pc.IdPermiso = c.IdComponente
                WHERE pc.IdPerfil = @idPadre";

            SqlParameter[] paramHijos = new SqlParameter[] { new SqlParameter("@idPadre", idFamiliaRaiz) };
            DataTable dtHijos = _conexion.ExecuteReader(queryHijos, paramHijos);

            // 3. Recorremos los hijos y aplicamos RECURSIVIDAD
            foreach (DataRow fila in dtHijos.Rows)
            {
                int idHijo = Convert.ToInt32(fila["IdComponente"]);
                string nombreHijo = fila["Nombre"].ToString();
                bool esFamilia = Convert.ToBoolean(fila["EsFamilia"]);

                if (esFamilia)
                {
                    // ¡RECURSIVIDAD COMPOSITE!
                    // Si el hijo es otra familia, este método se llama a sí mismo para traer su sub-árbol
                    FamiliaServices subFamilia = ObtenerArbolFamiliar(idHijo);

                    if (subFamilia is not null)
                        familiaArmada.Agregar(subFamilia);
                }
                else
                {
                    // Si es una hoja/patente, instanciamos PatenteServices pasándole su nombre obligatorio
                    PatenteServices permisoHoja = new PatenteServices(nombreHijo) { Id = idHijo };
                    familiaArmada.Agregar(permisoHoja);
                }
            }

            // 4. Devolvemos el árbol estructurado
            return familiaArmada;
        }
    }
}
