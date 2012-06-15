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

        public bool AdmiteDelaraciones
        {
            get { return this.admiteDeclaraciones; }
            set { this.admiteDeclaraciones = value; }
        }

        public Secuencia()
        {
            Activities = new Collection<Activity>();
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
                SecuenciaViewModel retorno = new SecuenciaViewModel();
                retorno.ListaActividades = new List<ActividadViewModelBase>();

                foreach (ActividadBase item in this.Activities)
                {
                    retorno.ListaActividades.Add(item.Datos);
                }

                return retorno;  
            }
        
        }

        public override void AsignarDatos(ActividadViewModelBase datos)
        {           
            try
            {
                SecuenciaViewModel datosMapeados = datos as SecuenciaViewModel;

                foreach (ActividadViewModelBase item in datosMapeados.ListaActividades)
                {
                    ActividadBase actividadCreada = ActividadFactory.CrearActividad(item);
                    Activities.Add(actividadCreada);
                }
            }
            catch (RuntimeBinderException)
            {
                throw;
            }
        }
    }
}
