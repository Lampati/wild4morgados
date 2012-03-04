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
using DiagramDesigner.DialogWindows;
using DiagramDesigner.EventArgsClasses;
using DataAccess.Interfases;
using DataAccess.Entidades;
using Globales.Enums;

namespace DiagramDesigner.UserControls.Toolbar
{
    /// <summary>
    /// Lógica de interacción para BarraToolbar.xaml
    /// </summary>
    public partial class BarraToolbarRibbon : UserControl
    {
        public Window Owner { get; set; }


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
                        this.ribbonGroupUndoRedoGargar.Visibility = System.Windows.Visibility.Collapsed;
                        this.ribbonGroupUndoRedoDesigner.Visibility = System.Windows.Visibility.Visible;

                        this.ribbonGroupDiagramacion.Visibility = System.Windows.Visibility.Visible;
                        this.ribbonGroupAlineacion.Visibility = System.Windows.Visibility.Visible;
                        ButtonFlujo.IsChecked = true;
                        ButtonTexto.IsChecked = false;                        
                        break;
                    case ModoVisual.Texto:
                        this.ribbonGroupUndoRedoGargar.Visibility = System.Windows.Visibility.Visible;
                        this.ribbonGroupUndoRedoDesigner.Visibility = System.Windows.Visibility.Collapsed;

                        this.ribbonGroupDiagramacion.Visibility = System.Windows.Visibility.Collapsed;
                        this.ribbonGroupAlineacion.Visibility = System.Windows.Visibility.Collapsed;
                        ButtonFlujo.IsChecked = false;
                        ButtonTexto.IsChecked = true;
                        break;
                }
            }
        }

        private bool esEjercicio;

        private EntidadBase archCargado = null;
        public EntidadBase ArchCargado
        {
            get { return archCargado; }
            set
            {
                archCargado = value;

                if (archCargado != null)
                {
                    TabDetallesEjercicio.Visibility = System.Windows.Visibility.Visible;
                    TabGeneral.Visibility = System.Windows.Visibility.Visible;
                    TabGeneral.IsSelected = true;

                    menuBttnGuardar.IsEnabled = true;
                    menuBttnGuardarComo.IsEnabled = true;
                    menuBttnImprimir.IsEnabled = true;

                    if (archCargado.GetType() == typeof(Ejercicio))
                    {
                        bttnSolGarGar.Visibility = System.Windows.Visibility.Collapsed;
                        menuBttnGuardarComoWeb.Visibility = System.Windows.Visibility.Visible;
                        bttnCrearTestPrueba.Visibility = System.Windows.Visibility.Visible;

                        esEjercicio = true;
                    }
                    else
                    {
                        bttnSolGarGar.Visibility = System.Windows.Visibility.Visible;
                        menuBttnGuardarComoWeb.Visibility = System.Windows.Visibility.Collapsed;
                        bttnCrearTestPrueba.Visibility = System.Windows.Visibility.Collapsed;

                        esEjercicio = false;
                    }
                }
                else
                {
                    TabDetallesEjercicio.Visibility = System.Windows.Visibility.Collapsed;
                    TabGeneral.Visibility = System.Windows.Visibility.Collapsed;

                    menuBttnGuardar.IsEnabled = false;
                    menuBttnGuardarComo.IsEnabled = false;
                    menuBttnImprimir.IsEnabled = false;
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

    

        private void ButtonTexto_Click(object sender, RoutedEventArgs e)
        {
            CambioModoEventFire(sender, new CambioModoEventArgs(ModoVisual.Texto));
        }

        private void ButtonFlujo_Click(object sender, RoutedEventArgs e)
        {
            CambioModoEventFire(sender, new CambioModoEventArgs(ModoVisual.Flujo));
        }

        

        private void ButtonBuscar_Click(object sender, RoutedEventArgs e)
        {
            AbrirBusquedaEventFire(sender, new AbrirBusquedaEventArgs(false));
        }

        private void ButtonBuscarYReemplazar_Click(object sender, RoutedEventArgs e)
        {
            AbrirBusquedaEventFire(sender, new AbrirBusquedaEventArgs(true));
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

        private void galleryDificultad_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ModificarPropiedadesEjercicioEventArgs eventArgs = new ModificarPropiedadesEjercicioEventArgs();
            eventArgs.NivelEjercicio = Convert.ToInt32(((Microsoft.Windows.Controls.Ribbon.RibbonGalleryItem)e.NewValue).Tag);
            ModificarPropiedadesEjercicioEventFire(sender, eventArgs);
        }

        private void bttnEnunciado_Click(object sender, RoutedEventArgs e)
        {
            TextEditionWindow textEditorWindow = new TextEditionWindow();
            textEditorWindow.Titulo = "Enunciado para este ejercicio";
            textEditorWindow.Texto = ArchCargado.Enunciado;
            textEditorWindow.EsEditable = esEjercicio;
            textEditorWindow.Owner = this.Owner;

            if (textEditorWindow.ShowDialog() == true)
            {
                if (esEjercicio)
                {
                    ModificarPropiedadesEjercicioEventArgs eventArgs = new ModificarPropiedadesEjercicioEventArgs();
                    eventArgs.Enunciado = textEditorWindow.Texto;
                    ModificarPropiedadesEjercicioEventFire(sender, eventArgs);
                }
            }         

            
        }

        private void bttnSolGarGar_Click(object sender, RoutedEventArgs e)
        {

            TextEditionWindow textEditorWindow = new TextEditionWindow();
            textEditorWindow.Titulo = "Solución GarGar para este ejercicio";
            textEditorWindow.Texto = ArchCargado.SolucionGargar;
            textEditorWindow.EsEditable = esEjercicio;
            textEditorWindow.EsContenidoGarGar = true;

            textEditorWindow.ShowDialog();
           
        }

        private void bttnSolTexto_Click(object sender, RoutedEventArgs e)
        {
            TextEditionWindow textEditorWindow = new TextEditionWindow();
            textEditorWindow.Titulo = "Enunciado para este ejercicio";
            textEditorWindow.Texto = ArchCargado.SolucionTexto;
            textEditorWindow.EsEditable = esEjercicio;

            if (textEditorWindow.ShowDialog() == true)
            {
                if (esEjercicio)
                {
                    ModificarPropiedadesEjercicioEventArgs eventArgs = new ModificarPropiedadesEjercicioEventArgs();
                    eventArgs.SolucionTexto = textEditorWindow.Texto;
                    ModificarPropiedadesEjercicioEventFire(sender, eventArgs);
                }
            }
          
        }
    }
}
