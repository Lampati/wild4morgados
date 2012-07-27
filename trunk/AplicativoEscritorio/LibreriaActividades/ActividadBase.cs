using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using InterfazTextoGrafico;
using System.ComponentModel;
using System.Activities.Presentation;

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
            set
            {
                idPropio = value;
                if (idPropio == 0)
                {

                }

                if (idPropio != long.MinValue)
                {
                    DisplayName = string.Format("{0} ({1})", this.GetType().Name, idPropio);
                }
                else
                {
                    DisplayName = string.Format("{0} ()", this.GetType().Name.ToString());
                }
            }

        }

        public ActividadBase() : base()
        {            
            //idPropio = ++_contadorGlobalAct;
        }

        

        public abstract void AsignarDatos(ActividadViewModelBase datos);

        protected ActivityDesigner actDesigner;
        public ActivityDesigner ActivDesigner
        {
            get
            {
                return actDesigner;
            }
            set
            {
                actDesigner = value;
            }
        }

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
