using Microsoft.Data.SqlClient;
using Services;
using Services.Perfiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Perfiles
{
    public class PerfilDAL
    {
        private readonly Conexion _conexion;
        private readonly string TABLA_PERFIL = "Perfil";
        private readonly string TABLA_COMPONENTES = "Componentes";
        public PerfilDAL()
        {
            this._conexion = new();
        }

        // Los métodos reciben los parámetros necesarios para armar la query
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

        #region VER PARA CREAR DER 
        /*
        Para que el polimorfismo que armamos en C# funcione perfecto, tus tablas en SQL Server tienen que tener exactamente estas columnas:

TABLA_COMPONENTES(La tabla maestra donde vive todo)

IdComponente(INT, PRIMARY KEY, IDENTITY) -> El número único.

Nombre(VARCHAR(100), NOT NULL) -> Ej: "Nutricionista" o "Crear_Dieta".

EsFamilia(BIT, NOT NULL) -> 1 si es un Perfil/Familia(tiene hijos). 0 si es un Permiso suelto(hoja).

TABLA_PERFIL(La tabla relacional o de jerarquía)
Aunque en tu código la llames TABLA_PERFIL, su nombre técnico en el DER suele ser Perfil_Componente porque une un Padre con un Hijo.

IdPerfil(INT, FOREIGN KEY) -> Apunta al IdComponente del Padre.

IdPermiso(INT, FOREIGN KEY) -> Apunta al IdComponente del Hijo(que puede ser una hoja u otra familia).

(Opcional pero recomendado): Una PRIMARY KEY compuesta por(IdPerfil, IdPermiso) para que no se puedan guardar permisos duplicados en el mismo perfil por accidente.

        */

        #endregion


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

            string query = $@"SELECT * FROM {TABLA_COMPONENTES}";

            DataTable dt = _conexion.ExecuteReader(query,null);

            foreach ( DataRow fila in dt.Rows )
            {
                int id = Convert.ToInt32(fila["IdComponente"]);
                string nombre = fila["Nombre"].ToString();
                bool esFamilia = Convert.ToBoolean(fila["EsFamilia"]);

                Componente comp;

                if (esFamilia)
                {
                    comp = new PerfilServices(nombre) { Id = id }; // Permiso
                }
                else
                {
                    comp = new Permiso(nombre) { Id = id }; // Hoja
                }

                if (comp is not null )
                    _perfil.Add(comp);
            }

            return _perfil;
        }


        public FamiliaServices ObtenerFamiliaPorId(int idFamiliaRaiz)
        {
            // 1. Buscamos los datos básicos de la familia raíz
            string queryPadre = $"SELECT IdComponente, Nombre FROM {TABLA_COMPONENTES} WHERE IdComponente = @id";
            DataTable dtPadre = _conexion.ExecuteReader(queryPadre, new SqlParameter[] { new SqlParameter("@id", idFamiliaRaiz) });

            if (dtPadre.Rows.Count == 0) return null; // No existe

            // Instanciamos el nodo principal
            FamiliaServices familiaArmada = new FamiliaServices(dtPadre.Rows[0]["Nombre"].ToString()) { Id = idFamiliaRaiz };

            // 2. Buscamos quiénes son los hijos de este padre en la tabla relacional
            string queryHijos = $@"
        SELECT c.IdComponente, c.Nombre, c.EsFamilia 
        FROM {TABLA_PERFIL} pc
        INNER JOIN {TABLA_COMPONENTES} c ON pc.IdPermiso = c.IdComponente
        WHERE pc.IdPerfil = @idPadre";

            DataTable dtHijos = _conexion.ExecuteReader(queryHijos, new SqlParameter[] { new SqlParameter("@idPadre", idFamiliaRaiz) });

            // 3. Recorremos los hijos encontrados
            foreach (DataRow fila in dtHijos.Rows)
            {
                int idHijo = Convert.ToInt32(fila["IdComponente"]);
                string nombreHijo = fila["Nombre"].ToString();
                bool esFamilia = Convert.ToBoolean(fila["EsFamilia"]);

                if (esFamilia)
                {
                    // ¡ACÁ ESTÁ LA MAGIA RECURSIVA! 
                    // Si el hijo es OTRA familia, el método se llama a sí mismo para armar el sub-árbol
                    FamiliaServices subFamilia = ObtenerFamiliaPorId(idHijo);
                    familiaArmada.Agregar(subFamilia);
                }
                else
                {
                    // Si es una hoja (Permiso), simplemente la instanciamos y la agregamos a la lista del padre
                    Permiso permisoHoja = new Permiso(nombreHijo) { Id = idHijo };
                    familiaArmada.Agregar(permisoHoja);
                }
            }

            // 4. Retornamos el árbol completo y ensamblado
            return familiaArmada;
        }


    }
}
