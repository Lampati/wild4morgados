using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiagramDesigner.Enums;

namespace DiagramDesigner
{
    /// <summary>
    /// Lógica de interacción para BarraToolbar.xaml
    /// </summary>
    public partial class BarraToolbar : UserControl
    {
        public delegate void CompilacionEventHandler(object o, CompilacionEventArgs e);
        public delegate void CambioModoEventHandler(object o, CambioModoEventArgs e);

        public event CompilacionEventHandler CompilacionEvent;
        public event CambioModoEventHandler CambioModoEvent;


        public BarraToolbar()
        {
            InitializeComponent();
        }

        private void ButtonCompilacion_Click(object sender, RoutedEventArgs e)
        {
            CompilacionEventFire(this, new CompilacionEventArgs(false));
        }

        private void ButtonEjecucion_Click(object sender, RoutedEventArgs e)
        {
            CompilacionEventFire(this, new CompilacionEventArgs(true));
        }

        private void CompilacionEventFire(object o, CompilacionEventArgs e)
        {
            if (CompilacionEvent != null)
            {
                CompilacionEvent(this, e);
            }

        }

        private void ButtonTexto_Click(object sender, RoutedEventArgs e)
        {
            CambioModoEventFire(this, new CambioModoEventArgs(ModoVisual.Texto));
        }

        private void ButtonFlujo_Click(object sender, RoutedEventArgs e)
        {
            CambioModoEventFire(this, new CambioModoEventArgs(ModoVisual.Flujo));
        }

        private void CambioModoEventFire(object o, CambioModoEventArgs e)
        {
            if (CambioModoEvent != null)
            {
                CambioModoEvent(this, e);
            }

        }
    }

    public class CompilacionEventArgs
    {
        private bool esEjecucion;
        public bool EsEjecucion
        {
            get
            {
                return esEjecucion;
            }
        }

        public CompilacionEventArgs(bool esEjec)
        {
            esEjecucion = esEjec;
        }
    }

    public class CambioModoEventArgs
    {
        private ModoVisual modoSeleccionado;
        public ModoVisual ModoSeleccionado
        {
            get
            {
                return modoSeleccionado;
            }
        }

        public CambioModoEventArgs(ModoVisual modo)
        {
            modoSeleccionado = modo;
        }
    }
}
