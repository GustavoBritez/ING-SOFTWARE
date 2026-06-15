using Services.Perfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Perfiles
{
    public class PatenteDAL
    {
        private Conexion _conexion = new();
        public PatenteDAL() { }

        // Manejo de la tabla relacional Perfil_Componente
        public void InsertarPermisoPerfil(int idPerfil, int idPermiso)
        {
            string query = "INSERT INTO Perfil (IdPerfil, IdPermiso) VALUES (@idPerfil, @idPermiso)";
            // Ejecución SQL...
        }

        public void EliminarPermisoPerfil(int idPerfil, int idPermiso)
        {
            string query = "DELETE FROM Perfil WHERE IdPerfil = @idPerfil AND IdPermiso = @idPermiso";
            // Ejecución SQL...
        }

        public void EliminarFamiliaPerfil(int idPerfil, int idFamilia)
        {
            string query = "DELETE FROM Perfil WHERE IdPerfil = @idPerfil AND IdPermiso = @idFamilia";
            // Ejecución SQL...
        }

        // Lecturas
        public List<Componente> ObtenerComponentesTotales()
        {
            // SELECT * FROM Componentes
            return new List<Componente>();
        }

        public List<Componente> ObtenerFamiliasPerfil()
        {
            // SELECT * FROM Componentes WHERE EsFamilia = 1
            return new List<Componente>();
        }

        public List<Componente> ObtenerPermisosPerfil()
        {
            // SELECT * FROM Componentes WHERE EsFamilia = 0
            return new List<Componente>();
        }

    }
}
