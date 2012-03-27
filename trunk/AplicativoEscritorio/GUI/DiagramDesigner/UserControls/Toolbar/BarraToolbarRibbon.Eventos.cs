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
        public delegate void ModificarPropiedadesEjercicioHandler(object o, ModificarPropiedadesEjercicioEventArgs e);
        public delegate void TestPruebaHandler(object o, TestPruebaEventArgs e);

        public event CompilacionEventHandler CompilacionEvent;
        public event CambioModoEventHandler CambioModoEvent;
        public event AbrirBusquedaEventHandler AbrirBusquedaEvent;
        public event SalvarConfiguracionEventHandler SalvarConfiguracionEvent;
        public event ModificarPropiedadesEjercicioHandler ModificarPropiedadesEjercicioEvent;
        public event TestPruebaHandler TestPruebaEvent;


        private void TestPruebaEventFire(object sender, TestPruebaEventArgs e)
        {
            if (TestPruebaEvent != null)
            {
                TestPruebaEvent(sender, e);
            }

        }

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
            if (AbrirBusquedaEvent != null)
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

        private void ModificarPropiedadesEjercicioEventFire(object sender, ModificarPropiedadesEjercicioEventArgs e)
        {
            if (ModificarPropiedadesEjercicioEvent != null)
            {
                ModificarPropiedadesEjercicioEvent(sender, e);
            }

        }

     
    }
}
