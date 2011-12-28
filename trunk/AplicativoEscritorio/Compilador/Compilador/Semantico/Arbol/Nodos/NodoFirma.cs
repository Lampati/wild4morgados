using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoFirma : NodoArbolSemantico
    {
        public NodoFirma(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            List<Firma> listaFirmas = this.hijosNodo[0].ListaFirma;
            this.ListaFirma = listaFirmas;

            if (listaFirmas.Count != new List<Firma>(listaFirmas.Distinct()).Count)
            {
                throw new ErrorSemanticoException(new StringBuilder("Existen parametros con el mismo nombre").ToString());
            }

            foreach (Firma f in listaFirmas)
            {
                if (!f.EsArreglo)
                {
                    this.TablaSimbolos.AgregarParametroDeProc(f.Lexema, f.Tipo, this.ContextoActual, this.NombreContextoLocal);
                }
                else
                {
                    this.TablaSimbolos.AgregarArregloParametroDeProc(f.Lexema, f.Tipo, this.ContextoActual, this.NombreContextoLocal);
                }
            }

            


            return this;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
                
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
                
        }

        public override void ChequearAtributos(Terminal t)
        {
            
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            this.ListaFirma = new List<Firma>(this.hijosNodo[0].ListaFirma.Distinct());

            foreach (Firma f in this.ListaFirma)
            {
                if (!f.EsArreglo)
                {
                    this.TablaSimbolos.AgregarParametroDeProc(f.Lexema, f.Tipo, this.ContextoActual, this.NombreContextoLocal);
                }
                else
                {
                    this.TablaSimbolos.AgregarArregloParametroDeProc(f.Lexema, f.Tipo, this.ContextoActual, this.NombreContextoLocal);
                }
            }

            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();
            strBldr.Append(this.hijosNodo[0].Codigo);
            this.Codigo = strBldr.ToString();
        }
    }
}
