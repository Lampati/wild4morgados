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
    [Designer(typeof(LlamarProcedimientoDesigner))]
    [ToolboxBitmap(typeof(LlamarProcedimiento), "Resources.LlamarProcedimiento.png")]
    public class LlamarProcedimiento : ActividadBase
    {
        public string NombreProcedimiento { get; set; }
        public string Parametros { get; set; }

        public LlamarProcedimiento()
        {
            SePuedeEliminar = true;
        }

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
                LlamarProcedimientoViewModel activ = new LlamarProcedimientoViewModel();
                activ.Id = Id;
                activ.NombreProcedimiento = this.NombreProcedimiento;
                activ.Parametros = this.Parametros;
                activ.ActividadReferenciada = this;

                IdPropio = activ.IdPropio;

                return activ;
            }

        }

        public override void AsignarDatos(ActividadViewModelBase datos)
        {
            try
            {
                LlamarProcedimientoViewModel datosMapeados = datos as LlamarProcedimientoViewModel;

                this.NombreProcedimiento = datosMapeados.NombreProcedimiento;
                this.Parametros = datosMapeados.Parametros;

                this.IdPropio = datosMapeados.IdPropio;
            }
            catch (RuntimeBinderException)
            {
                throw;
            }
        }
    }
}
