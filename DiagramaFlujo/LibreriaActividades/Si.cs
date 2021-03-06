﻿using System;
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
    [Designer(typeof(SiDesigner))]
    [ToolboxBitmap(typeof(Si), "Resources.Si.png")]
    public class Si : ActividadBase, IActivityTemplateFactory
    {
        // this property contains an activity that will be scheduled in the execute method
        // the WorkflowItemPresenter in the designer is bound to this to enable editing
        // of the value
        [Browsable(false)]
        public Activity BranchVerdadero { get; set; }
        [Browsable(false)]
        public Activity BranchFalso { get; set; }
        public string Condicion { get; set; }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            metadata.AddChild(BranchVerdadero);
            metadata.AddChild(BranchFalso);
            base.CacheMetadata(metadata);
        }

        protected override void Execute(NativeActivityContext context)
        {
            
        }

        public override void Ejecutar(StringBuilder sb)
        {
            if (BranchVerdadero == null && BranchFalso == null)
                return;

            sb.AppendLine(String.Format(Extension.Tabs + "SI ({0}) ENTONCES", Condicion));
            if (BranchVerdadero != null)
            {
                Extension.ProfundidadIdentacion++;
                BranchVerdadero.Ejecutar(sb);
                Extension.ProfundidadIdentacion--;
            }
            if (BranchFalso != null)
            {
                sb.AppendLine(Extension.Tabs + "SINO");
                Extension.ProfundidadIdentacion++;
                BranchFalso.Ejecutar(sb);
                Extension.ProfundidadIdentacion--;
            }
            sb.AppendLine(Extension.Tabs + "FINSI;");
        }

        public Activity Create(DependencyObject target)
        {
            return new Si()
            {
                DisplayName = "Si",
                BranchVerdadero = new Secuencia()
                {
                    SePuedeEliminar = false,
                    DisplayName = "Rama Verdadero"
                },
                BranchFalso = new Secuencia()
                {
                    SePuedeEliminar = false,
                    DisplayName = "Rama Falso"
                }
            };
        }
    }
}
