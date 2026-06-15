using DAL.Perfiles;
using Services.Perfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Perfiles
{
    public class PatenteBLL
    {
        private PatenteDAL _patenteDAL = new();

        public void AgregarPermisoAPerfil(int idPerfil, int idPermiso)
        {
            // Validaciones de negocio
            _patenteDAL.InsertarPermisoPerfil(idPerfil, idPermiso);
        }

        public void EliminarPermisoPerfil(int idPerfil, int idPermiso)
        {
            _patenteDAL.EliminarPermisoPerfil(idPerfil, idPermiso);
        }

        // Métodos que exigen retorno a la UI
        public List<Componente> ObtenerComponentesTotales() => _patenteDAL.ObtenerComponentesTotales();
        public List<Componente> ObtenerPermisosPerfil() => _patenteDAL.ObtenerPermisosPerfil();
    }
}
