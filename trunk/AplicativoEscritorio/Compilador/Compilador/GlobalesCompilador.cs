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

        internal const string PREFIJO_VARIABLES = "ProgramArVariable__00__";

        internal const short CANT_MAX_ERRORES_SINTACTICOS = 5;

        internal static int UltFila;
        internal static int UltCol;

        public enum TipoError
        {
            Compilacion,
            Semantico,
            Sintactico,            
            Ninguno
        }
    }
}
