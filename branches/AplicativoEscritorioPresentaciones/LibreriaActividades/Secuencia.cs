using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Activities.Statements;
using System.Collections.ObjectModel;
using System.Drawing;
using Microsoft.CSharp.RuntimeBinder;
using InterfazTextoGrafico;

namespace LibreriaActividades
{
    //[Designer("System.Activities.Core.Presentation.SequenceDesigner, System.Activities.Core.Presentation")]
    [ToolboxBitmap(typeof(Secuencia), "Resources.Secuencia.png")]
    public class Secuencia : ActividadBase
    {
        private bool admiteDeclaraciones;

        [Browsable(false)]
        public Collection<Activity> Activities { get; set; }

        public bool AdmiteDeclaraciones
        {
            get { return this.admiteDeclaraciones; }
            set { this.admiteDeclaraciones = value; }
        }

        public Secuencia()
        {
            Activities = new Collection<Activity>();
            SePuedeEliminar = false;
        }


        protected override void CacheMetadata(NativeActivityMetadata metadata) {
            metadata.SetChildrenCollection(Activities);
        }

        protected override void Execute(NativeActivityContext context)
        {
            
        }

        public override void Ejecutar(StringBuilder sb)
        {
            if (Activities != null)
            {
                foreach (ActividadBase a in this.Activities)
                    a.Ejecutar(sb);
            }
        }


        public override ActividadViewModelBase Datos
        {
            get
            {
                SecuenciaViewModel retorno = new SecuenciaViewModel(IdPropio);
                retorno.ListaActividades = new List<ActividadViewModelBase>();
                retorno.Id = Id;
                foreach (ActividadBase item in this.Activities)
                {
                    retorno.ListaActividades.Add(item.Datos);
                }
                retorno.ActividadReferenciada = this;

                IdPropio = retorno.IdPropio;

                return retorno;  
            }
        
        }

        public override void AsignarDatos(ActividadViewModelBase datos)
        {           
            try
            {
                SecuenciaViewModel datosMapeados = datos as SecuenciaViewModel;

                if (datosMapeados != null)
                {
                    foreach (ActividadViewModelBase item in datosMapeados.ListaActividades)
                    {
                        ActividadBase actividadCreada = ActividadFactory.CrearActividad(item);
                        Activities.Add(actividadCreada);
                    }

                    this.IdPropio = datosMapeados.IdPropio;
                }

                SePuedeEliminar = false;
            }
            catch (RuntimeBinderException)
            {
                throw;
            }
        }

        public override void ReasignarId()
        {
            ActividadViewModelBase datos = Datos;
            datos.ReasignarId();
            IdPropio = datos.IdPropio;

            foreach (ActividadBase item in Activities)
            {
                item.ReasignarId();
            }       
        }
    }
}
