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
using System.Windows.Shapes;
using Ragnarok.ModoTexto.Configuracion;

namespace Ragnarok.DialogWindows
{
    /// <summary>
    /// Interaction logic for TextEditionWindow.xaml
    /// </summary>
    public partial class PropertyEditionWindow : Window
    {
        private List<ObjetoVentana> objetos;

        public string Titulo
        {
            get
            {
                return (string)labelTitulo.Content;
            }
            set
            {
                labelTitulo.Content = value;
            }
        }

        public void AgregarSeparador()
        {
            this.AgregarSeparador(true);
        }

        public void AgregarSeparador(bool linea)
        {
            this.objetos.Add(new ObjetoVentana());

            RowDefinition rd = new RowDefinition();
            rd.Height = new GridLength(10);
            this.grdObjetos.RowDefinitions.Add(rd);

            if (linea)
            {
                Separator sep = new Separator();
                Grid.SetRow(sep, this.objetos.Count);
                Grid.SetColumnSpan(sep, 2);
                this.grdObjetos.Children.Add(sep);            
            }
            //this.Height += 10;
        }

        public void AgregarBotonera(RoutedEventHandler accionAceptar, RoutedEventHandler accionCancelar)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            sp.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            Button btnAceptar = new Button();
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Click += accionAceptar;
            btnAceptar.Margin = new Thickness(0, 0, 15, 0);
            btnAceptar.Width = 70;
            btnAceptar.Content = "Aceptar";
            btnAceptar.IsDefault = true;
            btnAceptar.Style =  Application.Current.Resources["BlueButton"] as Style;

            Button btnCancelar = new Button();
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Click += accionCancelar;
            btnCancelar.Margin = new Thickness(0, 0, 6, 0);
            btnCancelar.Width = 70;
            btnCancelar.Content = "Cancelar";
            btnCancelar.IsCancel = true;
            btnCancelar.Style =  Application.Current.Resources["BlueButton"] as Style;
            

            
            sp.Children.Add(btnAceptar);
            sp.Children.Add(btnCancelar);

            RowDefinition rd = new RowDefinition();
            rd.Height = new GridLength(50);
            this.grdObjetos.RowDefinitions.Add(rd);

            Grid.SetRow(sp, this.objetos.Count + 1);
            Grid.SetColumnSpan(sp, 2);

            this.grdObjetos.Children.Add(sp);
            this.Height += 30;
        }

        public void AgregarPropiedad(string titulo)
        {
            RowDefinition rd = new RowDefinition();
            rd.Height = GridLength.Auto;
            this.grdObjetos.RowDefinitions.Add(rd);

            TextBlock txtBlock = new TextBlock();
            txtBlock.Text = titulo;
            txtBlock.Margin = new Thickness(5, 0, 5, 0);
            txtBlock.TextWrapping = TextWrapping.Wrap;

            this.objetos.Add(new ObjetoVentana(txtBlock));

            Grid.SetRow(txtBlock, this.objetos.Count);
            Grid.SetColumnSpan(txtBlock, 2);

            this.grdObjetos.Children.Add(txtBlock);
            //this.Height += txtBlock.ActualHeight;
        }

        public void AgregarPropiedad(Control ctrl)
        {
            RowDefinition rd = new RowDefinition();
            rd.Height = GridLength.Auto;
            this.grdObjetos.RowDefinitions.Add(rd);

            this.objetos.Add(new ObjetoVentana(null, ctrl));
            Grid.SetRow(ctrl, this.objetos.Count);
            Grid.SetColumnSpan(ctrl, 2);

            this.grdObjetos.Children.Add(ctrl);
        }

      

        public void AgregarPropiedad(string titulo, Control ctrl)
        {
            RowDefinition rd = new RowDefinition();
            rd.Height = GridLength.Auto;
            this.grdObjetos.RowDefinitions.Add(rd);

            TextBlock txtBlock = new TextBlock();
            txtBlock.Margin = new Thickness(5,0,5,0);
            if (!String.IsNullOrEmpty(titulo))
                txtBlock.Text = String.Concat(titulo, ":");
            txtBlock.TextWrapping = TextWrapping.Wrap;
            this.objetos.Add(new ObjetoVentana(txtBlock, ctrl));
            Grid.SetRow(txtBlock, this.objetos.Count);
            Grid.SetRow(ctrl, this.objetos.Count);
            Grid.SetColumn(txtBlock, 0);
            Grid.SetColumn(ctrl, 1);

            this.grdObjetos.Children.Add(txtBlock);
            this.grdObjetos.Children.Add(ctrl);
            //this.Height += ctrl.Height;
        }

        public PropertyEditionWindow()
        {
            InitializeComponent();
            this.objetos = new List<ObjetoVentana>();
            this.Height = 60;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool primero = true;
            foreach (ObjetoVentana ov in this.objetos)
                if (ov.SeCompleto)
                {
                    if (ov.Controls != null)
                    {
                        double max = 0;                        
                        foreach (Control ctrl in ov.Controls)
                        {
                            if (primero)
                                if (ctrl.Focusable)
                                {
                                    ctrl.Focus();
                                    primero = false;
                                }                                    

                            if (ctrl.ActualHeight > max)
                                max = ctrl.ActualHeight;
                        }
                        this.Height += max;
                    }
                    else
                        this.Height += ov.Texto.ActualHeight;
                }
                else //Es un separador
                    this.Height += 10;
        }
    }
}
