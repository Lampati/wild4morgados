using System.Windows;
using CompiladorGargar.Resultado;
using WpfApplicationHotKey.WinApi;
using CompiladorGargar;
using System.Windows.Input;
using System;
using Utilidades;
using DiagramDesigner.Enums;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DataAccess;
using System.IO;
using Microsoft.Windows.Controls.Ribbon;
using DiagramDesigner.EventArgsClasses;
using DiagramDesigner.UserControls.Mensajes;
using DiagramDesigner.UserControls.Toolbar;
using DiagramDesigner.UserControls.Entorno;
using DataAccess.Interfases;
using DataAccess.Entidades;
using Globales.Enums;
using Microsoft.Win32;
using DiagramDesigner.Helpers;
using System.Windows.Documents;
using Globales;

namespace DiagramDesigner
{
    public partial class Window1 : RibbonWindow
    {
        #region Hotkeys Definicion
        HotKey hotKeyCompilar;
        HotKey hotKeyEjecutar;
        #endregion

        Compilador compilador;

        ConfiguracionAplicacion configApp;


        private EntidadBase archCargado = null;
        public EntidadBase ArchCargado
        {
            get { return archCargado; }
            set
            {
                archCargado = value;

                ToolbarAplicacion.ArchCargado = archCargado;
                Esquema.ArchCargado = archCargado;
                BarraEstado.ArchCargado = archCargado;

                if (archCargado != null)
                {
                    BarraMsgs.Visibility = System.Windows.Visibility.Visible;                    
                    Modo = archCargado.UltimoModoGuardado;

                    Title = string.Format("{0} -- {1}", ConstantesGlobales.NOMBRE_APLICACION, archCargado.Nombre);
                    
                }
                else
                {
                    BarraMsgs.Visibility = System.Windows.Visibility.Hidden;

                }

                
            }
        }


        private ModoVisual modo;
        public ModoVisual Modo
        {
            get { return this.modo; }
            set
            {
                this.modo = value;

                Esquema.Modo = value;
                BarraMsgs.Modo = value;
                BarraEstado.Modo = value;
                ToolbarAplicacion.Modo = value;

                if (ArchCargado != null)
                {
                    ArchCargado.UltimoModoGuardado = modo;
                }
            }
        }

        public Window1()
        {
            InitializeComponent();

            Title = ConstantesGlobales.NOMBRE_APLICACION;

            this.BarraMsgs.DoubleClickEvent += new BarraMensajes.DobleClickEnBarraMensajesEventHandler(BarraMsgs_DoubleClickEvent);
            this.Esquema.ModoTextoCambiarPosicionEvent += new EsquemaCentral.ModoTextoCambiarPosicionEventHandler(Esquema_ModoTextoCambiarPosicionEvent);

            this.ToolbarAplicacion.CompilacionEvent += new BarraToolbarRibbon.CompilacionEventHandler(ToolbarAplicacion_CompilacionEvent);
            this.ToolbarAplicacion.CambioModoEvent += new BarraToolbarRibbon.CambioModoEventHandler(ToolbarAplicacion_CambioModoEvent);
            this.ToolbarAplicacion.AbrirBusquedaEvent += new BarraToolbarRibbon.AbrirBusquedaEventHandler(ToolbarAplicacion_AbrirBusquedaEvent);
            this.ToolbarAplicacion.SalvarConfiguracionEvent += new BarraToolbarRibbon.SalvarConfiguracionEventHandler(ToolbarAplicacion_SalvarConfiguracionEvent);
            this.ToolbarAplicacion.ModificarPropiedadesEjercicioEvent += new BarraToolbarRibbon.ModificarPropiedadesEjercicioHandler(ToolbarAplicacion_ModificarPropiedadesEjercicioEvent);

            this.Loaded += new RoutedEventHandler(Window1_Loaded);

            this.SizeChanged += new SizeChangedEventHandler(Window1_SizeChanged);
            
            ConfigurarCompilador();

            hotKeyCompilar = new HotKey(ModifierKeys.None, Keys.F3, Window.GetWindow(this));
            hotKeyEjecutar = new HotKey(ModifierKeys.None, Keys.F4, Window.GetWindow(this));

            hotKeyCompilar.HotKeyPressed += new Action<HotKey>(hotKeyCompilar_HotKeyPressed);
            hotKeyEjecutar.HotKeyPressed += new Action<HotKey>(hotKeyCompilar_HotKeyPressed);


            
            

            configApp = new ConfiguracionAplicacion();
            configApp.Abrir(Path.Combine(Globales.ConstantesGlobales.PathEjecucionAplicacion,
                                         Globales.ConstantesGlobales.NOMBRE_ARCH_CONFIG_APLICACION));

            ToolbarAplicacion.DirEjCreados = configApp.DirectorioEjerciciosCreados;
            ToolbarAplicacion.DirEjDescargados = configApp.DirectorioEjerciciosDescargados;
            ToolbarAplicacion.DirResoluciones = configApp.DirectorioResolucionesEjercicios;
            ToolbarAplicacion.DirTemporales = configApp.DirectorioTemporal;
            ToolbarAplicacion.DirDefaultAbrir = configApp.DirectorioAbrirDefault; 
            //configApp.DirectorioTemporal = "bla";
            //configApp.DirectorioEjerciciosCreados = "bla43";
            //configApp.DirectorioEjerciciosDescargados = "bla1";
            //configApp.DirectorioResolucionesEjercicios = "bla2";

            //configApp.Guardar(Path.Combine(Globales.ConstantesGlobales.PathEjecucionAplicacion,
            //                               Globales.ConstantesGlobales.NOMBRE_ARCH_CONFIG_APLICACION));

            

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.New, New_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, Open_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, Save_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs, SaveAs_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, Print_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, Close_Executed));

            ToolbarAplicacion.Owner = this;
            
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            Modo = ModoVisual.Flujo;

            ArchCargado = null;
        }

       

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                bool continuar = SalvarSiUsuarioQuiere();

                if (continuar)
                {
                    string path;
                    switch (Convert.ToInt32(e.Parameter))
                    {
                        case 1:

                            path = FileDialogManager.ElegirUbicacionNuevoEjercicio(this, "Elegir nombre y ubicación para el nuevo ejercicio", configApp.DirectorioEjerciciosCreados);                            

                            if (!string.IsNullOrWhiteSpace(path))
                            {

                                Ejercicio ej = new Ejercicio();
                                ej.UltimoModoGuardado = ModoVisual.Texto;
                                ej.Modo = DataAccess.Enums.ModoEjercicio.Normal;
                                ej.ModificadoDesdeUltimoGuardado = false;
                                ej.PathGuardadoActual = path;
                                
                                ej.Guardar(ej.PathGuardadoActual);
                                ArchCargado = ej;
                                //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                                ej.ModificadoDesdeUltimoGuardado = false;
                            }
                            break;
                        case 2:

                            string pathEj = FileDialogManager.ElegirUbicacionNuevoEjercicio(this, "Elegir ejercicio a resolver", configApp.DirectorioEjerciciosDescargados);

                            if (!string.IsNullOrWhiteSpace(pathEj))
                            {

                                //chequeo de tipo de archivo
                                if (pathEj.ToLower().EndsWith(string.Format(".{0}", Globales.ConstantesGlobales.EXTENSION_EJERCICIO)))
                                {

                                    Ejercicio ej = new Ejercicio();
                                    ej.PathGuardadoActual = pathEj;
                                    ej.Abrir(ej.PathGuardadoActual);
                                    //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                                    ej.ModificadoDesdeUltimoGuardado = false;

                                    path = FileDialogManager.ElegirUbicacionNuevaResolucion(this, string.Format("Elegir nombre y ubicación para la nueva resolucón del ej {0}",ej.Nombre), configApp.DirectorioResolucionesEjercicios);

                                    if (!string.IsNullOrWhiteSpace(path))
                                    {

                                        ResolucionEjercicio res = new ResolucionEjercicio(ej);
                                        
                                        res.UltimoModoGuardado = ModoVisual.Texto;
                                        res.Modo = DataAccess.Enums.ModoEjercicio.Normal;
                                        res.ModificadoDesdeUltimoGuardado = false;
                                        res.PathGuardadoActual = path;                                        
                                        res.Guardar(res.PathGuardadoActual);
                                        ArchCargado = res;
                                        //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                                        res.ModificadoDesdeUltimoGuardado = false;
                                    }
                                }
                                else
                                {
                                    //Error formato no soportado
                                }
                            }
                            break;
                    }
                }
            }
        }

        

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                bool continuar = SalvarSiUsuarioQuiere();

                if (continuar)
                {
                    string path = FileDialogManager.ElegirArchivoParaAbrir(this, "Elija el archivo a abrir", configApp.DirectorioAbrirDefault);

                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        //chequeo de tipo de archivo
                        if (path.ToLower().EndsWith(string.Format(".{0}",Globales.ConstantesGlobales.EXTENSION_EJERCICIO)))
                        {
                            Ejercicio ej = new Ejercicio();
                            ej.PathGuardadoActual = path;
                            ej.Abrir(ej.PathGuardadoActual);
                            ArchCargado = ej;
                            //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                            ej.ModificadoDesdeUltimoGuardado = false;
                        }
                        else if (path.ToLower().EndsWith(string.Format(".{0}", Globales.ConstantesGlobales.EXTENSION_RESOLUCION)))
                        {
                            ResolucionEjercicio res = new ResolucionEjercicio();
                            res.PathGuardadoActual = path;
                            res.Abrir(res.PathGuardadoActual);
                            ArchCargado = res;
                            //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                            res.ModificadoDesdeUltimoGuardado = false;
                        }
                        else
                        {
                            //Error formato no soportado
                        }                        
                    }
                }
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                GuardarADisco();
            }
        }

        

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                
            }
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                if (Modo == ModoVisual.Texto)
                {

                    FlowDocument doc = new FlowDocument(new Paragraph(new Run(Esquema.textEditor.Text)));
                    doc.Name = "GarGar";

                    doc.ColumnWidth = printDialog.PrintableAreaWidth;
                    doc.PagePadding = new Thickness(25);

                    // Create IDocumentPaginatorSource from FlowDocument
                    IDocumentPaginatorSource idpSource = doc;

                    // Call PrintDocument method to send document to printer

                    printDialog.PrintDocument(idpSource.DocumentPaginator, "Documento Gargar");    
                }
                else
                {
                    printDialog.PrintVisual(Esquema.MyDesigner, "WPF Diagram");
                }
            }
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int i = 0;

            bool continuar = SalvarSiUsuarioQuiere();

            if (continuar)
            {
                Close();
            }
        }

        private bool SalvarSiUsuarioQuiere()
        {
            bool retorno = true;

            if (ArchCargado != null && ArchCargado.ModificadoDesdeUltimoGuardado)
            {
                MessageBoxResult result = MessageBox.Show("¿Desea guardar los cambios efectuados?", "ProgramAR", MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        retorno = false;
                        break;

                    case MessageBoxResult.No:
                        break;

                    case MessageBoxResult.Yes:
                        GuardarADisco();
                        break;
                }
            }

            return retorno;
        }

        private void GuardarADisco()
        {
            ArchCargado.Guardar(ArchCargado.PathGuardadoActual);
            ArchCargado.ModificadoDesdeUltimoGuardado = false;
        }

        void ToolbarAplicacion_SalvarConfiguracionEvent(object o, SalvarConfiguracionEventArgs e)
        {
            configApp.DirectorioEjerciciosCreados = ToolbarAplicacion.DirEjCreados;
            configApp.DirectorioEjerciciosDescargados = ToolbarAplicacion.DirEjDescargados;
            configApp.DirectorioResolucionesEjercicios = ToolbarAplicacion.DirResoluciones;
            configApp.DirectorioTemporal = ToolbarAplicacion.DirTemporales;
            configApp.DirectorioAbrirDefault = ToolbarAplicacion.DirDefaultAbrir;   
            


            configApp.Guardar(Path.Combine(Globales.ConstantesGlobales.PathEjecucionAplicacion,
                                         Globales.ConstantesGlobales.NOMBRE_ARCH_CONFIG_APLICACION));
        }

        void Window1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BarraMsgs.AjustarSize();
        }

        void ToolbarAplicacion_AbrirBusquedaEvent(object o, AbrirBusquedaEventArgs e)
        {
            if (this.Modo == ModoVisual.Texto)
            {
                Esquema.MostrarBuscar(e.EsBuscarYReemplazar);                  
                
            }
        }

        void ToolbarAplicacion_CambioModoEvent(object o, CambioModoEventArgs e)
        {
            //ToggleButton botonPresionado = (ToggleButton)o;

            ReiniciarIDEParaCompilacion();

            Modo = e.ModoSeleccionado;
            

        }

        void ToolbarAplicacion_CompilacionEvent(object o, CompilacionEventArgs e)
        {
            if (e.EsEjecucion)
            {
                Ejecutar();
            }
            else
            {
                Compilar();
            }
        }

        void Esquema_ModoTextoCambiarPosicionEvent(object o, ModoTextoCambiarPosicionEventArgs e)
        {
            this.BarraEstado.ModificarPosicionModoTexto(e.Fila, e.Columna);
        }

        void BarraMsgs_DoubleClickEvent(object o, DoubleClickEventArgs e)
        {
            this.Esquema.PosicionarseEn(e.Fila, e.Columna);
        }

        void ToolbarAplicacion_ModificarPropiedadesEjercicioEvent(object o, ModificarPropiedadesEjercicioEventArgs e)
        {
            if (e.Enunciado != null)
            {
                archCargado.Enunciado = e.Enunciado;
            }

            if (e.NivelEjercicio != null)
            {
                archCargado.NivelDificultad = e.NivelEjercicio.Value;
            }

            if (e.SolucionTexto != null)
            {
                archCargado.SolucionTexto = e.SolucionTexto;
            }

            
        }

        private void ConfigurarCompilador()
        {
            bool modoDebug = false;

            string directorioActual = Globales.ConstantesGlobales.PathEjecucionAplicacion;

            compilador = new Compilador(modoDebug, directorioActual, directorioActual, "prueba");
        }

        void hotKeyCompilar_HotKeyPressed(HotKey obj)
        {
            switch (obj.Key)
            {
                case Keys.F3:
                    Compilar();
                    break;
                case Keys.F4:
                    Ejecutar();
                    break;

                default:
                    break;
            }

        }

        private void Compilar()
        {
            ReiniciarIDEParaCompilacion();

            string programa = this.Esquema.GarGarACompilar;
            ResultadoCompilacion res = this.compilador.Compilar(programa);

            MostrarResultadosCompilacion(res);

            if (!string.IsNullOrEmpty(res.Error))
            {
                MessageBox.Show(res.Error);
            }

        }

        private void Ejecutar()
        {
            ReiniciarIDEParaCompilacion();

            string programa = this.Esquema.GarGarACompilar;
            ResultadoCompilacion res = this.compilador.Compilar(programa);

            MostrarResultadosCompilacion(res);

            if (!string.IsNullOrEmpty(res.Error))
            {
                MessageBox.Show(res.Error);
            }

            if (res.CompilacionGarGarCorrecta && res.GeneracionEjectuableCorrecto)
            {
                EjecucionManager.EjecutarConVentana(res.ArchEjecutableConRuta);
            }
        }

        private void ReiniciarIDEParaCompilacion()
        {
            //Aca reinicar la IDE
            this.BarraMsgs.BorrarTodosMensajes();
            
        }

        private void MostrarResultadosCompilacion(ResultadoCompilacion res)
        {
            if (res.CompilacionGarGarCorrecta && res.ResultadoCompPascal != null && res.ResultadoCompPascal.CompilacionPascalCorrecta)
            {
                BarraEstado.Estado = "Compilación Correcta";
            }
            else
            {
                BarraEstado.Estado = "Error en compilación";

                foreach (var item in res.ListaErrores)
                {
                    this.BarraMsgs.AgregarError(item.Descripcion, item.Fila, item.Columna);
                }
            }
        }
    }
}
