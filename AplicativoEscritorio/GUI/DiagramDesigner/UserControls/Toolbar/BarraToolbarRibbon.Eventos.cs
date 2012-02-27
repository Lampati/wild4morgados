using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DiagramDesigner.EventArgsClasses;

namespace DiagramDesigner.UserControls.Toolbar
{
    public partial class BarraToolbarRibbon : UserControl
    {
        public delegate void CompilacionEventHandler(object o, CompilacionEventArgs e);
        public delegate void CambioModoEventHandler(object o, CambioModoEventArgs e);
        public delegate void AbrirBusquedaEventHandler(object o, AbrirBusquedaEventArgs e);
        public delegate void SalvarConfiguracionEventHandler(object o, SalvarConfiguracionEventArgs e);

        public event CompilacionEventHandler CompilacionEvent;
        public event CambioModoEventHandler CambioModoEvent;
        public event AbrirBusquedaEventHandler AbrirBusquedaEvent;
        public event SalvarConfiguracionEventHandler SalvarConfiguracionEvent;

        private void CompilacionEventFire(object sender, CompilacionEventArgs e)
        {
            if (CompilacionEvent != null)
            {
                CompilacionEvent(sender, e);
            }

        }

        private void CambioModoEventFire(object sender, CambioModoEventArgs e)
        {
            if (CambioModoEvent != null)
            {
                CambioModoEvent(sender, e);
            }

        }

        private void AbrirBusquedaEventFire(object sender, AbrirBusquedaEventArgs e)
        {
            if (CambioModoEvent != null)
            {
                AbrirBusquedaEvent(sender, e);
            }

        }

        private void SalvarConfiguracionEventFire(object sender, SalvarConfiguracionEventArgs e)
        {
            if (SalvarConfiguracionEvent != null)
            {
                SalvarConfiguracionEvent(sender, e);
            }

        }
    }
}
