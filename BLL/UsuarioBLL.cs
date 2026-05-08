using BE;
using DAL;
using Microsoft.Data.SqlClient;

namespace BLL
{
    public class UsuarioBLL
    {


        public void RegistrarUsuario(UsuarioBE User)
        {

            User.Password = BCrypt.Net.BCrypt.HashPassword(User.Password);

            UsuarioDAL AccesoUsuario = new();
            AccesoUsuario.RegistrarUsuario(User);
        }

        public bool Login(string email, string pass)
        {
            UsuarioDAL AccesoUsuario = new();

            UsuarioBE User = AccesoUsuario.IniciarSesion(new UsuarioBE { Email = email , Password = pass});

            if ( User is null)
            {
                Console.WriteLine("Fallo el login");
                return false;
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(pass, User.Password);

            return isValid;
        }
    }
}