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
        private bool Estado;

        // ¡Cambiamos string Rol por int IdPerfil!
        private int IdPerfil;

        public string NombrePerfil { get; set; }

        public UsuarioBE(string nombre, string apellido, int dni, string nombreDeUsuario, string contraseña, int idPerfil, bool bloqueado, bool estado)
        {
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            NombreDeUsuario = nombreDeUsuario;
            Contraseña = contraseña;
            IdPerfil = idPerfil; 
            Bloqueado = bloqueado;
            Estado = estado;
        }

        public string _Apellido { get => Apellido; set => Apellido = value; }
        public bool _Bloqueado { get => Bloqueado; set => Bloqueado = value; }
        public string _Contraseña { get => Contraseña; set => Contraseña = value; }
        public int _Dni { get => Dni; set => Dni = value; }
        public string _Nombre { get => Nombre; set => Nombre = value; }
        public string _NombreDeUsuario { get => NombreDeUsuario; set => NombreDeUsuario = value; }
        public int _IdPerfil { get => IdPerfil; set => IdPerfil = value; }
        public bool _Estado { get => Estado; set => Estado = value; }
    }
}