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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiagramDesigner.TestsPruebas;
using System.Collections.ObjectModel;
using DataAccess.Entidades;
using System.Data;

namespace DiagramDesigner.UserControls
{
    /// <summary>
    /// Interaction logic for VariablesConValoresPresenter.xaml
    /// </summary>
    public partial class VariablesConValoresPresenter : UserControl
    {
        private ObservableCollection<Variable> variables;
        public ObservableCollection<Variable> Variables
        {
            get
            {
                return variables;
            }

            set
            {
                variables = value;

                //dataGridVarsEntrada.ItemsSource = variablesEntrada;
                //dataGridVarsEntrada.Items.Refresh();

                dataGridVariablesSalidaCompleta.ItemsSource = variables;
                dataGridVariablesSalidaCompleta.Items.Refresh();

                dataGridVariablesSalidaReducida.ItemsSource = variables;
                dataGridVariablesSalidaReducida.Items.Refresh();
            }
        }

        private bool esVersionReducida;
        public bool EsVersionReducida
        {
            get
            {
                return esVersionReducida;
            }
            set
            {
                esVersionReducida = value;

                if (esVersionReducida)
                {
                    dataGridVariablesSalidaCompleta.Visibility = System.Windows.Visibility.Collapsed;
                    dataGridVariablesSalidaReducida.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    dataGridVariablesSalidaReducida.Visibility = System.Windows.Visibility.Collapsed;
                    dataGridVariablesSalidaCompleta.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private bool validarVariables;
        public bool ValidarVariables
        {
            get
            {
                return validarVariables;
            }
            set
            {
                validarVariables = value;
            }
        }

        public VariablesConValoresPresenter()
        {
            InitializeComponent();

            validarVariables = false;
        }

        private void dataGridVariablesSalida_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Variable variable = e.Row.Item as Variable;
            if (variable != null && validarVariables)
            {
                
                if (variable.EsValida)
                {
                    e.Row.Background = new SolidColorBrush(Colors.White);
                }
                else
                {
                    e.Row.Background = new SolidColorBrush(Colors.Red);
                }
                
                // Access cell values values if needed...
                // var colValue = row["ColumnName1]";
                // var colValue2 = row["ColumName2]";                
                
            }     
        }
    }
}
