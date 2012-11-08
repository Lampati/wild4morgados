﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;


namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoBloqueSiCont: NodoArbolSemantico
    {
        public NodoBloqueSiCont(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {

            //Aca no hago nada con los labels, se hace todo en el padre
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (hijosNodo.Count > 2)
            {
                this.EsSino = true;
                ActividadViewModel = this.hijosNodo[1].ActividadViewModel;
            }
            else
            {
                this.EsSino = false;
                ActividadViewModel = null;
            }

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

            if (this.hijosNodo.Count > 2)
            {
                strBldr.AppendLine("else");
                strBldr.AppendLine("begin");
                strBldr.Append("\t").AppendLine(this.hijosNodo[1].Codigo.Replace("\r\n", "\r\n\t"));
                strBldr.AppendLine("end;");
            
            }          

            this.Codigo = strBldr.ToString();
        }
    }
}
