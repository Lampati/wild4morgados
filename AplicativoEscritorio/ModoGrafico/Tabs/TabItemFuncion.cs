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
        }

        public TabItemFuncion(ProcedimientoViewModel proc) : base()
        {
            actividadViewModel = proc;
            this.orden = LibreriaActividades.Extension.AsignarOrdenTab();

        }

        

        public override int Orden
        {
            get { return this.orden; }
        }
    }
}
