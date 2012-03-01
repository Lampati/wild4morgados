using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebProgramAR.DataAccess.Seguridad
{
    public static class ConstantesSeguridad
    {
        //Comparadores
        //enteros
        public const string MENOR_IGUAL = "<=";
        public const string MENOR = "<";
        public const string MAYOR_IGUAL = ">=";
        public const string MAYOR = ">";
        public const string IGUAL = "=";
        public const string DISTINTO = "!=";

        //string
        public const string EMPIEZA_CON_NOCASESENSITIVE = "LIKE '{0}%'";
        public const string TERMINA_CON_NOCASESENSITIVE = "LIKE '%{0}'";
        public const string IGUAL_NOCASESENSITIVE = "LIKE '{0}'";
        public const string DISTINTO_NOCASESENSITIVE = "NOT LIKE '{0}'";
        
        

    }
}
