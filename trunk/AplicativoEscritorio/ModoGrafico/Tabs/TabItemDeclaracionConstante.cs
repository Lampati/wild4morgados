using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.ViewModels;
using InterfazTextoGrafico;

namespace ModoGrafico.Tabs
{
    public class TabItemDeclaracionConstante : Tab
    {
        public TabItemDeclaracionConstante()
        {
            Tipo = Enums.TipoTab.TabItemDeclaracionConstante;
        }

        public TabItemDeclaracionConstante(SecuenciaViewModel proc)
            : base()
        {
            actividadViewModel = proc;
            Tipo = Enums.TipoTab.TabItemDeclaracionConstante;
        }

        public override int Orden
        {
            get { return 1; }
        }

        public override void Ejecutar(StringBuilder sb)
        {
            if (!Object.Equals(base.SecuenciaInicialProcedimiento, null) && base.SecuenciaInicialProcedimiento.Activities.Count > 0)
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
