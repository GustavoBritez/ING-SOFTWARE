using System;

namespace BE
{


    public class FrecuenciaConsumoBE : AnamnesisBE
    {
        public FrecuenciaConsumoBE()
        {
        }
        public enum Frecuencia
        {
            Nunca = 0,
            AlgunasVeces = 1,
            AMenudo = 2,
            TodosLosDias = 3
        }

        public Frecuencia CarnesRojas { get; set; }
        public Frecuencia CarnesBlancas { get; set; } // Pollo, pescado, cerdo magro
        public Frecuencia LácteosYDerivados { get; set; } // Leche, quesos, yogur
        public Frecuencia Frutas { get; set; }
        public Frecuencia Verduras { get; set; }
        public Frecuencia CerealesYLegumbres { get; set; } // Arroz, fideos, lentejas, pan
        public Frecuencia AzucaresYDulces { get; set; }
        public Frecuencia FriturasYComidaRapida { get; set; }
        public Frecuencia BebidasAzucaradas { get; set; } // Gaseosas, jugos industriales
        public Frecuencia BebidasAlcoholicas { get; set; }

    }
}