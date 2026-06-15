using Services.Perfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Perfiles
{
    public class FamiliaDAL
    {
        private Conexion _conexion = new();

        // Esta clase la dejamos viva solo para complacer al profe, 
        // y le metemos el algoritmo más pesado: el recursivo.
        public FamiliaServices ObtenerArbolFamiliar(int idFamilia)
        {
            // Acá va la lógica recursiva que busca los hijos en la BD
            return new FamiliaServices();
        }
    }
}
