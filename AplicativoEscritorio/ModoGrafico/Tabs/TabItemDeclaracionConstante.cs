using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.ViewModels;

namespace ModoGrafico.Tabs
{
    public class TabItemDeclaracionConstante : Tab
    {
        public override int Orden
        {
            get { return 1; }
        }

        public override void Ejecutar(StringBuilder sb)
        {
            if (!Object.Equals(base.init, null) && base.init.Activities.Count > 0)
            {
                sb.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("CONSTANTES"));
                LibreriaActividades.Extension.ProfundidadIdentacion++;
                base.Ejecutar(sb);
                LibreriaActividades.Extension.ProfundidadIdentacion--;
                sb.AppendLine();
            }
        }
    }
}
