using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace LibreriaActividades
{
    [Designer(typeof(AsignacionDesigner))]
    [ToolboxBitmap(typeof(Asignacion), "Resources.Asignacion.png")]
    public class Asignacion : ActividadBase
    {
        public string LadoIzquierdo { get; set; }
        public string LadoDerecho { get; set; }

        public override void Ejecutar(StringBuilder sb)
        {
            if (String.IsNullOrEmpty(this.LadoIzquierdo) || String.IsNullOrEmpty(this.LadoDerecho))
                return;

            sb.AppendLine(String.Format(Extension.Tabs + "{0} := {1};", this.LadoIzquierdo, this.LadoDerecho));
        }

        protected override void Execute(System.Activities.NativeActivityContext context) { }
    }
}
