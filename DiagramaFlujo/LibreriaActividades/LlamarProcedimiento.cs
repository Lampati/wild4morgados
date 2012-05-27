using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LibreriaActividades
{
    [Designer(typeof(LlamarProcedimientoDesigner))]
    public class LlamarProcedimiento : ActividadBase
    {
        public string NombreProcedimiento { get; set; }
        public string Parametros { get; set; }

        public override void Ejecutar(System.Activities.NativeActivityContext context)
        {
            this.Execute(context);
        }

        protected override void Execute(System.Activities.NativeActivityContext context)
        {
            if (String.IsNullOrEmpty(this.NombreProcedimiento))
                return;

            Extension.Code.AppendLine(String.Format("LLAMAR {0}({1});", this.NombreProcedimiento, this.Parametros));
        }
    }
}
