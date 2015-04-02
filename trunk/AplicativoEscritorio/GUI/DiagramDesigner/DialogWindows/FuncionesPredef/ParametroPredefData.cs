using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ragnarok.DialogWindows.FuncionesPredef
{
    // flanzani 11/11/2012
    // IDC_APP_4
    // Mostrar las funciones predefinidas por el framework
    // Objeto para mostrar los parametros de una rutina
    public class ParametroPredefData: INotifyPropertyChanged
    {
        private string texto;
        private string descripcion;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }



        public string Texto
        {
            get
            {
                return texto;
            }
            set
            {
                texto = value;
                NotifyPropertyChanged("Texto");
            }
        }

         public string Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                descripcion = value;
                NotifyPropertyChanged("Descripcion");
            }
        }
    }

    
}
