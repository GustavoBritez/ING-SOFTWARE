namespace BE
{
    public class BitacoraBE
    {
        private int Criticidad;
        private string Descripcion;
        private int Dni;
        private DateTime Fecha;
        private int Id_Evento;
        private string Modulo;

        public BitacoraBE(int criticidad, string descripcion, int dni, DateTime fecha, int id_Evento, string modulo)
        {
            Criticidad = criticidad;
            Descripcion = descripcion;
            Dni = dni;
            Fecha = fecha;
            Id_Evento = id_Evento;
            Modulo = modulo;
        }

        public int _Criticidad { get => Criticidad; set => Criticidad = value; }
        public string _Descripcion { get => Descripcion; set => Descripcion = value; }
        public int _Dni { get => Dni; set => Dni = value; }
        public DateTime _Fecha { get => Fecha; set => Fecha = value; }
        public int _Id_Evento { get => Id_Evento; set => Id_Evento = value; }
        public string _Modulo { get => Modulo; set => Modulo = value; }
    }
}   