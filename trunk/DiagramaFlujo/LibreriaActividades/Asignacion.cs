using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LibreriaActividades
{
    [Designer(typeof(AsignacionDesigner))]
    public class Asignacion : ActividadBase
    {
        public string LadoIzquierdo { get; set; }
        public string LadoDerecho { get; set; }

        public override void Ejecutar(System.Activities.NativeActivityContext context)
        {
            this.Execute(context);
        }

        protected override void Execute(System.Activities.NativeActivityContext context)
        {
            if (String.IsNullOrEmpty(this.LadoIzquierdo) || String.IsNullOrEmpty(this.LadoDerecho))
                return;
            
            Extension.Code.AppendLine(String.Format("{0} := {1};", this.LadoIzquierdo, this.LadoDerecho));
        }
    }
}
