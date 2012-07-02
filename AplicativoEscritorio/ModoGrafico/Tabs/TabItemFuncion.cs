using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ModoGrafico.ViewModels;
using InterfazTextoGrafico;

namespace ModoGrafico.Tabs
{
    public class TabItemFuncion : Tab
    {
        private int orden;

        public TabItemFuncion() : base()
        {
            this.orden = LibreriaActividades.Extension.AsignarOrdenTab();
            Tipo = Enums.TipoTab.TabItemFuncion;
        }

        public TabItemFuncion(ProcedimientoViewModel proc) : base()
        {
            actividadViewModel = proc;
            this.orden = LibreriaActividades.Extension.AsignarOrdenTab();
            Tipo = Enums.TipoTab.TabItemFuncion;

            header = proc.Nombre.ToUpper().Trim();
        }

        

        public override int Orden
        {
            get { return this.orden; }
        }
    }
}
