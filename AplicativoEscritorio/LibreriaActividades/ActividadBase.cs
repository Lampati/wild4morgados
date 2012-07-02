using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using InterfazTextoGrafico;
using System.ComponentModel;

namespace LibreriaActividades
{
    public abstract class ActividadBase : NativeActivity, INotifyPropertyChanged
    {

        private static int _contadorGlobalAct = 0;

        private bool sePuedeEliminar = true;

        public abstract void Ejecutar(StringBuilder sb);

        public virtual bool SePuedeEliminar
        {
            get { return this.sePuedeEliminar; }
            set { this.sePuedeEliminar = value; }
        }

        private bool contieneError = false;
        public bool ContieneError
        {
            get { return this.contieneError; }
            set 
            { 
                this.contieneError = value;
                NotifyPropertyChanged("ContieneError");
            }
        }

        public abstract ActividadViewModelBase Datos
        {
            get;
        }

        protected long idPropio;
        public long IdPropio
        {
            get
            {
                return idPropio;
            }

        }

        public ActividadBase() : base()
        {
            idPropio = ++_contadorGlobalAct;
        }

        public abstract void AsignarDatos(ActividadViewModelBase datos);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
