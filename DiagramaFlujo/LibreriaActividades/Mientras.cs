using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Activities.Presentation;
using System.Windows;
using System.Drawing;

namespace LibreriaActividades
{
    [ToolboxBitmap(typeof(Mientras), "Resources.Mientras.png")]
    public class Mientras : ActividadBase, IActivityTemplateFactory
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

            Extension.Code.AppendLine(String.Format(Extension.Tabs + "MIENTRAS ({0}) HACER", this.Condicion));
            if (Cuerpo != null)
            {
                Extension.ProfundidadIdentacion++;
                Cuerpo.Ejecutar(context);
                Extension.ProfundidadIdentacion--;
            }
            Extension.Code.AppendLine(Extension.Tabs + "FINMIENTRAS;");
        }

        public Activity Create(DependencyObject target)
        {
            return new Mientras()
            {
                DisplayName = "Mientras",
                Cuerpo = new Secuencia()
                {
                    DisplayName = "Hacer"
                }
            };
        }
    }
}
