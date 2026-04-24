namespace BE
{
    public class PacienteBE : UsuarioBE
    {
        public PacienteBE()
        {
        }

        public PacienteBE(string dNI, string obrasocial, DateTime anioNacimiento, float estatura, float peso)
        {
            DNI = dNI;
            Obrasocial = obrasocial;
            AnioNacimiento = anioNacimiento;
            Estatura = estatura;
            Peso = peso;
        }

        public string DNI { get; set; }
        public string Obrasocial { get; set; }
        public DateTime AnioNacimiento { get; set; }
        public float Estatura { get; set; }
        public float Peso { get; set; } 

    }
}