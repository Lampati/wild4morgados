﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner.ViewModels;

namespace DiagramDesigner.Tabs
{
    public class TabItemProcedimiento : Tab
    {
        private int orden;

        public TabItemProcedimiento() : base()
        {
            this.orden = LibreriaActividades.Extension.AsignarOrdenTab();
        }

        public override int Orden
        {
            get { return this.orden; }
        }

        public override void Ejecutar(StringBuilder sb)
        {
            sb.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("PROCEDIMIENTO {0}()", this.Header));
            if (!Object.Equals(base.initLocales, null) && base.initLocales.Activities.Count > 0)
                foreach (LibreriaActividades.ActividadBase ab in base.initLocales.Activities)
                    ab.Ejecutar(sb);
            sb.AppendLine("COMENZAR");
            LibreriaActividades.Extension.ProfundidadIdentacion++;
            base.Ejecutar(sb);
            LibreriaActividades.Extension.ProfundidadIdentacion--;
            sb.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("FINPROC;"));
            sb.AppendLine();
        }
    }
}
