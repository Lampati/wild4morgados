using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol.Nodos;

namespace CompiladorGargar
{

    class GeneracionCodigoHelpers
    {
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
        
    }


}
