using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DiagramDesigner.TestsPruebas
{
    public class Linea : INotifyPropertyChanged
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
                if (esSeleccionada != value)
                {
                    esSeleccionada = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("EsSeleccionada"));
                }

            }
        }

        private bool esHabilitada;
        public bool EsHabilitada
        {
            get
            {
                return esHabilitada;
            }
            set
            {
                if (esHabilitada != value)
                {
                    esHabilitada = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("EsHabilitada"));
                }

            }
        }

        public string Codigo { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
