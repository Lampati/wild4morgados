using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using InterfazTextoGrafico;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoDeclaracionesIni : NodoArbolSemantico
    {
        public NodoDeclaracionesIni(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
               
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
        {
       
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
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

        private void ArmarActividadViewModel()
        {
            DeclaracionesGlobalesViewModel act = new DeclaracionesGlobalesViewModel();
            act.ConstantesGlobales = this.hijosNodo[0].ActividadViewModel as SecuenciaViewModel;
            act.VariablesGlobales = this.hijosNodo[1].ActividadViewModel as SecuenciaViewModel;

            ActividadViewModel = act;
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
            // esto es asi pq las variables del proc, tienen que ser globales
            if (this.hijosNodo.Count > 1)
            {
                this.ConstantesGlobales = this.hijosNodo[0].Codigo;
                this.VariablesGlobales = this.hijosNodo[1].Codigo;
            }
            
        }
    }
}
