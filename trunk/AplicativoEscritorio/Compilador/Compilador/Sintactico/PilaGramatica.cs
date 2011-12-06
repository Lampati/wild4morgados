using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Lexicografico;
using Compilador.Semantico;
using Compilador.Semantico.Arbol;

namespace Compilador.Sintactico
{
    class PilaGramatica
    {
        private List<ElementoGramatica> elementosPila;


        public ArbolSemantico ArbolSemantico { get; set; }

        public PilaGramatica(NoTerminal simboloInicial)
        {
            this.elementosPila = new List<ElementoGramatica>();

            this.elementosPila.Add(Terminal.ElementoEOF());
            this.elementosPila.Add(simboloInicial);

            
            
        }

        public ElementoGramatica ObtenerTope()
        {
            return this.elementosPila.Last();
        }

        public void InsertarElemento(ElementoGramatica elem)
        {
            this.elementosPila.Add(elem);
            //this.elementosPila.Insert(this.elementosPila.Count - 1, elem);
        }

        public void DescartarTope()
        {
            this.elementosPila.RemoveAt(this.elementosPila.Count - 1);
        }

        public void TransformarProduccion(Produccion prod)
        {
            if (prod.Izq.Equals(this.ObtenerTope()))
            {
                this.DescartarTope();

                //Comentado hasta la parte semantica
                //this.PilaAtributos.InsertarElemento(new NodoPilaProduccionesAtributos(prod));
                this.ArbolSemantico.AgregarHijosNodoActual(prod);
                

                if (prod.Der != null)
                {
                    for (int i = prod.Der.Count - 1; i >= 0; i--)
                    {
                        this.InsertarElemento(prod.Der[i]);
                    }

                    //foreach(ElementoGramatica elem in prod.Der)
                    //{
                    //    this.InsertarElemento(elem);
                    //}
                }
            }
        }


        internal bool esVacia()
        {
            return this.elementosPila.Count == 0;
        }

        internal bool esFinDePila()
        {
            if (!esVacia())
            {
                if (this.ObtenerTope().GetType() == typeof(Terminal))
                {
                    Terminal t = (Terminal)this.ObtenerTope();
                    return t.Equals(Terminal.ElementoEOF());                    
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            StringBuilder strBldr = new StringBuilder(string.Empty);

            for (int i = this.elementosPila.Count - 1; i >= 0; i--)
            {
                strBldr.Append(this.elementosPila[i].ToString());
                strBldr.Append(" ");
            }

            return strBldr.ToString();
        }
    }
}
