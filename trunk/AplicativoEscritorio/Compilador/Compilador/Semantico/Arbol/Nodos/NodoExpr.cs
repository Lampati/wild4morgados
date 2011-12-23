using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;
using System.Diagnostics;
using Compilador.Semantico.Arbol.Temporales;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoExpr : NodoArbolSemantico
    {
        public NodoExpr(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
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

            if (this.TipoDato == NodoTablaSimbolos.TipoDeDato.Ninguno)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
            }

            this.AsignaParametros = this.hijosNodo[0].AsignaParametros || this.hijosNodo[1].AsignaParametros;

            if (operacion != TipoOperatoria.Ninguna)
            {
                this.TipoDato = this.hijosNodo[0].TipoDato;
                switch (operacion)
                {
                    case TipoOperatoria.Suma:
                        this.Valor = valor1 + valor2;
                        break;

                    case TipoOperatoria.Resta:
                        this.Valor = valor1 - valor2;
                        break;
                }

                this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.NombreContextoLocal, this.ToString());
                this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, this.TipoDato);
                this.Lugar = string.Copy(this.Temporal.Nombre);
            
            }
            else
            {
                this.Valor = valor1;

                this.Lexema = this.hijosNodo[0].Lexema;
                //this.Temporal = this.hijosNodo[0].Temporal;

                this.Lugar = string.Copy(this.hijosNodo[0].Lugar);
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
                    throw new ErrorSemanticoException(strbldr.ToString());
                }
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void CalcularExpresiones()
        {            
            //this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
            //this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, this.TipoDato);
            this.LugarExp = this.Lugar;

            this.hijosNodo[1].LugarExp = this.LugarExp;
      
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();


            strBldr.Append(this.hijosNodo[0].Codigo);
            strBldr.Append(this.hijosNodo[1].Codigo);
           

            this.Codigo = strBldr.ToString();
        }


    }
}
