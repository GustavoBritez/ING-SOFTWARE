using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class IdiomaBLL
    {
        private IdiomaDAL idiomaDAL= new IdiomaDAL();
        public List<Idioma> ObtenerIdiomas()=> idiomaDAL.ObtenerIdiomas();
        public string Traducir(string clave)=>idiomaDAL.Traducir(clave);

    }
}
