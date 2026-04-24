namespace BE
{
    public class AnamnesisBE
    {
        public AnamnesisBE(DateTime fecha, string matriculaProf, int historiaClinicaID, string motivoConsulta, string antecedentesPatologicos, string antecedentesFamiliares, int horasDeSueno, bool medicacionActual, bool consumoDeAlcohol, bool percepcionEstres)
        {
            Fecha = fecha;
            MatriculaProf = matriculaProf;
            HistoriaClinicaID = historiaClinicaID;
            MotivoConsulta = motivoConsulta;
            AntecedentesPatologicos = antecedentesPatologicos;
            AntecedentesFamiliares = antecedentesFamiliares;
            HorasDeSueno = horasDeSueno;
            MedicacionActual = medicacionActual;
            ConsumoDeAlcohol = consumoDeAlcohol;
            PercepcionEstres = percepcionEstres;
        }

        public AnamnesisBE()
        {
        }   

        public DateTime Fecha { get; set; }


        public string MatriculaProf { get; set; }

        // Tecnicamente una Anamnesis debe estar asociada a una Historia Clinica
        public int HistoriaClinicaID { get; set; } 

        // El Nutricionista debe completar con 280 caracteres el motivo de consulta
        public string MotivoConsulta { get; set; }
        public string AntecedentesPatologicos { get; set; }
        public string AntecedentesFamiliares { get; set; }

        public int HorasDeSueno { get; set; }

        public bool MedicacionActual { get; set; }
        public bool ConsumoDeAlcohol { get; set; }
        public bool PercepcionEstres { get; set; }

    }
}