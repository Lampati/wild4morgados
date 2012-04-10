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
using AplicativoEscritorio.DataAccess.Entidades;
using DataAccess.Entidades;

namespace DiagramDesigner.TestsPruebas
{
    /// <summary>
    /// Interaction logic for WindowConsultaTests.xaml
    /// </summary>
    public partial class WindowConsultaTests : Window
    {
        private ObservableCollection<TestPrueba> testPruebas;
        public ObservableCollection<TestPrueba> TestPruebas
        {
            get
            {
                return testPruebas;
            }

            set
            {
                testPruebas = value;
              
                //lstVarsEntrada.ItemsSource = variablesEntrada;
                //lstVarsEntrada.Items.Refresh();
            }
        }

        private ObservableCollection<Variable> variablesEntrada;
        public ObservableCollection<Variable> VariablesEntrada
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

        private ObservableCollection<Variable> variablesSalida;
        public ObservableCollection<Variable> VariablesSalida
        {
            get
            {
                return variablesSalida;
            }

            set
            {
                variablesSalida = value;
            }
        }

        
               

        public WindowConsultaTests()
        {
            InitializeComponent();


        }

        public void Validar()
        {
            StringBuilder strBldr = new StringBuilder();

            foreach (TestPrueba item in this.testPruebas)
            {
                if (!item.ValidarVariablesEntrada(variablesEntrada.ToList()))
                {
                    strBldr.AppendLine(item.MensajeErrorEntrada);
                }

                if (!item.ValidarVariablesSalida(variablesSalida.ToList()))
                {
                    strBldr.AppendLine(item.MensajeErrorSalida);
                }

                item.MensajeError = strBldr.ToString();
            }

            ActualizarLista();
            
            

        }

        private void ActualizarLista()
        {
            lstTests.ItemsSource = this.testPruebas;
            lstTests.Items.Refresh();
        }

        private void ButtonDetalles_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)e.OriginalSource;

            string id = boton.DataContext.ToString();

            TestPrueba test = this.testPruebas.Single(x => x.Id == id);

            WindowDetalleConsultaTest detalle = new WindowDetalleConsultaTest();
            detalle.TestPrueba = test;
            detalle.VariablesEntrada = variablesEntrada;
            detalle.VariablesSalida = variablesSalida;

            detalle.ShowDialog();

        }

        private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)e.OriginalSource;
            string id = boton.DataContext.ToString();

            this.testPruebas.Remove(this.testPruebas.Single(x => x.Id == id));

            ActualizarLista();
        }

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
