using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using InterfazTextoGrafico;

namespace LibreriaActividades
{
    [Designer(typeof(LlamarProcedimientoDesigner))]
    [ToolboxBitmap(typeof(LlamarProcedimiento), "Resources.LlamarProcedimiento.png")]
    public class LlamarProcedimiento : ActividadBase
    {
        public string NombreProcedimiento { get; set; }
        public string Parametros { get; set; }

        public override void Ejecutar(StringBuilder sb)
        {
            if (String.IsNullOrEmpty(this.NombreProcedimiento))
                return;

            sb.AppendLine(String.Format(Extension.Tabs + "LLAMAR {0}({1});", this.NombreProcedimiento, this.Parametros));
        }

        protected override void Execute(System.Activities.NativeActivityContext context) { }

        public override ActividadViewModelBase Datos
        {
            get
            {
                return null;
            }

        }

        public override void AsignarDatos(dynamic datos)
        {
            
        }
    }
}
