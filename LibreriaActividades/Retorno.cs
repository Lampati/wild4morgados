using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfazTextoGrafico;
using Microsoft.CSharp.RuntimeBinder;
using System.ComponentModel;
using System.Drawing;

namespace LibreriaActividades
{
    [Designer(typeof(RetornoDesigner))]
    [ToolboxBitmap(typeof(Retorno), "Resources.Mostrar.png")]
    public class Retorno : ActividadBase
    {
        public string Expresion { get; set; }

        public override void Ejecutar(StringBuilder sb)
        { }

        protected override void Execute(System.Activities.NativeActivityContext context) { }

        public override ActividadViewModelBase Datos
        {
            get
            {
                RetornoViewModel retorno = new RetornoViewModel();
                retorno.Expresion = this.Expresion;

                return retorno;
            }
        }

        public override void AsignarDatos(ActividadViewModelBase datos)
        {
            try
            {
                RetornoViewModel datosMapeados = datos as RetornoViewModel;

                this.Expresion = datosMapeados.Expresion;
            }
            catch (RuntimeBinderException)
            {
                throw;
            }
        }
    }
}
