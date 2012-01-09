﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoMaxArregloDec: NodoArbolSemantico
    {
        public NodoMaxArregloDec(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }


        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

    

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Numero ||
                t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Literal ||
                t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Verdadero ||
                t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Falso                 
                )

            {
                if (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Numero)
                {
                    int valor = t.ObtenerValor();
                    if (valor > 0)
                    {

                        this.RangoArreglo = valor.ToString();
                    }
                    else
                    {
                        throw new ErrorSemanticoException(new StringBuilder("El numero ").Append(valor.ToString()).Append(" no es mayor a 0. Solo se pueden usar constantes o numeros mayores a 0 al especificar el rango de los arreglos. ").ToString());                        
                    }
                }
                else
                {
                    throw new ErrorSemanticoException(new StringBuilder("").Append(t.Componente.Lexema).Append(" no es del tipo numero. Solo se pueden usar constantes numericas o numeros al especificar el rango de los arreglos. ").ToString());
                    
                }
            }
            else
            {

                string lexema = t.Componente.Lexema;

                if (this.TablaSimbolos.ExisteVariable(lexema, this.ContextoActual, this.NombreContextoLocal))
                {
                    if (!this.TablaSimbolos.EsModificableValorVarible(lexema, this.ContextoActual, this.NombreContextoLocal))
                    {
                        if (this.TablaSimbolos.ObtenerTipoVariable(lexema, this.ContextoActual, this.NombreContextoLocal) == NodoTablaSimbolos.TipoDeDato.Numero)
                        {
                            if (this.TablaSimbolos.RetornarValorConstante(lexema, this.ContextoActual, this.NombreContextoLocal) > 0)
                            {

                                this.RangoArreglo = lexema;
                            }
                            else
                            {
                                throw new ErrorSemanticoException(new StringBuilder("La constante numerica ").Append(lexema).Append(" no es mayor a 0. Solo se pueden usar constantes o numeros mayores a 0 al especificar el rango de los arreglos. ").ToString());
                            }
                        }
                        else
                        {
                            throw new ErrorSemanticoException(new StringBuilder("La constante ").Append(lexema).Append(" no es del tipo numero. Solo se pueden usar constantes numericas o numeros al especificar el rango de los arreglos. ").ToString());
                        }
                    }
                    else
                    {
                        throw new ErrorSemanticoException(new StringBuilder("La variable ").Append(lexema).Append(" no es una constante. Solo se pueden usar constantes o numeros al especificar el rango de los arreglos. ").ToString());
                    }
                }
                else
                {
                    throw new ErrorSemanticoException(new StringBuilder("La variable ").Append(lexema).Append(" no ya existia ").ToString());
                }
            }

            return this;
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

            strBldr.Append(this.hijosNodo[0].Lexema);   

            this.Codigo = strBldr.ToString();
        }
    }
}
