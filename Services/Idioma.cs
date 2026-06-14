using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Idioma
    {
        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public string ArchivoJson { get; set; }

        public Idioma()
        {
        }

        public Idioma(string nombre, string codigo, string archivoJson)
        {
            Nombre = nombre;
            Codigo = codigo;
            ArchivoJson = archivoJson;
        }
    }
}
