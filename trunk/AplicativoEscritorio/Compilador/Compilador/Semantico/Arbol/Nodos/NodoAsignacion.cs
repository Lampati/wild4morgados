using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Auxiliares;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoAsignacion: NodoArbolSemantico
    {
        public NodoAsignacion(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            string nombre = this.hijosNodo[0].Lexema;
            bool esArreglo = this.hijosNodo[0].EsArreglo;
            int indice = this.hijosNodo[0].IndiceArreglo;

            int valorExp = this.hijosNodo[2].Valor;
            NodoTablaSimbolos.TipoDeDato tipoExp = this.hijosNodo[2].TipoDato;

            NodoTablaSimbolos.TipoDeDato tipo;
            StringBuilder strbldr;


            if (!esArreglo)
            {
                if (this.TablaSimbolos.ExisteVariable(nombre))
                {
                    tipo = this.TablaSimbolos.ObtenerTipoVariable(nombre,this.ContextoActual,this.nombreContextoLocal);

                    if (tipo == tipoExp)
                    {
                        if (this.TablaSimbolos.EsModificableValorVarible(nombre, this.ContextoActual, this.nombreContextoLocal))
                        {
                            //this.TablaSimbolos.ModificarValorVarible(nombre, valorExp);

                            strbldr = new StringBuilder().Append("ASIGNACION: Uso en parte izquierda de variable ");
                            strbldr.Append(EnumUtils.stringValueOf(this.TablaSimbolos.ObtenerContextoVariable(nombre,this.ContextoActual,this.nombreContextoLocal)));
                            strbldr.Append(" ").Append(nombre);

                            this.TextoParaImprimirArbol = strbldr.ToString();
                        }
                        else
                        {
                            strbldr = new StringBuilder("La variable ").Append(nombre).Append(" esta declarada como una constante, no puede modificarse su valor.");
                            throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                        }
                    }
                    else
                    {
                        strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es del tipo ").Append(EnumUtils.stringValueOf(tipo));
                        strbldr.Append(" pero se le intento asignar el tipo ").Append(EnumUtils.stringValueOf(tipoExp));
                        throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                    }
                }
                else
                {
                    strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");
                    throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                }
            }
            else
            {
                if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.nombreContextoLocal))
                {
                    //if (this.TablaSimbolos.ExisteArreglo(nombre, indice))
                    //{
                        tipo = this.TablaSimbolos.ObtenerTipoArreglo(nombre);

                        if (tipo == tipoExp)
                        {
                            //this.TablaSimbolos.ModificarValorPosicionArreglo(nombre, indice, valorExp);

                            strbldr = new StringBuilder().Append("ASIGNACION: Uso en parte izquierda de arreglo Global");
                            strbldr.Append(" ").Append(nombre);

                            this.TextoParaImprimirArbol = strbldr.ToString();
                        }
                        else
                        {
                            strbldr = new StringBuilder("El arreglo ").Append(nombre).Append(" es del tipo ").Append(EnumUtils.stringValueOf(tipo));
                            strbldr.Append(" pero se le intento asignar el tipo ").Append(EnumUtils.stringValueOf(tipoExp));
                            throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                        }
                    //}
                    //else
                    //{
                    //    strbldr = new StringBuilder("El subindice ").Append(indice).Append(" esta fuera del rango permitido para el arreglo ").Append(nombre).Append(".");
                    //    throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                    //}

                }
                else
                {
                    strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");
                    throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                }
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
            

        }


    }
}
