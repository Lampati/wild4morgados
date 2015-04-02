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
    // Objeto para mostrar los rutinas 
    public class RutinaPredefData : INotifyPropertyChanged
    {
        
        private string nombre;
        private string tipo;
        private string descripcion;
        private List<ParametroPredefData> listaParams = new List<ParametroPredefData>();
        private string ejemplo;
        private bool esFunc;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
                NotifyPropertyChanged("Nombre");
            }
        }

        public string Tipo
        {
            get
            {
                return tipo;
            }
            set
            {
                tipo = value;
                NotifyPropertyChanged("Tipo");
            }
        }

        public bool EsFunc
        {
            get
            {
                return esFunc;
            }
            set
            {
                esFunc = value;
                NotifyPropertyChanged("EsFunc");
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

        public string Ejemplo
        {
            get
            {
                return ejemplo;
            }
            set
            {
                ejemplo = value;
                NotifyPropertyChanged("Ejemplo");
            }
        }

        public List<ParametroPredefData> ListaParams
        {
            get
            {
                return listaParams;
            }
            set
            {
                listaParams = value;
            }
        }



    }
}
