using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar
{
    public static class GlobalesCompilador
    {
        internal  const string NOMBRE_PROC_SALIDA = "salida";
        internal const string NOMBRE_PROC_PRINCIPAL = "principal";

        internal static int UltFila;
        internal static int UltCol;

        public enum TipoError
        {
            Sintactico,
            Semantico,
            Ninguno
        }
    }
}
