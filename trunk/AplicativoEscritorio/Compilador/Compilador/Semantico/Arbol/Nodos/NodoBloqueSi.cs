using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.Arbol.Labels;
using Compilador.Auxiliares;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoBloqueSi: NodoArbolSemantico
    {
        public NodoBloqueSi(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            //No me importa en este punto si es un Si o un Si-Sino... llegado el caso en el momento de calcular el codigo
            //le creo un nuevo label

            //Si es un Si-Sino, ambas I1 y I2, tienen el mismo fin, el que se hereda, asi que uso uno solo pq asi esta implementado
            this.LabelVerdadero = ManagerLabels.Instance.CrearNuevoLabel("SiVerdadero");
            this.LabelFalso = nodoPadre.LabelFin;
            this.LabelFin = nodoPadre.LabelFin;


        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            hijoAHeredar.LabelFin = this.LabelFin;
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            this.TextoParaImprimirArbol = "SI";
            this.EsSino = this.hijosNodo[4].EsSino;

            return this;
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
            StringBuilder strBldr = new StringBuilder("\t");

           
            this.Codigo = strBldr.ToString().Replace("\r\n", "\r\n\t").ToString().TrimEnd('\t');

            
        }
    }
}
