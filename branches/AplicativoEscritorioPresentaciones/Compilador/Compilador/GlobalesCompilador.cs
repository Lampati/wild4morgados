﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar
{
    public static class GlobalesCompilador
    {
        internal  const string NOMBRE_PROC_SALIDA = "salida";
        internal const string NOMBRE_PROC_PRINCIPAL = "principal";

        internal const string PREFIJO_VARIABLES = "ProgramArVariable__00__";

        internal const double MIN_VALOR_NUMERO = -2.9e39;
        internal const double MAX_VALOR_NUMERO = 1.7e38;

        internal const double MAX_LONG_CADENA = 250;        

        //internal const int CANT_MAX_ITERACIONES = 32000;

        //internal const short CANT_MAX_ERRORES_SINTACTICOS = 5;

        internal static int UltFila;
        internal static int UltCol;

        public static int CantMaxIteraciones { get; set; }
        public static int CantMaxErroresSintacticos { get; set; }

        public enum TipoError
        {
            Compilacion,
            Semantico,
            Sintactico,            
            Ninguno
        }

        public static void ReiniciarFilaYColumna()
        {
            GlobalesCompilador.UltFila = 0;
            GlobalesCompilador.UltCol = 0;
        }

        public static string ObtenerProgramaConEstructuraVacia()
        {
            StringBuilder strBldr = new StringBuilder();
            strBldr.AppendLine("procedimiento SALIDA()");
            strBldr.AppendLine("comenzar");
            strBldr.AppendLine("finproc;");
            strBldr.AppendLine();
            strBldr.AppendLine("procedimiento PRINCIPAL()");
            strBldr.AppendLine("comenzar");
            strBldr.AppendLine("llamar SALIDA();");
            strBldr.AppendLine("finproc;");
            


            return strBldr.ToString();
        }

        
    }
}