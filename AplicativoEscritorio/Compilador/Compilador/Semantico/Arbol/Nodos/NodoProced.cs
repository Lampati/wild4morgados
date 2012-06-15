using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoProced : NodoArbolSemantico
    {
        public NodoProced(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
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

        public override void ChequearAtributos(Terminal t)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                ArmarActividadViewModel();
            }
            else
            {
                ActividadViewModel = null;
            }
            return this;
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {               

                this.VariablesProcPrincipal = this.hijosNodo[0].VariablesProcPrincipal + this.hijosNodo[1].VariablesProcPrincipal;

                strBldr.Append(this.hijosNodo[0].Codigo);
                strBldr.Append(this.hijosNodo[1].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }

        private void ArmarActividadViewModel()
        {
            InterfazTextoGrafico.ProcedimientosViewModel activ = new InterfazTextoGrafico.ProcedimientosViewModel();

            if (this.hijosNodo[0].ActividadViewModel != null)
            {
                activ.Procedimientos.Add(this.hijosNodo[0].ActividadViewModel as InterfazTextoGrafico.ProcedimientoViewModel);
            }

            if (this.hijosNodo[1].ActividadViewModel != null)
            {
                activ.Procedimientos.AddRange(((InterfazTextoGrafico.ProcedimientosViewModel)this.hijosNodo[1].ActividadViewModel).Procedimientos);
            }

            ActividadViewModel = activ;
        }
    }
}
