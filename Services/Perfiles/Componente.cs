using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Perfiles
{
    public abstract class Componente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        protected Componente(string nombre)
        {
            this.Nombre = nombre;
        }

        public abstract bool EsCompuesto();
    }
}
