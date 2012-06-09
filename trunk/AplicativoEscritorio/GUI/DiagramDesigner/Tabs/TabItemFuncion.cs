using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DiagramDesigner.ViewModels;

namespace DiagramDesigner.Tabs
{
    public class TabItemFuncion : Tab
    {
        private int orden;

        public TabItemFuncion() : base()
        {
            this.orden = LibreriaActividades.Extension.AsignarOrdenTab();
        }

        public override int Orden
        {
            get { return this.orden; }
        }
    }
}
