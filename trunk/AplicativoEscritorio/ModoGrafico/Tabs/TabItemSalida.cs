using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ModoGrafico.ViewModels;
using InterfazTextoGrafico;

namespace ModoGrafico.Tabs
{
    public class TabItemSalida : Tab
    {
        public TabItemSalida()
        {
            Tipo = Enums.TipoTab.TabItemSalida;
        }

        public TabItemSalida(ProcedimientoViewModel proc)
            : base()
        {
            actividadViewModel = proc;
            Tipo = Enums.TipoTab.TabItemSalida;
            header = "SALIDA";

        }

    

        public override void Ejecutar(StringBuilder sb)
        {
            sb.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("PROCEDIMIENTO SALIDA()"));
            if (!Object.Equals(base.SecuenciaInicialDeclaraciones, null) && base.SecuenciaInicialDeclaraciones.Activities.Count > 0)
                foreach (LibreriaActividades.ActividadBase ab in base.SecuenciaInicialDeclaraciones.Activities)
                    ab.Ejecutar(sb);
            sb.AppendLine("COMENZAR");
            LibreriaActividades.Extension.ProfundidadIdentacion++;
            base.Ejecutar(sb);
            LibreriaActividades.Extension.ProfundidadIdentacion--;
            sb.AppendLine(LibreriaActividades.Extension.Tabs + String.Format("FINPROC;"));
        }

        public override int Orden
        {
            get { return int.MaxValue - 1; }
        }
    }
}
