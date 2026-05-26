using BE;
using DAL;

namespace BLL
{
    public class BitacoraBLL
    {

        private BitacoraDAL _bitacoraDAL;

        public List<BitacoraBE> BuscarEventos(DateTime desde, DateTime hasta)
        {

        }

        public void RegistrarEvento(BitacoraBE newBitacora)
        {
            GuardarBitacora(newBitacora);
        }

        private List<BitacoraBE> VerEventos()
        {
           
        }
    }
}