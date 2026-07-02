using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public record ResumenDigitoVerificador(string Tabla, string DVH, string DVV);

    public class DigitoVerificadorDAL
    {
        private const string CadenaConexion = "Data Source=.;Initial Catalog=ING;Integrated Security=True;Trust Server Certificate=True";

        public List<(string Schema, string Table)> ObtenerTablasPersistentes()
        {
            const string query = @"SELECT TABLE_SCHEMA, TABLE_NAME
                                   FROM INFORMATION_SCHEMA.TABLES
                                   WHERE TABLE_TYPE = 'BASE TABLE'
                                     AND TABLE_NAME NOT IN ('DV', 'sysdiagrams', '__EFMigrationsHistory')
                                   ORDER BY TABLE_SCHEMA, TABLE_NAME";

            DataTable dt = EjecutarConsulta(query);
            List<(string Schema, string Table)> tablas = new();

            foreach (DataRow row in dt.Rows)
            {
                tablas.Add((row["TABLE_SCHEMA"].ToString() ?? string.Empty, row["TABLE_NAME"].ToString() ?? string.Empty));
            }

            return tablas;
        }

        public DataTable ObtenerDatosTabla(string schema, string table)
        {
            string query = $"SELECT * FROM [{schema}].[{table}]";
            return EjecutarConsulta(query);
        }

        public bool ExisteTablaDV()
        {
            const string query = @"SELECT CAST(CASE WHEN OBJECT_ID(N'dbo.DV', N'U') IS NULL THEN 0 ELSE 1 END AS BIT)";
            object? resultado = EjecutarEscalar(query);
            return resultado is bool existe && existe;
        }

        public List<ResumenDigitoVerificador> ObtenerResumenPersistido()
        {
            const string query = @"SELECT NombreTabla, DVH, DVV
                                   FROM dbo.DV
                                   ORDER BY NombreTabla";

            DataTable dt = EjecutarConsulta(query);
            List<ResumenDigitoVerificador> resumen = new();

            foreach (DataRow row in dt.Rows)
            {
                resumen.Add(new ResumenDigitoVerificador(
                    row["NombreTabla"].ToString() ?? string.Empty,
                    row["DVH"].ToString() ?? string.Empty,
                    row["DVV"].ToString() ?? string.Empty));
            }

            return resumen;
        }

        public void ReemplazarResumen(IEnumerable<ResumenDigitoVerificador> resumen)
        {
            const string crearTabla = @"
IF OBJECT_ID(N'dbo.DV', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[DV](
        [NombreTabla] [nvarchar](128) NOT NULL,
        [DVH] [nvarchar](128) NOT NULL,
        [DVV] [nvarchar](128) NOT NULL,
        [FechaCalculo] [datetime2](7) NOT NULL CONSTRAINT [DF_DV_FechaCalculo] DEFAULT (SYSDATETIME()),
        CONSTRAINT [PK_DV] PRIMARY KEY CLUSTERED ([NombreTabla] ASC)
    )
END";

            using SqlConnection conexion = CrearConexion();
            conexion.Open();

            using SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                using (SqlCommand comandoCrear = new SqlCommand(crearTabla, conexion, transaccion))
                {
                    comandoCrear.CommandType = CommandType.Text;
                    comandoCrear.ExecuteNonQuery();
                }

                using (SqlCommand comandoEliminar = new SqlCommand("DELETE FROM dbo.DV", conexion, transaccion))
                {
                    comandoEliminar.CommandType = CommandType.Text;
                    comandoEliminar.ExecuteNonQuery();
                }

                foreach (ResumenDigitoVerificador item in resumen)
                {
                    using SqlCommand comandoInsertar = new SqlCommand(@"INSERT INTO dbo.DV (NombreTabla, DVH, DVV, FechaCalculo)
                                                                         VALUES (@nombreTabla, @dvh, @dvv, SYSDATETIME())", conexion, transaccion);
                    comandoInsertar.CommandType = CommandType.Text;
                    comandoInsertar.Parameters.AddWithValue("@nombreTabla", item.Tabla);
                    comandoInsertar.Parameters.AddWithValue("@dvh", item.DVH);
                    comandoInsertar.Parameters.AddWithValue("@dvv", item.DVV);
                    comandoInsertar.ExecuteNonQuery();
                }

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        private static SqlConnection CrearConexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        private static DataTable EjecutarConsulta(string query)
        {
            using SqlConnection conexion = CrearConexion();
            using SqlCommand comando = new SqlCommand(query, conexion);

            comando.CommandType = CommandType.Text;
            conexion.Open();

            using SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable resultado = new DataTable();
            adaptador.Fill(resultado);
            return resultado;
        }

        private static object? EjecutarEscalar(string query)
        {
            using SqlConnection conexion = CrearConexion();
            using SqlCommand comando = new SqlCommand(query, conexion);

            comando.CommandType = CommandType.Text;
            conexion.Open();
            return comando.ExecuteScalar();
        }
    }
}