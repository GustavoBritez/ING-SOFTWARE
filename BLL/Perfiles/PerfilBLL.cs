using DAL;
using DAL.Perfiles;
using Services.Perfiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class PerfilBLL
    {
        private readonly PerfilDAL _perfilDAL = new();


        public void AgregarFamiliaAPerfil(int idPerfil, int idFamilia)
        {
            if (TienePermisoDuplicado(idPerfil, idFamilia))
            {
                throw new ArgumentException("ERROR: El perfil ya tiene esta familia asignada.");
            }

            _perfilDAL.InsertarPermisoPerfil(idPerfil, idFamilia);
        }

        public void AgregarPermisoAPerfil(int idPerfil, int idPermiso)
        {
            if (TienePermisoDuplicado(idPerfil, idPermiso))
            {
                throw new ArgumentException("ERROR: El perfil ya tiene el permiso asignado.");
            }

            _perfilDAL.InsertarPermisoPerfil(idPerfil, idPermiso);
        }

        public void EliminarFamiliaPerfil(int idPerfil, int idFamilia)
        {
            _perfilDAL.EliminarFamiliaPerfil(idPerfil, idFamilia);
        }

        public void EliminarPermisoPerfil(int idPerfil, int idPermiso)
        {
            _perfilDAL.EliminarPermisoPerfil(idPerfil, idPermiso);
        }

        public List<Componente> ObtenerComponentesTotales()
        {
            return _perfilDAL.ObtenerComponentesTotales();
        }

        public List<Componente> ObtenerFamiliasPerfil()
        {
            return _perfilDAL.ObtenerComponentesTotales()
                             .Where(c => c.EsCompuesto())
                             .ToList();
        }

        public List<Componente> ObtenerPermisosPerfil()
        {
            return _perfilDAL.ObtenerComponentesTotales()
                             .Where(c => !c.EsCompuesto())
                             .ToList();
        }



        public bool TienePermisoDuplicado(int idPerfil, int idPermiso)
        {
            return false;
        }

        public void ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("ERROR: El nombre no puede estar vacío.");
        }


        public FamiliaServices ObtenerArbolCompletoDeUsuario(int idFamiliaDelUsuario)
        {
            if (idFamiliaDelUsuario <= 0)
                throw new ArgumentException("ID de perfil inválido.");

            return _perfilDAL.ObtenerFamiliaPorId(idFamiliaDelUsuario);
        }
    }
}