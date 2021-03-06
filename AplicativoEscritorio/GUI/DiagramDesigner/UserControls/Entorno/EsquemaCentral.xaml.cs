﻿using System;
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
using FindReplace;
using Ragnarok.ModoTexto.Configuracion;
using CompiladorGargar.Resultado;
using Utilidades;
using CompiladorGargar;
using WpfApplicationHotKey.WinApi;
using Ragnarok.EventArgsClasses;
using AplicativoEscritorio.DataAccess.Interfases;
using AplicativoEscritorio.DataAccess.Entidades;
using Globales.Enums;
using LibreriaActividades;


namespace Ragnarok.UserControls.Entorno
{
    /// <summary>
    /// Interaction logic for EsquemaCentral.xaml
    /// </summary>
    public partial class EsquemaCentral : UserControl
    {
        


        private ModoVisual modo;

        FindReplaceMgr findAndReplaceManager;

        public RagnarokWindow Owner { get; set; }

        public bool EstaIdentado { get; set; }

        public EsquemaCentral()
        {
            InitializeComponent();

            this.InicializarAvalon();

            this.textEditor.TextArea.Caret.PositionChanged += new EventHandler(Caret_PositionChanged);
            this.modoGrafico.ModoGraficoModificadoEvent += new ModoGrafico.ProgramadorGrafico.ModoGraficoModificadoEventHandler(modoGrafico_ModoGraficoModificadoEvent);

            this.modoGrafico.Owner = this.Owner;
        }

    

        void Caret_PositionChanged(object sender, EventArgs e)
        {
            if (this.textEditor.TextArea.Caret != null)
            {
                ModoTextoCambiarPosicionEventFire(
                    new ModoTextoCambiarPosicionEventArgs(
                        this.textEditor.TextArea.Caret.Line, 
                        this.textEditor.TextArea.Caret.Column)
                        );
            }
        }


        public InterfazTextoGrafico.ProgramaViewModel representacionGraficaActual;
        public InterfazTextoGrafico.ProgramaViewModel RepresentacionGraficaActual 
        {
            get
            {
                representacionGraficaActual = modoGrafico.ObtenerProgramaEnModoGrafico();
                archCargado.RepresentacionGrafica = representacionGraficaActual;
                return representacionGraficaActual;
            }
            set
            {
                representacionGraficaActual = value;
                archCargado.RepresentacionGrafica = representacionGraficaActual;
                modoGrafico.CargarProgramaEnModoGrafico(representacionGraficaActual);
            }
        }      

        public string GarGarACompilar
        {
            get
            {
                if (Modo == ModoVisual.Texto)
                {
                    return this.textEditor.Text;
                    
                }
                else
                {
                    return RepresentacionGraficaActual.Gargar;
                }
            }
            set
            {
                if (Modo == ModoVisual.Texto)
                {
                    this.textEditor.Text = value;
                }
                
            }
        }

        public ModoVisual Modo
        {
            get { return this.modo; }
            set
            {                
                this.modo = value;
                switch (this.modo)
                {
                    case ModoVisual.Flujo:                        
                        this.grdVisual.Visibility = System.Windows.Visibility.Visible;
                        this.grdTexto.Visibility = System.Windows.Visibility.Collapsed;
                        DesconfigurarBuscarYReemplazarModoTexto();
                        //MyDesigner.Focus();                        
                        break;
                    case ModoVisual.Texto:
                        this.grdVisual.Visibility = System.Windows.Visibility.Collapsed;
                        this.grdTexto.Visibility = System.Windows.Visibility.Visible;
                        ConfigurarBuscarYReemplazarModoTexto();
                        textEditor.TextArea.Focus();
                        break;
                }
            }
        }

        private EntidadBase archCargado = null;
        public EntidadBase ArchCargado
        {
            get { return archCargado; }
            set
            {
                archCargado = value;

                if (archCargado == null)
                {
                    this.grdVisual.Visibility = System.Windows.Visibility.Hidden;
                    this.grdTexto.Visibility = System.Windows.Visibility.Collapsed;
                    DesconfigurarBuscarYReemplazarModoTexto();
                }
                else
                {
                    if (archCargado.UltimoModoGuardado == AplicativoEscritorio.DataAccess.Enums.ModoVisual.Texto)
                    {
                        textEditor.Text = archCargado.Gargar;
                    }
                    else
                    {
                        RepresentacionGraficaActual = archCargado.RepresentacionGrafica; 
                    }
                }
            }
        }

        private void InicializarAvalon()
        {
            //InitializeComponent();

            ConfigurarModoTexto();

         
        }

        private void ConfigurarModoTexto()
        {
            ModoTextoConfiguracion.ConfigurarAvalonEdit(textEditor);
            textEditor.TextChanged += new EventHandler(textEditor_TextChanged);
            ConfigurarBuscarYReemplazarModoTexto();
        }

        void textEditor_TextChanged(object sender, EventArgs e)
        {
            if (ArchCargado != null)
            {
                ArchCargado.ModificadoDesdeUltimoGuardado = true;
                ArchCargado.CompilacionCorrecta = false;
                ArchCargado.EjecucionCorrecta = false;
                ArchCargado.Gargar = textEditor.Text;

                EstaIdentado = false;
            }
        }

        void modoGrafico_ModoGraficoModificadoEvent(object o, ModoGrafico.EventArgsClasses.ModoGraficoModificadoEventArgs e)
        {
            if (ArchCargado != null)
            {
                ArchCargado.ModificadoDesdeUltimoGuardado = true;
                ArchCargado.CompilacionCorrecta = false;
                ArchCargado.EjecucionCorrecta = false;
                try
                {
                    ArchCargado.RepresentacionGrafica = modoGrafico.ObtenerProgramaEnModoGrafico();
                }
                catch (InterfazTextoGrafico.Excepciones.ExcepcionLlamadaCircular ex)
                {
                    //ET (11/11/2012): Saco este show porque se notifica 2 veces
                    //MessageBox.Show(ex.Message, "Llamada circular detectada", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        public void ConfigurarBuscarYReemplazarModoTexto()
        {
            findAndReplaceManager = new FindReplaceMgr();

            findAndReplaceManager.CurrentEditor = new FindReplace.TextEditorAdapter(textEditor);
            findAndReplaceManager.ShowSearchIn = false;
            findAndReplaceManager.OwnerWindow = Window.GetWindow(this);

            CommandBindings.Add(findAndReplaceManager.FindBinding);
            CommandBindings.Add(findAndReplaceManager.ReplaceBinding);
            CommandBindings.Add(findAndReplaceManager.FindNextBinding);
        }

        public void DesconfigurarBuscarYReemplazarModoTexto()
        {
            if (findAndReplaceManager != null)
            {
                findAndReplaceManager.CurrentEditor = null;
                findAndReplaceManager.ShowSearchIn = false;
                findAndReplaceManager.OwnerWindow = null;

                CommandBindings.Clear();

                findAndReplaceManager = null;
            }
        }


        internal void PosicionarseEn(int fila, int col)
        {
            if (Modo == ModoVisual.Texto)
            {
                this.textEditor.TextArea.Caret.Line = fila;
                this.textEditor.TextArea.Caret.Column = col;
                this.textEditor.TextArea.Caret.Show();
                this.textEditor.TextArea.Caret.BringCaretToView();
                this.textEditor.TextArea.Focus();
            }
        }

        internal void MostrarBuscar(bool esBusqYReemp)
        {
            if (esBusqYReemp)
            {
                findAndReplaceManager.ShowAsReplace();
            }
            else
            {
                findAndReplaceManager.ShowAsFind();
            }
        }

      

        internal void CambiarTamanio(double width, double height)
        {
                grdTexto.MaxWidth = width;
                grdTexto.MaxHeight = height;
                grdVisual.MaxWidth = width;
                grdVisual.MaxHeight = height;
        }

        internal void PonerFocoEnActividad(string p, object actividadBase)
        {
            modoGrafico.PonerFocoEnActividad(p, actividadBase);
        }

        internal void PonerFocoEnTab(string p)
        {
            modoGrafico.PonerFocoEnTab(p);
        }

        
    }

    
}
