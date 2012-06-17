using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Activities.Presentation.Model;
using System.Activities.Presentation;
using System.Drawing;
using InterfazTextoGrafico;

namespace LibreriaActividades
{
    [Designer(typeof(DeclaracionVariableDesigner))]
    [ToolboxBitmap(typeof(DeclaracionVariable), "Resources.DeclaracionVariable.png")]
    public class DeclaracionVariable : ActividadBase
    {
        private System.Windows.Visibility visible;
        public string NombreVariable { get; set; }
        public eTipoVariable Tipo { get; set; }
        //public string Tamano { get; set; }
        //public eTipoVector TipoVector { get; set; }

        public override void Ejecutar(StringBuilder sb)
        {
            if (String.IsNullOrEmpty(this.NombreVariable))
                return;

            //if (this.Tipo == eTipoVariable.Vector)
            //    sb.AppendLine(String.Format(Extension.Tabs + "VAR {0} : ARREGLO[{1}] de {2};", this.NombreVariable, this.Tamano, this.TipoVector.ToString().ToUpper()));
            //else
                sb.AppendLine(String.Format(Extension.Tabs + "VAR {0} : {1};", this.NombreVariable, this.Tipo.ToString().ToUpper()));
        }

        public static void Attach(ModelItem modelItem)
        {
            EditingContext editingContext = modelItem.GetEditingContext();
        }

        [Browsable(false)]
        public System.Windows.Visibility Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
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
            DeclaracionVariableViewModel datosMapeados = datos as DeclaracionVariableViewModel;

            this.NombreVariable = datosMapeados.Nombre;

            switch (datosMapeados.Tipo)
            {
                case InterfazTextoGrafico.Enums.TipoDato.Numero:
                    this.Tipo = eTipoVariable.Numero;
                    break;
                case InterfazTextoGrafico.Enums.TipoDato.Texto:
                    this.Tipo = eTipoVariable.Texto;
                    break;
                case InterfazTextoGrafico.Enums.TipoDato.Booleano:
                    this.Tipo = eTipoVariable.Booleano;
                    break;
            }
        }
    }
}
