using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Perfiles
{
    public class PatenteServices : Componente
    {
        public PatenteServices(string nombre) : base(nombre)
        {
        }

        public override bool EsCompuesto() => false;
    }
}
