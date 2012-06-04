using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UsingWorkflowItemPresenter.ViewModels;

namespace UsingWorkflowItemPresenter
{
    public class TabItemDeclaracionConstante : Tab
    {
        public override int Orden
        {
            get { return 1; }
        }

        public override void Ejecutar(StringBuilder sb)
        {
            sb.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("CONSTANTES"));
            LibreriaActividades.Extension.ProfundidadIdentacion++;
            base.Ejecutar(sb);
            LibreriaActividades.Extension.ProfundidadIdentacion--;
            sb.AppendLine();
        }
    }
}
