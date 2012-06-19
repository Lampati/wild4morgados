using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ModoGrafico.ViewModels;
using InterfazTextoGrafico;

namespace ModoGrafico.Tabs
{
    public class TabItemDeclaracionVariable : Tab
    {
        public TabItemDeclaracionVariable()
        {
            Tipo = Enums.TipoTab.TabItemDeclaracionVariable;
        }

        public TabItemDeclaracionVariable(SecuenciaViewModel proc)
            : base()
        {
            actividadViewModel = proc;
            Tipo = Enums.TipoTab.TabItemDeclaracionVariable;
        }

        public override int Orden
        {
            get { return 2; }
        }

        public override void Ejecutar(StringBuilder sb)
        {
            if (!Object.Equals(base.SecuenciaInicialProcedimiento, null) && base.SecuenciaInicialProcedimiento.Activities.Count > 0)
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
