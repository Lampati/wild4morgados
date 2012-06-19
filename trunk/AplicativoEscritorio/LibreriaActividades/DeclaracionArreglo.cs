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
    [Designer(typeof(DeclaracionArregloDesigner))]
    [ToolboxBitmap(typeof(DeclaracionConstante), "Resources.DeclaracionArreglo.png")]
    public class DeclaracionArreglo : ActividadBase
    {
        private System.Windows.Visibility visible;
        public string Nombre { get; set; }
        public eTipoVariable Tipo { get; set; }
        public string Tope { get; set; }

        public override void Ejecutar(StringBuilder sb)
        {
            if (String.IsNullOrEmpty(this.Nombre))
                return;

            //if (this.Tipo == eTipoVariable.Vector)
            //    sb.AppendLine(String.Format(Extension.Tabs + "CONST {0} : ARREGLO[{1}] de {2};", this.NombreConstante, this.Tamano, this.TipoVector.ToString().ToUpper()));
            //else
                //sb.AppendLine(String.Format(Extension.Tabs + "CONST {0} : {1};", this.NombreConstante, this.Tipo.ToString().ToUpper()));

            sb.AppendLine(String.Format(Extension.Tabs + "VAR {0} : ARREGLO[{1}] de {2};", this.Nombre, this.Tope, this.Tipo.ToString().ToUpper()));
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
                DeclaracionArregloViewModel activ = new DeclaracionArregloViewModel();

                activ.Nombre = this.Nombre;
                activ.Tope = this.Tope;

                switch (this.Tipo)
                {
                    case eTipoVariable.Numero:
                        activ.Tipo = InterfazTextoGrafico.Enums.TipoDato.Numero;
                        break;
                    case eTipoVariable.Texto:
                        activ.Tipo = InterfazTextoGrafico.Enums.TipoDato.Texto;
                        break;
                    case eTipoVariable.Booleano:
                        activ.Tipo = InterfazTextoGrafico.Enums.TipoDato.Booleano;
                        break;                    
                    default:
                        break;
                }

                return activ;
            }

        }

        public override void AsignarDatos(ActividadViewModelBase datos)
        {
            DeclaracionArregloViewModel datosMapeados = datos as DeclaracionArregloViewModel;

            this.Nombre = datosMapeados.Nombre;
            this.Tope = datosMapeados.Tope;

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
