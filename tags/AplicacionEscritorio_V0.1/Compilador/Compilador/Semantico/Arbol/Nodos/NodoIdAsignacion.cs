﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Auxiliares;


namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoIdAsignacion: NodoArbolSemantico
    {
        public NodoIdAsignacion(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            this.Lexema = this.hijosNodo[0].Lexema;
            this.EsArreglo = this.hijosNodo[1].EsArreglo;

            //if (this.EsArreglo)
            //{
            //    this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
            //    this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, NodoTablaSimbolos.TipoDeDato.Entero);
            //}
            //if (this.EsArreglo)
            //{
            //    nombreContexto = "Global";
            //}
            //else
            //{
            //    nombreContexto = this.TablaSimbolos.ObtenerNombreContextoVariable(this.Lexema, this.ContextoActual, this.nombreContextoLocal);
            //}


            
            

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
            strBldr.Append(this.hijosNodo[0].LexemaVariable);
            strBldr.Append(this.hijosNodo[1].Codigo);
            

            this.Codigo = strBldr.ToString();
        }

    }
}