using Services;

namespace BE
{
    public class UsuarioBE
    {
        private string Apellido;
        private bool Bloqueado;
        private string Contraseña;
        private int Dni;
        private string Nombre;
        private string NombreDeUsuario;
        private string Rol;
        private bool Estado;

        public UsuarioBE(string nombre, string apellido, int dni, string nombreDeUsuario, string contraseña, string rol, bool bloqueado, bool estado)
        {
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            NombreDeUsuario = nombreDeUsuario;
            Contraseña = contraseña;
            Rol = rol;
            Bloqueado = bloqueado;
            Estado = estado;
        }

        public string _Apellido { get => Apellido; set => Apellido = value; }
        public bool _Bloqueado { get => Bloqueado; set => Bloqueado = value; }
        public string _Contraseña { get => Contraseña; set => Contraseña = value; }
        public int _Dni { get => Dni; set => Dni = value; }
        public string _Nombre { get => Nombre; set => Nombre = value; }
        public string _NombreDeUsuario { get => NombreDeUsuario; set => NombreDeUsuario = value; }
        public string _Rol { get => Rol; set => Rol = value; }
        public bool _Estado { get => Estado; set => Estado = value; }
    }
}