﻿using System.Windows;
using CompiladorGargar.Resultado;
using WpfApplicationHotKey.WinApi;
using CompiladorGargar;
using System.Windows.Input;
using System;
using Utilidades;
using Ragnarok.Enums;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using AplicativoEscritorio.DataAccess;
using System.IO;
using Microsoft.Windows.Controls.Ribbon;
using Ragnarok.EventArgsClasses;
using Ragnarok.UserControls.Mensajes;
using Ragnarok.UserControls.Toolbar;
using Ragnarok.UserControls.Entorno;
using AplicativoEscritorio.DataAccess.Interfases;
using AplicativoEscritorio.DataAccess.Entidades;
using Globales.Enums;
using Microsoft.Win32;
using Ragnarok.Helpers;
using System.Windows.Documents;
using Globales;
using System.Diagnostics;
using CompiladorGargar.Resultado.Auxiliares;
using EJEKOR;
using Ragnarok.TestsPruebas;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ragnarok.DialogWindows;
using DataAccess.Entidades;
using DataAccess;
using InterfazTextoGrafico;
using LibreriaActividades;
using ModoGrafico;
using CompiladorGargar.Sintactico.ErroresManager.Errores;
using SplashScreen;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using System.ComponentModel;
using Ragnarok.Tutorial;

namespace Ragnarok
{
    public partial class RagnarokWindow : RibbonWindow
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
                    Modo = (ModoVisual)archCargado.UltimoModoGuardado;

  					// flanzani 11/11/2012
        			// IDC_APP_5
        			// Tutorial para la aplicacion
        			// Al cargar un archivo, disparo el tutorial para modo grafico o texto
                    if (Modo == ModoVisual.Texto)
                    {
                        TutorialManager.MostrarTutorialModoTexto(this);
                    }
                    else
                    {
                        TutorialManager.MostrarTutorialModoGrafico(this);
                    }

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
                    ArchCargado.UltimoModoGuardado = (AplicativoEscritorio.DataAccess.Enums.ModoVisual)modo;
                }
            }
        }

        private void ProbarCarga()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int id = 1; id < 10; id++)
            {
                Ejercicio ej = new Ejercicio(id);
                ej.Enunciado = "Enunciado del ejercicio";
                ej.EsValidoSubirWeb = true;
                ej.Gargar = "GarGar";
                ej.Modo = AplicativoEscritorio.DataAccess.Enums.ModoEjercicio.Normal;
                ej.NivelDificultad = 3;
                ej.SolucionTexto = "Tendrías que haber puesto estoo!!";

                TestPrueba tp = new TestPrueba();
                tp.CodigoGarGarProcSalida = "Codigo GarGar de salida";
                tp.Descripcion = "Descripcion de test de prueba";
                tp.VariablesEntrada = new List<VariableTest>();
                tp.Id = "1";
                tp.Nombre = "Test Prueba 1";
                VariableTest vt = new VariableTest();
                vt.Descripcion = "Descripcion de variable de test de entrada";
                vt.Nombre = "Nombre de variable de test de entrada";
                vt.Contexto = "Contexto Variable Test de entrada";
                vt.TipoDato = "Entero";
                vt.ValorEsperado = "Valor esperado 4";
                vt.VariableMapeada = "var Pepiono";
                PosicionVariableTest pvt = new PosicionVariableTest();
                pvt.Posicion = 232;
                pvt.Valor = "Valor";
                vt.Posiciones.Add(pvt);
                tp.VariablesEntrada.Add(vt);

                VariableTest vt2 = new VariableTest();
                vt2.Descripcion = "Descripcion de variable de test de salida";
                vt2.Nombre = "Nombre de variable de test de salida";
                vt.Contexto = "Contexto Variable Test de salida";
                vt.TipoDato = "booleano";
                vt2.ValorEsperado = "Valor esperado 4";
                vt2.VariableMapeada = "var Pepiono";
                PosicionVariableTest pvt2 = new PosicionVariableTest();
                pvt2.Posicion = 12;
                pvt2.Valor = "Valor12";
                vt2.Posiciones.Add(pvt2);
                tp.VariablesSalida = new List<VariableTest>();
                tp.VariablesSalida.Add(vt2);

                ej.AgregarTestPrueba(tp);
                string ejer = @"D:\Acustico\" + id.ToString() + ".gej";
                ej.Guardar(ejer);

                ej = new Ejercicio();
                ej.Abrir(new FileInfo(ejer));

                sb.Append(File.ReadAllText(ejer) + ",");
            }
            File.WriteAllText(@"D:\\Acustico\Ejercicios.txt", sb.ToString());
        }

        public RagnarokWindow()
        {
            //ProbarCarga();
            InitializeComponent();

            

            Title = ConstantesGlobales.NOMBRE_APLICACION;

            this.BarraMsgs.DoubleClickEvent += new BarraMensajes.DobleClickEnBarraMensajesEventHandler(BarraMsgs_DoubleClickEvent);
            this.Esquema.ModoTextoCambiarPosicionEvent += new EsquemaCentral.ModoTextoCambiarPosicionEventHandler(Esquema_ModoTextoCambiarPosicionEvent);
            

            this.ToolbarAplicacion.CompilacionEvent += new BarraToolbarRibbon.CompilacionEventHandler(ToolbarAplicacion_CompilacionEvent);
            this.ToolbarAplicacion.CambioModoEvent += new BarraToolbarRibbon.CambioModoEventHandler(ToolbarAplicacion_CambioModoEvent);
            this.ToolbarAplicacion.AbrirBusquedaEvent += new BarraToolbarRibbon.AbrirBusquedaEventHandler(ToolbarAplicacion_AbrirBusquedaEvent);
            this.ToolbarAplicacion.SalvarConfiguracionEvent += new BarraToolbarRibbon.SalvarConfiguracionEventHandler(ToolbarAplicacion_SalvarConfiguracionEvent);
            this.ToolbarAplicacion.ModificarPropiedadesEjercicioEvent += new BarraToolbarRibbon.ModificarPropiedadesEjercicioHandler(ToolbarAplicacion_ModificarPropiedadesEjercicioEvent);
            this.ToolbarAplicacion.TestPruebaEvent += new BarraToolbarRibbon.TestPruebaHandler(ToolbarAplicacion_TestPruebaEvent);
            this.ToolbarAplicacion.IdentarEvent += new BarraToolbarRibbon.IdentarEventHandler(ToolbarAplicacion_IdentarEvent);

            this.Loaded += new RoutedEventHandler(RagnarokWindow_Loaded);
			// flanzani 11/11/2012
	        // IDC_APP_5
    	    // Tutorial para la aplicacion
        	// Controlo estos eventos para poder reposicionar el globo del tutorial si muevo o cambio de tamaño la app
            this.SizeChanged += new SizeChangedEventHandler(RagnarokWindow_SizeChanged);
            this.LocationChanged += new EventHandler(RagnarokWindow_LocationChanged);



            string pathAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Program.AR");

            ConfiguracionAplicacion.Abrir(Path.Combine(pathAppData,
                                         Globales.ConstantesGlobales.NOMBRE_ARCH_CONFIG_APLICACION));

            GlobalesCompilador.CantMaxErroresSintacticos = ConfiguracionAplicacion.CantMaxErroresSintacticos;
            GlobalesCompilador.CantMaxIteraciones = ConfiguracionAplicacion.CantMaxIteraciones;


       
            

            ToolbarAplicacion.DirEjCreados = ConfiguracionAplicacion.DirectorioEjerciciosCreados;
            ToolbarAplicacion.DirEjDescargados = ConfiguracionAplicacion.DirectorioEjerciciosDescargados;
            ToolbarAplicacion.DirResoluciones = ConfiguracionAplicacion.DirectorioResolucionesEjercicios;
            ToolbarAplicacion.DirTemporales = ConfiguracionAplicacion.DirectorioTemporal;
            ToolbarAplicacion.DirDefaultAbrir = ConfiguracionAplicacion.DirectorioAbrirDefault; 
            
            ConfigurarCompilador();

            hotKeyCompilar = new HotKey(ModifierKeys.None, Keys.F3, Window.GetWindow(this));
            hotKeyEjecutar = new HotKey(ModifierKeys.None, Keys.F4, Window.GetWindow(this));

            hotKeyCompilar.HotKeyPressed += new Action<HotKey>(hotKeyCompilar_HotKeyPressed);
            hotKeyEjecutar.HotKeyPressed += new Action<HotKey>(hotKeyCompilar_HotKeyPressed);
            
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

            Esquema.Owner = this;
          
        }

		// flanzani 11/11/2012
        // IDC_APP_5
    	// Tutorial para la aplicacion
        //Logica de reposicionamiento del globo del tutorial si muevo o cambio la pagina

        Point locationActual = new Point(0, 0);

        void RagnarokWindow_LocationChanged(object sender, EventArgs e)
        {
			// flanzani 11/11/2012
	        // IDC_APP_5
    	    // Tutorial para la aplicacion
        	// Guardo la posicion en este evento, ya que el sizeChanged 
			//no tengo la ubicacion correcta de la ventana al maximizarla 
            RagnarokWindow wind = sender as RagnarokWindow;
            locationActual = new Point(wind.Left, wind.Top);
            TutorialManager.UbicacionVentanaRagnarok = locationActual;
            TutorialManager.Reposicionar();
        }

        void RagnarokWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            TutorialManager.Reposicionar();
            
        }     

        void ToolbarAplicacion_IdentarEvent(object o, IdentarEventArgs e)
        {
            IdentarTexto();

            this.Esquema.EstaIdentado = true;
        }

        private void IdentarTexto()
        {
            Identador ident = new Identador(this.Esquema.GarGarACompilar);
            string codigoIdentado = ident.Identar();

            this.Esquema.GarGarACompilar = codigoIdentado;
            this.Esquema.EstaIdentado = true;
        }

        

        void RagnarokWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageListener.Instance.ReceiveMessage(string.Format("Finalizando carga del programa..."));
            
            Modo = ModoVisual.Flujo;

            ArchCargado = null;

            Splasher.CloseSplash();


			// flanzani 14/11/2012
            // IDC_APP_7
            // Desactivar el tutorial desde el popup
            // Me subscribo al evento estatico de tutorial desactivado
            PopUpGlobo.TutorialDesativadoEvent += new PopUpGlobo.TutorialDesativadoEventHandler(PopUpGlobo_TutorialDesativadoEvent);

			// flanzani 11/11/2012
	        // IDC_APP_5
    	    // Tutorial para la aplicacion
        	// Configuracion inicial de la parte del tutorial en la app
            this.ToolbarAplicacion.chkTutorial.IsChecked = ConfiguracionAplicacion.TutorialActivo;
            TutorialManager.UbicacionVentanaRagnarok = new Point(0, 0);
            TutorialManager.Activado = ConfiguracionAplicacion.TutorialActivo;
            TutorialManager.MostrarTutorialAperturaAplicacion(this);
       
         
        }

		// flanzani 14/11/2012
        // IDC_APP_7
        // Desactivar el tutorial desde el popup
        // Modifico la configuracion al desactivar el tutorial desde el popup
        void PopUpGlobo_TutorialDesativadoEvent(object o, EventArgs e)
        {
            TutorialManager.Activado = false;
            ConfiguracionAplicacion.TutorialActivo = false;
            ToolbarAplicacion.chkTutorial.IsChecked = false;

            SalvarConfiguracion();
        }



       

        

        void ToolbarAplicacion_SalvarConfiguracionEvent(object o, SalvarConfiguracionEventArgs e)
        {
            SalvarConfiguracion();
        }

        private void SalvarConfiguracion()
        {
            ConfiguracionAplicacion.DirectorioEjerciciosCreados = ToolbarAplicacion.DirEjCreados;
            ConfiguracionAplicacion.DirectorioEjerciciosDescargados = ToolbarAplicacion.DirEjDescargados;
            ConfiguracionAplicacion.DirectorioResolucionesEjercicios = ToolbarAplicacion.DirResoluciones;
            ConfiguracionAplicacion.DirectorioTemporal = ToolbarAplicacion.DirTemporales;
            ConfiguracionAplicacion.DirectorioAbrirDefault = ToolbarAplicacion.DirDefaultAbrir;
            // flanzani 11/11/2012
            // IDC_APP_5
            // Tutorial para la aplicacion
            // Agregamos que se salve en el arch de config si el tutorial esta activo o no
            ConfiguracionAplicacion.TutorialActivo = ToolbarAplicacion.chkTutorial.IsChecked.Value;

            string pathAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Program.AR");

            ConfiguracionAplicacion.Guardar(Path.Combine(pathAppData,
                                         Globales.ConstantesGlobales.NOMBRE_ARCH_CONFIG_APLICACION));
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

            if (Modo != e.ModoSeleccionado)
            {

                if (Modo == ModoVisual.Texto)
                {
                    //Para ponerle lo minimo si ta vacio para poder transformarlo
                    if (string.IsNullOrEmpty(this.Esquema.GarGarACompilar))
                    {
                        this.Esquema.GarGarACompilar = GlobalesCompilador.ObtenerProgramaConEstructuraVacia();
                    }

                    ProgramadorGrafico.ReiniciarContadoresId();

                    ResultadoCompilacion res = Compilar(this.Esquema.GarGarACompilar);

                    if (res.CompilacionGarGarCorrecta && res.ResultadoCompPascal != null && res.ResultadoCompPascal.CompilacionPascalCorrecta)
                    {
                        Modo = e.ModoSeleccionado;
                        
                        Esquema.RepresentacionGraficaActual = res.RepresentacionGrafica;

						// flanzani 11/11/2012
	        			// IDC_APP_5
			    	    // Tutorial para la aplicacion
        				// Muestro el tutorial de modo grafico al cambiar a ese modo
                        TutorialManager.MostrarTutorialModoGrafico(this);
                    }
                    else
                    {
                        //Pq no hago el cambio, mantengo el actual
                        RibbonToggleButton botonPresionado = e.SourceEvent.Source as RibbonToggleButton;
                        botonPresionado.IsChecked = false;
                    }

                }
                else
                {
                    try
                    {
                        ProgramaViewModel programa = Esquema.RepresentacionGraficaActual;

                        ResultadoCompilacion res = Compilar(programa.Gargar);

                        if (res.CompilacionGarGarCorrecta && res.ResultadoCompPascal != null && res.ResultadoCompPascal.CompilacionPascalCorrecta)
                        {
                            Modo = e.ModoSeleccionado;

                            Esquema.GarGarACompilar = new Identador(programa.Gargar).Identar();

							// flanzani 11/11/2012
	        				// IDC_APP_5
			    	    	// Tutorial para la aplicacion
        					// Muestro el tutorial de modo texto al cambiar a ese modo
                            TutorialManager.MostrarTutorialModoTexto(this);
                        }
                        else
                        {
                            //Pq no hago el cambio, mantengo el actual
                            RibbonToggleButton botonPresionado = e.SourceEvent.Source as RibbonToggleButton;
                            botonPresionado.IsChecked = false;
                        }
                    }
                    catch (InterfazTextoGrafico.Excepciones.ExcepcionLlamadaCircular ex)
                    {
                        MessageBox.Show(ex.Message, "Llamada circular detectada", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        RibbonToggleButton botonPresionado = e.SourceEvent.Source as RibbonToggleButton;
                        botonPresionado.IsChecked = false;
                    }
                }

            }
            else
            {
                //Presione el mismo boton que ya estaba, lo dejo.
                RibbonToggleButton botonPresionado = e.SourceEvent.Source as RibbonToggleButton;
                botonPresionado.IsChecked = true;
            }
            //Modo = e.ModoSeleccionado;

        }

        void ToolbarAplicacion_TestPruebaEvent(object o, TestPruebaEventArgs e)
        {
            switch (e.Accion)
            {
                case TestPruebaEventArgs.TipoAccion.Crear:
                    CrearTestPrueba();
                    break;
                case TestPruebaEventArgs.TipoAccion.Consultar:
                    ConsultarTestPrueba();
                    break;
                case TestPruebaEventArgs.TipoAccion.Ejecutar:
                    EjecutarTestPrueba();
                    break;
                default:
                    break;
            }
        }

        private void EjecutarTestPrueba()
        {
            if (archCargado.TestsPrueba == null || archCargado.TestsPrueba.Count == 0)
            {
                MessageBox.Show("El ejercicio no tiene test de prueba para ejecutar", "GarGar Dev", MessageBoxButton.OK);
            }
            else
            {
                bool continuar = true;

                if (Modo == ModoVisual.Texto && !this.Esquema.EstaIdentado)
                {
                    MessageBoxResult result = MessageBox.Show("Para que los test de prueba funcionen correctamente, es necesario que el codigo este correctamente identado. ¿Desea identar el codigo ahora?", "GarGar Dev", MessageBoxButton.YesNoCancel);

                    if (result != MessageBoxResult.Cancel)
                    {
                        if (result == MessageBoxResult.Yes)
                        {
                            IdentarTexto();
                        }
                    }
                    else
                    {
                        continuar = false;
                    }
                }

                if (continuar)
                {
                    ResultadoCompilacion res = null;

                    if (this.Modo == ModoVisual.Texto)
                    {
                        res = Compilar(this.Esquema.GarGarACompilar);
                    }
                    else
                    {
                        try
                        {
                            ProgramaViewModel pvm = this.Esquema.RepresentacionGraficaActual;
                            pvm.ValidarRepetidos();
                            res = Compilar(pvm.Gargar);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }


                    if (res != null && res.CompilacionGarGarCorrecta && res.ResultadoCompPascal != null && res.ResultadoCompPascal.CompilacionPascalCorrecta)
                    {
                        List<NodoTablaSimbolos> aux = res.TablaSimbolos.ObtenerVariablesDelProcPrincipal();
                        aux.AddRange(res.TablaSimbolos.ObtenerVariablesGlobales());

                        ObservableCollection<Variable> listaVariablesEntrada = TransformarAVariables(aux);
                        ObservableCollection<Variable> listaVariablesSalida = TransformarAVariables(res.TablaSimbolos.ObtenerParametrosDelProcSalida());

                        WindowEjecucionTest testWindow = new WindowEjecucionTest(this.compilador);
                        testWindow.VariablesEntrada = listaVariablesEntrada;
                        testWindow.VariablesSalida = listaVariablesSalida;
                        testWindow.Tests = new ObservableCollection<TestPrueba>(this.archCargado.TestsPrueba);
                        testWindow.Codigo = res.CodigoGarGar;
                        testWindow.ListaLineasValidas = res.ListaLineasValidas;
                        testWindow.ListaLineasContenidoProcSalida = res.ListaLineasContenidoProcSalida;

                        testWindow.Title = "Tests de Prueba - Ejecutar";

                        testWindow.Owner = this;
                        ApplyBlurEffect();

                        testWindow.ShowDialog();

                        ClearBlurEffect();

                    }
                }
            }
        }

        private void ConsultarTestPrueba()
        {
            if (archCargado.TestsPrueba == null || archCargado.TestsPrueba.Count == 0)
            {
                MessageBox.Show("No ha creado ningun test de prueba aun. No hay tests de prueba para mostrar", "GarGar Dev", MessageBoxButton.OK);
            }
            else
            {
                ResultadoCompilacion res = null;

                if (this.Modo == ModoVisual.Texto)
                {
                    res = Compilar(this.Esquema.GarGarACompilar);
                }
                else
                {
                    try
                    {
                        ProgramaViewModel pvm = this.Esquema.RepresentacionGraficaActual;
                        pvm.ValidarRepetidos();
                        res= Compilar(pvm.Gargar);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }


                if (res != null && res.CompilacionGarGarCorrecta && res.ResultadoCompPascal != null && res.ResultadoCompPascal.CompilacionPascalCorrecta)
                {
                    List<NodoTablaSimbolos> aux = res.TablaSimbolos.ObtenerVariablesDelProcPrincipal();
                    aux.AddRange(res.TablaSimbolos.ObtenerVariablesGlobales());

                    ObservableCollection<Variable> listaVariablesEntrada = TransformarAVariables(aux);
                    ObservableCollection<Variable> listaVariablesSalida = TransformarAVariables(res.TablaSimbolos.ObtenerParametrosDelProcSalida());

                    WindowConsultaTests testWindow = new WindowConsultaTests(false);
                    testWindow.VariablesEntrada = listaVariablesEntrada;
                    testWindow.VariablesSalida = listaVariablesSalida;
                    testWindow.TestPruebas = new ObservableCollection<TestPrueba>(archCargado.TestsPrueba);
                    testWindow.Validar();

                    testWindow.Title = "Tests de Prueba - Mis Tests";

                    testWindow.Owner = this;
                    ApplyBlurEffect();

                    bool? confirmado = testWindow.ShowDialog();

                    if (confirmado.HasValue && confirmado.Value)
                    {
                        //Por si elegi borrar alguno
                        List<string> idsRemover = new List<string>();
                       
                        foreach (TestPrueba item in archCargado.TestsPrueba)
                        {
                            if (!testWindow.TestPruebas.Contains(item))
                            {
                                idsRemover.Add(item.Id);
                            }
                        }

                        

                        archCargado.TestsPrueba.RemoveAll(x => idsRemover.Contains(x.Id));
                        
                    }

                    ClearBlurEffect();

                }
            }
        }

        private void CrearTestPrueba()
        {
            bool continuar = true;
            if (Modo == ModoVisual.Texto && !this.Esquema.EstaIdentado)
            {
                MessageBoxResult result = MessageBox.Show("Para que los test de prueba funcionen correctamente, es necesario que el codigo este correctamente identado. ¿Desea identar el codigo ahora?", "GarGar Dev", MessageBoxButton.YesNoCancel);

                if (result != MessageBoxResult.Cancel)
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        IdentarTexto();
                    }
                }
                else
                {
                    continuar = false;
                }
            }

            if (continuar)
            {

                ResultadoCompilacion res = null;

                if (this.Modo == ModoVisual.Texto)
                {
                    res = Compilar(this.Esquema.GarGarACompilar);
                }
                else
                {
                    try
                    {
                        ProgramaViewModel pvm = this.Esquema.RepresentacionGraficaActual;
                        pvm.ValidarRepetidos();
                        res = Compilar(pvm.Gargar);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }


                if (res != null && res.CompilacionGarGarCorrecta && res.ResultadoCompPascal != null && res.ResultadoCompPascal.CompilacionPascalCorrecta)
                {
                    List<NodoTablaSimbolos> aux = res.TablaSimbolos.ObtenerVariablesDelProcPrincipal();
                    aux.AddRange(res.TablaSimbolos.ObtenerVariablesGlobales());

                    ObservableCollection<Variable> listaVariablesEntrada = TransformarAVariables(aux);
                    ObservableCollection<Variable> listaVariablesSalida = TransformarAVariables(res.TablaSimbolos.ObtenerParametrosDelProcSalida());

                    WindowCreacionTest testWindow = new WindowCreacionTest(this.compilador);
                    testWindow.VariablesEntrada = listaVariablesEntrada;
                    testWindow.VariablesSalida = listaVariablesSalida;
                    testWindow.Codigo = res.CodigoGarGar;
                    testWindow.ListaLineasValidas = res.ListaLineasValidas;

                    testWindow.Title = "Tests de Prueba - Crear";

                    testWindow.Owner = this;
                    ApplyBlurEffect();

                    testWindow.ShowDialog();

                    if (testWindow.TestGenerado != null)
                    {
                        this.archCargado.TestsPrueba.Add(testWindow.TestGenerado);
                    }

                    ClearBlurEffect();
                }
            }
            
        }

        private ObservableCollection<Variable> TransformarAVariables(List<NodoTablaSimbolos> list)
        {
            ObservableCollection<Variable> listaRetorno = new ObservableCollection<Variable>();

            foreach (var item in list)
            {
                listaRetorno.Add(new Variable(item.Nombre, item.NombreParaCodigo, 
                    (AplicativoEscritorio.DataAccess.Enums.TipoContexto) item.Contexto, item.NombreContextoLocal, 
                    (AplicativoEscritorio.DataAccess.Enums.TipoDato) item.TipoDato, item.EsArreglo, item.Valor)
                    );
                //listaRetorno.Add(new Variable(item));
            }

            return listaRetorno;
        }

        void ToolbarAplicacion_CompilacionEvent(object o, CompilacionEventArgs e)
        {

            if (e.EsEjecucion)
            {
                if (this.Modo == ModoVisual.Texto)
                {
                    EjecutarConResultado(this.Esquema.GarGarACompilar);
                }
                else
                {
                    try
                    {
                        ProgramaViewModel pvm = this.Esquema.RepresentacionGraficaActual;
                        pvm.ValidarRepetidos();
                        EjecutarConResultado(pvm.Gargar);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                if (this.Modo == ModoVisual.Texto)
                {
                    Compilar(this.Esquema.GarGarACompilar);
                }
                else
                {
                    try
                    {
                        ProgramaViewModel pvm = this.Esquema.RepresentacionGraficaActual;
                        pvm.ValidarRepetidos();
                        Compilar(pvm.Gargar);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        void Esquema_ModoTextoCambiarPosicionEvent(object o, ModoTextoCambiarPosicionEventArgs e)
        {
            this.BarraEstado.ModificarPosicionModoTexto(e.Fila, e.Columna);
        }

        void BarraMsgs_DoubleClickEvent(object o, DoubleClickEventArgs e)
        {
            if (Modo == ModoVisual.Texto)
            {
                this.Esquema.PosicionarseEn(e.Fila, e.Columna);
            }
            else
            {
                //if (e.ActividadReferenciada != null)
                //{
                //    //this.Esquema.PonerFocoEnActividad(e.FiguraProcedimientoNombre, e.ActividadReferenciada);
                //    this.Esquema.PonerFocoEnTab(e.FiguraProcedimientoNombre);
                    
                //}

                this.Esquema.PonerFocoEnTab(e.FiguraProcedimientoNombre);
            }
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
            
            bool modoDebug = Debugger.IsAttached;

            string directorioActual = Globales.ConstantesGlobales.PathEjecucionAplicacion;

            compilador = new Compilador(modoDebug, ConfiguracionAplicacion.DirectorioTemporal, ConfiguracionAplicacion.DirectorioTemporal, "prueba");
        }

        void hotKeyCompilar_HotKeyPressed(HotKey obj)
        {
            if (IsActive && ArchCargado != null)
            {
                switch (obj.Key)
                {
                    case Keys.F3:
                        ToolbarAplicacion_CompilacionEvent(this, new CompilacionEventArgs(false));
                        break;
                    case Keys.F4:
                        ToolbarAplicacion_CompilacionEvent(this, new CompilacionEventArgs(true));
                        break;
                    default:
                        break;
                }
            }
        }

        private ResultadoCompilacion Compilar(string programa)
        {
            ReiniciarIDEParaCompilacion();

            ConfigurarCompilador();

            //string programa = this.Esquema.GarGarACompilar;
            ResultadoCompilacion res = this.compilador.Compilar(programa);
            res.CodigoGarGar = programa;

            string arbol = res.ArmarArbol();

            MostrarResultadosCompilacion(res);

            if (res.CompilacionGarGarCorrecta && res.ResultadoCompPascal != null && res.ResultadoCompPascal.CompilacionPascalCorrecta)
            {
                if (ArchCargado != null)
                {
                    ArchCargado.CompilacionCorrecta = true;
                }
            }

            if (!string.IsNullOrEmpty(res.Error))
            {
                MessageBox.Show(res.Error);
            }

            return res;
        }

        private void EjecutarConResultado(string programa)
        {
            ResultadoEjecucion res = Ejecutar(programa);

            if (res.ResEjecucion != null)
            {                
                ResultadoEjecucionDialog resultadosDialog = new ResultadoEjecucionDialog(res.ResEjecucion);

                // flanzani 22/11/2012
                // IDC_APP_8
                // Agregar el tiempo de ejecucion 
                // Agrego el tiempo
                resultadosDialog.TiempoEjecucion = res.Segundos;


                resultadosDialog.Owner = this;
                ApplyBlurEffect();

                resultadosDialog.ShowDialog();

                try
                {
                    File.Delete(res.ResCompilacion.ArchTemporalResultadosEjecucionConRuta);
                }
                catch
                {                    
                    
                }

                ClearBlurEffect();

                Focus();
            }
            else
            {
                if (res != null &&
                    res.ResCompilacion != null &&
                    res.ResCompilacion.CompilacionGarGarCorrecta &&
                    res.ResCompilacion.ResultadoCompPascal != null &&
                    res.ResCompilacion.ResultadoCompPascal.CompilacionPascalCorrecta)
                {
                    MessageBox.Show("No se pudo continuar con la ejecución porque el archivo que la contenia fue borrado",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
        }

        private ResultadoEjecucion Ejecutar(string programa)
        {
            ResultadoEjecucion resultadoEjecucion = new ResultadoEjecucion();

            ReiniciarIDEParaCompilacion();

            ConfigurarCompilador();

            //string programa = this.Esquema.GarGarACompilar;
            ResultadoCompilacion res = this.compilador.Compilar(programa);

            MostrarResultadosCompilacion(res);

            if (!string.IsNullOrEmpty(res.Error))
            {
                MessageBox.Show(res.Error); 
            }

            resultadoEjecucion.ResCompilacion = res;

            if (res.CompilacionGarGarCorrecta && res.GeneracionEjectuableCorrecto)
            {
                if (ArchCargado != null)
                {
                    ArchCargado.CompilacionCorrecta = true;
                }

                Stopwatch stp = Stopwatch.StartNew();
                EjecucionManager.EjecutarConVentana(res.ArchEjecutableConRuta);
                stp.Stop();

                resultadoEjecucion.Segundos = stp.Elapsed.TotalSeconds;

                if (File.Exists(res.ArchTemporalResultadosEjecucionConRuta))
                {
                    resultadoEjecucion.ResEjecucion = new ArchResultado(res.ArchTemporalResultadosEjecucionConRuta);
                }
                else
                {
                    resultadoEjecucion.ResEjecucion = null;
                }
            }

            return resultadoEjecucion;
        }

        private void ReiniciarIDEParaCompilacion()
        {
            //Aca reinicar la IDE
            this.BarraMsgs.BorrarTodosMensajes();
            
        }

        private delegate void VoidHandler();

        public static void DoEvents()
        {
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new VoidHandler(() => { }));
        }

        private void MostrarResultadosCompilacion(ResultadoCompilacion res)
        {
            if (res.CompilacionGarGarCorrecta && res.ResultadoCompPascal != null && res.ResultadoCompPascal.CompilacionPascalCorrecta)
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    BarraEstado.ColocarResCompilacion(true);
                    BarraEstado.Estado = "Compilación Correcta";
                }));

                RagnarokWindow.DoEvents();

                //BarraEstado.ColocarResCompilacion(true);
                //BarraEstado.Estado = "Compilación Correcta";
            }
            else
            {
                BarraEstado.ColocarResCompilacion(false);
                BarraEstado.Estado = "Error en compilación";

                res.FiltrarListaErroresSintacticos();

                if (Modo == ModoVisual.Flujo)
                {
                    MostrarResultadosModoGrafico(res);
                }
                else
                {
                    MostrarResultadosModoTexto(res);
                }

                
            }
        }

        private void MostrarResultadosModoTexto(ResultadoCompilacion res)
        {
            ICSharpCode.AvalonEdit.Rendering.SubrayadoRenderer sr = this.Esquema.textEditor.TextArea.TextView.BackgroundRenderers[0] as
                    ICSharpCode.AvalonEdit.Rendering.SubrayadoRenderer;

            foreach (var item in res.ListaErrores)
            {
                string errorMostrar = item.MensajeError == null ? item.Descripcion : item.MensajeError.MensajeModoTexto;
                this.BarraMsgs.AgregarError(errorMostrar, item.Fila, item.Columna);
                sr.AgregarLinea(item.Fila);
            }

            if (res.ResultadoCompPascal != null && res.ResultadoCompPascal.ListaErrores != null)
            {
                foreach (ResultadoCompilacionPascalLinea item in res.ResultadoCompPascal.ListaErrores)
                {
                    if (item.Mostrar)
                    {
                        this.BarraMsgs.AgregarError(item.ErrorTraducido, item.Fila, 0);
                        sr.AgregarLinea(item.Fila);
                    }
                }
            }

            this.Esquema.textEditor.TextArea.TextView.InvalidateLayer(ICSharpCode.AvalonEdit.Rendering.KnownLayer.Background);
        }

        private void MostrarResultadosModoGrafico(ResultadoCompilacion res)
        {

            foreach (var item in res.ListaErrores)
            {
                ActividadViewModelBase act = this.Esquema.RepresentacionGraficaActual.EncontrarActividadPorLinea(item.Fila);
                if (act != null)
                {
                    if (item.MensajeError != null)
                    {

                        item.MensajeError = AsignarMensajeErrorCorrecto(item.MensajeError, act);
                        
                        
                    }

                    string errorMostrar = item.MensajeError == null ? item.Descripcion : item.MensajeError.MensajeModoGrafico;
                    this.BarraMsgs.AgregarErrorModoGrafico(errorMostrar, act.IdPropio.ToString(), act.Contexto, act.NombreActividad, act.ActividadReferenciada);
                }
                
            }

            if (res.ResultadoCompPascal != null && res.ResultadoCompPascal.ListaErrores != null)
            {
                foreach (ResultadoCompilacionPascalLinea item in res.ResultadoCompPascal.ListaErrores)
                {
                    if (item.Mostrar)
                    {
                        ActividadViewModelBase act = this.Esquema.RepresentacionGraficaActual.EncontrarActividadPorLinea(item.Fila);
                        this.BarraMsgs.AgregarErrorModoGrafico(item.ErrorTraducido, act.Id, act.Contexto, act.NombreActividad, act.ActividadReferenciada);
                    }
                }
            }
            
        }

        private MensajeError AsignarMensajeErrorCorrecto(MensajeError mensajeError, ActividadViewModelBase act)
        {
            MensajeError retorno = mensajeError;

            Type tipo = act.GetType();

            if (!mensajeError.EsErrorBienDefinido && tipo == typeof(AsignacionViewModel))
            {
                retorno = new CompiladorGargar.Sintactico.ErroresManager.Errores.ErrorAsignacionValidacionPorDefault();
            }
            else
            {
                CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias sentencia = ObtenerSentenciaAcordeATipoActividad(tipo);

                if (sentencia != Sentencias.Ninguno 
                    && !mensajeError.SentenciasQueTienenElError.Contains(sentencia) )
                {

                    switch (sentencia)
                    {
                        case CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias.Mientras:
                            retorno = new ErrorMientrasValidacionPorDefault();
                            break;
                        case CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias.Si:
                            retorno = new ErrorSiValidacionPorDefault();
                            break;
                        case CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias.Leer:
                            retorno = new ErrorLeerValidacionPorDefault();
                            break;
                        case CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias.Mostrar:
                            retorno = new ErrorMostrarValidacionPorDefault();
                            break;
                        case CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias.LlamarProcedimiento:
                            retorno = new ErrorLlamadoProcValidacionPorDefault();
                            break;
                        case CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias.Asignacion:
                            retorno = new ErrorAsignacionValidacionPorDefault();
                            break;
                        case CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias.DeclaracionVariable:
                            retorno = new ErrorDeclaracionVariableValidacionPorDefault();
                            break;
                        case CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias.DeclaracionConstante:
                            retorno = new ErrorDeclaracionConstanteGenerico();
                            break;
                        case CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias.DeclaracionFuncion:
                            retorno = new ErrorDeclaracionFuncionValidacionPorDefault();
                            break;
                        case CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias.DeclaracionProcedimiento:
                            retorno = new ErrorDeclaracionProcedimientoValidacionPorDefault();
                            break;
                    }
                }
            }
            return retorno;
        }

        private CompiladorGargar.Sintactico.ErroresManager.Errores.Sentencias ObtenerSentenciaAcordeATipoActividad(Type tipo)
        {
            string nombreTipo = tipo.Name;
            if (!string.IsNullOrEmpty(nombreTipo))
            {
                if (nombreTipo.ToUpper().Equals("SiViewModel".ToUpper()))
                {
                    return Sentencias.Si;
                }
                else if (nombreTipo.ToUpper().Equals("MientrasViewModel".ToUpper()))
                {
                    return Sentencias.Mientras;
                }
                else if (nombreTipo.ToUpper().Equals("DeclaracionConstanteViewModel".ToUpper()))
                {
                    return Sentencias.DeclaracionConstante;
                }
                else if (nombreTipo.ToUpper().Equals("DeclaracionVariableViewModel".ToUpper()))
                {
                    return Sentencias.DeclaracionVariable;
                }
                else if (nombreTipo.ToUpper().Equals("DeclaracionArregloViewModel".ToUpper()))
                {
                    return Sentencias.DeclaracionVariable;
                }
                else if (nombreTipo.ToUpper().Equals("AsignacionViewModel".ToUpper()))
                {
                    return Sentencias.Asignacion;
                }
                else if (nombreTipo.ToUpper().Equals("SecuenciaViewModel".ToUpper()))
                {
                    return Sentencias.Ninguno;
                }
                else if (nombreTipo.ToUpper().Equals("DeclaracionConstanteViewModel".ToUpper()))
                {
                    return Sentencias.DeclaracionConstante;
                }
                else if (nombreTipo.ToUpper().Equals("LeerViewModel".ToUpper()))
                {
                    return Sentencias.Leer;
                }
                else if (nombreTipo.ToUpper().Equals("MostrarViewModel".ToUpper()))
                {
                    return Sentencias.Mostrar;
                }
                else if (nombreTipo.ToUpper().Equals("LlamarProcedimientoViewModel".ToUpper()))
                {
                    return Sentencias.LlamarProcedimiento;
                }

            }

            return Sentencias.Ninguno;
        }

        private void RibbonWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool continuar = SalvarSiUsuarioQuiere();

            if (!continuar)
            {
                e.Cancel = true;
            }

            try
            {
                //ET (11/11/2012): Si el directorio de los archivos temporales es el mismo que el de los archivos creados/descargados/resoluciones, se borran todos y no es lo que se busca
                DirectoriosManager.BorrarArchivosDelDirPorExtensionExcluida(ConfiguracionAplicacion.DirectorioTemporal, new List<string>() { ".gej", ".gres" });
            }
            catch
            {

            }
        }


        /// <summary>
        /// Apply Blur Effect on the window
        /// </summary>
        public void ApplyBlurEffect()
        {
            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 20;
            this.Effect = objBlur;
        }
        /// <summary>
        /// Remove Blur Effects
        /// </summary>
        public void ClearBlurEffect()
        {
            this.Effect = null;
        }
    }
}
