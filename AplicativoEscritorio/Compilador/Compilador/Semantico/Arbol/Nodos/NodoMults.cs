﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Semantico.Arbol.Temporales;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoMults: NodoArbolSemantico
    {
        public NodoMults(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            //Hago la operacion igual, si los tipos no eran iguales, simplemente tiro la excepcion.
            //Por defecto uso el tipo1 para asignar el tipo de este nodo.

            int valor1 = this.hijosNodo[0].Valor;
            int valor2 = this.hijosNodo[1].Valor;
            TipoOperatoria operacion = this.hijosNodo[1].Operacion;

            this.TipoDato = this.hijosNodo[0].TipoDato;

            if (operacion != TipoOperatoria.Ninguna)
            {
   
                switch (operacion)
                {
                    case TipoOperatoria.Multiplicacion:
                        this.Valor = valor1 * valor2;
                        break;

                    case TipoOperatoria.Division:
                        this.Valor = valor1 / valor2;
                        break;
                }

                this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
                //this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, this.TipoDato);
                this.Lugar = this.Temporal.Nombre;
            
            }
            else
            {
                this.Valor = valor1;

                this.Lexema = this.hijosNodo[0].Lexema;
                this.Temporal = this.hijosNodo[0].Temporal;
                this.TipoDato = this.hijosNodo[0].TipoDato;

                this.Lugar = this.hijosNodo[0].Lugar;
            }

           

            return this;
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            
            
        }

        public override void ChequearAtributos(Terminal t)
        {
            TipoOperatoria operacion = this.hijosNodo[1].Operacion;

            NodoTablaSimbolos.TipoDeDato tipo1 = this.hijosNodo[0].TipoDato;        
            NodoTablaSimbolos.TipoDeDato tipo2 = this.hijosNodo[1].TipoDato;

            if (operacion != TipoOperatoria.Ninguna)
            {
                if (tipo1 != tipo2)
                {
                    StringBuilder strbldr = new StringBuilder("Se esta intentando operar con distintos tipos");
                    throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                }
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void CalcularExpresiones()
        {
            if (this.hijosNodo[1].Operacion != TipoOperatoria.Ninguna)
            {
                //this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
                this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, this.TipoDato);
                this.LugarMul = this.Temporal.Nombre;

                this.hijosNodo[1].LugarMul = this.LugarMul;
                this.hijosNodo[1].LugarExp = this.LugarExp;
            }
            else
            {
                this.LugarMul = this.Lugar;
                //this.LugarMul = this.hijosNodo[0].Lugar;
            }
        }

        public override void CalcularCodigo()
        {
            //StringBuilder strBldr = new StringBuilder();

            //strBldr.Append(this.hijosNodo[0].Codigo);
            //strBldr.Append(this.hijosNodo[1].Codigo);

            //if (this.hijosNodo[1].Operacion != TipoOperatoria.Ninguna)
            //{
            //    switch (this.hijosNodo[1].Operacion)
            //    {
            //        case TipoOperatoria.Multiplicacion:
            //            if (this.TipoDato == NodoTablaSimbolos.TipoDeDato.Entero)
            //            {
            //                strBldr.Append(GeneracionCodigoHelpers.GenerarMultiplicacion(this.Lugar, this.hijosNodo[0].Lugar, this.hijosNodo[1].Lugar));
            //            }
            //            else if (this.TipoDato == NodoTablaSimbolos.TipoDeDato.Natural)
            //            {
            //                strBldr.Append(GeneracionCodigoHelpers.GenerarMultiplicacionNatural(this.Lugar, this.hijosNodo[0].Lugar, this.hijosNodo[1].Lugar));
            //            }
            //            break;

            //        case TipoOperatoria.Division:
            //            if (this.TipoDato == NodoTablaSimbolos.TipoDeDato.Entero)
            //            {
            //                strBldr.Append(GeneracionCodigoHelpers.GenerarDivision(this.Lugar, this.hijosNodo[0].Lugar, this.hijosNodo[1].Lugar));
            //            }
            //            else if (this.TipoDato == NodoTablaSimbolos.TipoDeDato.Natural)
            //            {
            //                strBldr.Append(GeneracionCodigoHelpers.GenerarDivisionNatural(this.Lugar, this.hijosNodo[0].Lugar, this.hijosNodo[1].Lugar));
            //            }
            //            break;
            //    }
            //}

            //this.Codigo = strBldr.ToString();


            StringBuilder strBldr = new StringBuilder();

            strBldr.Append(this.hijosNodo[0].Codigo);

            if (this.hijosNodo[1].Operacion != TipoOperatoria.Ninguna)
            {
                if (this.hijosNodo[1].ObtenerHijo(1).EsOtraExpresion)
                {
                    strBldr.Append(this.hijosNodo[1].Codigo);
                }

                switch (this.hijosNodo[1].Operacion)
                {
                    case TipoOperatoria.Multiplicacion:
                        if (this.TipoDato == NodoTablaSimbolos.TipoDeDato.Entero)
                        {
                            strBldr.Append(GeneracionCodigoHelpers.GenerarMultiplicacion(this.LugarMul, this.hijosNodo[0].Lugar, this.hijosNodo[1].Lugar));
                        }
                        else if (this.TipoDato == NodoTablaSimbolos.TipoDeDato.Natural)
                        {
                            strBldr.Append(GeneracionCodigoHelpers.GenerarMultiplicacionNatural(this.LugarMul, this.hijosNodo[0].Lugar, this.hijosNodo[1].Lugar));
                        }
                        break;

                    case TipoOperatoria.Division:
                        if (this.TipoDato == NodoTablaSimbolos.TipoDeDato.Entero)
                        {
                            strBldr.Append(GeneracionCodigoHelpers.GenerarDivision(this.LugarMul, this.hijosNodo[0].Lugar, this.hijosNodo[1].Lugar));
                        }
                        else if (this.TipoDato == NodoTablaSimbolos.TipoDeDato.Natural)
                        {
                            strBldr.Append(GeneracionCodigoHelpers.GenerarDivisionNatural(this.LugarMul, this.hijosNodo[0].Lugar, this.hijosNodo[1].Lugar));
                        }
                        break;
                }

                if (!this.hijosNodo[1].ObtenerHijo(1).EsOtraExpresion)
                {
                    strBldr.Append(this.hijosNodo[1].Codigo);
                }
            }
            else
            {
                strBldr.Append(this.hijosNodo[1].Codigo);
            }
            

            

            this.Codigo = strBldr.ToString();
        }
    }
}
