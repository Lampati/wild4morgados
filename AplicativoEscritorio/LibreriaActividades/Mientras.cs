﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Activities.Presentation;
using System.Windows;
using System.Drawing;
using Microsoft.CSharp.RuntimeBinder;
using InterfazTextoGrafico;

namespace LibreriaActividades
{
    [ToolboxBitmap(typeof(Mientras), "Resources.Mientras.png")]
    public class Mientras : ActividadBase, IActivityTemplateFactory
    {
        public string Condicion { get; set; }
        public Activity Cuerpo { get; set; }

        public override void Ejecutar(StringBuilder sb)
        {
            if (String.IsNullOrEmpty(this.Condicion))
                return;

            sb.AppendLine(String.Format(Extension.Tabs + "MIENTRAS ({0}) HACER", this.Condicion));
            if (Cuerpo != null)
            {
                Extension.ProfundidadIdentacion++;
                Cuerpo.Ejecutar(sb);
                Extension.ProfundidadIdentacion--;
            }
            sb.AppendLine(Extension.Tabs + "FINMIENTRAS;");
        }

        protected override void Execute(System.Activities.NativeActivityContext context) { }

        public Activity Create(DependencyObject target)
        {
            return new Mientras()
            {
                DisplayName = "Mientras",
                SePuedeEliminar = true,
                Cuerpo = new Secuencia()
                {
                    DisplayName = "Hacer",
                    SePuedeEliminar = false
                }
                
            };
        }

        public override ActividadViewModelBase Datos
        {
            get
            {
                MientrasViewModel retorno = new MientrasViewModel(IdPropio);
                retorno.Condicion = this.Condicion;
                retorno.Cuerpo = ((Secuencia)this.Cuerpo).Datos as SecuenciaViewModel;
                retorno.Id = Id;
                retorno.ActividadReferenciada = this;

                IdPropio = retorno.IdPropio;

                return retorno;
            }
        }

        public override void AsignarDatos(ActividadViewModelBase datos)
        {
            try
            {
                MientrasViewModel datosMapeados = datos as MientrasViewModel;

                this.Condicion = datosMapeados.Condicion;

                ActividadBase actSec = ActividadFactory.CrearActividad(datosMapeados.Cuerpo);
                this.Cuerpo = actSec;

                if (this.Cuerpo == null)
                {
                    Cuerpo = new Secuencia();
                }

                this.Cuerpo.DisplayName = "Hacer";

                this.IdPropio = datosMapeados.IdPropio;
                
            }
            catch (RuntimeBinderException)
            {
                throw;
            }
        }

        public override void ReasignarId()
        {
            ActividadViewModelBase datos = Datos;
            datos.ReasignarId();
            IdPropio = datos.IdPropio;

            Secuencia secCuerpo = ((Secuencia)this.Cuerpo);
            if (secCuerpo != null)
            {
                secCuerpo.ReasignarId();
            }

        }
    }
}
