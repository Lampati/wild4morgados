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
                if (this.TablaSimbolos.ExisteArreglo(nombre))
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
            StringBuilder strBldr = new StringBuilder("\t");

            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("------COMIENZO ASIGNACION-----"));

            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("------COMIENZO PARTEIZQ-----"));
            strBldr.Append(this.hijosNodo[0].Codigo);
            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("------FINAL PARTEIZQ-----"));

            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("------COMIENZO PARTEDER-----"));
            string codigoHijo2 = this.hijosNodo[2].Codigo;

            if (!String.IsNullOrEmpty(codigoHijo2))
            {
                strBldr.Append(GeneracionCodigoHelpers.GenerarPush("AX"));
                strBldr.Append(this.hijosNodo[2].Codigo);
                strBldr.Append(GeneracionCodigoHelpers.GenerarPop("AX"));
            }

            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("------FINAL PARTEDER-----"));


            strBldr.Append(GeneracionCodigoHelpers.GenerarPush("Ax"));

            if (this.hijosNodo[2].Lugar == null || this.hijosNodo[2].Lugar.Equals(string.Empty))
            {
                if (this.hijosNodo[0].EsArreglo)
                {
                    strBldr.Append(GeneracionCodigoHelpers.AssignArray(this.hijosNodo[0].Lugar,this.hijosNodo[2].Valor.ToString()));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarPop("AX"));
                }
                else
                {
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMovHaciaAx(this.hijosNodo[2].Valor.ToString()));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMovDesdeAx(this.hijosNodo[0].Lugar));
                }


            }
            else
            {
                if (this.hijosNodo[0].EsArreglo)
                {
                    strBldr.Append(GeneracionCodigoHelpers.AssignArray(this.hijosNodo[0].Lugar, this.hijosNodo[2].Lugar));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarPop("AX"));
                }
                else
                {
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMovHaciaAx(this.hijosNodo[2].Lugar));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMovDesdeAx(this.hijosNodo[0].Lugar));
                }
            }

            strBldr.Append(GeneracionCodigoHelpers.GenerarPop("Ax"));

            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("------FINAL ASIGNACION-----"));


            this.Codigo = strBldr.ToString().Replace("\r\n", "\r\n\t").ToString().TrimEnd('\t');

        }


    }
}
