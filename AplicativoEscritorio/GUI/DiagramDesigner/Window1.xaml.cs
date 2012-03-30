﻿using System.Windows;
using CompiladorGargar.Resultado;
using WpfApplicationHotKey.WinApi;
using CompiladorGargar;
using System.Windows.Input;
using System;
using Utilidades;
using DiagramDesigner.Enums;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using AplicativoEscritorio.DataAccess;
using System.IO;
using Microsoft.Windows.Controls.Ribbon;
using DiagramDesigner.EventArgsClasses;
using DiagramDesigner.UserControls.Mensajes;
using DiagramDesigner.UserControls.Toolbar;
using DiagramDesigner.UserControls.Entorno;
using AplicativoEscritorio.DataAccess.Interfases;
using AplicativoEscritorio.DataAccess.Entidades;
using Globales.Enums;
using Microsoft.Win32;
using DiagramDesigner.Helpers;
using System.Windows.Documents;
using Globales;
using System.Diagnostics;
using CompiladorGargar.Resultado.Auxiliares;
using EJEKOR;
using DiagramDesigner.TestsPruebas;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        private void ProbarCarga()
        {
            Ejercicio ej = new Ejercicio(4);
            ej.Enunciado = "Enunciado del ejercicio";
            ej.EsValidoSubirWeb = true;
            ej.Gargar = "GqarGar";
            ej.Modo = AplicativoEscritorio.DataAccess.Enums.ModoEjercicio.Normal;
            ej.NivelDificultad = 3;
            ej.SolucionTexto = "Tendrías que haber puesto estoo!!";

            TestPrueba tp = new TestPrueba();
            tp.CodigoGarGarProcSalida = "Codigo GarGar de salida";
            tp.Descripcion = "Descripcion de test de prueba";
            tp.VariablesEntrada = new List<VariableTest>();
            VariableTest vt = new VariableTest();
            vt.Descripcion = "Descripcion de variable de test de entrada";
            vt.Nombre = "Nombre de variable de test de entrada";
            vt.ValorEsperado = "Valor esperado 4";
            vt.VariableMapeada = "var Pepiono";
            tp.VariablesEntrada.Add(vt);

            VariableTest vt2 = new VariableTest();
            vt2.Descripcion = "Descripcion de variable de test de salida";
            vt2.Nombre = "Nombre de variable de test de salida";
            vt2.ValorEsperado = "Valor esperado 4";
            vt2.VariableMapeada = "var Pepiono";
            tp.VariablesSalida = new List<VariableTest>();
            tp.VariablesSalida.Add(vt2);

            ej.AgregarTestPrueba(tp);
            ej.Guardar(@"D:\Acustico\4.gej");

            ej = new Ejercicio();
            ej.Abrir(new FileInfo(@"D:\Acustico\3.gej"));
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
            this.ToolbarAplicacion.TestPruebaEvent += new BarraToolbarRibbon.TestPruebaHandler(ToolbarAplicacion_TestPruebaEvent);


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

       

        

        void ToolbarAplicacion_SalvarConfiguracionEvent(object o, SalvarConfiguracionEventArgs e)
        {
            configApp.DirectorioEjerciciosCreados = ToolbarAplicacion.DirEjCreados;
            configApp.DirectorioEjerciciosDescargados = ToolbarAplicacion.DirEjDescargados;
            configApp.DirectorioResolucionesEjercicios = ToolbarAplicacion.DirResoluciones;
            configApp.DirectorioTemporal = ToolbarAplicacion.DirTemporales;
            configApp.DirectorioAbrirDefault = ToolbarAplicacion.DirDefaultAbrir;  

            configApp.Guardar();
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

        void ToolbarAplicacion_TestPruebaEvent(object o, TestPruebaEventArgs e)
        {

            ResultadoCompilacion res = Compilar();

            if (res.CompilacionGarGarCorrecta && res.ResultadoCompPascal != null && res.ResultadoCompPascal.CompilacionPascalCorrecta)
            {


                List<NodoTablaSimbolos> aux = res.TablaSimbolos.ObtenerVariablesDelProcPrincipal();
                aux.AddRange(res.TablaSimbolos.ObtenerVariablesGlobales());

                ObservableCollection<Variables> listaVariablesEntrada = TransformarAVariables(aux);
                ObservableCollection<Variables> listaVariablesSalida = TransformarAVariables(res.TablaSimbolos.ObtenerParametrosDelProcSalida());

                WindowCreacionTest testWindow = new WindowCreacionTest();
                testWindow.VariablesEntrada = listaVariablesEntrada;
                testWindow.VariablesSalida = listaVariablesSalida;
                testWindow.Codigo = res.CodigoGarGar;

                testWindow.Title = "Tests de Prueba - Crear";

                testWindow.ShowDialog();


            }

        }

        private ObservableCollection<Variables> TransformarAVariables(List<NodoTablaSimbolos> list)
        {
            ObservableCollection<Variables> listaRetorno = new ObservableCollection<Variables>();

            foreach (var item in list)
            {
                listaRetorno.Add(new Variables(item));
            }

            return listaRetorno;
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
            
            bool modoDebug = Debugger.IsAttached;

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

        private ResultadoCompilacion Compilar()
        {
            ReiniciarIDEParaCompilacion();

            string programa = this.Esquema.GarGarACompilar;
            ResultadoCompilacion res = this.compilador.Compilar(programa);
            res.CodigoGarGar = programa;

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
                if (ArchCargado != null)
                {
                    ArchCargado.CompilacionCorrecta = true;
                }

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

                if (res.ResultadoCompPascal != null && res.ResultadoCompPascal.ListaErrores != null)
                {
                    foreach (ResultadoCompilacionPascalLinea item in res.ResultadoCompPascal.ListaErrores)
                    {
                        if (item.Mostrar)
                        {
                            this.BarraMsgs.AgregarError(item.ErrorTraducido, item.Fila, 0);
                        }
                    }
                }
            }
        }

        private void RibbonWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool continuar = SalvarSiUsuarioQuiere();

            if (!continuar)
            {
                e.Cancel = true;
            }
        }
    }
}
