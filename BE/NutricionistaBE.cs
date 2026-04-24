namespace BE
{
    public class NutricionistaBE : ProfesionalBE
    {
        public NutricionistaBE()
        {
        }

        public NutricionistaBE(string matricula, string nombre, string email, int id )
        {
            Matricula = matricula;
            Nombre = nombre;
            Email = email;
            ID = id;
        }   
    }
}