﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoVariables : NodoArbolSemantico
    {
        public NodoVariables(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.EsConstante = false;
            this.DeclaracionesPermitidas = TipoDeclaracionesPermitidas.Variables;
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

        private void ArmarActividadViewModel()
        {
            InterfazTextoGrafico.SecuenciaViewModel activ = new InterfazTextoGrafico.SecuenciaViewModel();
            activ.ListaActividades.Add(this.hijosNodo[1].ActividadViewModel);

            if (this.hijosNodo[3].ActividadViewModel != null)
            {
                activ.ListaActividades.AddRange(((InterfazTextoGrafico.SecuenciaViewModel)this.hijosNodo[3].ActividadViewModel).ListaActividades);
            }


            ActividadViewModel = activ;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[1].Codigo).Append(" ");
                strBldr.AppendLine(";");
                strBldr.Append(this.hijosNodo[3].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
