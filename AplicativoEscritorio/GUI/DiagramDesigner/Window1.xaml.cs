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


            }
        }

        public Window1()
        {
            InitializeComponent();

            this.BarraMsgs.DoubleClickEvent += new BarraMensajes.DobleClickEnBarraMensajesEventHandler(BarraMsgs_DoubleClickEvent);
            this.Esquema.ModoTextoCambiarPosicionEvent += new EsquemaCentral.ModoTextoCambiarPosicionEventHandler(Esquema_ModoTextoCambiarPosicionEvent);

            this.ToolbarAplicacion.CompilacionEvent += new BarraToolbarRibbon.CompilacionEventHandler(ToolbarAplicacion_CompilacionEvent);
            this.ToolbarAplicacion.CambioModoEvent += new BarraToolbarRibbon.CambioModoEventHandler(ToolbarAplicacion_CambioModoEvent);
            this.ToolbarAplicacion.AbrirBusquedaEvent += new BarraToolbarRibbon.AbrirBusquedaEventHandler(ToolbarAplicacion_AbrirBusquedaEvent);
            this.ToolbarAplicacion.SalvarConfiguracionEvent += new BarraToolbarRibbon.SalvarConfiguracionEventHandler(ToolbarAplicacion_SalvarConfiguracionEvent);

            this.SizeChanged += new SizeChangedEventHandler(Window1_SizeChanged);
            //NO TENGO EL XML!!!
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


            Modo = ModoVisual.Flujo;
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                int i = 0;  
            }
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                int i = 0;
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                int i = 0;
            }
        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                int i = 0;
            }
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "WPF Diagram");
            }
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int i = 0;

            MessageBoxResult result = MessageBox.Show("¿Desea guardar los cambios efectuados?", "ProgramAR", MessageBoxButton.YesNoCancel);

            switch (result)
            {
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.No:
                    Close();
                    break;
                case MessageBoxResult.Yes:
                    Close();
                    break;
                default:
                    break;
            }
        }

        void ToolbarAplicacion_SalvarConfiguracionEvent(object o, SalvarConfiguracionEventArgs e)
        {
            configApp.DirectorioEjerciciosCreados = ToolbarAplicacion.DirEjCreados;
            configApp.DirectorioEjerciciosDescargados = ToolbarAplicacion.DirEjDescargados;
            configApp.DirectorioResolucionesEjercicios = ToolbarAplicacion.DirResoluciones;
            configApp.DirectorioTemporal = ToolbarAplicacion.DirTemporales;   

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
