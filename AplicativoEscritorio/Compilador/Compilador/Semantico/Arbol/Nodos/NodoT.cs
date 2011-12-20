using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoT : NodoArbolSemantico
    {
        public NodoT(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
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
                if (this.hijosNodo.Count > 3)
                {
                    this.Valor = this.hijosNodo[2].Valor;
                    this.TipoDato = this.hijosNodo[2].TipoDato;
                    this.Lexema = this.hijosNodo[2].Lexema;
                    this.Temporal = this.hijosNodo[2].Temporal;

                    this.Lugar = this.hijosNodo[2].Lugar;

                    //Para ya poner directamente el numero aca si es el caso
                    if (this.Lugar == null || this.Lugar.Equals(string.Empty))
                    {
                        this.Lugar = this.hijosNodo[2].Valor.ToString();
                    }

                    //Si es mayor a 0 es pq es (EXPR) entonces lo pongo
                    this.EsOtraExpresion = true;

                    if (this.hijosNodo[2].TipoDato != TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Booleano)
                    {                       
                        throw new ErrorSemanticoException("Unicamente se pueden negar expresiones booleanas");
                    }

                    this.AsignaParametros = this.hijosNodo[2].AsignaParametros;
                }
                else
                {

                    this.Valor = this.hijosNodo[1].Valor;
                    this.TipoDato = this.hijosNodo[1].TipoDato;
                    this.Lexema = this.hijosNodo[1].Lexema;
                    this.Temporal = this.hijosNodo[1].Temporal;

                    this.Lugar = this.hijosNodo[1].Lugar;

                    //Para ya poner directamente el numero aca si es el caso
                    if (this.Lugar == null || this.Lugar.Equals(string.Empty))
                    {
                        this.Lugar = this.hijosNodo[1].Valor.ToString();
                    }

                    //Si es mayor a 0 es pq es (EXPR) entonces lo pongo
                    this.EsOtraExpresion = true;

                    this.AsignaParametros = this.hijosNodo[1].AsignaParametros;
                }
            }
            else
            {
                this.Valor = this.hijosNodo[0].Valor;
                this.TipoDato = this.hijosNodo[0].TipoDato;
                this.Lexema = this.hijosNodo[0].Lexema;
                this.Temporal = this.hijosNodo[0].Temporal;

                this.Lugar = this.hijosNodo[0].Lugar;

                //Para ya poner directamente el numero aca si es el caso
                if (this.Lugar == null || this.Lugar.Equals(string.Empty))
                {
                    this.Lugar = this.hijosNodo[0].Valor.ToString();
                }

                //No es (EXPR) entonces lo pongo
                this.EsOtraExpresion = false;

                this.AsignaParametros = this.hijosNodo[0].AsignaParametros;
            }

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
            if (this.hijosNodo.Count > 1)
            {
                this.Codigo = this.hijosNodo[1].Codigo;  
            }
            else
            {                
                this.Codigo = this.hijosNodo[0].Codigo;                   
                
            }
        }
    }
}
