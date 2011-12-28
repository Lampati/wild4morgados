using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar
{
    public static class Global
    {
        public  const string NOMBRE_PROC_SALIDA = "salida";
        public  const string NOMBRE_PROC_PRINCIPAL = "principal";

        public static int UltFila;
        public static int UltCol;

        public enum TipoError
        {
            Sintactico,
            Semantico,
            Ninguno
        }
    }
}
