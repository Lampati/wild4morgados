﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Auxiliares;
using Compilador.Semantico.Arbol.Temporales;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoLectura : NodoArbolSemantico
    {
        public NodoLectura(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre, elem)
        {

        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {

        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            string nombre = this.hijosNodo[1].Lexema;
            bool esArreglo = this.hijosNodo[1].EsArreglo;
            int indice = this.hijosNodo[1].IndiceArreglo;

            NodoTablaSimbolos.TipoDeDato tipo;
            StringBuilder strbldr;

            if (!esArreglo)
            {
                if (this.TablaSimbolos.ExisteVariable(nombre))
                {
                    tipo = this.TablaSimbolos.ObtenerTipoVariable(nombre, this.ContextoActual, this.nombreContextoLocal);

                    if (tipo == NodoTablaSimbolos.TipoDeDato.Numero)
                    {
                        if (this.TablaSimbolos.EsModificableValorVarible(nombre,this.ContextoActual,this.nombreContextoLocal))
                        {
                            strbldr = new StringBuilder().Append("LECTURA: Uso en parte izquierda de variable ");
                            strbldr.Append(EnumUtils.stringValueOf(this.TablaSimbolos.ObtenerContextoVariable(nombre, this.ContextoActual, this.nombreContextoLocal)));
                            strbldr.Append(" ").Append(nombre);

                            this.TextoParaImprimirArbol = strbldr.ToString();

                            string nombreContexto = this.TablaSimbolos.ObtenerNombreContextoVariable(nombre,this.ContextoActual,this.nombreContextoLocal);
                            
                            this.Lugar = new StringBuilder(nombreContexto).Append(nombre).ToString();
               
                            //this.TablaSimbolos.ModificarValorVarible(nombre, int.MinValue);
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
                        strbldr.Append(" pero la funcion leer lee solo enteros.");
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

                        if (tipo == NodoTablaSimbolos.TipoDeDato.Numero)
                        {
                            //this.TablaSimbolos.ModificarValorPosicionArreglo(nombre,indice,int.MinValue);
                            strbldr = new StringBuilder().Append("LECTURA: Uso en parte izquierda de arreglo Global");
                            strbldr.Append(" ").Append(nombre);

                            this.TextoParaImprimirArbol = strbldr.ToString();

                            this.Lexema = nombre;
                            this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
                            this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, NodoTablaSimbolos.TipoDeDato.Numero);

                            this.Lugar = this.Temporal.Nombre;
                        }
                        else
                        {
                            strbldr = new StringBuilder("El arreglo ").Append(nombre).Append(" es del tipo ").Append(EnumUtils.stringValueOf(tipo));
                            strbldr.Append(" pero la funcion leer lee solo enteros.");
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
            StringBuilder strBldr = new StringBuilder("\t");
            strBldr.Append(this.hijosNodo[1].Codigo);

            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("------COMIENZO LECTURA-----"));
            strBldr.Append(GeneracionCodigoHelpers.GenerarPush("Ax"));

            strBldr.Append(GeneracionCodigoHelpers.GenerarPush("0000h"));


            strBldr.Append(GeneracionCodigoHelpers.GenerarPush("0001h"));
            strBldr.Append(GeneracionCodigoHelpers.GenerarCall("readln"));
            strBldr.Append(GeneracionCodigoHelpers.GenerarPop("Ax"));
            strBldr.Append(GeneracionCodigoHelpers.GenerarMovDesdeAx(this.Lugar));           

            strBldr.Append(GeneracionCodigoHelpers.GenerarPop("Ax"));
            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("--------FINAL LECTURA--------"));

            this.Codigo = strBldr.ToString().Replace("\r\n", "\r\n\t").ToString().TrimEnd('\t');
        }
    }
}
