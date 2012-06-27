﻿using System;
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
        public string Elemento { get; set; }
        public bool ConPausa { get; set; }

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
                MostrarViewModel activ = new MostrarViewModel();
                activ.ElementosAMostrar = this.Elemento;
                activ.ConPausa = this.ConPausa;
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

            }
            catch (RuntimeBinderException)
            {
                throw;
            }
        }
    }
}