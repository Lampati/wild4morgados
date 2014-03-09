using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DataAccess.Entidades
{
    public class PosicionArreglo 
    {
        
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

        public int Posicion { get; set; }

        public PosicionArreglo(int pos, string v)
        {
            Posicion = pos;
            Valor = v;
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
