﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using UsingWorkflowItemPresenter.ViewModels;

namespace UsingWorkflowItemPresenter
{
    public class TabItemPrincipal : Tab
    {
        public override void Ejecutar(StringBuilder sb)
        {
            sb.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("PROCEDIMIENTO PRINCIPAL()"));
            LibreriaActividades.Extension.ProfundidadIdentacion++;
            base.Ejecutar(sb);
            LibreriaActividades.Extension.ProfundidadIdentacion--;
            sb.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("FINPROC;"));
        }

        public override int Orden
        {
            get { return int.MaxValue; }
        }
    }
}
