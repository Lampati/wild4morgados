﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol.Nodos;

namespace CompiladorGargar
{

    class GeneracionCodigoHelpers
    {
        private static string variableContadoraLineas;
        public static string VariableContadoraLineas
        {
            get
            {
                return variableContadoraLineas;
            }
        }


        public static string DefinirFuncionesBasicas()
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine("function FrameworkProgramArProgramAr0000001EscribirBooleano( x : boolean) : string ;");
            strBldr.AppendLine("VAR ");
            strBldr.AppendLine("retorno : string; ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("if ( x ) then");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\tretorno := 'verdadero'; ");
            strBldr.AppendLine("end ");
            strBldr.AppendLine("else ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\tretorno := 'falso'; ");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine("\tFrameworkProgramArProgramAr0000001EscribirBooleano := retorno; ");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

           


            return strBldr.ToString();
        }

        public static string EscribirValorBooleano(string codigo)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("FrameworkProgramArProgramAr0000001EscribirBooleano( ");
            strBldr.Append(codigo);
            strBldr.Append(") ");
            

            return strBldr.ToString();
        }

        public static string PausarHastaEntradaTeclado()
        {

            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine(" readkey; ");

            return strBldr.ToString();
        }

        public static string AsignarLinea(int linea)
        {
            return string.Format("{0} := {1};",VariableContadoraLineas,linea);
        }


        internal static void ReiniciarValoresVariablesAleatorias()
        {
            variableContadoraLineas = Utilidades.RandomManager.RandomStringConPrefijo("ProgramAr_ContLineas",20,true);
        }
    }


}