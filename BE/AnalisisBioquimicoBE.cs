namespace BE
{
    public class AnalisisBioquimicoBE
    {
        public AnalisisBioquimicoBE()
        {
        }

        public AnalisisBioquimicoBE(int iD, int historiaClinicaID, DateTime fecha, float glucemia, float colesterolTotal, float trigliceridos, float colesterolHDL, float colesterolLDL, float hemoglobinaGlicosilada, float insulinaBasal, float hierro, float vitaminaD)
        {
            ID = iD;
            HistoriaClinicaID = historiaClinicaID;
            Fecha = fecha;
            Glucemia = glucemia;
            ColesterolTotal = colesterolTotal;
            Trigliceridos = trigliceridos;
            ColesterolHDL = colesterolHDL;
            ColesterolLDL = colesterolLDL;
            HemoglobinaGlicosilada = hemoglobinaGlicosilada;
            InsulinaBasal = insulinaBasal;
            Hierro = hierro;
            VitaminaD = vitaminaD;
        }

        /// <summary>
        ///  De aqui salen los graficos con un Historico de Analisis Bioquimicos.
        ///  Para realizar un seguimiento del paciente a lo largo del tiempo
        /// </summary>

        public int ID { get; set; }
        public int HistoriaClinicaID { get; set; }
        
        public DateTime Fecha { get; set; } 

        public float Glucemia { get; set; } // mg/dL
        public float ColesterolTotal { get; set; } // mg/dL
        public float Trigliceridos { get; set; } // mg/dL

        public float ColesterolHDL { get; set; } // mg/dL (Colesterol "Bueno")
        public float ColesterolLDL { get; set; } // mg/dL (Colesterol "Malo")

        // Índices relacionados a diabetes y metabolismo
        public float HemoglobinaGlicosilada { get; set; } // % (HbA1c)
        public float InsulinaBasal { get; set; } // mUI/L
        
        // Micronutrientes comunes que se monitorean
        // Hierro por la anemia
        public float Hierro { get; set; } // mcg/dL
        public float VitaminaD { get; set; } // ng/mL
    }
}