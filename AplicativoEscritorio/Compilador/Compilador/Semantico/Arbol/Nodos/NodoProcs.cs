using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoProcs : NodoArbolSemantico
    {
        public NodoProcs(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            hijoAHeredar.ProcPrincipalYaCreadoyCorrecto = this.ProcPrincipalYaCreadoyCorrecto;
            hijoAHeredar.ProcPrincipalCrearUnaVez = this.ProcPrincipalCrearUnaVez;
            hijoAHeredar.ProcSalidaYaCreadoyCorrecto = this.ProcSalidaYaCreadoyCorrecto;
            hijoAHeredar.ProcSalidaCrearUnaVez = this.ProcSalidaCrearUnaVez;
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.ProcPrincipalYaCreadoyCorrecto = hijoASintetizar.ProcPrincipalYaCreadoyCorrecto;
            this.ProcPrincipalCrearUnaVez = hijoASintetizar.ProcPrincipalCrearUnaVez;
            this.ProcSalidaYaCreadoyCorrecto = hijoASintetizar.ProcSalidaYaCreadoyCorrecto;
            this.ProcSalidaCrearUnaVez = hijoASintetizar.ProcSalidaCrearUnaVez;
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo[0].ActividadViewModel != null)
            {
                ActividadViewModel = this.hijosNodo[0].ActividadViewModel;
            }

            return this;
        }

      
        public override void CalcularCodigo()
        {
            this.VariablesProcPrincipal = this.hijosNodo[0].VariablesProcPrincipal;
            this.Codigo = this.hijosNodo[0].Codigo;
        }
    }
}
