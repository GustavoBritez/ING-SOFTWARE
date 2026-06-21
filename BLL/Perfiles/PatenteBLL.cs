using DAL.Perfiles;
using Services;
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
            _patenteDAL.InsertarPermisoPerfil(idPerfil, idPermiso);
        }

        public void EliminarPermisoPerfil(int idPerfil, int idPermiso)
        {
            _patenteDAL.EliminarPermisoPerfil(idPerfil, idPermiso);
        }
        //uso este, es el bueno
        public void CrearNuevoPermiso(string nombrePermiso)
        {
            if (string.IsNullOrWhiteSpace(nombrePermiso))
            {
                throw new ArgumentException("El nombre del permiso no puede estar vacío.");
            }

            if (_patenteDAL.ExistePermisoPorNombre(nombrePermiso))
            {
                throw new ArgumentException($"Ya existe un permiso registrado con el nombre '{nombrePermiso}'. Por favor, elija un nombre diferente.");
            }

            _patenteDAL.InsertarPatenteNueva(nombrePermiso);

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Creacion de Patente";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Permisos");
        }
        public List<Perfil> ObtenerComponentesTotales() => _patenteDAL.ObtenerComponentesTotales();
        public List<Perfil> ObtenerPermisosPerfil() => _patenteDAL.ObtenerPermisosPerfil();


        public void EliminarPermiso(int idPermiso, string nombrePermiso)
        {
            // Mandamos la orden directa a la DAL para que haga el borrado en cascada
            _patenteDAL.EliminarPermisoDefinitivo(idPermiso);

            // Registramos la acción fuerte en la bitácora
            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Eliminacion de Patente";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Permisos");
        }

        
    }
}
