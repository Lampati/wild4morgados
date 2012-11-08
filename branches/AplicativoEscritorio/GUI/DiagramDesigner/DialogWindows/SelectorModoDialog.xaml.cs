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
using System.ComponentModel;

namespace Ragnarok.DialogWindows
{
    /// <summary>
    /// Interaction logic for SelectorModoDialog.xaml
    /// </summary>
    public partial class SelectorModoDialog : Window
    {
        private bool preventClose = true;

        public enum TiposModo
        {
            ModoGrafico,
            ModoTexto
        }

        private TiposModo modoElegido;
        public TiposModo ModoElegido
        {
            get
            {
                return modoElegido;
            }
        }

        public SelectorModoDialog()
        {
            InitializeComponent();
        }

        private void bttnTexto_Click(object sender, RoutedEventArgs e)
        {
            modoElegido = TiposModo.ModoTexto;
            preventClose = false;
            Close();
        }

        private void bttnGrafico_Click(object sender, RoutedEventArgs e)
        {
            modoElegido = TiposModo.ModoGrafico;
            preventClose = false;
            Close();
        }
   

        private void Window_Closing(object sender, CancelEventArgs e)
        {           

            e.Cancel = preventClose;
        }

    }
}
