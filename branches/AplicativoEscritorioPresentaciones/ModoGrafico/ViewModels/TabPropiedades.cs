using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.Interfaces;
using InterfazTextoGrafico.Enums;
using InterfazTextoGrafico;
using System.ComponentModel;

namespace ModoGrafico.ViewModels
{
    public class TabPropiedades : IPropiedadesContexto, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private string nombre;
        private TipoDato tipoRetorno;
        private string retorno;
        private List<ParametroViewModel> parametros = new List<ParametroViewModel>();
        

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {               

                if (!string.IsNullOrEmpty(value))
                {
                    this.nombre = value;
                    NotifyPropertyChanged("Nombre");
                }
            }
        }

        public TipoDato TipoRetorno
        {
            get
            {
                return tipoRetorno;
            }
            set
            {
                tipoRetorno = value;

                NotifyPropertyChanged("TipoRetorno");
            }
        }

        public string Retorno
        {
            get
            {
                return retorno;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.retorno = value;
                    NotifyPropertyChanged("Retorno");
                }
            }
        }

        public List<ParametroViewModel> Parametros
        {
            get
            {
                return parametros;
            }
            set
            {
                parametros = value;
            }
        }

        

        protected void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
