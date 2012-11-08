using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AplicativoEscritorio.DataAccess.Enums;

namespace DataAccess.Entidades
{
    public class Variable : INotifyPropertyChanged
    {
        public bool EsValida { get; set; }


        public string NombreConContexto
        {
            get
            {
                return string.Format("{0} ({1})", Nombre, Contexto);
            }
        }

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
        public TipoDato TipoDato { get; set; }
        public bool EsArreglo { get; set; }        
        public string TamanioTipo { get; set; }
        public double TopeArr { get; set; }

        public List<PosicionArreglo> Posiciones { get; set; }

        //public Variable(NodoTablaSimbolos nodo)
        //{
        //    EsSeleccionada = false;
        //    Nombre = nodo.Nombre;
        //    Descripcion = string.Empty;
        //    NombreCodigo = nodo.NombreParaCodigo;
        //    Contexto = (nodo.Contexto == NodoTablaSimbolos.TipoContexto.Global) ? nodo.Contexto.ToString() : nodo.NombreContextoLocal;
        //    TipoDato = nodo.TipoDato;
        //    EsArreglo = nodo.EsArreglo;
        //    TopeArr = nodo.Valor;
        //    TamanioTipo = (nodo.EsArreglo) ? string.Format("Arreglo con tope {0}", nodo.Valor) : "Variable";

        //}

        public Variable(string nombre, string nombreCodigo, TipoContexto contexto, string nombreContLocal ,TipoDato tipoDato, bool esArreglo, double valor)
        {
            EsSeleccionada = false;
            Nombre = nombre;
            Descripcion = string.Empty;
            NombreCodigo = nombreCodigo;
            Contexto = (contexto == TipoContexto.Global) ? contexto.ToString() : nombreContLocal;
            TipoDato = tipoDato;
            EsArreglo = esArreglo;
            TopeArr = valor;
            TamanioTipo = (esArreglo) ? string.Format("Arreglo con tope {0}", valor) : "Variable";

        }

        //public Variable(NodoTablaSimbolos nodo, List<PosicionArreglo> posis)
        //{
        //    EsSeleccionada = false;
        //    Nombre = nodo.Nombre;
        //    Descripcion = string.Empty;
        //    NombreCodigo = nodo.NombreParaCodigo;
        //    Contexto = (nodo.Contexto == NodoTablaSimbolos.TipoContexto.Global) ? nodo.Contexto.ToString() : nodo.NombreContextoLocal;
        //    TipoDato = nodo.TipoDato;
        //    EsArreglo = nodo.EsArreglo;
        //    TopeArr = nodo.Valor;
        //    TamanioTipo = (nodo.EsArreglo) ? string.Format("Arreglo con tope {0}", nodo.Valor) : "Variable";

        //    Posiciones = posis;
        //}

        public Variable(string nombre, string nombreCodigo, TipoContexto contexto, string nombreContLocal, TipoDato tipoDato, bool esArreglo, double valor, List<PosicionArreglo> posis)
        {
            EsSeleccionada = false;
            Nombre = nombre;
            Descripcion = string.Empty;
            NombreCodigo = nombreCodigo;
            Contexto = (contexto == TipoContexto.Global) ? contexto.ToString() : nombreContLocal;
            TipoDato = tipoDato;
            EsArreglo = esArreglo;
            TopeArr = valor;
            TamanioTipo = (esArreglo) ? string.Format("Arreglo con tope {0}", valor) : "Variable";

            Posiciones = posis;
        }


        public Variable(string n, string cont, string tipoDato, string tipoVar, bool esArr, string val, List<PosicionArreglo> posis)
        {
            EsSeleccionada = false;
            Nombre = n;
            EsArreglo = esArr;
            Descripcion = string.Empty;
            
            TipoDato = ConvertirATipoDatoEnum(tipoDato);
            
            Valor = val;
            Contexto = cont;
            if (posis != null)
            {
                TopeArr = posis.Count;
            }
            //TamanioTipo = (nodo.EsArreglo) ? string.Format("Arreglo con tope {0}", nodo.ValorInt) : "Variable";

            Posiciones = posis;

            if (!string.IsNullOrWhiteSpace(tipoVar))
            {
                TamanioTipo = tipoVar;
            }
            else
            {
                TamanioTipo = (EsArreglo) ? string.Format("Arreglo con tope {0}", TopeArr) : "Variable";
            }
        }



        private TipoDato ConvertirATipoDatoEnum(string tipoDato)
        {
            TipoDato retorno = TipoDato.Ninguno;
            switch (tipoDato.ToUpper())
            {
                case "NUMERO":
                    retorno = TipoDato.Numero;
                    break;
                case "TEXTO":
                    retorno = TipoDato.Texto;
                    break;
                case "BOOLEANO":
                    retorno = TipoDato.Booleano;
                    break;
                default:
                    break;
            }

            return retorno;
        }

        public void SetearTipoVariable()
        {            
            
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion

        public void SetearDatosArregloPostCargadoPosiciones()
        {
            TopeArr = Posiciones.Count;
            TamanioTipo = (EsArreglo) ? string.Format("Arreglo con tope {0}", TopeArr) : "Variable";            
        }
    }
}
