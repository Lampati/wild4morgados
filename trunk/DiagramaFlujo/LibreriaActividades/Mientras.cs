using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace LibreriaActividades
{
    [Designer(typeof(MientrasDesigner))]
    public class Mientras : ActividadBase
    {
        public string Condicion { get; set; }
        public Activity Cuerpo { get; set; }

        public override void Ejecutar(System.Activities.NativeActivityContext context)
        {
            this.Execute(context);
        }

        protected override void Execute(System.Activities.NativeActivityContext context)
        {
            if (String.IsNullOrEmpty(this.Condicion))
                return;

            Extension.Code.AppendLine(String.Format("MIENTRAS ({0}) HACER", this.Condicion));
            if (Cuerpo != null)
                Cuerpo.Ejecutar(context);
            Extension.Code.AppendLine("FINMIENTRAS;");
        }
    }
}
