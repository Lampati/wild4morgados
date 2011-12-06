using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.Arbol.Labels;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoLinea: NodoArbolSemantico
    {
        public NodoLinea(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.LabelFin = ManagerLabels.Instance.CrearNuevoLabel("FinLinea");
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            hijoAHeredar.LabelFin = this.LabelFin;
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {

            
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
            strBldr.Append(this.hijosNodo[0].Codigo);

            /*
            if ((this.hijosNodo[0].GetType() == typeof(NodoBloqueMientras)) || (this.hijosNodo[0].GetType() == typeof(NodoBloqueSi)))
            {                
                strBldr.Append(GeneracionCodigoHelpers.GenerarLabel(LabelFin.Nombre));
            }
            */
            this.Codigo = strBldr.ToString();
        }
    }
}
