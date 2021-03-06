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
using Ragnarok.EventArgsClasses;
using Globales.Enums;

namespace Ragnarok.UserControls.Toolbar
{
    /// <summary>
    /// Lógica de interacción para BarraToolbar.xaml
    /// </summary>
    public partial class BarraToolbar : UserControl
    {
        public delegate void CompilacionEventHandler(object o, CompilacionEventArgs e);
        public delegate void CambioModoEventHandler(object o, CambioModoEventArgs e);
        public delegate void AbrirBusquedaEventHandler(object o, AbrirBusquedaEventArgs e);

        public event CompilacionEventHandler CompilacionEvent;
        public event CambioModoEventHandler CambioModoEvent;
        public event AbrirBusquedaEventHandler AbrirBusquedaEvent;

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

        public BarraToolbar()
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
            CambioModoEventFire(sender, new CambioModoEventArgs(ModoVisual.Texto,e));
        }

        private void ButtonFlujo_Click(object sender, RoutedEventArgs e)
        {
            CambioModoEventFire(sender, new CambioModoEventArgs(ModoVisual.Flujo,e));
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
    }

    //public class CompilacionEventArgs
    //{
    //    private bool esEjecucion;
    //    public bool EsEjecucion
    //    {
    //        get
    //        {
    //            return esEjecucion;
    //        }
    //    }

    //    public CompilacionEventArgs(bool esEjec)
    //    {
    //        esEjecucion = esEjec;
    //    }
    //}

    //public class CambioModoEventArgs
    //{
    //    private ModoVisual modoSeleccionado;
    //    public ModoVisual ModoSeleccionado
    //    {
    //        get
    //        {
    //            return modoSeleccionado;
    //        }
    //    }

    //    public CambioModoEventArgs(ModoVisual modo)
    //    {
    //        modoSeleccionado = modo;
    //    }
    //}

    //public class AbrirBusquedaEventArgs
    //{
    //    private bool esBuscarYReemplazar;
    //    public bool EsBuscarYReemplazar
    //    {
    //        get
    //        {
    //            return esBuscarYReemplazar;
    //        }
    //    }

    //    public AbrirBusquedaEventArgs(bool esReemp)
    //    {
    //        esBuscarYReemplazar = esReemp;
    //    }
    //}
}
