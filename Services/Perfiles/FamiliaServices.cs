using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Perfiles
{
    public class FamiliaServices : Componente
    {
        private List<Componente> _hijos = new();
        public IReadOnlyList<Componente> Hijos => _hijos.AsReadOnly();

        public FamiliaServices(string nombre) : base(nombre)
        {
        }

        public void Agregar(Componente c) => _hijos.Add(c);

        public override bool EsCompuesto() => true;

    }
}
