using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Semantico.Arbol.Nodos.Auxiliares;
using Compilador.Auxiliares;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoV : NodoArbolSemantico
    {
        public NodoV(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            List<Variable> variables = this.hijosNodo[0].VariablesACrear;
            NodoTablaSimbolos.TipoDeDato tipo = this.hijosNodo[2].TipoDato;

            StringBuilder textoParaArbol = new StringBuilder();
         
          
            foreach(Variable v in variables)
            {

                if (!v.EsArreglo)
                {
                    if (!this.TablaSimbolos.ExisteVariable(v.Lexema, this.ContextoActual, this.nombreContextoLocal))
                    {
                        this.TablaSimbolos.AgregarVariable(v.Lexema, tipo, this.EsConstante, this.ContextoActual, this.nombreContextoLocal);
                        textoParaArbol.Append("Declaracion de variable ").Append(v.Lexema).Append(" ").Append(EnumUtils.stringValueOf(this.ContextoActual));
                        textoParaArbol.Append(" de tipo ").Append(EnumUtils.stringValueOf(tipo));

                    }
                    else
                    {
                        throw new ErrorSemanticoException(new StringBuilder("La variable ").Append(v.Lexema).Append(" ya existia en ese contexto").ToString(),
                            t.Componente.Fila, t.Componente.Columna);
                    }
                }
                else
                {
                    if (this.ContextoActual == NodoTablaSimbolos.TipoContexto.Global)
                    {
                        if (!this.TablaSimbolos.ExisteArreglo(v.Lexema))
                        {
                            this.TablaSimbolos.AgregarArreglo(v.Lexema, tipo, v.IndiceArreglo, false);
                            textoParaArbol.Append("Declaracion de arreglo ").Append(v.Lexema).Append(" ").Append(EnumUtils.stringValueOf(this.ContextoActual));
                            textoParaArbol.Append(" de tipo ").Append(EnumUtils.stringValueOf(tipo));
                            textoParaArbol.Append(" de ").Append(v.IndiceArreglo.ToString()).Append(" posiciones.");
                        }
                        else
                        {
                            throw new ErrorSemanticoException(new StringBuilder("El arreglo ").Append(v.Lexema).Append(" ya existia").ToString(),
                                t.Componente.Fila, t.Componente.Columna);
                        }
                    }
                    else
                    {
                        StringBuilder strbldr = new StringBuilder("No se pueden declarar arreglos fuera del contexto global");
                        throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                    }
                }
            }

            this.TextoParaImprimirArbol = textoParaArbol.ToString();

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
            return this;
        }
    }
}
