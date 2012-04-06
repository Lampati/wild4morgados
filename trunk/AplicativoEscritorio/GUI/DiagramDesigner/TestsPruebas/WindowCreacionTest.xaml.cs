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
using DataAccess.Entidades;
using CompiladorGargar.Resultado;
using EJEKOR;

namespace DiagramDesigner.TestsPruebas
{
    /// <summary>
    /// Interaction logic for WindowCreacionTest.xaml
    /// </summary>
    public partial class WindowCreacionTest : Window
    {
        private ArchResultado archResultadoEjecucuion;
        
        #region Properties

        public TestPrueba TestGenerado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        private CompiladorGargar.Compilador compilador;

        private List<int> listaLineasValidas;
        public List<int> ListaLineasValidas
        {
            get
            {
                return listaLineasValidas;
            }

            set
            {
                listaLineasValidas = value;

                if (lineas != null)
                {
                    for (int i = 0; i < lineas.Count; i++)
                    {
                        lineas[i].EsHabilitada = listaLineasValidas.Contains(i);
                    }
                }
            }
        }

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

                //dataGridVarsEntrada.ItemsSource = variablesEntrada;
                //dataGridVarsEntrada.Items.Refresh();

                lstVarsEntrada.ItemsSource = variablesEntrada;
                lstVarsEntrada.Items.Refresh();
            }
        }

        private ObservableCollection<Variable> variablesEntradaSeleccionadas;
        public ObservableCollection<Variable> VariablesEntradaSeleccionadas
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

                //lstVarsEntradaFinal.ItemsSource = variablesEntradaSeleccionadas;
                //lstVarsEntradaFinal.Items.Refresh();

                dataVarsEntradaFinal.Variables = variablesEntradaSeleccionadas;
               
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

                //dataGridVarsEntrada.ItemsSource = variablesEntrada;
                //dataGridVarsEntrada.Items.Refresh();

                lstVarsSalida.ItemsSource = variablesSalida;
                lstVarsSalida.Items.Refresh();

                
            }
        }


        private ObservableCollection<Variable> variablesSalidaSeleccionadas;
        public ObservableCollection<Variable> VariablesSalidaSeleccionadas
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

                //lstVarsSalidaFinal.ItemsSource = variablesSalidaSeleccionadas;
                //lstVarsSalidaFinal.Items.Refresh();

                dataVarsSalidaFinal.Variables = variablesSalidaSeleccionadas;
            }
        }

        private void CompletarValoresVariablesSeleccionadas(ObservableCollection<Variable> seleccionadas, ObservableCollection<Variable> total)
        {
            foreach (Variable item in seleccionadas)
            {
              
                Variable varConTodoCargado = total.Single(x => x.NombreCodigo == item.NombreCodigo);

                item.Valor = varConTodoCargado.Valor;
                item.Posiciones = varConTodoCargado.Posiciones;                                   
                
            }
        }

        #endregion

        public WindowCreacionTest(CompiladorGargar.Compilador comp)
        {
            InitializeComponent();

            listaLineasValidas = new List<int>();

            compilador = comp;
        }

        private ObservableCollection<Linea> ArmarLineas(string[] arr)
        {
            ObservableCollection<Linea> aux = new ObservableCollection<Linea>();
            int i = 1;
            foreach (string item in arr)
            {
                aux.Add(new Linea() { Codigo = item, EsHabilitada = true, Numero = i });
                i++;
            }

            return aux;
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
            VariablesEntradaSeleccionadas = new ObservableCollection<Variable>(variablesEntrada.Where(x => x.EsSeleccionada));
            int i = VariablesEntradaSeleccionadas.Count;

            this.wizard.CurrentPage.AllowNext = i > 0;
        }



        private void CheckBoxSalida_Click(object sender, RoutedEventArgs e)
        {
            VariablesSalidaSeleccionadas = new ObservableCollection<Variable>(VariablesSalida.Where(x => x.EsSeleccionada));
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

            int lineaElegida = lineas.Single(x => x.EsSeleccionada).Numero;

            this.compilador.MarcarEntrada = true;
            this.compilador.LineaEntrada = lineaElegida;

            ResultadoCompilacion res = this.compilador.Compilar(this.Codigo);            

            if (res.CompilacionGarGarCorrecta && res.GeneracionEjectuableCorrecto)
            {
                try
                {
                    EjecucionManager.EjecutarConVentana(res.ArchEjecutableConRuta);

                    archResultadoEjecucuion = new ArchResultado(res.ArchTemporalResultadosEjecucionConRuta);

                    CompletarValoresVariablesSeleccionadas(variablesEntradaSeleccionadas, archResultadoEjecucuion.Entradas.First().Variables);
                    CompletarValoresVariablesSeleccionadas(variablesSalidaSeleccionadas, archResultadoEjecucuion.VariablesSalida);

                    VariablesEntradaSeleccionadas = variablesEntradaSeleccionadas;
                    VariablesSalidaSeleccionadas = variablesSalidaSeleccionadas;

                    stackEjecucionSatisfactoria.Visibility = System.Windows.Visibility.Visible;
                    this.wizard.CurrentPage.AllowBack = false;
                    this.wizard.CurrentPage.AllowNext = true;
                }
                catch (Exception)
                {
                    //Mostrar error pq no se puede continuar
                }               
            }
            else
            {
                //Mostrar error pq no se puede continuar
            }

            this.compilador.MarcarEntrada = false;
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
