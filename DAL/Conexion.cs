using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer;
namespace DAL
{
    internal class Conexion : IDisposable
    {

        private const string _cadenaConexion = "Data Source=.;Initial Catalog=TestHash;Integrated Security=True;Trust Server Certificate=True";
        private const int time= 30;
        private SqlConnection conexion;
        private SqlTransaction transaction;

        /// <summary>
        /// Constructor - Inicializamos la conexion
        /// </summary>

        public Conexion()
        {
            conexion = new SqlConnection(_cadenaConexion);
            transaction = null;
        }

        public bool AbrirConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                    Console.WriteLine("Conexion Abierta Exitosamente");
                    return true;
                }
                return true;
            }
            catch ( SqlException ex)
            {
                throw new Exception($"Error al abrir la conexión: {ex.Message}");
            }
            
        }

        /// <summary>
        /// Cierra la conexion a la base de datos
        /// </summary
    
        public bool CerrarConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                    Console.WriteLine("Conexion Cerrada Exitosamente");
                    return true;
                }
                return true;
            }
            catch( SqlException ex )
            {
                Console.Write($"Error al cerrar la conexion {ex.Message}");
                return false;
            }
        }

        public DataTable Leer(string nombreSP, params SqlParameter[] parametros)
        {
            DataTable dtResultados = new DataTable();

            try
            {
                AbrirConexion();
                using (SqlCommand comando = new SqlCommand(nombreSP, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandTimeout = time;

                    if (parametros != null && parametros.Length > 0)
                    {
                        comando.Parameters.AddRange(parametros);
                    }
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(dtResultados);
                    }
                }
                Console.WriteLine($"Procedimiento almacenado '{nombreSP}' ejecutado exitosamente.");
                return dtResultados;

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
                return dtResultados;
            }
            finally
            {
                CerrarConexion();
            }
        }

        public bool Escribir(string nombreSP, params SqlParameter[] parametros)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand comando = new SqlCommand(nombreSP, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandTimeout = time;
                    if (parametros != null && parametros.Length > 0)
                    {
                        comando.Parameters.AddRange(parametros);
                    }

                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine($"Escritura Exitosa");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"No se afectaron filas, verifique los datos ingresados.");
                        return false;
                    }
                }
            }
            catch( SqlException ex )
            {
                Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
                return false;
            }
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
            if (conexion != null)
            {
                conexion.Dispose();
                conexion = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}
