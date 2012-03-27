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

namespace DiagramDesigner.TestsPruebas
{
    /// <summary>
    /// Interaction logic for WindowCreacionTest.xaml
    /// </summary>
    public partial class WindowCreacionTest : Window
    {
        private List<NodoTablaSimbolos> variablesEntrada;
        public List<NodoTablaSimbolos> VariablesEntrada
        {
            get
            {
                return variablesEntrada;
            }

            set
            {
                variablesEntrada = value;
                
            }
        }
        
        public List<NodoTablaSimbolos> VariablesSalida { get; set; }

        public WindowCreacionTest()
        {
            InitializeComponent();

            dataGridVarsEntrada.DataContext = variablesEntrada;

        }
    }
}
