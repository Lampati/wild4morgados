using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
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
                    this.TipoDato = this.hijosNodo[2].TipoDato;
                    this.Lexema = this.hijosNodo[2].Lexema;


                    this.EsArregloEnParametro = this.hijosNodo[2].EsArregloEnParametro;
                    //this.Lugar = this.hijosNodo[2].Lugar;

                    

                    if (this.hijosNodo[2].TipoDato != TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Booleano)
                    {                       
                        throw new ErrorSemanticoException("Unicamente se pueden negar expresiones booleanas");
                    }

                    this.AsignaParametros = this.hijosNodo[2].AsignaParametros;
                    this.UsaVariablesGlobales = this.hijosNodo[2].UsaVariablesGlobales;
                }
                else
                {

                    this.TipoDato = this.hijosNodo[1].TipoDato;
                    this.Lexema = this.hijosNodo[1].Lexema;
                    this.EsArregloEnParametro = this.hijosNodo[1].EsArregloEnParametro;

                   

                    this.AsignaParametros = this.hijosNodo[1].AsignaParametros;
                    this.UsaVariablesGlobales = this.hijosNodo[1].UsaVariablesGlobales;
                }
            }
            else
            {
                this.TipoDato = this.hijosNodo[0].TipoDato;
                this.Lexema = this.hijosNodo[0].Lexema;
                this.ValorConstanteNumerica = this.hijosNodo[0].ValorConstanteNumerica;

                this.EsArregloEnParametro = this.hijosNodo[0].EsArregloEnParametro;

              
                this.AsignaParametros = this.hijosNodo[0].AsignaParametros;
                this.UsaVariablesGlobales = this.hijosNodo[0].UsaVariablesGlobales;
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
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 3)
            {
                strBldr.Append("NOT ");
                strBldr.Append(" ( ");
                strBldr.Append(this.hijosNodo[2].Codigo);
                strBldr.Append(" ) ");
            }
            else if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(" ( ");
                strBldr.Append(this.hijosNodo[1].Codigo);
                strBldr.Append(" ) ");
            }
            else
            {
                strBldr.Append(" ").Append(this.hijosNodo[0].Codigo).Append(" ");
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
