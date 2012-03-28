using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace DiagramDesigner.TestsPruebas
{
    public class Variables
    {
        public bool EsSeleccionada { get; set; }
        public string Nombre { get; set; }
        public string Contexto { get; set; }
        public NodoTablaSimbolos.TipoDeDato TipoDato { get; set; }
        public bool EsArreglo { get; set; }        
        public string TamanioTipo { get; set; }

        public Variables(NodoTablaSimbolos nodo)
        {
            EsSeleccionada = false;
            Nombre = nodo.Nombre;
            Contexto = (nodo.Contexto == NodoTablaSimbolos.TipoContexto.Global) ? nodo.Contexto.ToString() : nodo.NombreContextoLocal;
            TipoDato = nodo.TipoDato;
            EsArreglo = nodo.EsArreglo;
            TamanioTipo = (nodo.EsArreglo) ? string.Format("Arreglo con tope {0}", nodo.ValorInt) : "Variable";
        }
    }
}
