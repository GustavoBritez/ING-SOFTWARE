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
    public class FamiliaBLL
    {
        private PatenteDAL _patenteDAL = new(); 
        private FamiliaDAL _familiaDAL = new();

        public FamiliaBLL( ) { }

        public void CrearNuevaFamilia(string nombreFamilia)
        {
            if (string.IsNullOrWhiteSpace(nombreFamilia))
            {
                throw new ArgumentException("El nombre de la familia no puede estar vacío.");
            }

            if (_familiaDAL.ExisteFamiliaPorNombre(nombreFamilia))
            {
                throw new ArgumentException($"Ya existe una familia registrada con el nombre '{nombreFamilia}'. Por favor, elija un nombre diferente.");
            }

            _familiaDAL.InsertarFamiliaNueva(nombreFamilia); 

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Creación de nueva Familia: '{nombreFamilia}'";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Familia");
        }

        public void AgregarFamiliaAPerfil(int idPerfil, int idFamilia)
        {
            _patenteDAL.InsertarFamiliaPerfil(idPerfil, idFamilia);
        }

        public void EliminarPermisoFamilia(int idFamilia, int idPermiso, string nombreFamilia, string nombrePermiso)
        {
            // 1. Validamos que exista la relación usando el método ExisteRelacionPermisoFamilia que armamos en el paso anterior
            if (!_familiaDAL.ExisteRelacionPermisoFamilia(idFamilia, idPermiso))
            {
                throw new ArgumentException($"El permiso '{nombrePermiso}' no se encuentra asignado a la familia '{nombreFamilia}'.");
            }

            // 2. Si existe, lo borramos
            _familiaDAL.EliminarPermisoFamilia(idFamilia, idPermiso);

            // 3. Bitácora
            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Desvincular Permiso '{nombrePermiso}' de Familia '{nombreFamilia}'";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Familia");
        }

        public void AgregarPermisoAFamilia(int idFamilia, int idPermiso, string nombrePermiso, string nombreFamilia)
        {
            if (_familiaDAL.ExisteRelacionPermisoFamilia(idFamilia, idPermiso))
            {
                throw new ArgumentException($"El permiso '{nombrePermiso}' ya se encuentra dentro de la familia '{nombreFamilia}'.");
            }

            _familiaDAL.InsertarPermisoFamilia(idFamilia, idPermiso);

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Asignar Permiso '{nombrePermiso}' a Familia '{nombreFamilia}'";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Familia");
        }

        public FamiliaServices ObtenerArbolFamiliar(int idFamiliaRaiz)
        {
            return _familiaDAL.ObtenerArbolFamiliar(idFamiliaRaiz);
        }

        public List<Perfil> ObtenerFamiliasPerfil() => _patenteDAL.ObtenerFamiliasPerfil();
       
        public void EliminarFamilia(int idFamilia, string nombreFamilia)
        {
            // Ejecutamos el borrado en cascada
            _familiaDAL.EliminarFamilia(idFamilia);

            // Dejamos registro en la bitácora
            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Eliminación en cascada de la Familia: '{nombreFamilia}'";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Familia");
        }
    }
    
}
