using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.Arbol.Labels;
using Compilador.Auxiliares;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoBloqueMientras: NodoArbolSemantico
    {
        public NodoBloqueMientras(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.LabelVerdadero = ManagerLabels.Instance.CrearNuevoLabel("MientrasVerdadero");
            this.LabelFalso = nodoPadre.LabelFin;
            this.LabelFin = ManagerLabels.Instance.CrearNuevoLabel("MientrasFin");
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            hijoAHeredar.LabelFin = this.LabelFin;
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TextoParaImprimirArbol = "MIENTRAS";
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
            StringBuilder strBldr = new StringBuilder("\t");

            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("------COMIENZO BLOQUEMIENTRAS-----"));

            strBldr.Append(GeneracionCodigoHelpers.GenerarPush("Ax"));
            CodeLabel auxLabel = ManagerLabels.Instance.CrearNuevoLabel("MientrasRetorno");

            strBldr.Append(GeneracionCodigoHelpers.GenerarLabel(auxLabel.Nombre));

            strBldr.Append(this.hijosNodo[1].Codigo);

            if (this.hijosNodo[1].Comparacion == TipoComparacion.None)
            {
                if (((NodoExprBool)this.hijosNodo[1]).EsPar)
                {
                    strBldr.Append(GeneracionCodigoHelpers.GenerarJump(this.LabelVerdadero.Nombre, TipoComparacion.EqualZero));
                }
                else
                {
                    strBldr.Append(GeneracionCodigoHelpers.GenerarJump(this.LabelVerdadero.Nombre, TipoComparacion.NotEqualZero));
                }

            }
            else
            {
                strBldr.Append(GeneracionCodigoHelpers.GenerarJump(this.LabelVerdadero.Nombre, this.hijosNodo[1].Comparacion));                
            }
            strBldr.Append(GeneracionCodigoHelpers.GenerarJumpIncondicional(this.LabelFalso.Nombre));


            strBldr.Append(GeneracionCodigoHelpers.GenerarLabel(LabelVerdadero.Nombre));            
            strBldr.Append(this.hijosNodo[3].Codigo);


            
            //strBldr.Append(GeneracionCodigoHelpers.GenerarLabel(LabelFin.Nombre));
            strBldr.Append(GeneracionCodigoHelpers.GenerarJumpIncondicional(auxLabel.Nombre));

            strBldr.Append(GeneracionCodigoHelpers.GenerarLabel(LabelFalso.Nombre));

            strBldr.Append(GeneracionCodigoHelpers.GenerarPop("Ax"));

            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("-------FINAL BLOQUEMIENTRAS------"));

            this.Codigo = strBldr.ToString().Replace("\r\n", "\r\n\t").ToString().TrimEnd('\t');
        }
    }
}
