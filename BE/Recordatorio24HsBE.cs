

namespace BE
{

    public class Recordatorio24HsBE : AnamnesisBE
    {

        /// <summary>
        ///  ¿ Para que sirve el Recordatorio 24Hs ?
        ///  Sirve para conocer que consume el paciente en un dia tipico
        ///  Solo se espera conocer esa informacion, apartir de esto el profecional busca hacer recomendaciones
        ///  No deberiamos usar esto para realizar ningun grafico.
        ///  
        /// El recordatorio 24Hs se realiza en paciente nuevos donde no se conoce sus habitos.
        /// 
        /// </summary>
        public Recordatorio24HsBE()
        {
        }

        public Recordatorio24HsBE(string desayuno, string almuerzo, string merienda, string cena, string colaciones, int cantidadVasosAgua, bool realizoActividadFisica, bool suplementosVitaminas )
        {

            Desayuno = desayuno;
            Almuerzo = almuerzo;
            Merienda = merienda;
            Cena = cena;
            Colaciones = colaciones;
            CantidadVasosAgua = cantidadVasosAgua;
            RealizoActividadFisica = realizoActividadFisica;
            SuplementosVitaminas = suplementosVitaminas;
        }


        // Se ingresan datos " Milanesa con pure ", " 2 medialunas con cafe con leche ", " 1 yogur con frutas ".
        public string Desayuno { get; set; }
        public string Almuerzo { get; set; }
        public string Merienda { get; set; }
        public string Cena { get; set; }
        public string Colaciones { get; set; }

        public int CantidadVasosAgua { get; set; }
        public bool RealizoActividadFisica { get; set; }
        public bool SuplementosVitaminas { get; set; }
       
    }
}