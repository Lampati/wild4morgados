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

            this.Codigo = strBldr.ToString().Replace("\r\n", "\r\n\t").ToString().TrimEnd('\t');
        }
    }
}
