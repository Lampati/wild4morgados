using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoVisualizar : NodoArbolSemantico
    {
        public bool ConSaltoLinea { get; set; }

        public bool ConPausa { get; set; }

        public NodoVisualizar(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

      

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            

            LineaCorrespondiente = GlobalesCompilador.UltFila;

            this.ListaElementosVisualizar = this.hijosNodo[2].ListaElementosVisualizar;

            this.ConPausa = ((NodoMostrarOp)this.hijosNodo[0]).ConPausa;

            ArmarActividadViewModel();

            return this;
        }

        private void ArmarActividadViewModel()
        {
            InterfazTextoGrafico.MostrarViewModel activ = new InterfazTextoGrafico.MostrarViewModel();
            activ.ConPausa = this.ConPausa;
            activ.ElementosAMostrar = this.hijosNodo[2].Gargar;
            ActividadViewModel = activ;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine(GeneracionCodigoHelpers.AsignarLinea(LineaCorrespondiente));

            strBldr.Append("WriteLn ");
            strBldr.Append("( ");
            strBldr.Append(this.hijosNodo[2].Codigo);
            strBldr.Append(") ");
            strBldr.Append(";");

            if (ConPausa)
            {
                strBldr.AppendLine();
                strBldr.AppendLine(GeneracionCodigoHelpers.PausarHastaEntradaTeclado());
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
