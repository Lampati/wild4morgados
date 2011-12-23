using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.Arbol.Temporales;
using Compilador.Semantico.TablaDeSimbolos;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoL: NodoArbolSemantico
    {
        public NodoL(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre, elem)
        {

        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {

        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo[0].GetType() == typeof(NodoTerminal))
            {
                this.Lexema = this.hijosNodo[0].Lexema.Replace("'", string.Empty);

                this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.NombreContextoLocal, this.ToString(), this.Lexema);
                this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, NodoTablaSimbolos.TipoDeDato.String, this.Temporal.Valor);

                this.Lugar = this.Temporal.Nombre;
            }
            else
            {
                if (this.hijosNodo[0].Lugar == null || this.hijosNodo[0].Lugar.Equals(string.Empty))
                {
                    this.Lugar = this.hijosNodo[0].Valor.ToString();
                }
                else
                {
                    this.Lugar = this.hijosNodo[0].Lugar;
                }                
            }

            this.ListaElementosVisualizar.Add(string.Copy(this.Lugar));

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

            if (this.hijosNodo[0].GetType() != typeof(NodoTerminal))
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
            }
            else
            {
                strBldr.Append(this.hijosNodo[0].Lexema);
            }


            this.Codigo = strBldr.ToString();
        }
    }
}
