using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Perfiles
{
    public class Permiso : Componente
    {
        public Permiso( string nombre )
        {
            this.Nombre = nombre;
        }   

        public override bool EsCompuesto()
        {
            // El permiso es una HOJA no tiene Hijos
            return false;
        }
    }
}
