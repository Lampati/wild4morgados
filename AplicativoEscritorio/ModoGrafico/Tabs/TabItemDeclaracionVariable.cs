using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ModoGrafico.ViewModels;

namespace ModoGrafico.Tabs
{
    public class TabItemDeclaracionVariable : Tab
    {
        public override int Orden
        {
            get { return 2; }
        }

        public override void Ejecutar(StringBuilder sb)
        {
            if (!Object.Equals(base.init, null) && base.init.Activities.Count > 0)
            {
                sb.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("VARIABLES"));
                LibreriaActividades.Extension.ProfundidadIdentacion++;
                base.Ejecutar(sb);
                LibreriaActividades.Extension.ProfundidadIdentacion--;
                sb.AppendLine();
            }
        }
    }
}
