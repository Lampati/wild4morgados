﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Semantico.Arbol.Temporales;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoExp: NodoArbolSemantico
    {
        public NodoExp(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
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
                TipoOperatoria operacion = this.hijosNodo[2].Operacion;

                this.Operacion = this.hijosNodo[0].Operacion;

                this.TipoDato = this.hijosNodo[0].TipoDato;

                this.AsignaParametros = this.hijosNodo[1].AsignaParametros || this.hijosNodo[2].AsignaParametros;

                if (operacion != TipoOperatoria.Ninguna)
                {                   

                    //this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
                    //this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, this.TipoDato);

                    //this.Lugar = string.Copy(this.Temporal.Nombre);

                    this.Lugar = string.Copy(this.hijosNodo[1].Lugar);
                }
                else
                {
                    

                    this.Lexema = this.hijosNodo[0].Lexema;
                    this.Temporal = this.hijosNodo[0].Temporal;

                    

                    this.Lugar = string.Copy(this.hijosNodo[1].Lugar);                    
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
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
                }
                else
                {
                    if (tipo1 != tipo2)
                    {
                        StringBuilder strbldr = new StringBuilder("Se esta intentando operar con distintos tipos");
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
                }

                //this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal,this.ToString());
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
                }
            }
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
                strBldr.Append(this.hijosNodo[1].Codigo);
                strBldr.Append(this.hijosNodo[2].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
