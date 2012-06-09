using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner.ViewModels;

namespace DiagramDesigner.Tabs
{
    public class TabItemAgregar : Tab
    {
        public override void Ejecutar(StringBuilder sb)
        {
            //no hace nada este tab
        }

        protected override void RecrearWorkflowDesigner()
        {
            //no hago nada, no tengo que recrear el designer
        }

        public override int Orden
        {
            get { return 0; }
        }
    }
}
