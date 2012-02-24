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
using FolderBrowser;

namespace DiagramDesigner
{
    /// <summary>
    /// Lógica de interacción para BarraToolbar.xaml
    /// </summary>
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

        

        private ModoVisual modo;
        public ModoVisual Modo
        {
            get { return this.modo; }
            set
            {
                this.modo = value;
                switch (this.modo)
                {
                    case ModoVisual.Flujo:
                        this.GrupoDiagramacion.Visibility = System.Windows.Visibility.Visible;
                        this.GrupoAlineacion.Visibility = System.Windows.Visibility.Visible;
                        ButtonFlujo.IsChecked = true;
                        ButtonTexto.IsChecked = false;
                        break;
                    case ModoVisual.Texto:
                        this.GrupoDiagramacion.Visibility = System.Windows.Visibility.Collapsed;
                        this.GrupoAlineacion.Visibility = System.Windows.Visibility.Collapsed;
                        ButtonFlujo.IsChecked = false;
                        ButtonTexto.IsChecked = true;
                        break;
                }
            }
        }

        
        public string DirTemporales
        {
            get
            {
                return textBoxDirTemp.Text;
            }

            set
            {
                textBoxDirTemp.Text = value; 
            }
        }

        public string DirEjCreados
        {
            get
            {
                return textBoxDirEjCreados.Text;
            }

            set
            {
                textBoxDirEjCreados.Text = value;
            }
        }

        public string DirEjDescargados
        {
            get
            {
                return textBoxDirEjDescargados.Text;
            }

            set
            {
                textBoxDirEjDescargados.Text = value;
            }
        }

        public string DirResoluciones
        {
            get
            {
                return textBoxDirResoluciones.Text;
            }

            set
            {
                textBoxDirResoluciones.Text = value;
            }
        }

        public BarraToolbarRibbon()
        {
            InitializeComponent();

           

        }

      



        private void ButtonCompilacion_Click(object sender, RoutedEventArgs e)
        {
            CompilacionEventFire(sender, new CompilacionEventArgs(false));
        }

        private void ButtonEjecucion_Click(object sender, RoutedEventArgs e)
        {
            CompilacionEventFire(sender, new CompilacionEventArgs(true));
        }

        private void CompilacionEventFire(object sender, CompilacionEventArgs e)
        {
            if (CompilacionEvent != null)
            {
                CompilacionEvent(sender, e);
            }

        }

        private void ButtonTexto_Click(object sender, RoutedEventArgs e)
        {
            CambioModoEventFire(sender, new CambioModoEventArgs(ModoVisual.Texto));
        }

        private void ButtonFlujo_Click(object sender, RoutedEventArgs e)
        {
            CambioModoEventFire(sender, new CambioModoEventArgs(ModoVisual.Flujo));
        }

        private void CambioModoEventFire(object sender, CambioModoEventArgs e)
        {
            if (CambioModoEvent != null)
            {
                CambioModoEvent(sender, e);
            }

        }

        private void ButtonBuscar_Click(object sender, RoutedEventArgs e)
        {
            AbrirBusquedaEventFire(sender, new AbrirBusquedaEventArgs(false));
        }

        private void ButtonBuscarYReemplazar_Click(object sender, RoutedEventArgs e)
        {
            AbrirBusquedaEventFire(sender, new AbrirBusquedaEventArgs(true));
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

        private void bttnDirEjCreados_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog().Value == true)
            {
                textBoxDirEjCreados.Text = fd.SelectedFolder;
            }

            SalvarConfiguracionEventFire(this, new SalvarConfiguracionEventArgs());

        }

        private void bttnDirEjDescargados_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog().Value == true)
            {
                textBoxDirEjDescargados.Text = fd.SelectedFolder;
            }

            SalvarConfiguracionEventFire(this, new SalvarConfiguracionEventArgs());
        }

        private void bttnDirRes_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog().Value == true)
            {
                textBoxDirResoluciones.Text = fd.SelectedFolder;
            }

            SalvarConfiguracionEventFire(this, new SalvarConfiguracionEventArgs());
        }

        private void bttnDirTemporales_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog().Value == true)
            {
                textBoxDirTemp.Text = fd.SelectedFolder;
            }

            SalvarConfiguracionEventFire(this, new SalvarConfiguracionEventArgs());
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

    public class AbrirBusquedaEventArgs
    {
        private bool esBuscarYReemplazar;
        public bool EsBuscarYReemplazar
        {
            get
            {
                return esBuscarYReemplazar;
            }
        }

        public AbrirBusquedaEventArgs(bool esReemp)
        {
            esBuscarYReemplazar = esReemp;
        }
    }

    public class SalvarConfiguracionEventArgs
    {

    }
}
