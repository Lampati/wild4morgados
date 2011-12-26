using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoDeclaracionesIni : NodoArbolSemantico
    {
        public NodoDeclaracionesIni(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
               
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
        {
       
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
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
            // esto es asi pq las variables del proc, tienen que ser globales
            this.ConstantesGlobales  = this.hijosNodo[1].Codigo;
            this.VariablesGlobales = this.hijosNodo[3].Codigo;

           

            
        }
    }
}
