using DAL;
using BE;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Data;

namespace DAL
{
    public class UsuarioDAL
    {
        enum CRUD
        {
            CREATE = 1,
            READ = 2,
            UPDATE = 3,
            DELETE = 4,
            READALL = 5
        }
        private Conexion conexion = new();

        public void RegistrarUsuario (UsuarioBE User)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@OP", CRUD.CREATE),
                new SqlParameter("@USUARIO", User.Email) { SqlValue = User.Email},
                new SqlParameter("@PASS", User.Password) { SqlValue = User.Password}
            };

            conexion.Escribir("SP_CRUD_USUARIO", parameter);

        }

        public void IniciarSesion(UsuarioBE User)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@OP", CRUD.READ),
                new SqlParameter("@USUARIO", User.Email) { SqlValue = User.Email},
                new SqlParameter("@PASS", User.Password) { SqlValue = User.Password}
            };

            DataTable dt = conexion.Leer("SP_CRUD_USUARIO", parameter);

        }
    }
}