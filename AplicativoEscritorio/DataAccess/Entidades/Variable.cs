using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DataAccess.Entidades
{
    public class Variable : INotifyPropertyChanged
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

        public List<PosicionArreglo> Posiciones { get; set; }

        public Variable(NodoTablaSimbolos nodo)
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

        public Variable(NodoTablaSimbolos nodo, List<PosicionArreglo> posis)
        {
            EsSeleccionada = false;
            Nombre = nodo.Nombre;
            Descripcion = string.Empty;
            NombreCodigo = nodo.NombreParaCodigo;
            Contexto = (nodo.Contexto == NodoTablaSimbolos.TipoContexto.Global) ? nodo.Contexto.ToString() : nodo.NombreContextoLocal;
            TipoDato = nodo.TipoDato;
            EsArreglo = nodo.EsArreglo;
            TamanioTipo = (nodo.EsArreglo) ? string.Format("Arreglo con tope {0}", nodo.ValorInt) : "Variable";

            Posiciones = posis;
        }


        public Variable(string n, string cont, string tipoDato, string tipoVar, bool esArr, string val, List<PosicionArreglo> posis)
        {
            EsSeleccionada = false;
            Nombre = n;
            Descripcion = string.Empty;
            TamanioTipo = tipoVar;
            TipoDato = ConvertirATipoDatoEnum(tipoDato);
            EsArreglo = esArr;
            Valor = val;
            Contexto = cont;
            //TamanioTipo = (nodo.EsArreglo) ? string.Format("Arreglo con tope {0}", nodo.ValorInt) : "Variable";

            Posiciones = posis;
        }

        private NodoTablaSimbolos.TipoDeDato ConvertirATipoDatoEnum(string tipoDato)
        {
            NodoTablaSimbolos.TipoDeDato retorno = NodoTablaSimbolos.TipoDeDato.Ninguno;
            switch (tipoDato.ToUpper())
            {
                case "NUMERO":
                    retorno = NodoTablaSimbolos.TipoDeDato.Numero;
                    break;
                case "TEXTO":
                    retorno = NodoTablaSimbolos.TipoDeDato.Texto;
                    break;
                case "BOOLEANO":
                    retorno = NodoTablaSimbolos.TipoDeDato.Booleano;
                    break;
                default:
                    break;
            }

            return retorno;
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
