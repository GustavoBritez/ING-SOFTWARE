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
        private string Idioma;
        private int IdPerfil; // ¡ESTO SE HABÍA BORRADO!

        // Actualizamos string rol por int idPerfil
        public UsuarioBE(string nombre, string apellido, int dni, string nombreDeUsuario, string contraseña, int idPerfil, bool bloqueado, bool estado, string idioma)
        {
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            NombreDeUsuario = nombreDeUsuario;
            Contraseña = contraseña;
            IdPerfil = idPerfil; // Asignamos el perfil
            Bloqueado = bloqueado;
            Estado = estado;
            Idioma = idioma;
        }

        public string _Apellido { get => Apellido; set => Apellido = value; }
        public bool _Bloqueado { get => Bloqueado; set => Bloqueado = value; }
        public string _Contraseña { get => Contraseña; set => Contraseña = value; }
        public int _Dni { get => Dni; set => Dni = value; }
        public string _Nombre { get => Nombre; set => Nombre = value; }
        public string _NombreDeUsuario { get => NombreDeUsuario; set => NombreDeUsuario = value; }
        public bool _Estado { get => Estado; set => Estado = value; }
        public string _Idioma { get => Idioma; set => Idioma = value; }
        public int _IdPerfil { get => IdPerfil; set => IdPerfil = value; } // ¡PROPIEDAD RECUPERADA!
    }
}