using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Perfiles;
using Services.Perfiles;

namespace BLL.Perfiles
{
    public class FamiliaBLL
    {
        private PatenteDAL _patenteDAL = new(); 
        private FamiliaDAL _familiaDAL = new();

        public FamiliaBLL( ) { }

        public void AgregarFamiliaAPerfil(int idPerfil, int idFamilia)
        {
            _patenteDAL.InsertarPermisoPerfil(idPerfil, idFamilia);
        }

        public void EliminarFamiliaPerfil(int idPerfil, int idFamilia)
        {
            _patenteDAL.EliminarFamiliaPerfil(idPerfil, idFamilia);
        }

        public FamiliaServices ObtenerArbolFamiliar(int idFamiliaRaiz)
        {
            return _familiaDAL.ObtenerArbolFamiliar(idFamiliaRaiz);
        }

        public List<Componente> ObtenerFamiliasPerfil() => _patenteDAL.ObtenerFamiliasPerfil();
    }
    
}
