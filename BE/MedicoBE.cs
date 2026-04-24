namespace BE
{
    public class MedicoBE : ProfesionalBE
    {
        public MedicoBE()
        {

        }

        public MedicoBE(string matricula, string nombre, string email, int id)
        {
            Matricula = matricula;
            Nombre = nombre;
            Email = email;
            ID = id;
        }
    }
}