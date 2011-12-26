using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Auxiliares;


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

            NodoTablaSimbolos.TipoDeDato tipo;
            StringBuilder strbldr;

            if (!esArreglo)
            {
                if (this.TablaSimbolos.ExisteVariable(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    tipo = this.TablaSimbolos.ObtenerTipoVariable(nombre, this.ContextoActual, this.NombreContextoLocal);

                    
                    if (this.TablaSimbolos.EsModificableValorVarible(nombre,this.ContextoActual,this.NombreContextoLocal))
                    {                    

                            

                        if (tipo == NodoTablaSimbolos.TipoDeDato.Booleano)
                        {
                            strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es booleana, no se le puede asignar un valor desde el teclado, porque sus unicos valores son verdadero y falso.");
                            throw new ErrorSemanticoException(strbldr.ToString());
                        }
               
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

                       

                        this.Lexema = nombre;
                        

                        if (tipo == NodoTablaSimbolos.TipoDeDato.Booleano)
                        {
                            strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es booleana, no se le puede asignar un valor desde el teclado, porque sus unicos valores son verdadero y falso.");
                            throw new ErrorSemanticoException(strbldr.ToString());
                        }
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
