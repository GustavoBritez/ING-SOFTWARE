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
        public void AgregarPerfilAFamilia(int idPerfil, int idFamilia, string nombrePerfil)
        {

            if (_perfilDAL.ExisteRelacionFamiliaPerfil(idPerfil, idFamilia))
            {
                throw new ArgumentException($"El perfil '{nombrePerfil}' ya existe en esta familia.");
            }

            _perfilDAL.InsertarPerfilFamilia(idPerfil, idFamilia);

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Asignar Perfil '{nombrePerfil}' a Familia";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }

        public void AgregarPermisoAPerfil(int idPerfil, int idPermiso, string nombrePermiso)
        {
            // 1. Validamos usando el nuevo método de la DAL
            if (_perfilDAL.ExisteRelacionPermisoPerfil(idPerfil, idPermiso))
            {
                // Disparamos la excepción con el texto exacto
                throw new ArgumentException($"El permiso '{nombrePermiso}' ya existe en este perfil.");
            }

            // 2. Si no existe, procedemos a vincularlo
            _perfilDAL.InsertarPermisoPerfil(idPerfil, idPermiso);

            // 3. Registro en Bitácora
            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Asignar Permiso a Perfil";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }

        public void CrearNuevoPermiso(string nombrePermiso)
        {
            if (string.IsNullOrWhiteSpace(nombrePermiso))
            {
                throw new ArgumentException("ERROR: El nombre del permiso no puede estar vacío.");
            }

            _patenteDAL.InsertarPatenteNueva(nombrePermiso);

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Creación de nuevo permiso: {nombrePermiso}";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }
        #endregion 

        #region Eliminar
        public void EliminarPerfilAFamilia(int idPerfil, int idFamilia, string nombreFamilia)
        {
            // 1. Validamos que la relación REALMENTE exista en la base de datos
            // Usamos el mismo método de la DAL, pero le ponemos un "!" adelante (que significa NO)
            if (!_perfilDAL.ExisteRelacionFamiliaPerfil(idPerfil, idFamilia))
            {
                throw new ArgumentException($"La familia '{nombreFamilia}' no se encuentra asignada a este perfil.");
            }

            // 2. Si existe, procedemos a borrarla
            _perfilDAL.EliminarPerfilAFamilia(idPerfil, idFamilia);

            // 3. Registrar evento en la bitacora
            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Eliminar Perfil de Familia";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }
        public void EliminarPerfil(int idPerfil, string nombrePerfil)
        {
            // Frenamos si hay gente usándolo
            if (_perfilDAL.PerfilTieneUsuarios(idPerfil))
            {
                throw new ArgumentException($"No se puede eliminar el perfil '{nombrePerfil}' porque hay usuarios en el sistema que lo tienen asignado. Quíteles este perfil primero.");
            }

            _perfilDAL.EliminarPerfilDefinitivo(idPerfil);

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Eliminación en cascada del Perfil: '{nombrePerfil}'";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }
        public void EliminarPermisoPerfil(int idPerfil, int idPermiso, string nombrePermiso)
        {
            // 1. Validamos que la relación REALMENTE exista en la base de datos
            if (!_perfilDAL.ExisteRelacionPermisoPerfil(idPerfil, idPermiso))
            {
                throw new ArgumentException($"El permiso '{nombrePermiso}' no se encuentra asignado a este perfil.");
            }

            // 2. Si existe, procedemos a borrarlo de la tabla puente Perfil_Permiso
            _perfilDAL.EliminarPermisoPerfil(idPerfil, idPermiso);

            // 3. Registrar evento en la bitácora
            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Desvincular Permiso '{nombrePermiso}' de Perfil";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }
        #endregion

        #region Solo lo usamos para cargar las 3 grillas
        public List<Perfil> ObtenerComponentesTotales()
        {
            return _perfilDAL.ObtenerComponentesTotales();
        }

        public List<Perfil> ObtenerFamiliasPerfil()
        {
            return _perfilDAL.ObtenerComponentesTotales()
                             .Where(c => c.EsCompuesto())
                             .ToList();
        }

        public List<Perfil> ObtenerPermisosPerfil()
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

        public void CrearNuevoPerfil(string nombrePerfil)
        {
            if (string.IsNullOrWhiteSpace(nombrePerfil))
            {
                throw new ArgumentException("ERROR: El nombre del perfil no puede estar vacío.");
            }

            _perfilDAL.InsertarPerfilNuevo(nombrePerfil);

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Creación de nuevo Perfil: {nombrePerfil}";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }

        public List<Perfil> ObtenerPerfiles()
        {
            return _perfilDAL.ObtenerPerfiles();
        }

        public FamiliaServices ObtenerArbolPerfil(int idPerfil)
        {
            return _perfilDAL.ObtenerArbolPerfil(idPerfil);
        }

    }
}