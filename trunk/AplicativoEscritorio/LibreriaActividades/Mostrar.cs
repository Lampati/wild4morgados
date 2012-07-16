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
    [Designer(typeof(MostrarDesigner))]
    [ToolboxBitmap(typeof(Mostrar), "Resources.Mostrar.png")]
    public class Mostrar : ActividadBase
    {
        //private string idProp = string.Empty;
        //public string IdProp 
        //{
        //    get
        //    {
        //        return idProp;
        //    }
        //    set
        //    {
        //        idProp = value;
        //        DisplayName = string.Format("Mostrar ({0})",idProp);
        //    }
        //}

        public string Elemento { get; set; }
        public bool ConPausa { get; set; }

        public Mostrar()
            : base()
        {

        }

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
                MostrarViewModel activ = new MostrarViewModel(this.IdPropio);
                activ.ElementosAMostrar = this.Elemento;
                activ.ConPausa = this.ConPausa;
                activ.Id = Id;                
                activ.ActividadReferenciada = this;

                IdPropio = activ.IdPropio;

                return activ;
            }

        }

        public override void AsignarDatos(ActividadViewModelBase datos)
        {
            try
            {
                MostrarViewModel datosMapeados = datos as MostrarViewModel;

                this.Elemento = datosMapeados.ElementosAMostrar;
                this.ConPausa = datosMapeados.ConPausa;
                this.IdPropio = datosMapeados.IdPropio;

            }
            catch (RuntimeBinderException)
            {
                throw;
            }
        }
    }
}
