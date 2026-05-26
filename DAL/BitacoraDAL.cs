namespace DAL
{
    using BE;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;

    public class BitacoraDAL
    {
        private readonly Conexion conexion = new();
        private const string TABLA_BITACORA = "Bitacora"; /// Nombre de la TABLA Bitacora en la BD - SQL Server 2019 NO PROBE EN 2020
        private const string TABLA_USUARIOS = "Usuarios"; /// Nombre de la TABLA usuarios en la BD - SQL Server 2019 NO PROBE EN 2020

        public List<BitacoraBE> FiltrarBitacora(DateTime desde, DateTime hasta)
        {
            List<BitacoraBE> eventos = new List<BitacoraBE>();

            try
            {
                string query = $@"SELECT Criticidad, Descripcion, DNI, Fecha, Id_Evento, Modulo
                                  FROM {TABLA_BITACORA}
                                  WHERE Fecha BETWEEN @desde AND @hasta
                                  ORDER BY Fecha DESC";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@desde", desde),
                    new SqlParameter("@hasta", hasta)
                };

                var reader = conexion.ExecuteReader(query, parametros);

                while (reader.Read())
                {
                    eventos.Add(new BitacoraBE(
                        criticidad: (int)reader["Criticidad"],
                        descripcion: reader["Descripcion"].ToString(),
                        dni: (int)reader["DNI"],
                        fecha: (DateTime)reader["Fecha"],
                        id_Evento: (int)reader["Id_Evento"],
                        modulo: reader["Modulo"].ToString()
                    ));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener eventos por fecha: {ex.Message}");
                throw;
            }

            return eventos;
        }

        public void GuardarBitacora(BitacoraBE newBitacora)
        {
            try
            {
                string query = $@"INSERT INTO {TABLA_BITACORA} (Criticidad, Descripcion, DNI, Fecha, Id_Evento, Modulo)
                                  VALUES (@criticidad, @descripcion, @dni, @fecha, @idEvento, @modulo)";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@criticidad", newBitacora._Criticidad),
                    new SqlParameter("@descripcion", newBitacora._Descripcion),
                    new SqlParameter("@dni", newBitacora._Dni),
                    new SqlParameter("@fecha", newBitacora._Fecha),
                    new SqlParameter("@idEvento", newBitacora._Id_Evento),
                    new SqlParameter("@modulo", newBitacora._Modulo)
                };

                conexion.ExecuteNonQuery(query, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar evento en bitacora: {ex.Message}");
                throw;
            }
        }

        public List<BitacoraBE> ObtenerBitacora()
        {
            List<BitacoraBE> eventos = new List<BitacoraBE>();

            try
            {
                string query = $@"SELECT Criticidad, Descripcion, DNI, Fecha, Id_Evento, Modulo
                                  FROM {TABLA_BITACORA}
                                  ORDER BY Fecha DESC";

                var reader = conexion.ExecuteReader(query, null);

                while (reader.Read())
                {
                    eventos.Add(new BitacoraBE(
                        criticidad: (int)reader["Criticidad"],
                        descripcion: reader["Descripcion"].ToString(),
                        dni: (int)reader["DNI"],
                        fecha: (DateTime)reader["Fecha"],
                        id_Evento: (int)reader["Id_Evento"],
                        modulo: reader["Modulo"].ToString()
                    ));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los eventos: {ex.Message}");
                throw;
            }

            return eventos;
        }
    }
}