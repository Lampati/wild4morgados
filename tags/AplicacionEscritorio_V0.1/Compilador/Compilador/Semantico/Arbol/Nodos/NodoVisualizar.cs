﻿using System;
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

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            LineaCorrespondiente = GlobalesCompilador.UltFila;

            this.ListaElementosVisualizar = this.hijosNodo[2].ListaElementosVisualizar;

            this.ConPausa = ((NodoMostrarOp)this.hijosNodo[0]).ConPausa;

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
