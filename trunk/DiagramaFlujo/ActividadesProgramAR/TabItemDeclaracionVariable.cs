using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using UsingWorkflowItemPresenter.ViewModels;

namespace UsingWorkflowItemPresenter
{
    public class TabItemDeclaracionVariable : Tab
    {
        public override int Orden
        {
            get { return 2; }
        }

        public override void Ejecutar(StringBuilder sb)
        {
            sb.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("VARIABLES"));
            LibreriaActividades.Extension.ProfundidadIdentacion++;
            base.Ejecutar(sb);
            LibreriaActividades.Extension.ProfundidadIdentacion--;
            sb.AppendLine();
        }
    }
}
