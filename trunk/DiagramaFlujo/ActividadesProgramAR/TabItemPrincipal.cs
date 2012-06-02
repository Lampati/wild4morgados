using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using UsingWorkflowItemPresenter.ViewModels;

namespace UsingWorkflowItemPresenter
{
    public class TabItemPrincipal : Tab
    {
        public override void Ejecutar()
        {
            LibreriaActividades.Extension.Code.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("PROCEDIMIENTO PRINCIPAL();"));
            LibreriaActividades.Extension.ProfundidadIdentacion++;
            base.Ejecutar();
            LibreriaActividades.Extension.ProfundidadIdentacion--;
            LibreriaActividades.Extension.Code.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("FINPROC;"));
        }
    }
}
