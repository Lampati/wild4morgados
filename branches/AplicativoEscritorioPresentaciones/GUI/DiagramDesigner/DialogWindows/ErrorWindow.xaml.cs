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
using System.Collections.ObjectModel;
using InterfazTextoGrafico;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ModoGrafico.Views
{
  

    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window, INotifyPropertyChanged
    {
        private string errorDetalles;
        public string ErrorDetalles
        {
            get
            {
                return errorDetalles;
            }
            set
            {
                errorDetalles = value;

                txtDetallesError.Text = errorDetalles;
            }
        }

        public ErrorWindow()
        {
            InitializeComponent();

            ErrorDetalles = "No hay información disponible para este error";
        }

        private void bttnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Height = 360;            
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            Height = 220;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }      
    }
}
