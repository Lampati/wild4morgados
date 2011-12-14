using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Semantico.Arbol.Temporales;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoMult: NodoArbolSemantico
    {
        public NodoMult(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            //Hago la operacion igual, si los tipos no eran iguales, simplemente tiro la excepcion.
            //Por defecto uso el tipo del primer hijo para asignar el tipo de este nodo.

            if (this.hijosNodo.Count > 1)
            {
                int valor1 = this.hijosNodo[1].Valor;
                int valor2 = this.hijosNodo[2].Valor;
                

                this.Operacion = this.hijosNodo[0].Operacion;

                this.TipoDato = this.hijosNodo[0].TipoDato;

                if (this.Operacion != TipoOperatoria.Ninguna)
                {

                    switch (this.Operacion)
                    {
                        case TipoOperatoria.Multiplicacion:
                            //this.Valor = valor1 * valor2;
                            break;

                        case TipoOperatoria.Division:
                            //this.Valor = valor1 / valor2;
                            break;
                    }

                    //this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
                    //this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, this.TipoDato);

                    //this.Lugar = this.Temporal.Nombre;

                    this.Lugar = this.hijosNodo[1].Lugar;


                
                }
                else
                {
                    this.Valor = valor1;

                    this.Lexema = this.hijosNodo[0].Lexema;
                    this.Temporal = this.hijosNodo[0].Temporal;

                    this.Lugar = this.hijosNodo[1].Lugar;
                    

                    this.TipoDato = this.hijosNodo[0].TipoDato;
                }
            }
            else
            {
                this.Operacion = TipoOperatoria.Ninguna;
            }

            return this;
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            
            
        }

        public override void ChequearAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                NodoTablaSimbolos.TipoDeDato tipo1 = this.hijosNodo[0].TipoDato;
                NodoTablaSimbolos.TipoDeDato tipo2 = this.hijosNodo[1].TipoDato;
                NodoTablaSimbolos.TipoDeDato tipo3 = this.hijosNodo[2].TipoDato;

                if (tipo3 != NodoTablaSimbolos.TipoDeDato.Ninguno)
                {
                    if (!((tipo1 == tipo2) && (tipo2 == tipo3)))
                    {
                        StringBuilder strbldr = new StringBuilder("Se esta intentando operar con distintos tipos");
                        throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                    }
                }
                else
                {
                    if (tipo1 != tipo2)
                    {
                        StringBuilder strbldr = new StringBuilder("Se esta intentando operar con distintos tipos");
                        throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                    }
                }

                //this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
                //this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, tipo2);
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            //Se podria probar con poner ninguna operacion asi no sigue arrastrando errores de tipo??

            return this;
        }


        public override void CalcularExpresiones()
        {
            if (this.hijosNodo.Count > 1)
            {
                if (this.hijosNodo[2].Operacion != TipoOperatoria.Ninguna)
                {
                    this.hijosNodo[2].LugarExp = this.LugarExp;
                    this.hijosNodo[2].LugarMul = this.LugarMul;
                }
            }
        }

        public override void CalcularCodigo()
        {
            


            StringBuilder strBldr = new StringBuilder();

            

            this.Codigo = strBldr.ToString();
        }
    }
}
