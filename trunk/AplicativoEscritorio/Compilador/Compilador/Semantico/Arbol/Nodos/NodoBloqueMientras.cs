using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

using CompiladorGargar.Auxiliares;
using Utilidades;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoBloqueMientras : NodoArbolSemantico
    {
        private string nombreVarControladora;

        public NodoBloqueMientras(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
       
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
          
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
           
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            nombreVarControladora = RandomManager.RandomStringConPrefijo("mientrasVar", 20, true);
            this.TablaSimbolos.AgregarAuxiliarParaCodIntermedio(nombreVarControladora, TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Numero);

            ArmarActividadViewModel();
           

            return this;
        }

        private void ArmarActividadViewModel()
        {
            InterfazTextoGrafico.MientrasViewModel activ = new InterfazTextoGrafico.MientrasViewModel();
            activ.Condicion = this.hijosNodo[2].Gargar;
            activ.Cuerpo = this.hijosNodo[5].ActividadViewModel as InterfazTextoGrafico.SecuenciaViewModel;

            ActividadViewModel = activ;
        }

        public override void ChequearAtributos(Terminal t)
        {

        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine(GeneracionCodigoHelpers.AsignarLinea(this.hijosNodo[2].LineaCorrespondiente));

            strBldr.AppendLine(string.Format("{0} := 0;", nombreVarControladora));
            strBldr.Append("While ");
            strBldr.Append("( ");
            strBldr.Append(this.hijosNodo[2].Codigo);
            strBldr.Append(") ");
            strBldr.AppendLine("do");
            strBldr.AppendLine("begin");

            strBldr.Append("\t").AppendLine(string.Format("if ( {0} = {1} ) then", nombreVarControladora, GlobalesCompilador.CANT_MAX_ITERACIONES));
            strBldr.Append("\t").AppendLine("begin");
            strBldr.Append("\t").Append("\t").AppendLine("raise EIteracionInfinitaException.Create('')");
            strBldr.Append("\t").AppendLine("end;");
            //strBldr.AppendLine(string.Format("WriteLn({0},' / ', {1});", nombreVarControladora, GlobalesCompilador.CANT_MAX_ITERACIONES));
            strBldr.AppendLine(string.Format("{0} := {0} + 1;", nombreVarControladora));


            strBldr.Append("\t").AppendLine(this.hijosNodo[5].Codigo.Replace("\r\n", "\r\n\t"));            
            strBldr.AppendLine("end;");

            this.Codigo = strBldr.ToString();
        }

       
    }
}
