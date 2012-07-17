using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.ViewModels;

namespace ModoGrafico.Tabs
{
    public class TabItemAgregarFuncion : Tab
    {

        public TabItemAgregarFuncion()
            : base()
        {
            Tipo = Enums.TipoTab.TabItemAgregarFuncion;
        }

        public override void Ejecutar(StringBuilder sb)
        {
            //no hace nada este tab
        }

        protected override void RecrearWorkflowDesigner()
        {
            //no hago nada, no tengo que recrear el designer
        }

        public override void ActualizarPropiedadesTab(Interfaces.IPropiedadesContexto props)
        {
        }

        public override Interfaces.IPropiedadesContexto ObtenerPropiedadesTab()
        {
            return null;
        }

        public override int Orden
        {
            get { return 0; }
        }
    }
}
