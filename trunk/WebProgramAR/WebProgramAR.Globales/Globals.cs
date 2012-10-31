using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebProgramAR.Globales
{
    public static class Globals
    {
        public const string MATCH_EMAIL_PATTERN =
                  @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				        [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
           + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				        [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
           + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


     

        public enum TiposRoles
        {
            administrador,
            moderador,
            profesor
        }

        public enum EstadosEjercicios
        {
            Pendiente = 1,
            Aprobado = 2,
            Desaprobado = 3
        }
    }
    
}
