﻿using System;
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
using Utilidades;

namespace DiagramDesigner.TestsPruebas
{
    /// <summary>
    /// Interaction logic for WindowCreacionTest.xaml
    /// </summary>
    public partial class WindowEjecucionTest : Window
    {
        private ArchResultado archResultadoEjecucuion;
        private TestPrueba testElegido;
        
        #region Properties

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

        public List<int> ListaLineasContenidoProcSalida { get; set; }        

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


        private ObservableCollection<TestPrueba> tests;
        public ObservableCollection<TestPrueba> Tests
        {
            get
            {
                return tests;
            }

            set
            {
                tests = value;

                lstTests.ItemsSource = tests;
                lstTests.Items.Refresh();
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

              

                
            }
        }


     

        private void CompletarValoresVariablesSeleccionadas(ObservableCollection<Variable> seleccionadas, ObservableCollection<Variable> total)
        {
            foreach (Variable item in seleccionadas)
            {

                Variable varConTodoCargado = total.Single(x => x.Nombre.ToUpper() == item.Nombre.ToUpper() && x.Contexto.ToUpper() == item.Contexto.ToUpper());

                item.Valor = varConTodoCargado.Valor;
                item.Posiciones = varConTodoCargado.Posiciones;                                   
                
            }
        }

        #endregion

        public WindowEjecucionTest(CompiladorGargar.Compilador comp)
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
            Close();
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

        private void CheckBoxTest_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chequeada = (CheckBox)sender;

            if (chequeada.IsChecked == true)
            {
                foreach (var item in tests)
                {
                    item.EsSeleccionada = false;
                }

                chequeada.IsChecked = true;
            }
            else
            {
                chequeada.IsChecked = true;
            }

            testElegido = tests.Single(x => x.EsSeleccionada);

            foreach (var item in testElegido.VariablesEntrada)
            {

                if (item.EsArreglo)
                {
                    item.TipoVariable = string.Format("Arreglo con tope {0}", item.Posiciones.Count);
                }
                else
                {
                    item.TipoVariable = "Variable";
                }

                List<Variable> auxLista = new List<Variable>(variablesEntrada);

                foreach (var variable in variablesEntrada)
                {
                    auxLista.RemoveAll(x => x.Contexto.ToUpper() == "GLOBAL" && variable.Contexto.ToUpper() == "PRINCIPAL" && variable.Nombre.ToUpper() == x.Nombre.ToUpper());
                }

                item.PosiblesMapeos =   auxLista.ToList().FindAll(x => x.EsArreglo == item.EsArreglo 
                                        && x.TipoDato.ToString().ToUpper() == item.TipoDato.ToUpper()                                        
                                        );
            }

            dataGridVariablesEntradaTest.ItemsSource = testElegido.VariablesEntrada;
            dataGridVariablesEntradaTest.Items.Refresh();


            foreach (var item in testElegido.VariablesSalida)
            {

                if (item.EsArreglo)
                {
                    item.TipoVariable = string.Format("Arreglo con tope {0}", item.Posiciones.Count);
                }
                else
                {
                    item.TipoVariable = "Variable";
                }

                item.PosiblesMapeos = variablesSalida.ToList().FindAll(x => x.EsArreglo == item.EsArreglo
                                        && x.TipoDato.ToString().ToUpper() == item.TipoDato.ToUpper()
                                        );
            }

            dataGridVariablesSalidaTest.ItemsSource = testElegido.VariablesSalida;
            dataGridVariablesSalidaTest.Items.Refresh();
            

            this.wizard.CurrentPage.AllowNext = chequeada.IsChecked == true;
        }

        private void ButtonEjecutar_Click(object sender, RoutedEventArgs e)
        {
            bttnEjecutar.IsEnabled = false;

           
            string parteEntrada = ArmarCodigoParteEntrada(this.testElegido.VariablesEntrada);
            string parteSalida = ArmarCodigoParteSalida(this.testElegido.VariablesSalida);            

            

            int lineaElegida = lineas.Single(x => x.EsSeleccionada).Numero;

            this.compilador.ReemplazarEntrada = true;
            this.compilador.LineaEntrada = lineaElegida;
            this.compilador.CodigoReemplazoEntrada = parteEntrada;

            this.compilador.ReemplazarSalida = true;
            this.compilador.CodigoReemplazoSalida = parteSalida;
            this.compilador.LineaComienzoReemplazoSalida = ListaLineasContenidoProcSalida.First();
            this.compilador.LineaFinReemplazoSalida = ListaLineasContenidoProcSalida.Last();

            ResultadoCompilacion res = this.compilador.Compilar(this.Codigo);            

            if (res.CompilacionGarGarCorrecta && res.GeneracionEjectuableCorrecto)
            {
                try
                {
                    EjecucionManager.EjecutarConVentana(res.ArchEjecutableConRuta);

                    archResultadoEjecucuion = new ArchResultado(res.ArchTemporalResultadosEjecucionConRuta);

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

            this.compilador.ReemplazarEntrada = false;
            this.compilador.ReemplazarSalida = false;
        }

        private string ArmarCodigoParteEntrada(List<VariableTest> list)
        {
            StringBuilder strBldrCodigoGarGarEntrada = new StringBuilder();
            string controladorVar = "{0} := {1};";
            string controladorPosArr = "{0}[{2}] := {1};";

            foreach (var item in list)
            {
                if (item.EsArreglo)
                {
                    for (int j = 0; j < item.Posiciones.Count; j++)
                    {
                        switch (item.TipoDato.ToUpper())
                        {
                            case "TEXTO":
                                strBldrCodigoGarGarEntrada.AppendFormat(controladorPosArr, item.VariableMapeada, string.Format("{0}{1}{0}", "'", item.Posiciones[j].Valor), item.Posiciones[j].Posicion).AppendLine(); 
                                break;
                            case "NUMERO":
                                strBldrCodigoGarGarEntrada.AppendFormat(controladorPosArr, item.VariableMapeada, item.Posiciones[j].Valor, item.Posiciones[j].Posicion).AppendLine();
                                break;
                            case "BOOLEANO":
                                string aux = item.Posiciones[j].Valor.ToUpper() == "TRUE" ? "verdadero" : "falso";
                                strBldrCodigoGarGarEntrada.AppendFormat(controladorPosArr, item.VariableMapeada, aux, item.Posiciones[j].Posicion).AppendLine();
                                break;
                            default:
                                break;
                        }
                    }

                }
                else
                {
                    switch (item.TipoDato.ToUpper())
                    {
                        case "TEXTO":
                            strBldrCodigoGarGarEntrada.AppendFormat(controladorVar, item.VariableMapeada, string.Format("{0}{1}{0}", "'", item.ValorEsperado)).AppendLine(); ;
                            break;
                        case "NUMERO":
                            strBldrCodigoGarGarEntrada.AppendFormat(controladorVar, item.VariableMapeada, item.ValorEsperado).AppendLine();
                            break;
                        case "BOOLEANO":
                            string aux = item.ValorEsperado.ToUpper() == "TRUE" ? "verdadero" : "falso";
                            strBldrCodigoGarGarEntrada.AppendFormat(controladorVar, item.VariableMapeada, aux).AppendLine();
                            break;
                        default:
                            break;
                    }
                }
            }

            return strBldrCodigoGarGarEntrada.ToString();
        }

        private string ArmarCodigoParteSalida(List<VariableTest> list)
        {
            StringBuilder strBldrCodigoGarGarSalida = new StringBuilder();


            string controladorValorVar = "si ({0} = {1}) entonces \r\n Mostrar('La variable {0} contenia el valor correcto: {2}'); \r\n sino \r\n MostrarP('La variable {0} debia contener el valor {2} pero contenia el valor ',{0}); \r\nfinsi;";
            string controladorValorPosArr = "si ({0}[{3}] = {1}) entonces \r\n Mostrar('La posicion {3} del arreglo {0} contenia el valor correcto: {2}'); \r\n sino \r\n MostrarP('La posicion {3} del arreglo {0} debia contener el valor {2} pero contenia el valor ',{0}[{3}]); \r\nfinsi;";

            foreach (var item in list)
            {
                if (item.EsArreglo)
                {
                    for (int j = 0; j < item.Posiciones.Count; j++)
                    {
                        switch (item.TipoDato.ToUpper())
                        {
                            case "TEXTO":
                                strBldrCodigoGarGarSalida.AppendFormat(controladorValorPosArr, item.VariableMapeada, string.Format("{0}{1}{0}", "'", item.Posiciones[j].Valor), string.Format("{0}{1}{0}", "'", item.Posiciones[j].Valor), item.Posiciones[j].Posicion).AppendLine(); ;
                                break;
                            case "NUMERO":
                                strBldrCodigoGarGarSalida.AppendFormat(controladorValorPosArr, item.VariableMapeada, item.Posiciones[j].Valor, item.Posiciones[j].Valor, item.Posiciones[j].Posicion).AppendLine();
                                break;
                            case "BOOLEANO":
                                string aux = item.Posiciones[j].Valor.ToUpper() == "TRUE" ? "verdadero" : "falso";
                                strBldrCodigoGarGarSalida.AppendFormat(controladorValorPosArr, item.VariableMapeada, aux, aux, item.Posiciones[j].Posicion).AppendLine();
                                break;
                            default:
                                break;
                        }
                    }

                }
                else
                {
                    switch (item.TipoDato.ToUpper())
                    {
                        case "TEXTO":
                            strBldrCodigoGarGarSalida.AppendFormat(controladorValorVar, item.VariableMapeada, string.Format("{0}{1}{0}", "'", item.ValorEsperado), string.Format("{0}{1}{0}", '"', item.ValorEsperado)).AppendLine(); ;
                            break;
                        case "NUMERO":
                            strBldrCodigoGarGarSalida.AppendFormat(controladorValorVar, item.VariableMapeada, item.ValorEsperado, item.ValorEsperado).AppendLine();
                            break;
                        case "BOOLEANO":
                            string aux = item.ValorEsperado.ToUpper() == "TRUE" ? "verdadero" : "falso";
                            strBldrCodigoGarGarSalida.AppendFormat(controladorValorVar, item.VariableMapeada, aux, aux).AppendLine();
                            break;
                        default:
                            break;
                    }
                }
            }

            testElegido.CodigoGarGarProcSalida = strBldrCodigoGarGarSalida.ToString();

            return strBldrCodigoGarGarSalida.ToString();
        }

       

        private void ComboBoxEntrada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = e.OriginalSource as ComboBox;
            Variable variableElegida = combo.SelectedItem as Variable;

            var bla = sender as FrameworkElement;
            if (bla == null)
                return;
            VariableTest variableOrig = bla.DataContext as VariableTest;

            variableOrig.VariableMapeada = variableElegida.Nombre;

            bool todosElegidos = true;            

            foreach (var item in testElegido.VariablesEntrada)
            {
                todosElegidos &= !string.IsNullOrWhiteSpace(item.VariableMapeada);
            }          


            this.wizard.CurrentPage.AllowNext = todosElegidos;
        }


        private void ComboBoxSalida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = e.OriginalSource as ComboBox;
            Variable variableElegida = combo.SelectedItem as Variable;

            var bla = sender as FrameworkElement;
            if (bla == null)
                return;
            VariableTest variableOrig = bla.DataContext as VariableTest;

            variableOrig.VariableMapeada = variableElegida.Nombre;

            bool todosElegidos = true;

            foreach (var item in testElegido.VariablesSalida)
            {
                todosElegidos &= !string.IsNullOrWhiteSpace(item.VariableMapeada);
            }


            this.wizard.CurrentPage.AllowNext = todosElegidos;
        }
    }


}
