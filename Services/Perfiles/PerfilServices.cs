using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Perfiles
{
    public class PerfilServices : Componente
    {
        // Privada para su manipulacion
        private List<Permiso> Componentes;

        // publica para su consulta
        public IReadOnlyList<Permiso> _Componentes => Componentes.AsReadOnly();

        public PerfilServices( string nombre )
        {
            this.Componentes = new List<Permiso>();
            this.Nombre = nombre;
        }

        #region "Metodos ABM"
        public void AgregarComponente( Permiso newPermiso )
        {
            if (!Componentes.Contains(newPermiso))
                this.Componentes.Add(newPermiso);
        }

        public void EliminarComponente( Permiso eliminarPermiso )
        {
            if (Componentes.Contains(eliminarPermiso))
                this.Componentes.Remove(eliminarPermiso);
        }
        #endregion
        public override bool EsCompuesto()
        {
            return true;
        }
    }
}
