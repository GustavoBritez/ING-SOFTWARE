using Microsoft.Data.SqlClient;
using Services.Perfiles;
using System.Data;

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
                    // Instanciamos el Nodo (Rol)
                    comp = new FamiliaServices(nombre) { Id = id };
                }
                else
                {
                    // Instanciamos la Hoja (Acción suelta)
                    comp = new PatenteServices(nombre) { Id = id };
                }

                if (comp is not null )
                    _perfil.Add(comp);
            }
            return _perfil;
        }

        public void InsertarPatenteNueva(string nombrePermiso)
        {
            string query = "INSERT INTO Componentes (Nombre, EsFamilia) VALUES (@nombre, 0)";

            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@nombre", nombrePermiso)
            };

            _conexion.ExecuteNonQuery(query, parametros);
        }

    }
}
