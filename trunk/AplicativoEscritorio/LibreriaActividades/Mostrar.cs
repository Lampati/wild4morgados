using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using InterfazTextoGrafico;

namespace LibreriaActividades
{
    [Designer(typeof(MostrarDesigner))]
    [ToolboxBitmap(typeof(Mostrar), "Resources.Mostrar.png")]
    public class Mostrar : ActividadBase
    {
        public string Elemento { get; set; }

        public override void Ejecutar(StringBuilder sb)
        {
            if (String.IsNullOrEmpty(this.Elemento))
                return;

            sb.AppendLine(String.Format(Extension.Tabs + "MOSTRAR({0});", this.Elemento));
        }

        protected override void Execute(System.Activities.NativeActivityContext context) { }

        public override ActividadViewModelBase Datos
        {
            get
            {
                return null;
            }

        }

        public override void AsignarDatos(ActividadViewModelBase datos)
        {
            
        }
    }
}
