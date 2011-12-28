using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
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

            NodoTablaSimbolos.TipoDeDato tipoExp = this.hijosNodo[2].TipoDato;

            NodoTablaSimbolos.TipoDeDato tipo;
            StringBuilder strbldr;


            if (!esArreglo)
            {
                if (this.TablaSimbolos.ExisteVariable(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    tipo = this.TablaSimbolos.ObtenerTipoVariable(nombre,this.ContextoActual,this.NombreContextoLocal);

                    if (tipo == tipoExp)
                    {
                        if (this.TablaSimbolos.EsModificableValorVarible(nombre, this.ContextoActual, this.NombreContextoLocal))
                        {

                            //esto es para agarrar que no se haga nada raro en el procedimiento salida
                            if (this.TablaSimbolos.EsParametroDeEsteProc(nombre, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.ModificaParametros = true;
                            }
                            this.AsignaParametros = this.hijosNodo[2].AsignaParametros;


                          
                        }
                        else
                        {
                            strbldr = new StringBuilder("La variable ").Append(nombre).Append(" esta declarada como una constante, no puede modificarse su valor.");
                            throw new ErrorSemanticoException(strbldr.ToString());
                        }
                    }
                    else
                    {
                        strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es del tipo ").Append(EnumUtils.stringValueOf(tipo));
                        strbldr.Append(" pero se le intento asignar el tipo ").Append(EnumUtils.stringValueOf(tipoExp));
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
                                      
                }
                else
                {
                    if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.NombreContextoLocal))
                    {
                        strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es un arreglo. Debe usar un indice para asignarle el contenido");
                        strbldr.Append(" a una de sus posiciones. No se puede asignar el contenido total de un arreglo a otro. ");
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
                    else
                    {
                        strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
                }
            }
            else
            {
                if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    //if (this.TablaSimbolos.ExisteArreglo(nombre, indice))
                    //{
                    tipo = this.TablaSimbolos.ObtenerTipoArreglo(nombre, this.ContextoActual, this.NombreContextoLocal);

                        if (tipo == tipoExp)
                        {
                            //this.TablaSimbolos.ModificarValorPosicionArreglo(nombre, indice, valorExp);

                            //esto es para agarrar que no se haga nada raro en el procedimiento salida
                            if (this.TablaSimbolos.EsParametroDeEsteProc(nombre, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.ModificaParametros = true;
                            }
                            this.AsignaParametros = this.hijosNodo[2].AsignaParametros;                          
                        }
                        else
                        {
                            strbldr = new StringBuilder("El arreglo ").Append(nombre).Append(" es del tipo ").Append(EnumUtils.stringValueOf(tipo));
                            strbldr.Append(" pero se le intento asignar el tipo ").Append(EnumUtils.stringValueOf(tipoExp));
                            throw new ErrorSemanticoException(strbldr.ToString());
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

            strBldr.Append(this.hijosNodo[0].Codigo);
            strBldr.Append(" := ");
            strBldr.Append(this.hijosNodo[2].Codigo);
            strBldr.Append(";");

            this.Codigo = strBldr.ToString();

        }


    }
}
