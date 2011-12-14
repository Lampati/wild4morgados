 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoIdAsign: NodoArbolSemantico
    {
        public NodoIdAsign(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                this.IndiceArreglo = this.hijosNodo[1].Valor;
                this.EsArreglo = true;
                this.TipoDato = this.hijosNodo[1].TipoDato;

                this.Temporal = this.hijosNodo[1].Temporal;
                if (this.Temporal != null)
                {
                    this.Lugar = this.hijosNodo[1].Temporal.Nombre;
                }
                else
                {
                    this.Valor = this.hijosNodo[1].Valor;
                }
                
                
            }
            else
            {
                this.IndiceArreglo = 0;
                this.EsArreglo = false;
            }
            return this;
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {            
        }

        public override void ChequearAtributos(Terminal t)
        {
            if (this.EsArreglo)
            {
                if (this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Numero)
                {
                    throw new ErrorSemanticoException(new StringBuilder("El subindice del arreglo debe ser natural.").ToString(),
                    t.Componente.Fila, t.Componente.Columna);
                }
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            this.TipoDato = NodoTablaSimbolos.TipoDeDato.Numero;
            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder sb = new StringBuilder();

             

            this.Codigo = sb.ToString();
        } 
    }
}
