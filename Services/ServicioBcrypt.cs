using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class ServicioBcrypt
    {
        public string HashearContraseña(string contraseña)
        {
            return BCrypt.Net.BCrypt.HashPassword(contraseña);
        }
        public bool ValidarContraseña(string contraseñaPlana, string hashGuardado)
        {
            return BCrypt.Net.BCrypt.Verify(contraseñaPlana, hashGuardado);
        }
    }
}
