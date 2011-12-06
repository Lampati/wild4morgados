using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Semantico.Arbol.Nodos;

namespace Compilador
{

    class GeneracionCodigoHelpers
    {

        public static string GenerarComentario(string comentario)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append("; ").AppendLine(comentario);

            return strbldr.ToString();
        }

        public static string GenerarLabel(string label)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append(label).AppendLine(":");

            return strbldr.ToString();
        }

        public static string GenerarParam(string label)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append("PARAM\t").AppendLine(label);

            return strbldr.ToString();
        }

        public static string GenerarCall(string label)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append("CALL\t").AppendLine(label);

            return strbldr.ToString();
        }

        public static string GenerarMov(string destino, string desde)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append("MOV\t").Append(destino).Append(", ").AppendLine(desde);

            return strbldr.ToString();
        }

        public static string GenerarMovHaciaAx(string desde)
        {
            return GeneracionCodigoHelpers.GenerarMov("AX", desde);
        }

        public static string GenerarMovDesdeAx(string hacia)
        {
            return GeneracionCodigoHelpers.GenerarMov(hacia, "AX");
        }

        public static string GenerarCmp(string destino, string desde)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append("CMP\t").Append(destino).Append(", ").AppendLine(desde);

            return strbldr.ToString();
        }


        public static string GenerarJump(string label, NodoArbolSemantico.TipoComparacion comp)
        {
            StringBuilder strbldr = new StringBuilder();

            switch (comp)
            {
                case NodoArbolSemantico.TipoComparacion.Greater:
                    strbldr.Append("JG\t").AppendLine(label);
                    break;
                case NodoArbolSemantico.TipoComparacion.GreaterOrEquals:
                    strbldr.Append("JGE\t").AppendLine(label);
                    break;                
                case NodoArbolSemantico.TipoComparacion.Less:
                    strbldr.Append("JL\t").AppendLine(label);
                    break;
                case NodoArbolSemantico.TipoComparacion.LessOrEquals:
                    strbldr.Append("JLE\t").AppendLine(label);
                    break;
                
                case NodoArbolSemantico.TipoComparacion.Above:
                    strbldr.Append("JA\t").AppendLine(label);
                    break;
                case NodoArbolSemantico.TipoComparacion.AboveOrEquals:
                    strbldr.Append("JAE\t").AppendLine(label);
                    break;
                case NodoArbolSemantico.TipoComparacion.Below:
                    strbldr.Append("JB\t").AppendLine(label);
                    break;
                case NodoArbolSemantico.TipoComparacion.BelowOrEquals:
                    strbldr.Append("JBE\t").AppendLine(label);
                    break;

                case NodoArbolSemantico.TipoComparacion.Equals:
                    strbldr.Append("JE\t").AppendLine(label);
                    break;
                case NodoArbolSemantico.TipoComparacion.NotEquals:
                    strbldr.Append("JNE\t").AppendLine(label);
                    break;

                case NodoArbolSemantico.TipoComparacion.EqualZero:
                    strbldr.Append("JZ\t").AppendLine(label);
                    break;
                case NodoArbolSemantico.TipoComparacion.NotEqualZero:
                    strbldr.Append("JNZ\t").AppendLine(label);
                    break;

                case NodoArbolSemantico.TipoComparacion.LessThanZero:
                    strbldr.Append("JS\t").AppendLine(label);
                    break;
                case NodoArbolSemantico.TipoComparacion.GreaterThanZero:
                    strbldr.Append("JNS\t").AppendLine(label);
                    break;

            }

            return strbldr.ToString();
        }

        public static string GenerarJumpIncondicional(string codigoDestino)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append("JMP\t").AppendLine(codigoDestino);

            return strbldr.ToString();
        }

        public static string GenerarLea(string destino, string origen)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.AppendLine(String.Format("LEA\t{0}, {1}", destino, origen));

            return strbldr.ToString();
        }

        public static string GenerarInc(string registro)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.AppendLine(String.Format("INC\t{0}", registro));

            return strbldr.ToString();
        }

        public static string GenerarLoop(string label)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.AppendLine(String.Format("LOOP\t{0}", label));

            return strbldr.ToString();
        }




        public static string ExprBool(string op1, string op2)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(GeneracionCodigoHelpers.GenerarMov("AX", op1));
            sb.Append(GeneracionCodigoHelpers.GenerarCmp("AX", op2));


            return sb.ToString();
        }

        public static string AssignArray(string labelVar, string valor)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(GeneracionCodigoHelpers.GenerarMov("CX", valor));
            sb.Append(GeneracionCodigoHelpers.GenerarMov("BX", "2"));
            sb.AppendLine(String.Format("MUL\t{0}", "BX"));
            sb.Append(GeneracionCodigoHelpers.GenerarMov("DI", "AX"));
            sb.Append(GeneracionCodigoHelpers.GenerarMov(String.Format("{0}[{1}]", labelVar, "DI"), "CX"));

            return sb.ToString();
        }

        public static string MoveArray(string labelVar)
        {
            StringBuilder sb = new StringBuilder();


            sb.Append(GeneracionCodigoHelpers.GenerarLabel("L1"));
            sb.AppendLine(String.Format("ADD\t{0}, {1}", "BX", "2"));
            sb.Append(GeneracionCodigoHelpers.GenerarLoop("L1"));
            sb.AppendLine(String.Format("SUB\t{0}, {1}", "BX", "2"));

            return sb.ToString();
        }

        public static string OperarAritmeticamente(string operador, string labelRetorno, string op1, string op2)
        {
            StringBuilder sb = new StringBuilder();

            switch (operador)
            {
                case "+":
                case "++":
                    sb.Append(GeneracionCodigoHelpers.GenerarSuma(labelRetorno, op1, op2));
                    break;
                case "-":
                case "--":
                    sb.Append(GeneracionCodigoHelpers.GenerarResta(labelRetorno, op1, op2));
                    break;
                case "*":
                case "**":
                    sb.Append(GeneracionCodigoHelpers.GenerarMultiplicacion(labelRetorno, op1, op2));
                    break;
                case "/":
                case "//":
                    sb.Append(GeneracionCodigoHelpers.GenerarDivision(labelRetorno, op1, op2));
                    break;
            }

            return sb.ToString();
        }

        public static string GenerarSuma(string label, string op1, string op2)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append(GenerarMov("AX", op1));
            strbldr.AppendLine(String.Format("ADD\t{0}, {1}", "AX", op2));
            strbldr.Append(GenerarMov(label, "AX"));

            return strbldr.ToString();
        }

        public static string GenerarResta(string label, string op1, string op2)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append(GenerarMov("AX", op1));
            strbldr.AppendLine(String.Format("SUB\t{0}, {1}", "AX", op2));
            strbldr.Append(GenerarMov(label, "AX"));

            return strbldr.ToString();
        }

        public static string GenerarMultiplicacion(string label, string op1, string op2)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append(GenerarMovHaciaAx(op2));
            strbldr.Append(GenerarMovDesdeAx("divisorAuxiliar"));
            strbldr.Append(GenerarMov("AX", op1));
            strbldr.AppendLine(String.Format("IMUL\t{0}", "divisorAuxiliar"));
            strbldr.Append(GenerarMov(label, "AX"));

            return strbldr.ToString();
        }

        public static string GenerarDivision(string label, string op1, string op2)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append(GenerarMovHaciaAx(op2));
            strbldr.AppendLine(String.Format("ADD\t{0}, {1}", "AX", "0"));
            //Para no dividr por cero
            strbldr.Append(GenerarJump("labelDivisionPorCero", NodoArbolSemantico.TipoComparacion.EqualZero));

            strbldr.Append(GenerarMovDesdeAx("divisorAuxiliar"));
            strbldr.Append(GenerarMov("DX", "0"));
            strbldr.Append(GenerarMov("AX", op1));
            strbldr.AppendLine(String.Format("IDIV\t{0}", "divisorAuxiliar"));
            strbldr.Append(GenerarMov(label, "AX"));

            return strbldr.ToString();
        }

        public static string GenerarMultiplicacionNatural(string label, string op1, string op2)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append(GenerarMovHaciaAx(op2));
            strbldr.Append(GenerarMovDesdeAx("divisorAuxiliar"));
            strbldr.Append(GenerarMov("AX", op1));
            strbldr.AppendLine(String.Format("MUL\t{0}", "divisorAuxiliar"));
            strbldr.Append(GenerarMov(label, "AX"));

            return strbldr.ToString();
        }

        public static string GenerarDivisionNatural(string label, string op1, string op2)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.Append(GenerarMovHaciaAx(op2));
            strbldr.AppendLine(String.Format("ADD\t{0}, {1}", "AX", "0"));
            //Para no dividr por cero
            strbldr.Append(GenerarJump("labelDivisionPorCero", NodoArbolSemantico.TipoComparacion.EqualZero));

            strbldr.Append(GenerarMovDesdeAx("divisorAuxiliar"));
            strbldr.Append(GenerarMov("DX", "0"));
            strbldr.Append(GenerarMov("AX", op1));           
            strbldr.AppendLine(String.Format("DIV\t{0}", "divisorAuxiliar"));
            strbldr.Append(GenerarMov(label, "AX"));

            return strbldr.ToString();
        }

        public static string GenerarPush(string registro)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.AppendLine(String.Format("PUSH\t{0}", registro));

            return strbldr.ToString();
        }

        public static string GenerarPop(string registro)
        {
            StringBuilder strbldr = new StringBuilder();

            strbldr.AppendLine(String.Format("POP\t{0}", registro));

            return strbldr.ToString();
        }


        public static string GenerarEsPar(string labelVariable)
        {
            StringBuilder strbldr = new StringBuilder();

            //strbldr.Append(GenerarMov("divisorAuxiliar", "2"));
            //strbldr.Append(GenerarMov("DX", "0"));
            //strbldr.Append(GenerarMov("AX", labelVariable));
            //strbldr.AppendLine(String.Format("IDIV\t{0}", "divisorAuxiliar"));
            //strbldr.Append(GenerarCmp("DX", "0"));

            strbldr.Append(GenerarMov("Ax", labelVariable));
            strbldr.AppendLine(String.Format("TEST\t Ax, 0001h"));

            return strbldr.ToString();
        }

        public static string GenerarError(string labelError, string nombreVarError, int cantChars )
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append(GeneracionCodigoHelpers.GenerarLabel(labelError));
            strBldr.AppendFormat("PUSH OFFSET\t{0}", nombreVarError).AppendLine();
            strBldr.AppendFormat("PUSH\t{0}", cantChars.ToString()).AppendLine();
            strBldr.Append(GeneracionCodigoHelpers.GenerarCall("writeSTR"));
            strBldr.AppendLine("INT\t21h");

            return strBldr.ToString();
        }
        
    }


}
