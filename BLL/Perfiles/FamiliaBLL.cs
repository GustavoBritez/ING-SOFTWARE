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

        public void AgregarFamiliaAPerfil(int idPerfil, int idFamilia)
        {
            _patenteDAL.InsertarFamiliaPerfil(idPerfil, idFamilia);
        }

        public void EliminarFamiliaPerfil(int idPerfil, int idFamilia)
        {
            _patenteDAL.EliminarFamiliaPerfil(idPerfil, idFamilia);
        }

        public FamiliaServices ObtenerArbolFamiliar(int idFamiliaRaiz)
        {
            return _familiaDAL.ObtenerArbolFamiliar(idFamiliaRaiz);
        }

        public List<Perfil> ObtenerFamiliasPerfil() => _patenteDAL.ObtenerFamiliasPerfil();

        public void CrearNuevaFamilia(string nombreFamilia)
        {
            if (string.IsNullOrWhiteSpace(nombreFamilia))
            {
                throw new ArgumentException("ERROR: El nombre de la familia no puede estar vacío.");
            }

            _familiaDAL.InsertarFamiliaNueva(nombreFamilia);

            EventoBLL bitacoraBLL = new();
            int dniActual = ServicesSessionManager.Instancia.ObtenerDniUsuarioActual();
            string descripcion = $"Creación de nueva Familia: {nombreFamilia}";
            bitacoraBLL.RegistrarEvento(3, descripcion, dniActual, "Perfil");
        }
        public List<string> ObtenerPerfilesDeFamilia(int idFamilia)
        {
            return _familiaDAL.ObtenerPerfilesDeFamilia(idFamilia);
        }
    }
    
}
