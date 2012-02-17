﻿using System.Windows;
using CompiladorGargar.Resultado;
using WpfApplicationHotKey.WinApi;
using CompiladorGargar;
using System.Windows.Input;
using System;
using Utilidades;
using DiagramDesigner.Enums;
using System.Windows.Controls;

namespace DiagramDesigner
{
    public partial class Window1 : Window
    {
        #region Hotkeys Definicion
        HotKey hotKeyCompilar;
        HotKey hotKeyEjecutar;
        #endregion

        Compilador compilador;
        


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
            //this.BarraMsgs.AgregarMensaje("Mensaje para mostrar aquí en la barrica de herramienticas!!");

             
            
            

            this.BarraMsgs.DoubleClickEvent += new BarraMensajes.DobleClickEnBarraMensajesEventHandler(BarraMsgs_DoubleClickEvent);
            this.Esquema.ModoTextoCambiarPosicionEvent += new EsquemaCentral.ModoTextoCambiarPosicionEventHandler(Esquema_ModoTextoCambiarPosicionEvent);

            this.ToolbarAplicacion.CompilacionEvent += new BarraToolbar.CompilacionEventHandler(ToolbarAplicacion_CompilacionEvent);
            this.ToolbarAplicacion.CambioModoEvent += new BarraToolbar.CambioModoEventHandler(ToolbarAplicacion_CambioModoEvent);
            this.ToolbarAplicacion.AbrirBusquedaEvent += new BarraToolbar.AbrirBusquedaEventHandler(ToolbarAplicacion_AbrirBusquedaEvent);

            //NO TENGO EL XML!!!
            ConfigurarCompilador();

            hotKeyCompilar = new HotKey(ModifierKeys.None, Keys.F3, Window.GetWindow(this));
            hotKeyEjecutar = new HotKey(ModifierKeys.None, Keys.F4, Window.GetWindow(this));

            hotKeyCompilar.HotKeyPressed += new Action<HotKey>(hotKeyCompilar_HotKeyPressed);
            hotKeyEjecutar.HotKeyPressed += new Action<HotKey>(hotKeyCompilar_HotKeyPressed);

            Modo = ModoVisual.Flujo;
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
            Button botonPresionado = (Button)o;


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

            string directorioActual = AppDomain.CurrentDomain.BaseDirectory;

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