using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.ViewModels;

namespace ModoGrafico.Tabs
{
    public class TabItemAgregarProcedimiento : Tab
    {

        public TabItemAgregarProcedimiento()
            : base()
        {
            Tipo = Enums.TipoTab.TabItemAgregarProcedimiento;
        }

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
