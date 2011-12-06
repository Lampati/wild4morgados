using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoY: NodoArbolSemantico
    {
        public NodoY(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }


        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {            
            StringBuilder strbldr;

            this.EsFuncion = false;
            this.EsArreglo = false;

            if (this.hijosNodo.Count > 1)
            {
                string tipoEntrada = this.hijosNodo[0].Lexema;

                this.Lugar = this.hijosNodo[1].Lugar;

                switch (tipoEntrada)
                {
                    case "(":
                        this.EsFuncion = true;
                        this.ListaFirma = this.hijosNodo[1].ListaFirma;
                        break;

                    case "[":
                        this.EsArreglo = true;
                        this.IndiceArreglo = this.hijosNodo[1].Valor;
                        this.TipoDato = this.hijosNodo[1].TipoDato;
                        break;
                }
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
                if (this.TipoDato != NodoTablaSimbolos.TipoDeDato.Natural)
                {
                    throw new ErrorSemanticoException(new StringBuilder("El subindice del arreglo debe ser natural.").ToString(),
                        t.Componente.Fila, t.Componente.Columna);
                }
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {            
            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[1].Codigo);

                if (this.EsArreglo)
                {
                    strBldr.Append(GeneracionCodigoHelpers.GenerarPush("Cx"));

                    if (this.Lugar == null || this.Lugar.Equals(string.Empty))
                    {
                        //this.Codigo = "uso el valor " + this.Valor.ToString() + "\r\n";
                        strBldr.Append(GeneracionCodigoHelpers.GenerarMov("Cx", this.Valor.ToString()));
                    }
                    else
                    {
                        //this.Codigo = "obtengo el valor del temp: " + this.Lugar + "\r\n";
                        strBldr.Append(GeneracionCodigoHelpers.GenerarMov("Cx", this.Lugar));
                    } 
                }              
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
