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

namespace Ragnarok.TestsPruebas
{
    /// <summary>
    /// Interaction logic for WindowConsultaTests.xaml
    /// </summary>
    public partial class WindowConsultaTests : Window
    {
        
        private bool esReadOnly;


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

        
               

        public WindowConsultaTests(bool modoReadOnly)
        {
            InitializeComponent();

            esReadOnly = modoReadOnly;

            if (modoReadOnly)
            {
                gridTests.Columns.RemoveAt(3);
                bttnCancelar.Visibility = System.Windows.Visibility.Collapsed;
            }

            
        }

        public void Validar()
        {
            StringBuilder strBldr = new StringBuilder();

            foreach (TestPrueba item in this.testPruebas)
            {
                bool entrada;
                bool salida;
                StringBuilder erroresEntrada = new StringBuilder();
                StringBuilder erroresSalida = new StringBuilder();

                entrada = item.ValidarVariablesEntrada(variablesEntrada.ToList() );
                salida = item.ValidarVariablesSalida(variablesSalida.ToList());

                if (!entrada)
                {
                    strBldr.AppendLine(item.MensajeErrorEntrada);
                    
                }

                if (!salida)
                {
                    strBldr.AppendLine(item.MensajeErrorSalida);
                }

                item.EsValido = entrada && salida;

                if (!item.EsValido)
                {
                    item.MensajeError = strBldr.ToString();
                }
                else
                {
                    item.MensajeError = "El test no tiene errores en sus variables";
                }
                
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
            
            detalle.VariablesEntrada = variablesEntrada;
            detalle.VariablesSalida = variablesSalida;
            detalle.TestPrueba = test;

            detalle.Title = string.Format("Variables del test {0}", test.Nombre);

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

            DialogResult =  !esReadOnly;
            
            Close();
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
