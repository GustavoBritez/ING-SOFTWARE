using BE;
using DAL;
using Microsoft.Data.SqlClient;

namespace BLL
{
    public class UsuarioBLL
    {


        public void RegistrarUsuario( UsuarioBE User )
        {

            User.Password = BCrypt.Net.BCrypt.HashPassword( User.Password );

            UsuarioDAL AccesoUsuario = new();
            AccesoUsuario.RegistrarUsuario( User );
        }

    }
}