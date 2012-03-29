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
using System.ComponentModel;
using AplicativoEscritorio.DataAccess.Entidades;

namespace DiagramDesigner.TestsPruebas
{
    /// <summary>
    /// Interaction logic for WindowCreacionTest.xaml
    /// </summary>
    public partial class WindowCreacionTest : Window
    {
        #region Properties

        public TestPrueba TestGenerado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set 
            { 
                codigo = value;
                Lineas = ArmarLineas(codigo.Split(new string[] { "\r\n" }, StringSplitOptions.None));
            }
        }

        private ObservableCollection<Linea> ArmarLineas(string[] arr)
        {
            ObservableCollection<Linea> aux = new ObservableCollection<Linea>();
            foreach (string item in arr)
            {
                aux.Add(new Linea() { Codigo = item, EsHabilitada = true });
            }

            return aux;
        }

        private ObservableCollection<Linea> lineas;
        public ObservableCollection<Linea> Lineas
        {
            get
            {
                return lineas;
            }

            set
            {
                lineas = value;

                //dataGridVarsEntrada.ItemsSource = variablesEntrada;
                //dataGridVarsEntrada.Items.Refresh();

                lstLineas.ItemsSource = lineas;
                lstLineas.Items.Refresh();
            }
        }

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

        private ObservableCollection<Variables> variablesEntradaSeleccionadas;
        public ObservableCollection<Variables> VariablesEntradaSeleccionadas
        {
            get
            {

                return variablesEntradaSeleccionadas;
            }

            set
            {
                variablesEntradaSeleccionadas = value;

                //dataGridVarsEntrada.ItemsSource = variablesEntrada;
                //dataGridVarsEntrada.Items.Refresh();

                dataGridVariablesEntradaElegidas.ItemsSource = variablesEntradaSeleccionadas;
                dataGridVariablesEntradaElegidas.Items.Refresh();

                lstVarsEntradaFinal.ItemsSource = variablesEntradaSeleccionadas;
                lstVarsEntradaFinal.Items.Refresh();
               
            }
        }

        private ObservableCollection<Variables> variablesSalida;
        public ObservableCollection<Variables> VariablesSalida
        {
            get
            {
                return variablesSalida;
            }

            set
            {
                variablesSalida = value;

                //dataGridVarsEntrada.ItemsSource = variablesEntrada;
                //dataGridVarsEntrada.Items.Refresh();

                lstVarsSalida.ItemsSource = variablesSalida;
                lstVarsSalida.Items.Refresh();

                
            }
        }


        private ObservableCollection<Variables> variablesSalidaSeleccionadas;
        public ObservableCollection<Variables> VariablesSalidaSeleccionadas
        {
            get
            {

                return variablesSalidaSeleccionadas;
            }

            set
            {
                variablesSalidaSeleccionadas = value;

                //dataGridVarsEntrada.ItemsSource = variablesEntrada;
                //dataGridVarsEntrada.Items.Refresh();

                dataGridVariablesSalidaElegidas.ItemsSource = variablesSalidaSeleccionadas;
                dataGridVariablesSalidaElegidas.Items.Refresh();

                lstVarsSalidaFinal.ItemsSource = variablesSalidaSeleccionadas;
                lstVarsSalidaFinal.Items.Refresh();
            }
        }

        #endregion

        public WindowCreacionTest()
        {
            InitializeComponent();
        }  

        void wizard_Cancelled(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void wizard_Finished(object sender, RoutedEventArgs e)
        {
            int i = 0;

            TestGenerado = new TestPrueba();

            TestGenerado.Descripcion = Descripcion;

            foreach (var item in variablesEntradaSeleccionadas)
            {
                TestGenerado.VariablesEntrada.Add(new VariableTest()
                {
                    Descripcion = item.Descripcion,
                    Nombre = item.Nombre,
                    ValorEsperado = item.Valor
                   
                });
            }

            Close();
        }

        
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            VariablesEntradaSeleccionadas = new ObservableCollection<Variables>(variablesEntrada.Where(x => x.EsSeleccionada));
            int i = VariablesEntradaSeleccionadas.Count;

            this.wizard.CurrentPage.AllowNext = i > 0;
        }



        private void CheckBoxSalida_Click(object sender, RoutedEventArgs e)
        {
            VariablesSalidaSeleccionadas = new ObservableCollection<Variables>(VariablesSalida.Where(x => x.EsSeleccionada));
            int i = VariablesSalidaSeleccionadas.Count;

            this.wizard.CurrentPage.AllowNext = i > 0;
        }

        private void CheckBoxLinea_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chequeada = (CheckBox)sender;            

            if (chequeada.IsChecked == true)
            {
                foreach (var item in lineas)
                {
                    item.EsSeleccionada = false;
                }

                chequeada.IsChecked = true;
            }

            this.wizard.CurrentPage.AllowNext = chequeada.IsChecked == true;
        }

        private void ButtonEjecutar_Click(object sender, RoutedEventArgs e)
        {
            bttnEjecutar.IsEnabled = false;
            stackEjecucionSatisfactoria.Visibility = System.Windows.Visibility.Visible;
            this.wizard.CurrentPage.AllowBack = false;
            this.wizard.CurrentPage.AllowNext =  true;
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            Nombre = txtNombre.Text;

            this.wizard.CurrentPage.AllowFinish = txtNombre.Text != string.Empty && txtDescripcion.Text != string.Empty;

        }

        private void txtDescripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            Descripcion = txtDescripcion.Text;

            this.wizard.CurrentPage.AllowFinish = txtNombre.Text != string.Empty && txtDescripcion.Text != string.Empty;
        }
    }


   
}
