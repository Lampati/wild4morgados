using System;
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
            this.TieneLecturas = true;
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
                    tipo = this.TablaSimbolos.ObtenerTipoVariable(nombre, this.ContextoActual, this.NombreContextoLocal);

                    
                    if (this.TablaSimbolos.EsModificableValorVarible(nombre,this.ContextoActual,this.NombreContextoLocal))
                    {
                        strbldr = new StringBuilder().Append("LECTURA: Uso en parte izquierda de variable ");
                        strbldr.Append(EnumUtils.stringValueOf(this.TablaSimbolos.ObtenerContextoVariable(nombre, this.ContextoActual, this.NombreContextoLocal)));
                        strbldr.Append(" ").Append(nombre);

                        this.TextoParaImprimirArbol = strbldr.ToString();

                        string nombreContexto = this.TablaSimbolos.ObtenerNombreContextoVariable(nombre,this.ContextoActual,this.NombreContextoLocal);
                            
                        this.Lugar = new StringBuilder(nombreContexto).Append(nombre).ToString();
               
                        //this.TablaSimbolos.ModificarValorVarible(nombre, int.MinValue);
                    }
                    else
                    {
                        strbldr = new StringBuilder("La variable ").Append(nombre).Append(" esta declarada como una constante, no puede modificarse su valor.");
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
                    
                    //else
                    //{
                    //    strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es del tipo ").Append(EnumUtils.stringValueOf(tipo));
                    //    strbldr.Append(" pero la funcion leer lee solo enteros.");
                    //    throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                    //}
                }
                else
                {
                    strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }
            }
            else
            {
                if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    //if (this.TablaSimbolos.ExisteArreglo(nombre, indice))
                    //{
                        tipo = this.TablaSimbolos.ObtenerTipoArreglo(nombre);

                        //this.TablaSimbolos.ModificarValorPosicionArreglo(nombre,indice,int.MinValue);
                        strbldr = new StringBuilder().Append("LECTURA: Uso en parte izquierda de arreglo Global");
                        strbldr.Append(" ").Append(nombre);

                        this.TextoParaImprimirArbol = strbldr.ToString();

                        this.Lexema = nombre;
                        this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.NombreContextoLocal, this.ToString());
                        this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, NodoTablaSimbolos.TipoDeDato.Numero);

                        this.Lugar = this.Temporal.Nombre;
                        
                        //else
                        //{
                        //    strbldr = new StringBuilder("El arreglo ").Append(nombre).Append(" es del tipo ").Append(EnumUtils.stringValueOf(tipo));
                        //    strbldr.Append(" pero la funcion leer lee solo enteros.");
                        //    throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                        //}
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
                    throw new ErrorSemanticoException(strbldr.ToString());
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
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("Readln ");
            strBldr.Append("( ");
            strBldr.Append(this.hijosNodo[1].Codigo);
            strBldr.Append(") ");
            strBldr.Append(";");

            this.Codigo = strBldr.ToString();
        }
    }
}
