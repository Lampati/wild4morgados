using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
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

            if (DeclaracionesPermitidas == TipoDeclaracionesPermitidas.Variables)
            {

                foreach (Variable v in variables)
                {
                    if (!this.hijosNodo[2].EsArreglo)
                    //if (!v.EsArreglo)
                    {
                        if (!this.TablaSimbolos.ExisteVariableEnEsteContexto(v.Lexema, this.ContextoActual, this.NombreContextoLocal))
                        {
                            this.TablaSimbolos.AgregarVariable(v.Lexema, tipo, this.EsConstante, this.ContextoActual, this.NombreContextoLocal);
                            textoParaArbol.Append("Declaracion de variable ").Append(v.Lexema).Append(" ").Append(EnumUtils.stringValueOf(this.ContextoActual));
                            textoParaArbol.Append(" de tipo ").Append(EnumUtils.stringValueOf(tipo));

                        }
                        else
                        {
                            throw new ErrorSemanticoException(new StringBuilder("La variable ").Append(v.Lexema).Append(" ya existia en ese contexto").ToString());
                        }
                    }
                    else
                    {

                        if (!this.TablaSimbolos.ExisteArregloEnEsteContexto(v.Lexema, this.ContextoActual, this.NombreContextoLocal))
                        {
                            this.TablaSimbolos.AgregarArreglo(v.Lexema, tipo, this.ContextoActual, this.NombreContextoLocal, v.IndiceArreglo, false);
                            textoParaArbol.Append("Declaracion de arreglo ").Append(v.Lexema).Append(" ").Append(EnumUtils.stringValueOf(this.ContextoActual));
                            textoParaArbol.Append(" de tipo ").Append(EnumUtils.stringValueOf(tipo));
                            textoParaArbol.Append(" de ").Append(v.IndiceArreglo.ToString()).Append(" posiciones.");
                        }
                        else
                        {
                            throw new ErrorSemanticoException(new StringBuilder("El arreglo ").Append(v.Lexema).Append(" ya existia").ToString());
                        }
                    }
                }
            }
            else
            {
                throw new ErrorSemanticoException(new StringBuilder("No se permiten declarar variables aqui. Las variables deben ser creadas en el contexto global al principio del programa o en la zona de declaraciones de un procedimiento o funcion").ToString());
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
            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();


            strBldr.Append(this.hijosNodo[0].Codigo);
            strBldr.Append(" ");
            strBldr.Append(":");
            strBldr.Append(" ");
            strBldr.Append(this.hijosNodo[2].Codigo);
            

            this.Codigo = strBldr.ToString();
        }
    }
}
