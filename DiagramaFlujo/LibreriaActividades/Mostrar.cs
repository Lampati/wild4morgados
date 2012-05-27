using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LibreriaActividades
{
    [Designer(typeof(MostrarDesigner))]
    public class Mostrar : ActividadBase
    {
        public string Elemento { get; set; }

        public override void Ejecutar(System.Activities.NativeActivityContext context)
        {
            this.Execute(context);
        }

        protected override void Execute(System.Activities.NativeActivityContext context)
        {
            if (String.IsNullOrEmpty(this.Elemento))
                return;

            Extension.Code.AppendLine(String.Format("MOSTRAR({0});", this.Elemento));
        }
    }
}
