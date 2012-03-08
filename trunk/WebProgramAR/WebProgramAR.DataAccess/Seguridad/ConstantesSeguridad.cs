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
        public const string MENOR_IGUAL = "MENOR_IGUAL";  //"<=";
        public const string MENOR = "MENOR";  //"<";
        public const string MAYOR_IGUAL = "MAYOR_IGUAL";  //">=";
        public const string MAYOR = "MAYOR";  //">";
        public const string IGUAL = "IGUAL";  //"=";
        public const string DISTINTO = "DISTINTO";  //"!=";

        //string
        public const string EMPIEZA_CON_NOCASESENSITIVE = "EMPIEZA_CON_NOCASESENSITIVE";  //"LIKE '{0}%'";
        public const string TERMINA_CON_NOCASESENSITIVE = "TERMINA_CON_NOCASESENSITIVE";  //"LIKE '%{0}'";
        public const string IGUAL_NOCASESENSITIVE = "IGUAL_NOCASESENSITIVE";  //"LIKE '{0}'";
        public const string CONTIENE_NOCASESENSITIVE = "CONTIENE_NOCASESENSITIVE";  //"LIKE '%{0}%'";
        public const string DISTINTO_NOCASESENSITIVE = "DISTINTO_NOCASESENSITIVE";  //"NOT LIKE '{0}'";



        public const string TIPO_INT = "INT";
        public const string TIPO_BOOL = "BOOL";
        public const string TIPO_STRING = "STRING";
        public const string TIPO_DATETIME = "DATETIME";

    }
}
