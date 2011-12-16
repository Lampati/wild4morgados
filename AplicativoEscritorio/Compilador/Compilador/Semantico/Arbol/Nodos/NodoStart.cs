using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Sintactico.Gramatica;
using Compilador.Auxiliares;
using Compilador.Semantico.Arbol.Temporales;
using Compilador.Semantico.Arbol.Labels;
using System.IO;
using System.Configuration;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoStart : NodoArbolSemantico 
    {
        public NodoStart(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ContextoActual = NodoTablaSimbolos.TipoContexto.Global;
            this.nombreContextoLocal = EnumUtils.stringValueOf(NodoTablaSimbolos.TipoContexto.Global);
            this.ProcPrincipalYaCreadoyCorrecto = false;
            this.ProcPrincipalCrearUnaVez = true;

            this.ProcSalidaYaCreadoyCorrecto = false;
            this.ProcSalidaCrearUnaVez = true;
        }

        public string MemoriaGlobal { get; set; }

        public override void ChequearAtributos(Terminal t)
        {
            ManagerTemporales inst = ManagerTemporales.Instance;
            if (!this.ProcPrincipalYaCreadoyCorrecto)
            {
                StringBuilder strbldr = new StringBuilder("Error en el procedimiento principal: Debe haber unicamente un procedimiento principal y debe ser el ultimo.");
                throw new ErrorSemanticoException(strbldr.ToString());
            }

            if (!this.ProcSalidaYaCreadoyCorrecto)
            {
                StringBuilder strbldr = new StringBuilder("Error en el procedimiento salida: Debe haber unicamente un procedimiento salida");
                throw new ErrorSemanticoException(strbldr.ToString());
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            if (!this.ProcPrincipalYaCreadoyCorrecto)
            {
                this.ProcPrincipalYaCreadoyCorrecto = true;
            }

            if (!this.ProcSalidaYaCreadoyCorrecto)
            {
                this.ProcSalidaYaCreadoyCorrecto = true;
            }

            return this;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
        {
            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
        {
            this.TextoParaImprimirArbol = this.ToString();
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();
            
            
            this.Codigo = strBldr.ToString();
        }
    }
}
