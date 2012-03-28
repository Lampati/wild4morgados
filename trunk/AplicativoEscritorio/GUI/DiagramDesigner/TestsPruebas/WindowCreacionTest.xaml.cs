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
using CompiladorGargar.Semantico.TablaDeSimbolos;
using System.Collections.ObjectModel;

namespace DiagramDesigner.TestsPruebas
{
    /// <summary>
    /// Interaction logic for WindowCreacionTest.xaml
    /// </summary>
    public partial class WindowCreacionTest : Window
    {


        private ObservableCollection<Variables> variablesEntrada;
        public ObservableCollection<Variables> VariablesEntrada
        {
            get
            {
                return variablesEntrada;
            }

            set
            {
                variablesEntrada = value;

                //dataGridVarsEntrada.ItemsSource = variablesEntrada;
                //dataGridVarsEntrada.Items.Refresh();

                lstVarsEntrada.ItemsSource = variablesEntrada;
                lstVarsEntrada.Items.Refresh();
            }
        }


        public ObservableCollection<Variables> VariablesSalida { get; set; }

        public WindowCreacionTest()
        {
            InitializeComponent();


            wizard.Cancelled += new RoutedEventHandler(wizard_Cancelled);
            //dataGridVarsEntrada.DataContext = variablesEntrada;

            lstVarsEntrada.DataContext = variablesEntrada;

        }

        void wizard_Cancelled(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            int i = variablesEntrada.Where(x => x.EsSeleccionada).Count();

            this.wizard.CurrentPage.AllowNext = i > 0;
        }

  
    }
}
