using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using InterfazTextoGrafico;
using Microsoft.CSharp.RuntimeBinder;
using System.ComponentModel.Design;

namespace LibreriaActividades
{
    [Designer(typeof(AsignacionDesigner), typeof(IDesigner))]
    [ToolboxBitmap(typeof(Asignacion), "Resources.Asignacion.png")]
    public class Asignacion : ActividadBase
    {
           

        public string LadoIzquierdo { get; set; }
        public string LadoDerecho { get; set; }

        public Asignacion() 
            : base()
        {
          
              
        }

        public override void Ejecutar(StringBuilder sb)
        {
            if (String.IsNullOrEmpty(this.LadoIzquierdo) || String.IsNullOrEmpty(this.LadoDerecho))
                return;

            sb.AppendLine(String.Format(Extension.Tabs + "{0} := {1};", this.LadoIzquierdo, this.LadoDerecho));
        }

        protected override void Execute(System.Activities.NativeActivityContext context) { }

        public override ActividadViewModelBase Datos
        {
            get
            {
                AsignacionViewModel activ = new AsignacionViewModel(this.IdPropio);
                activ.Id = Id;                
                activ.LadoIzquierdo = this.LadoIzquierdo;
                activ.LadoDerecho = this.LadoDerecho;
                activ.ActividadReferenciada = this;

                IdPropio = activ.IdPropio;

                return activ;
            }
          
        }

        public override void AsignarDatos(ActividadViewModelBase datos)
        {
            try
            {
                AsignacionViewModel datosMapeados = datos as AsignacionViewModel;

                this.LadoIzquierdo = datosMapeados.LadoIzquierdo;
                this.LadoDerecho = datosMapeados.LadoDerecho;
                this.IdPropio = datosMapeados.IdPropio;
            }
            catch (RuntimeBinderException)
            {
                throw;
            }
        }
    }
}
