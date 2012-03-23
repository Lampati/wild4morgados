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
using System.Windows.Shapes;
using DiagramDesigner.ModoTexto.Configuracion;

namespace DiagramDesigner.DialogWindows
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
            this.Height += 10;
        }

        public void AgregarBotonera()
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            sp.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            Button btnAceptar = new Button();
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Click += new RoutedEventHandler(btnAceptar_Click);
            btnAceptar.Margin = new Thickness(20, 0, 0, 0);
            btnAceptar.Width = 75;
            btnAceptar.Content = "Aceptar";

            Button btnCancelar = new Button();
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Click += new RoutedEventHandler(btnCancelar_Click);
            btnCancelar.Margin = new Thickness(0, 0, 20, 0);
            btnCancelar.Width = 75;
            btnCancelar.Content = "Cancelar";
            btnCancelar.IsCancel = true;

            sp.Children.Add(btnCancelar);
            sp.Children.Add(btnAceptar);

            RowDefinition rd = new RowDefinition();
            rd.Height = new GridLength(50);
            this.grdObjetos.RowDefinitions.Add(rd);

            Grid.SetRow(sp, this.objetos.Count + 1);
            Grid.SetColumnSpan(sp, 2);

            this.grdObjetos.Children.Add(sp);
            this.Height += 50;
        }

        public void AgregarPropiedad(string titulo, Control ctrl)
        {
            this.objetos.Add(new ObjetoVentana(titulo, ctrl));

            RowDefinition rd = new RowDefinition();
            rd.Height = GridLength.Auto;
            this.grdObjetos.RowDefinitions.Add(rd);

            TextBlock txtBlock = new TextBlock();
            txtBlock.Text = String.Concat(titulo, ":");
            txtBlock.TextWrapping = TextWrapping.Wrap;
            Grid.SetRow(txtBlock, this.objetos.Count);
            Grid.SetRow(ctrl, this.objetos.Count);
            Grid.SetColumn(txtBlock, 0);
            Grid.SetColumn(ctrl, 1);

            this.grdObjetos.Children.Add(txtBlock);
            this.grdObjetos.Children.Add(ctrl);
            this.Height += ctrl.Height;
        }

        public PropertyEditionWindow()
        {
            InitializeComponent();
            this.objetos = new List<ObjetoVentana>();
            this.Height = 40;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }        
    }
}
