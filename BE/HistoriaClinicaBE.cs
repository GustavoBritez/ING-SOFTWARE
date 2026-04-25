namespace BE
{
    public class HistoriaClinicaBE
    {
        public HistoriaClinicaBE()
        {
        }

        public HistoriaClinicaBE(int iD, int pacienteID)
        {
            ID = iD;
            PacienteID = pacienteID;
        }


        public List<AnamnesisBE> Anamnesis { get; set; } = new List<AnamnesisBE>();
        public List<RegistroAntropometricoBE> RegistroAntropometrico { get; set; } = new List<RegistroAntropometricoBE>();
        public List<AnalisisBioquimicoBE> AnalisisBioquimico { get; set; } = new List<AnalisisBioquimicoBE>();  

        // ID de la Historia Clinica, se autogenera al crearla preferiblemente AUTOINCREMENT en la base de datos
        public int ID { get; set; }

        // ID del Paciente al que pertenece esta Historia Clinica, se asocia a un Paciente en la base de datos
        public int PacienteID { get; set; }


    }
}