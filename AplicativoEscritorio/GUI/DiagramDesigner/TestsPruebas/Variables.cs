using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DiagramDesigner.TestsPruebas
{
    public class Variables : INotifyPropertyChanged
    {
        private bool esSeleccionada;
        public bool EsSeleccionada
        {
            get
            {
                return esSeleccionada;
            }
            set
            {
                if(esSeleccionada != value)
                {
                    esSeleccionada = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("EsSeleccionada"));
                }
                
            }
        }

        private string valor;
        public string Valor
        {
            get
            {
                return valor;
            }
            set
            {
                if (valor != value)
                {
                    valor = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Valor"));
                }

            }
        }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombreCodigo { get; set; }
        public string Contexto { get; set; }
        public NodoTablaSimbolos.TipoDeDato TipoDato { get; set; }
        public bool EsArreglo { get; set; }        
        public string TamanioTipo { get; set; }

        public Variables(NodoTablaSimbolos nodo)
        {
            EsSeleccionada = false;
            Nombre = nodo.Nombre;
            Descripcion = string.Empty;
            NombreCodigo = nodo.NombreParaCodigo;
            Contexto = (nodo.Contexto == NodoTablaSimbolos.TipoContexto.Global) ? nodo.Contexto.ToString() : nodo.NombreContextoLocal;
            TipoDato = nodo.TipoDato;
            EsArreglo = nodo.EsArreglo;
            TamanioTipo = (nodo.EsArreglo) ? string.Format("Arreglo con tope {0}", nodo.ValorInt) : "Variable";
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion
    }
}
