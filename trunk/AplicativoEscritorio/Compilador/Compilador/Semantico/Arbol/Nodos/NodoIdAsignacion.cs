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
            this.IndiceArreglo = this.hijosNodo[1].IndiceArreglo;

            //if (this.EsArreglo)
            //{
            //    this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
            //    this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, NodoTablaSimbolos.TipoDeDato.Entero);
            //}
            string nombreContexto;
            //if (this.EsArreglo)
            //{
            //    nombreContexto = "Global";
            //}
            //else
            //{
            //    nombreContexto = this.TablaSimbolos.ObtenerNombreContextoVariable(this.Lexema, this.ContextoActual, this.nombreContextoLocal);
            //}

            nombreContexto = this.TablaSimbolos.ObtenerNombreContextoVariable(this.Lexema, this.ContextoActual, this.NombreContextoLocal);

            this.Lugar = new StringBuilder(nombreContexto).Append(this.Lexema).ToString();
            

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
            strBldr.Append(this.hijosNodo[0].Lexema);
            strBldr.Append(this.hijosNodo[1].Codigo);
            

            this.Codigo = strBldr.ToString();
        }

    }
}
