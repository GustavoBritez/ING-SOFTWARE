using DAL;
using DAL.Perfiles;
using Services;
using Services.Perfiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class PerfilBLL
    {
        private readonly PerfilDAL _perfilDAL = new();
        private readonly PatenteDAL _patenteDAL = new();
        #region Agregar
        public void AgregarFamiliaAPerfil(int idPerfil, int idFamilia)
        {
            if (TienePermisoDuplicado(idPerfil, idFamilia))
            {
                throw new ArgumentException("ERROR: El perfil ya tiene esta familia asignada.");
            }

            _perfilDAL.InsertarPermisoPerfil(idPerfil, idFamilia);


            /// Registrar evento en la bitacora
            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Asignar Perfil a Familia";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }
        
        public void AgregarPermisoAPerfil(int idPerfil, int idPermiso)
        {
            if (TienePermisoDuplicado(idPerfil, idPermiso))
            {
                throw new ArgumentException("ERROR: El perfil ya tiene el permiso asignado.");
            }

            _perfilDAL.InsertarPermisoPerfil(idPerfil, idPermiso);

            /// Registrar evento en la bitacora
            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Asignar Permiso a Perfil";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }
        // Adentro de BLL.Perfiles.PatenteBLL
        public void CrearNuevoPermiso(string nombrePermiso)
        {
            if (string.IsNullOrWhiteSpace(nombrePermiso))
            {
                throw new ArgumentException("ERROR: El nombre del permiso no puede estar vacío.");
            }

            _patenteDAL.InsertarPatenteNueva(nombrePermiso);

            // 3. Registramos en tu Bitácora (¡Excelente práctica!)
            // Ojo: Asegurate de tener los using necesarios acá arriba (using BLL; etc)
            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Creación de nuevo permiso: {nombrePermiso}";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }
        #endregion 

        #region Eliminar
        public void EliminarFamiliaPerfil(int idPerfil, int idFamilia)
        {
            _perfilDAL.EliminarFamiliaPerfil(idPerfil, idFamilia);

            /// Registrar evento en la bitacora
            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Eliminar Perfil a Familia";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }

        public void EliminarPermisoPerfil(int idPerfil, int idPermiso)
        {
            _perfilDAL.EliminarPermisoPerfil(idPerfil, idPermiso);

            /// Registrar evento en la bitacora
            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Eliminar Permiso a Perfil";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }
        #endregion

        #region Solo lo usamos para cargar las 3 grillas
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
        #endregion

        private bool TienePermisoDuplicado(int idPerfil, int idPermiso)
        {
            return false;
        }

    }
}