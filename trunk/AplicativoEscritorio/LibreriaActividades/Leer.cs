using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using InterfazTextoGrafico;
using Microsoft.CSharp.RuntimeBinder;

namespace LibreriaActividades
{
    [Designer(typeof(LeerDesigner))]
    [ToolboxBitmap(typeof(Leer), "Resources.Mostrar.png")]
    public class Leer : ActividadBase
    {
        public string Parametro { get; set; }

        public override void Ejecutar(StringBuilder sb)
        {
            if (String.IsNullOrEmpty(this.Parametro))
                return;

            sb.AppendLine(String.Format(Extension.Tabs + "LEER ({0});", this.Parametro));
        }

        protected override void Execute(System.Activities.NativeActivityContext context) { }

        public override ActividadViewModelBase Datos
        {
            get
            {
                LeerViewModel retorno = new LeerViewModel();
                retorno.Parametro = this.Parametro;

                return retorno;
            }
        }

        public override void AsignarDatos(ActividadViewModelBase datos)
        {
            try
            {
                LeerViewModel datosMapeados = datos as LeerViewModel;

                this.Parametro = datosMapeados.Parametro;

            }
            catch (RuntimeBinderException)
            {
                throw;
            }
        }
    }
}
