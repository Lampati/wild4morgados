using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Auxiliares;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoC : NodoArbolSemantico
    {
        public NodoC(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            string nombre = this.hijosNodo[0].Lexema;
            NodoTablaSimbolos.TipoDeDato tipo = this.hijosNodo[2].TipoDato;
            int valor = this.hijosNodo[4].Valor;                       

            if (!this.TablaSimbolos.ExisteVariable(nombre, this.ContextoActual, this.nombreContextoLocal))
            {
                if (tipo != this.hijosNodo[4].TipoDato)
                {
                    throw new ErrorSemanticoException(new StringBuilder("Se intento asignar un tipo invalido a ").Append(nombre).ToString(),
                        t.Componente.Fila, t.Componente.Columna);
                }

                this.TablaSimbolos.AgregarVariable(nombre, tipo, this.EsConstante, this.ContextoActual, this.nombreContextoLocal, valor);

                StringBuilder textoParaArbol = new StringBuilder().Append("Declaracion de constante ").Append(nombre).Append(" ").Append(EnumUtils.stringValueOf(this.ContextoActual));
                textoParaArbol.Append(" de tipo ").Append(EnumUtils.stringValueOf(tipo));
                textoParaArbol.Append(" con valor ").Append(valor.ToString());

                this.TextoParaImprimirArbol = textoParaArbol.ToString();
            
            }
            else
            {                
                throw new ErrorSemanticoException(new StringBuilder("La variable ").Append(nombre).Append(" ya existia en ese contexto").ToString(),
                    t.Componente.Fila,t.Componente.Columna);
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
            string nombre = this.hijosNodo[0].Lexema;
            NodoTablaSimbolos.TipoDeDato tipo = this.hijosNodo[2].TipoDato;
            int valor = this.hijosNodo[4].Valor;

            if (!this.TablaSimbolos.ExisteVariable(nombre, this.ContextoActual, this.nombreContextoLocal))
            {
                this.TablaSimbolos.AgregarVariable(nombre, tipo, this.EsConstante, this.ContextoActual, this.nombreContextoLocal);
            }

            return this;
        }
    }
}
