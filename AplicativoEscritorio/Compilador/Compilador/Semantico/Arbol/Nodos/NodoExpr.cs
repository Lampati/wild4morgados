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

                this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
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
            //this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
            //this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, this.TipoDato);
            this.LugarExp = this.Lugar;

            this.hijosNodo[1].LugarExp = this.LugarExp;
      
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            //strBldr.Append(this.hijosNodo[0].Codigo);
            //strBldr.Append(this.hijosNodo[1].Codigo);

            //strBldr.Append(this.hijosNodo[1].Codigo);
            //strBldr.Append(this.hijosNodo[0].Codigo);

            //if (this.hijosNodo[1].Operacion != TipoOperatoria.Ninguna)
            //{
            //    switch (this.hijosNodo[1].Operacion)
            //    {
            //        case TipoOperatoria.Suma:
            //            strBldr.Append(GeneracionCodigoHelpers.GenerarSuma(this.Lugar, this.hijosNodo[0].Lugar, this.hijosNodo[1].Lugar));
            //            break;

            //        case TipoOperatoria.Resta:
            //            strBldr.Append(GeneracionCodigoHelpers.GenerarResta(this.Lugar, this.hijosNodo[0].Lugar, this.hijosNodo[1].Lugar));
            //            break;
            //    }
            //}

            //this.Codigo = strBldr.ToString();


            strBldr.Append(this.hijosNodo[0].Codigo);
            

            if (this.hijosNodo[1].Operacion != TipoOperatoria.Ninguna)
            {
                strBldr.Append(this.hijosNodo[1].ObtenerHijo(1).Codigo);

                //Para que no se le asigne null, en caso que sea un numero nomas.
                if (this.hijosNodo[1].ObtenerHijo(1).LugarMul == null || this.hijosNodo[1].ObtenerHijo(1).LugarMul == string.Empty)
                {
                    this.hijosNodo[1].ObtenerHijo(1).LugarMul = this.hijosNodo[1].ObtenerHijo(1).Lugar;
                }

                switch (this.hijosNodo[1].Operacion)
                {
                        

                    case TipoOperatoria.Suma:
                        strBldr.Append(GeneracionCodigoHelpers.GenerarSuma(this.LugarExp, this.hijosNodo[0].LugarMul, this.hijosNodo[1].ObtenerHijo(1).LugarMul));
                        break;

                    case TipoOperatoria.Resta:
                        strBldr.Append(GeneracionCodigoHelpers.GenerarResta(this.LugarExp, this.hijosNodo[0].LugarMul, this.hijosNodo[1].ObtenerHijo(1).LugarMul));
                        break;
                }
            }

            
            strBldr.Append(this.hijosNodo[1].Codigo);

            if (this.hijosNodo[1].Operacion == TipoOperatoria.Ninguna)
            {
                if (!(this.hijosNodo[0].LugarMul == null || this.hijosNodo[0].LugarMul == string.Empty || this.hijosNodo[0].LugarMul == this.LugarExp))
                {
                    strBldr.Append(GeneracionCodigoHelpers.GenerarPush("AX"));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMovHaciaAx(this.hijosNodo[0].LugarMul));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMovDesdeAx(this.LugarExp));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarPop("AX"));
                }
            }

            this.Codigo = strBldr.ToString();
        }


    }
}
