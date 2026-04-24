namespace BE
{
    public class RegistroAntropometricoBE
    {
        public RegistroAntropometricoBE()
        {
        }

        public RegistroAntropometricoBE(int iD, int historiaClinicaID, DateTime fechaRegistro, float peso, float talla, float perimetroCintura, float perimetroCadera, float porcentajeGrasa, float porcentajeMusculo, float kilogramosGrasa, float kilogramosMusculo)
        {
            ID = iD;
            HistoriaClinicaID = historiaClinicaID;
            FechaRegistro = fechaRegistro;
            Peso = peso;
            Talla = talla;
            PerimetroCintura = perimetroCintura;
            PerimetroCadera = perimetroCadera;
            PorcentajeGrasa = porcentajeGrasa;
            PorcentajeMusculo = porcentajeMusculo;
            KilogramosGrasa = kilogramosGrasa;
            KilogramosMusculo = kilogramosMusculo;
        }

        public int ID { get; set; } /// Este ID es autogenerado en la BD
        public int HistoriaClinicaID { get; set; } /// Se asocia a una Historia Clinica existente


        public DateTime FechaRegistro { get; set; } 

        public float Peso { get; set; } // Ej: 75.5 kg

        // Estatura (Necesaria para calcular el IMC )
        public float Talla { get; set; } // Ej: 1.75 metros

        // Perímetros (Ideales para gráficos de riesgo cardiovascular)
        public float PerimetroCintura { get; set; } // cm
        public float PerimetroCadera { get; set; }  // cm

        // Composición corporal ( Para gráficos de torta o barras apiladas)
        public float PorcentajeGrasa { get; set; }  // %
        public float PorcentajeMusculo { get; set; } // %
        public float KilogramosGrasa { get; set; }   // kg
        public float KilogramosMusculo { get; set; } // kg

    }
}