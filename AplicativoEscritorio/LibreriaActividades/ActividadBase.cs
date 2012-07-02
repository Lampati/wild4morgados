using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using InterfazTextoGrafico;

namespace LibreriaActividades
{
    public abstract class ActividadBase : NativeActivity
    {

        private static int _contadorGlobalAct = 0;

        private bool sePuedeEliminar = true;

        public abstract void Ejecutar(StringBuilder sb);

        public virtual bool SePuedeEliminar
        {
            get { return this.sePuedeEliminar; }
            set { this.sePuedeEliminar = value; }
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
    }
}
