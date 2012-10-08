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
using Ragnarok.Enums;
using Ragnarok.DialogWindows;
using Ragnarok.EventArgsClasses;
using AplicativoEscritorio.DataAccess.Interfases;
using AplicativoEscritorio.DataAccess.Entidades;
using Globales.Enums;
using Microsoft.Windows.Controls.Ribbon;
using AplicativoEscritorio.DataAccess;
using DataAccess;
using System.Diagnostics;
using System.IO;
using ModoGrafico.Views;
using Utilidades;

namespace Ragnarok.UserControls.Toolbar
{
    /// <summary>
    /// Lógica de interacción para BarraToolbar.xaml
    /// </summary>
    public partial class BarraToolbarRibbon : UserControl
    {
        public RagnarokWindow Owner { get; set; }


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
                        //this.ribbonGroupUndoRedoGargar.Visibility = System.Windows.Visibility.Collapsed;
                        //this.ribbonGroupUndoRedoDesigner.Visibility = System.Windows.Visibility.Visible;

                        //this.ribbonGroupDiagramacion.Visibility = System.Windows.Visibility.Visible;
                        //this.ribbonGroupAlineacion.Visibility = System.Windows.Visibility.Visible;

                        this.ribbonGroupIdentacion.Visibility = System.Windows.Visibility.Collapsed;
                        this.ribbonGroupBusqueda.Visibility = System.Windows.Visibility.Collapsed;

                        ButtonFlujo.IsChecked = true;
                        ButtonTexto.IsChecked = false;    
                    
                        //hasta que sepamos que imprimir, queda deshabilitado en modo grafico
                        menuBttnImprimir.IsEnabled = false;

                        break;

                    case ModoVisual.Texto:
                        //this.ribbonGroupUndoRedoGargar.Visibility = System.Windows.Visibility.Visible;
                        //this.ribbonGroupUndoRedoDesigner.Visibility = System.Windows.Visibility.Collapsed;

                        //this.ribbonGroupDiagramacion.Visibility = System.Windows.Visibility.Collapsed;
                        //this.ribbonGroupAlineacion.Visibility = System.Windows.Visibility.Collapsed;

                        this.ribbonGroupIdentacion.Visibility = System.Windows.Visibility.Visible;
                        this.ribbonGroupBusqueda.Visibility = System.Windows.Visibility.Visible;

                        ButtonFlujo.IsChecked = false;
                        ButtonTexto.IsChecked = true;

                        menuBttnImprimir.IsEnabled = true;

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
                       

                        galleryDificultad.SelectedValue = archCargado.NivelDificultad.ToString();

                        cboBoxDificultad.Visibility = System.Windows.Visibility.Visible;    

                        bttnSolGarGar.Visibility = System.Windows.Visibility.Collapsed;
                        menuBttnGuardarComoWeb.Visibility = System.Windows.Visibility.Visible;
                        bttnCrearTestPrueba.Visibility = System.Windows.Visibility.Visible;
                        bttnConsultarTestPrueba.Visibility = System.Windows.Visibility.Visible;
                        ribbonGroupDetallesDescarga.Visibility = System.Windows.Visibility.Collapsed;

                        esEjercicio = true;
                    }
                    else
                    {

                        ResolucionEjercicio ejCargado = archCargado as ResolucionEjercicio;

                        txtEjercicioId.Text = ejCargado.EjercicioId.ToString();
                        txtEjercicioId.ToolTip = string.Format("Puede encontrar el ejercicio {0} en la web de Program.Ar", ejCargado.EjercicioId);

                        if (ejCargado.EjercicioId == 0)
                        {
                            txtEjercicioId.Text = "--";
                            txtEjercicioId.ToolTip = "El ejercicio no tiene un id asignado ya que no fue descargado de la web";
                        }

                        txtBlockDificultad.Text = archCargado.NivelDificultad.ToString();

                        cboBoxDificultad.Visibility = System.Windows.Visibility.Collapsed;

                        bttnSolGarGar.Visibility = System.Windows.Visibility.Visible;
                        menuBttnGuardarComoWeb.Visibility = System.Windows.Visibility.Collapsed;
                        bttnCrearTestPrueba.Visibility = System.Windows.Visibility.Collapsed;
                        bttnConsultarTestPrueba.Visibility = System.Windows.Visibility.Collapsed;
                        ribbonGroupDetallesDescarga.Visibility = System.Windows.Visibility.Visible;
                        
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

        private void VaciarYLlenarComboDificultad()
        {
            galleryDificultad.Items.Clear();
            galleryDificultad.Items.Add(new RibbonGalleryItem() { Content = 10, ContextMenu = null, Tag = 10 });
            galleryDificultad.Items.Add(new RibbonGalleryItem() { Content = 9, ContextMenu = null, Tag = 9 });
            galleryDificultad.Items.Add(new RibbonGalleryItem() { Content = 8, ContextMenu = null, Tag = 8 });
            galleryDificultad.Items.Add(new RibbonGalleryItem() { Content = 7, ContextMenu = null, Tag = 7 });
            galleryDificultad.Items.Add(new RibbonGalleryItem() { Content = 6, ContextMenu = null, Tag = 6 });
            galleryDificultad.Items.Add(new RibbonGalleryItem() { Content = 5, ContextMenu = null, Tag = 5 });
            galleryDificultad.Items.Add(new RibbonGalleryItem() { Content = 4, ContextMenu = null, Tag = 4 });
            galleryDificultad.Items.Add(new RibbonGalleryItem() { Content = 3, ContextMenu = null, Tag = 3 });
            galleryDificultad.Items.Add(new RibbonGalleryItem() { Content = 2, ContextMenu = null, Tag = 2 });
            galleryDificultad.Items.Add(new RibbonGalleryItem() { Content = 1, ContextMenu = null, Tag = 1 });
        }

        public string DirDefaultAbrir
        {
            get
            {
                return textBoxDirAbrirDefault.Text;
            }

            set
            {
                textBoxDirAbrirDefault.Text = value;
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

            Sincronizacion.Eventos.Handler.GuardarEjercicioEvent += new Sincronizacion.Eventos.Handler.GuardarEjercicioHandler(Handler_GuardarEjercicioEvent);
            Sincronizacion.Eventos.Handler.ConectandoEvent += new Sincronizacion.Eventos.Handler.ConectandoHandler(Handler_ConectandoEvent);
            Sincronizacion.Eventos.Handler.ConectadoEvent += new Sincronizacion.Eventos.Handler.ConectadoHandler(Handler_ConectadoEvent);
            Sincronizacion.Eventos.Handler.FinalizadoEvent += new Sincronizacion.Eventos.Handler.FinalizadoHandler(Handler_FinalizadoEvent);
            Sincronizacion.Eventos.Handler.InvocandoMetodoEvent += new Sincronizacion.Eventos.Handler.InvocandoMetodoHandler(Handler_InvocandoMetodoEvent);
            Sincronizacion.Eventos.Handler.ErrorConexionEvent += new Sincronizacion.Eventos.Handler.ErrorConexionHandler(Handler_ErrorConexionEvent);

            #if (DEBUG)
            
            
                TabTestsAutomatizados.Visibility = System.Windows.Visibility.Visible;
            
            
            #else
            
                TabTestsAutomatizados.Visibility = System.Windows.Visibility.Collapsed;
            #endif

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
            CambioModoEventFire(sender, new CambioModoEventArgs(ModoVisual.Texto, e));
        }

        private void ButtonFlujo_Click(object sender, RoutedEventArgs e)
        {
            CambioModoEventFire(sender, new CambioModoEventArgs(ModoVisual.Flujo, e));
        }

        private void ButtonIdentar_Click(object sender, RoutedEventArgs e)
        {
            IdentarEventFire(sender, new IdentarEventArgs());
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
            FolderPickerLib.FolderPickerDialog fd = new FolderPickerLib.FolderPickerDialog();
            fd.InitialPath = textBoxDirEjCreados.Text;
            fd.Title = "Elija la carpeta por defecto para los ejercicios creados";
            fd.WindowStartupLocation = WindowStartupLocation.CenterOwner;            
            fd.Width = 700;

            fd.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();

            if (fd.ShowDialog().Value == true)
            {
                textBoxDirEjCreados.Text = fd.SelectedPath;
            }

            this.Owner.ClearBlurEffect();

            //FolderBrowserDialog fd = new FolderBrowserDialog();
            //if (fd.ShowDialog().Value == true)
            //{
            //    textBoxDirEjCreados.Text = fd.SelectedFolder;
            //}



            SalvarConfiguracionEventFire(this, new SalvarConfiguracionEventArgs());
        }

        private void bttnDirEjDescargados_Click(object sender, RoutedEventArgs e)
        {
            //FolderBrowserDialog fd = new FolderBrowserDialog();
            //if (fd.ShowDialog().Value == true)
            //{
            //    textBoxDirEjDescargados.Text = fd.SelectedFolder;
            //}

            FolderPickerLib.FolderPickerDialog fd = new FolderPickerLib.FolderPickerDialog();
            fd.InitialPath = textBoxDirEjDescargados.Text;
            fd.Title = "Elija la carpeta por defecto para los ejercicios descargados";
            fd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            fd.Width = 700;

            fd.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();

            if (fd.ShowDialog().Value == true)
            {
                textBoxDirEjDescargados.Text = fd.SelectedPath;
            }

            this.Owner.ClearBlurEffect();

            SalvarConfiguracionEventFire(this, new SalvarConfiguracionEventArgs());
        }

        private void bttnDirRes_Click(object sender, RoutedEventArgs e)
        {
            //FolderBrowserDialog fd = new FolderBrowserDialog();
            //if (fd.ShowDialog().Value == true)
            //{
            //    textBoxDirResoluciones.Text = fd.SelectedFolder;
            //}

            FolderPickerLib.FolderPickerDialog fd = new FolderPickerLib.FolderPickerDialog();
            fd.InitialPath = textBoxDirResoluciones.Text;
            fd.Title = "Elija la carpeta por defecto para las resoluciones";
            fd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            fd.Width = 700;

            fd.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();

            if (fd.ShowDialog().Value == true)
            {
                textBoxDirResoluciones.Text = fd.SelectedPath;
            }

            this.Owner.ClearBlurEffect();

            SalvarConfiguracionEventFire(this, new SalvarConfiguracionEventArgs());
        }

        private void bttnDirTemporales_Click(object sender, RoutedEventArgs e)
        {
            //FolderBrowserDialog fd = new FolderBrowserDialog();
            //if (fd.ShowDialog().Value == true)
            //{
            //    textBoxDirTemp.Text = fd.SelectedFolder;
            //}

            FolderPickerLib.FolderPickerDialog fd = new FolderPickerLib.FolderPickerDialog();
            fd.InitialPath = textBoxDirTemp.Text;
            fd.Title = "Elija la carpeta donde se almacenaran los temporales de la aplicación";
            fd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            fd.Width = 700;

            fd.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();

            if (fd.ShowDialog().Value == true)
            {
                textBoxDirTemp.Text = fd.SelectedPath;
            }

            this.Owner.ClearBlurEffect();

            SalvarConfiguracionEventFire(this, new SalvarConfiguracionEventArgs());
        }

        private void bttnDirAbrirDefault_Click(object sender, RoutedEventArgs e)
        {
            //FolderBrowserDialog fd = new FolderBrowserDialog();

            //if (fd.ShowDialog().Value == true)
            //{
            //    textBoxDirAbrirDefault.Text = fd.SelectedFolder;
            //}

            FolderPickerLib.FolderPickerDialog fd = new FolderPickerLib.FolderPickerDialog();
            fd.InitialPath = textBoxDirAbrirDefault.Text;
            fd.Title = "Elija la carpeta que se visualizara por defecto al elegir Abrir";
            fd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            fd.Width = 700;

            fd.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();

            if (fd.ShowDialog().Value == true)
            {
                textBoxDirAbrirDefault.Text = fd.SelectedPath;
            }

            this.Owner.ClearBlurEffect();

            SalvarConfiguracionEventFire(this, new SalvarConfiguracionEventArgs());
        }

        private void galleryDificultad_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ModificarPropiedadesEjercicioEventArgs eventArgs = new ModificarPropiedadesEjercicioEventArgs();
            eventArgs.NivelEjercicio = Convert.ToInt16(((Microsoft.Windows.Controls.Ribbon.RibbonGalleryItem)e.NewValue).Content.ToString());
            ModificarPropiedadesEjercicioEventFire(sender, eventArgs);
        }

        private void bttnEnunciado_Click(object sender, RoutedEventArgs e)
        {
            TextEditionWindow textEditorWindow = new TextEditionWindow();
            textEditorWindow.Titulo = "Enunciado";
            textEditorWindow.Copete = "Enunciado para este ejercicio";
            textEditorWindow.Texto = ArchCargado.Enunciado;
            textEditorWindow.EsEditable = esEjercicio;
            textEditorWindow.Owner = this.Owner;

            this.Owner.ApplyBlurEffect();

            if (textEditorWindow.ShowDialog() == true)
            {
                if (esEjercicio)
                {
                    ModificarPropiedadesEjercicioEventArgs eventArgs = new ModificarPropiedadesEjercicioEventArgs();
                    eventArgs.Enunciado = textEditorWindow.Texto;
                    ModificarPropiedadesEjercicioEventFire(sender, eventArgs);
                }
            }

            this.Owner.ClearBlurEffect();

        }

        private Sincronizacion.Servicio servicio;

        private Sincronizacion.Servicio Servicio
        {
            get
            {
                if (Object.Equals(this.servicio, null))
                    this.servicio = new Sincronizacion.Servicio();

                List<string> wsdls = null;
                if (!Object.Equals(ConfiguracionAplicacion.UrlsDescargaEjercicios, null))
                    wsdls = new List<string>(ConfiguracionAplicacion.UrlsDescargaEjercicios.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
                this.servicio.Urls = wsdls;
                this.servicio.Directorio = ConfiguracionAplicacion.DirectorioEjerciciosDescargados;

                return this.servicio;
            }
        }

        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);

        private void EscribirLabel(Label lbl, string texto)
        {
            Application.Current.Dispatcher.BeginInvoke(
                            System.Windows.Threading.DispatcherPriority.Send,
                            new Action(() => lbl.Content = texto));
        }

        private void EscribirProgress(ProgressBar pBar, double value)
        {
            UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(pBar.SetValue);

            Dispatcher.Invoke(updatePbDelegate,
                System.Windows.Threading.DispatcherPriority.Background,
                new object[] { ProgressBar.ValueProperty, value });
        }

        private void SetearResultadoDialog(PropertyEditionWindow pew, bool resultado)
        {
            Application.Current.Dispatcher.BeginInvoke(
                            System.Windows.Threading.DispatcherPriority.Background,
                            new Action(() => 
                                {
                                    if (!resultado)
                                        MessageBox.Show(mensajeRet, "Error Sincronización", MessageBoxButton.OK, MessageBoxImage.Error);
                                    pew.DialogResult = resultado;
                                }
                                ));
        }

        #region Consumo Eventos
        void Handler_InvocandoMetodoEvent(string texto, object lbl)
        {
            this.EscribirLabel((Label)lbl, texto);
        }

        void Handler_FinalizadoEvent(string texto, object lbl)
        {
            this.EscribirLabel((Label)lbl, texto);
        }

        void Handler_ConectadoEvent(string url, object lbl)
        {
            this.EscribirLabel((Label)lbl, "Conectando a " + url);
        }

        void Handler_ConectandoEvent(string url, object lbl)
        {
            this.EscribirLabel((Label)lbl, "Conectado a " + url + "!");
        }

        void Handler_GuardarEjercicioEvent(object barra, object lblInfo, int cant, int total)
        {
            ProgressBar pBar = (ProgressBar)barra;
            Label lbl = (Label)lblInfo;

            if (total.Equals(0))
            {
                this.EscribirLabel(lbl, "No hay ejercicios nuevos a descargar.");
                EscribirProgress(pBar, 100);
            }
            else
            {
                double value = ((double)cant / (double)total) * 100;

                this.EscribirLabel(lbl, "Guardando ejercicio " + cant.ToString() + " de " + total.ToString() + " ...");

                EscribirProgress(pBar, value);
            }
        }

        static string mensajeRet;

        void Handler_ErrorConexionEvent(string txt, object lbl)
        {
            this.EscribirLabel((Label)lbl, txt);
            mensajeRet = txt;
        }
        #endregion

        #region Metodos Threading
        void SincronizarGlobales(object propertyWindow)
        {
            PropertyEditionWindow propertyEditorWindow = (PropertyEditionWindow)propertyWindow;

            bool res = this.Servicio.EjerciciosGlobales();
            mensajeRet = "No se han encontrado Ejercicios Globales para descargar." + Environment.NewLine + "Es posible que ya los tenga todos o que no haya ninguno disponible para descargar.";
            SetearResultadoDialog(propertyEditorWindow, res);
        }

        void SincronizarCurso(object propiedades)
        {
            object[] props = (object[])propiedades;
            PropertyEditionWindow propertyEditorWindow = (PropertyEditionWindow)props[0];
            int cursoId = (int)props[1];

            bool res = this.Servicio.EjerciciosPorCurso(cursoId);
            mensajeRet = String.Format("No se han encontrado ejercicios del Curso {0} para descargar." + Environment.NewLine + "Es posible que ya los tenga todos o que el curso no exista.", cursoId);
            SetearResultadoDialog(propertyEditorWindow, res);
        }

        void SincronizarEjercicio(object propiedades)
        {
            object[] props = (object[])propiedades;
            PropertyEditionWindow propertyEditorWindow = (PropertyEditionWindow)props[0];
            int ejercicioId = (int)props[1];

            bool res = this.Servicio.EjerciciosPorId(ejercicioId);
            mensajeRet = String.Format("No se ha encontrado el ejercicio {0} para descargar." + Environment.NewLine + "Es posible que ya lo tenga o que el ejercicio no exista.", ejercicioId);
            SetearResultadoDialog(propertyEditorWindow, res);
        }
        #endregion

        private void btnSincroGeneral_Click(object sender, RoutedEventArgs e)
        {
            ConfiguracionAplicacion.RecrearDirectorios();

            PropertyEditionWindow propertyEditorWindow = new PropertyEditionWindow();
            propertyEditorWindow.Titulo = "Sincronización General (Ejercicios Globales)";
            propertyEditorWindow.Owner = this.Owner;

            propertyEditorWindow.AgregarSeparador();

            propertyEditorWindow.AgregarPropiedad("Los ejercicios a descargar son los ejercicios globales (disponibles para todos) sin importar curso/ejercicio.");


            Label lblInfo = new Label();
            lblInfo.Width = 420;
            lblInfo.Height = 25;
            lblInfo.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            lblInfo.Foreground = new SolidColorBrush(Colors.SlateGray);
            propertyEditorWindow.AgregarPropiedad(lblInfo);

            ProgressBar pBar = new ProgressBar();
            pBar.Orientation = Orientation.Horizontal;
            pBar.Width = 420;
            pBar.Height = 20;
            pBar.Maximum = 100;
            pBar.Visibility = System.Windows.Visibility.Hidden;
            propertyEditorWindow.AgregarPropiedad(pBar);

           

            Sincronizacion.Servicio srv = this.Servicio;

            srv.BarraProgreso = pBar;
            srv.LabelInfo = lblInfo;

            System.Threading.Thread th = null;
            propertyEditorWindow.AgregarBotonera(
                new RoutedEventHandler((snd, ev) =>
                    {
                        if (Object.Equals(th, null) || !th.IsAlive)
                        {
                            pBar.Visibility = System.Windows.Visibility.Visible;
                            th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(SincronizarGlobales));
                            th.Start(propertyEditorWindow);
                        }
                    }),
                new RoutedEventHandler((snd, ev) =>
                    {
                        if (!Object.Equals(th, null) && th.IsAlive)
                            th.Abort();
                        propertyEditorWindow.DialogResult = false;
                    }
                    ));

            propertyEditorWindow.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();

            propertyEditorWindow.ShowDialog();

            this.Owner.ClearBlurEffect();
        }

        private void btnSincroCurso_Click(object sender, RoutedEventArgs e)
        {
            ConfiguracionAplicacion.RecrearDirectorios();

            PropertyEditionWindow propertyEditorWindow = new PropertyEditionWindow();
            propertyEditorWindow.Titulo = "Sincronización por Curso";
            propertyEditorWindow.Owner = this.Owner;

            propertyEditorWindow.AgregarSeparador();

            propertyEditorWindow.AgregarPropiedad("Se descargaran los ejercicios del curso especificado. Podrá encontrar los identificadores en la web de programAr.");

            propertyEditorWindow.AgregarSeparador(false);

            TextBox txtCurso = new TextBox();
            txtCurso.Height = 20;
            txtCurso.Width = 300;
            txtCurso.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            propertyEditorWindow.AgregarPropiedad("ID Curso", txtCurso);

            //propertyEditorWindow.AgregarSeparador(false);

            Label lblInfo = new Label();
            lblInfo.Width = 420;
            lblInfo.Height = 25;
            lblInfo.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //lblInfo.Margin = new Thickness(5, 0, 5, 0);
            lblInfo.Foreground = new SolidColorBrush(Colors.SlateGray);
            propertyEditorWindow.AgregarPropiedad(lblInfo);

            ProgressBar pBar = new ProgressBar();
            pBar.Orientation = Orientation.Horizontal;
            pBar.Width = 420;
            pBar.Height = 20;
            pBar.Maximum = 100;
            pBar.Visibility = System.Windows.Visibility.Hidden;
            propertyEditorWindow.AgregarPropiedad(pBar);          

            Sincronizacion.Servicio srv = this.Servicio;

            srv.BarraProgreso = pBar;
            srv.LabelInfo = lblInfo;

            System.Threading.Thread th = null;
            propertyEditorWindow.AgregarBotonera(
               new RoutedEventHandler((snd, ev) =>
               {
                   if (Object.Equals(th, null) || !th.IsAlive)
                   {
                       int cursoId = 0;
                       if (int.TryParse(txtCurso.Text, out cursoId))
                       {
                           pBar.Visibility = System.Windows.Visibility.Visible;
                           th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(SincronizarCurso));
                           th.Start(new object[] { propertyEditorWindow, cursoId });
                       }
                       else
                           lblInfo.Content = "Debe ingresar un numero valido de curso";
                   }
               }),
               new RoutedEventHandler((snd, ev) =>
                   {
                       if (!Object.Equals(th, null) && th.IsAlive)
                           th.Abort();
                       propertyEditorWindow.DialogResult = false;
                   }
                   ));

            propertyEditorWindow.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();

            propertyEditorWindow.ShowDialog();

            this.Owner.ClearBlurEffect();
        }

        private void btnSincroEjercicio_Click(object sender, RoutedEventArgs e)
        {
            ConfiguracionAplicacion.RecrearDirectorios();

            PropertyEditionWindow propertyEditorWindow = new PropertyEditionWindow();
            propertyEditorWindow.Titulo = "Sincronización por Ejercicio";
            propertyEditorWindow.Owner = this.Owner;

            propertyEditorWindow.AgregarSeparador();

            propertyEditorWindow.AgregarPropiedad("Se descargará el ejercicio especificado. Podrá encontrar los identificadores en la web de programAr.");

            propertyEditorWindow.AgregarSeparador(false);

            TextBox txtEjercicio = new TextBox();
            txtEjercicio.Height = 20;
            txtEjercicio.Width = 300;
            txtEjercicio.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            propertyEditorWindow.AgregarPropiedad("ID Ejercicio", txtEjercicio);

            Label lblInfo = new Label();
            lblInfo.Width = 420;
            lblInfo.Height = 25;
            lblInfo.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            lblInfo.Foreground = new SolidColorBrush(Colors.SlateGray);
            propertyEditorWindow.AgregarPropiedad(lblInfo);

            ProgressBar pBar = new ProgressBar();
            pBar.Orientation = Orientation.Horizontal;
            pBar.Width = 420;
            pBar.Height = 20;
            pBar.Maximum = 100;
            pBar.Visibility = System.Windows.Visibility.Hidden;
            propertyEditorWindow.AgregarPropiedad(pBar);

         

            Sincronizacion.Servicio srv = this.Servicio;

            srv.BarraProgreso = pBar;
            srv.LabelInfo = lblInfo;

            System.Threading.Thread th = null;
            propertyEditorWindow.AgregarBotonera(
               new RoutedEventHandler((snd, ev) =>
               {
                   if (Object.Equals(th, null) || !th.IsAlive)
                   {
                       int ejercicioId = 0;
                       if (int.TryParse(txtEjercicio.Text, out ejercicioId))
                       {
                           pBar.Visibility = System.Windows.Visibility.Visible;
                           th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(SincronizarEjercicio));
                           th.Start(new object[] { propertyEditorWindow, ejercicioId });
                       }
                       else
                           lblInfo.Content = "Debe ingresar un numero valido de ejercicio";
                   }
               }),
               new RoutedEventHandler((snd, ev) =>
                   {
                       if (!Object.Equals(th, null) && th.IsAlive)
                           th.Abort();
                       propertyEditorWindow.DialogResult = false;
                   }
            ));

            propertyEditorWindow.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();

            propertyEditorWindow.ShowDialog();

            this.Owner.ClearBlurEffect();
        }

        private void btnPropiedadesSincro_Click(object sender, RoutedEventArgs e)
        {
            //PropertyEditionWindow propertyEditorWindow = new PropertyEditionWindow();
            //propertyEditorWindow.Titulo = "Propiedades de Sincronización";
            //propertyEditorWindow.Owner = this.Owner;

            //propertyEditorWindow.AgregarSeparador();

            //TextBox txtIP = new TextBox();
            //txtIP.Height = 80;
            //txtIP.Width = 300;
            //txtIP.TextWrapping = TextWrapping.Wrap;
            //txtIP.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            //txtIP.AcceptsReturn = true;
            //txtIP.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //txtIP.Text = ConfiguracionAplicacion.UrlsDescargaEjercicios;
            //propertyEditorWindow.AgregarPropiedad("IP/Host Servidor (ingresar cada valor en una nueva línea)", txtIP);

            ///*propertyEditorWindow.AgregarSeparador(false);
            
            //TextBox txtTimeout = new TextBox();
            //txtTimeout.Height = 20;
            //txtTimeout.Width = 30;
            //txtTimeout.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //propertyEditorWindow.AgregarPropiedad("Timeout (segs)", txtTimeout);*/

            //propertyEditorWindow.AgregarSeparador();

            //propertyEditorWindow.AgregarBotonera(
            //    new RoutedEventHandler((snd, ev) => 
            //    {
            //        ConfiguracionAplicacion.UrlsDescargaEjercicios = txtIP.Text;
            //        ConfiguracionAplicacion.Guardar(System.IO.Path.Combine(Globales.ConstantesGlobales.PathEjecucionAplicacion,
            //                             Globales.ConstantesGlobales.NOMBRE_ARCH_CONFIG_APLICACION));
            //        propertyEditorWindow.DialogResult = true;
            //    }),
            //    new RoutedEventHandler((snd, ev) => propertyEditorWindow.DialogResult = false));

            //if (propertyEditorWindow.ShowDialog() == true)
            //{
            
            //}

            

            PropiedadesSincronizacionDialog dialog = new PropiedadesSincronizacionDialog();
            dialog.CargarServidores(ConfiguracionAplicacion.UrlsDescargaEjercicios);
            dialog.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();
            dialog.ShowDialog();

            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value == true)
            {
                if (dialog.Servidores.Count > 0)
                {
                    ConfiguracionAplicacion.UrlsDescargaEjercicios = string.Join("\r\n",dialog.Servidores);
                    ConfiguracionAplicacion.Guardar(System.IO.Path.Combine(Globales.ConstantesGlobales.PathEjecucionAplicacion,
                                         Globales.ConstantesGlobales.NOMBRE_ARCH_CONFIG_APLICACION));
                }
            }

            this.Owner.ClearBlurEffect();
        }

        private void bttnSolGarGar_Click(object sender, RoutedEventArgs e)
        {

            TextEditionWindow textEditorWindow = new TextEditionWindow();
            textEditorWindow.Titulo = "Solución GarGar";
            textEditorWindow.Copete = "Solución GarGar para este ejercicio";
            textEditorWindow.Texto = ArchCargado.SolucionGargar;
            textEditorWindow.EsEditable = esEjercicio;
            textEditorWindow.EsContenidoGarGar = true;
            textEditorWindow.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();

            textEditorWindow.ShowDialog();

            this.Owner.ClearBlurEffect();
        }

        private void bttnSolTexto_Click(object sender, RoutedEventArgs e)
        {
            TextEditionWindow textEditorWindow = new TextEditionWindow();
            textEditorWindow.Titulo = "Solución";
            textEditorWindow.Copete = "Solución para este ejercicio";
            textEditorWindow.Texto = ArchCargado.SolucionTexto;
            textEditorWindow.EsEditable = esEjercicio;
            textEditorWindow.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();

            if (textEditorWindow.ShowDialog() == true)
            {
                if (esEjercicio)
                {
                    ModificarPropiedadesEjercicioEventArgs eventArgs = new ModificarPropiedadesEjercicioEventArgs();
                    eventArgs.SolucionTexto = textEditorWindow.Texto;
                    ModificarPropiedadesEjercicioEventFire(sender, eventArgs);
                }
            }

            this.Owner.ClearBlurEffect();
          
        }

      

        private void bttnCrearTestPrueba_Click(object sender, RoutedEventArgs e)
        {
            TestPruebaEventFire(this, new TestPruebaEventArgs(TestPruebaEventArgs.TipoAccion.Crear));
        }

        private void bttnEjecutarTestPrueba_Click(object sender, RoutedEventArgs e)
        {
            TestPruebaEventFire(this, new TestPruebaEventArgs(TestPruebaEventArgs.TipoAccion.Ejecutar));
        }

        private void bttnConsultarTestPrueba_Click(object sender, RoutedEventArgs e)
        {
            TestPruebaEventFire(this, new TestPruebaEventArgs(TestPruebaEventArgs.TipoAccion.Consultar));
        }

        private void bttnTestBatch_Click(object sender, RoutedEventArgs e)
        {
            TesterCompilador.TesterBatchWindow window = null;

            RibbonButton botonPresionado = (RibbonButton)e.Source;             
            switch (Convert.ToInt32(botonPresionado.CommandParameter))
            {
                case 1:
                    window = new TesterCompilador.TesterBatchWindow(TesterCompilador.ModoTest.EjerciciosCorrectos);
                    break;

                case 2:
                    window = new TesterCompilador.TesterBatchWindow(TesterCompilador.ModoTest.ErroresSintacticos);
                    break;

            }
            if (window != null)
            {
                window.Show();
            }
        }

        private void bttnMostrarMensajesError_Click(object sender, RoutedEventArgs e)
        {
            TesterCompilador.ErroresMensajeDisplayerWindow window = new TesterCompilador.ErroresMensajeDisplayerWindow();

            window.Show();
        }

        private void bttnAyudaManual_Click(object sender, RoutedEventArgs e)
        {
            AbrirArchivoManual("ManualRagnarok.pdf", global::Ragnarok.Properties.Resources.ManualRagnarok, "El manual del aplicativo Ragnarok no se pudo abrir");
        }

        private void bttnAyudaGarGarManual_Click(object sender, RoutedEventArgs e)
        {
            AbrirArchivoManual("ManualGargar.pdf", global::Ragnarok.Properties.Resources.ManualGarGar, "El manual del lenguaje GarGar no se pudo abrir");
        }

        private void AbrirArchivoManual(string fileName, byte[] arch, string error)
        {
            try
            {
                DirectoriosManager.CrearDirectorioSiNoExiste(ConfiguracionAplicacion.DirectorioTemporal, false);
                string pathEntero = System.IO.Path.Combine(ConfiguracionAplicacion.DirectorioTemporal, fileName);

                if (File.Exists(pathEntero))
                {
                    if (!FileManager.IsFileLocked(new FileInfo(pathEntero)))
                    {
                        File.Delete(pathEntero);
                        System.IO.File.WriteAllBytes(pathEntero, arch);
                    }                
                    
                }              

                System.Diagnostics.Process.Start(pathEntero);
            }
            catch (Exception)
            {
                MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bttnAcercaDe_Click(object sender, RoutedEventArgs e)
        {
            AcercaDeWindow acercaDeWindow = new AcercaDeWindow();
            acercaDeWindow.Owner = this.Owner;
            this.Owner.ApplyBlurEffect();

            acercaDeWindow.ShowDialog();

            this.Owner.ClearBlurEffect();
        }
    }
}
