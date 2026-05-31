using BE;
using System;
using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class BitacoraBLL
    {
        private BitacoraDAL _bitacoraDAL;

        public BitacoraBLL()
        {
            _bitacoraDAL = new BitacoraDAL();
        }

        public List<BitacoraBE> BuscarEventos(DateTime desde, DateTime hasta)
        {
            try
            {
                return _bitacoraDAL.FiltrarBitacora(desde, hasta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar eventos: {ex.Message}");
                throw;
            }
        }

        public List<BitacoraBE> VerEventos()
        {
            try
            {
                return _bitacoraDAL.ObtenerBitacora();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener eventos: {ex.Message}");
                throw;
            }
        }
        public bool RegistrarEvento(int criticidad, string descripcion, int dni, string modulo)
        {
            try
            {
                BitacoraBE evento = new BitacoraBE(criticidad, descripcion, dni, DateTime.Now, modulo);
                _bitacoraDAL.GuardarBitacora(evento);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar evento en bitácora: {ex.Message}");
                return false;
            }
        }
    }
}