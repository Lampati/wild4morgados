using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Activities.Presentation.Model;
using System.Activities.Presentation;

namespace LibreriaActividades
{
    [Designer(typeof(DeclaracionDesigner))]
    public class Declaracion : ActividadBase
    {
        private System.Windows.Visibility visible;
        public string NombreVariable { get; set; }
        public eTipoVariable Tipo { get; set; }
        public string Tamano { get; set; }

        public override void Ejecutar(System.Activities.NativeActivityContext context)
        {
            this.Execute(context);
        }

        public static void Attach(ModelItem modelItem)
        {
            EditingContext editingContext = modelItem.GetEditingContext();
        }

        public System.Windows.Visibility Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }

        protected override void Execute(System.Activities.NativeActivityContext context)
        {
            if (String.IsNullOrEmpty(this.NombreVariable))
                return;

            if (this.Tipo == eTipoVariable.Vector)
                Extension.Code.AppendLine(String.Format("VAR {0} : ARREGLO[{1}] de {2};", this.NombreVariable, this.Tamano, this.Tipo));
            else
                Extension.Code.AppendLine(String.Format("VAR {0} : {1};", this.NombreVariable, this.Tipo));
        }
    }
}
