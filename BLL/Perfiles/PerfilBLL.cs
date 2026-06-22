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
        public void AgregarFamiliaAlPerfil(int idPerfil, int idFamilia, string nombrePerfil)
        {

            if (_perfilDAL.ExisteRelacionFamiliaPerfil(idPerfil, idFamilia))
            {
                throw new ArgumentException($"La familia'{nombrePerfil}' ya existe en este Perfil");
            }
            _perfilDAL.InsertarFamiliaAlPerfi(idPerfil, idFamilia);

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Asignar Perfil a Familia";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }

        public void AgregarPermisoAFamilia(int idPerfil, int idPermiso, string nombrePermiso)
        {
            if (_perfilDAL.ExisteRelacionPermisoPerfil(idPerfil, idPermiso))
            {
                throw new ArgumentException($"El permiso '{nombrePermiso}' ya existe en esta familia.");
            }

            _perfilDAL.InsertarPermisoAFamilia(idPerfil, idPermiso);

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Asignar Permiso a Perfil";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }
        #endregion 

        #region Eliminar
        
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
            string descripcion = $"Eliminacion Perfil";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }

        public void EliminarFamiliaDePerfil(int idPerfil, int idFamilia, string nombrePerfil, string nombreFamilia)
        {
            if (!_perfilDAL.ExisteRelacionFamiliaPerfil(idPerfil, idFamilia))
            {
                throw new ArgumentException($"La familia '{nombreFamilia}' no está asignada directamente al perfil '{nombrePerfil}'. \n\nEs probable que la esté heredando a través de otra familia contenedora (como se ve en el árbol). Para quitarla, debe desvincular la familia principal.");
            }

            _perfilDAL.EliminarFamiliaDePerfil(idPerfil, idFamilia);

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Desvincular Familia '{nombreFamilia}' del Perfil '{nombrePerfil}'";
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

        public void CrearNuevoPerfil(string nombrePerfil)
        {
            if (string.IsNullOrWhiteSpace(nombrePerfil))
            {
                throw new ArgumentException("El nombre del perfil no puede estar vacío.");
            }

            if (_perfilDAL.ExistePerfilPorNombre(nombrePerfil))
            {
                throw new ArgumentException($"Ya existe un perfil registrado con el nombre '{nombrePerfil}'. Por favor, elija un nombre diferente.");
            }

            _perfilDAL.InsertarPerfilNuevo(nombrePerfil);

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Creación de nuevo Perfil: '{nombrePerfil}'";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }

        public int ObtenerIdPerfilPorNombre(string nombreRol)
        {
            // Si el texto viene vacío por algún motivo, devolvemos 0 o tiramos error
            if (string.IsNullOrWhiteSpace(nombreRol))
            {
                throw new ArgumentException("El nombre del rol no puede estar vacío.");
            }

            // Llamamos a la DAL para que haga el trabajo sucio
            return _perfilDAL.ObtenerIdPerfilPorNombre(nombreRol);
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