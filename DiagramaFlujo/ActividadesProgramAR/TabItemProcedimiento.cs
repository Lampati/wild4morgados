using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UsingWorkflowItemPresenter.ViewModels;

namespace UsingWorkflowItemPresenter
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
            //Aca irian las variables
            sb.AppendLine("COMENZAR");
            LibreriaActividades.Extension.ProfundidadIdentacion++;
            base.Ejecutar(sb);
            LibreriaActividades.Extension.ProfundidadIdentacion--;
            sb.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("FINPROC;"));
            sb.AppendLine();
        }
    }
}
