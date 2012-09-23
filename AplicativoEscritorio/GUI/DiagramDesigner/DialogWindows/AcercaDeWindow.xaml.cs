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

namespace Ragnarok.DialogWindows
{
    /// <summary>
    /// Interaction logic for AcercaDeWindow.xaml
    /// </summary>
    public partial class AcercaDeWindow : Window
    {
        public AcercaDeWindow()
        {
            InitializeComponent();
        }

        private void bttnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
