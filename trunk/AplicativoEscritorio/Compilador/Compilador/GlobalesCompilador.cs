using System;
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

        internal const int CANT_MAX_ITERACIONES = 32000;

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

        public static void AgregarLibreriasFramework(TablaSimbolos tablaSimbolos)
        {
            AgregarLibreriaNormal(tablaSimbolos);
            AgregarLibreriaMatematica(tablaSimbolos);

        }

        private static void AgregarLibreriaNormal(TablaSimbolos tablaSimbolos)
        {
            string nombre;
            string nombreFunc;
            string codigo;
            List<FirmaProc> parametros;

            nombre = "EsPar";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionEsPar(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Booleano, codigo, nombreFunc);

            nombre = "EsImpar";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionEsImpar(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Booleano, codigo, nombreFunc);

            nombre = "Redondear";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionRedondearAEntero(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            nombre = "Truncar";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionTruncar(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);
        }



        private static void AgregarLibreriaMatematica(TablaSimbolos tablaSimbolos)
        {
            string nombre;
            string nombreFunc;
            string codigo;
            List<FirmaProc> parametros;

            nombre = "Potencia";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false));
            parametros.Add(new FirmaProc("exp", NodoTablaSimbolos.TipoDeDato.Numero, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionPotencia(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            nombre = "Raiz";
            nombreFunc = string.Format("FrameworkProgramArProgramAr0000001{0}", nombre);
            parametros = new List<FirmaProc>();
            parametros.Add(new FirmaProc("num", NodoTablaSimbolos.TipoDeDato.Numero, false));
            parametros.Add(new FirmaProc("exp", NodoTablaSimbolos.TipoDeDato.Numero, false));
            codigo = GeneracionCodigoHelpers.ArmarFuncionRaiz(nombreFunc);
            tablaSimbolos.AgregarFuncionDelFramework(nombre, parametros, NodoTablaSimbolos.TipoDeDato.Numero, codigo, nombreFunc);

            


        }
    }
}
