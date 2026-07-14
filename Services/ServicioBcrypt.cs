using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class ServicioBcrypt
    {
        //DV
        private const int WorkFactor = 10;

        public string HashearContraseña(string contraseña)
        {
            return BCrypt.Net.BCrypt.HashPassword(contraseña);
        }
        public bool ValidarContraseña(string contraseñaPlana, string hashGuardado)
        {
            return BCrypt.Net.BCrypt.Verify(contraseñaPlana, hashGuardado);
        }

        //DV
        public static string CalcularDV(string datos)
        {
            // BCrypt.HashPassword genera un hash único cada vez
            return BCrypt.Net.BCrypt.HashPassword(datos, WorkFactor);
        }

        //DV
        public static bool ValidarDV(string datos, string dvGuardado)
        {
            // BCrypt.Verify compara los datos con el hash guardado
            return BCrypt.Net.BCrypt.Verify(datos, dvGuardado);
        }


    }
}
