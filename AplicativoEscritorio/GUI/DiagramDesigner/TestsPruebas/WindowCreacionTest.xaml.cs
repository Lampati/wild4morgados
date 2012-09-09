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
using Utilidades;
using System.IO;

namespace Ragnarok.TestsPruebas
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

                Variable varConTodoCargado = total.Single(x => x.Nombre.ToUpper() == item.Nombre.ToUpper() && x.Contexto.ToUpper() == item.Contexto.ToUpper());

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
            this.TestGenerado = null;
            Close();
        }

        private void wizard_Finished(object sender, RoutedEventArgs e)
        {
            int i = 0;

            TestGenerado = new TestPrueba();

            TestGenerado.Nombre = Nombre;
            TestGenerado.Descripcion = Descripcion;



            foreach (var item in variablesEntradaSeleccionadas)
            {
                VariableTest varTest = new VariableTest()
                {
                    Descripcion = item.Descripcion,
                    Nombre = item.Nombre,
                    ValorEsperado = item.Valor,
                    EsArreglo = item.EsArreglo,
                    TipoDato = item.TipoDato.ToString(),
                    Contexto = item.Contexto
                };

                foreach (var pos in item.Posiciones)
                {
                    varTest.Posiciones.Add(new PosicionVariableTest() { Posicion = pos.Posicion, Valor = pos.Valor });
                }
                
                TestGenerado.VariablesEntrada.Add(varTest);
            }

            StringBuilder strBldrCodigoGarGarSalida = new StringBuilder();
            string controladorVar = "si ({0} = {1}) entonces \r\n Mostrar('La variable {0} contenia el valor correcto: {2}'); \r\n sino \r\n MostrarP('La variable {0} debia contener el valor {2} pero contenia el valor ',{0}); \r\nfinsi;";
            string controladorPosArr = "si ({0}[{3}] = {1}) entonces \r\n Mostrar('La posicion {3} del arreglo {0} contenia el valor correcto: {2}'); \r\n sino \r\n MostrarP('La posicion {3} del arreglo {0} debia contener el valor {2} pero contenia el valor ',{0}); \r\nfinsi;";

            foreach (var item in variablesSalidaSeleccionadas)
            {
                VariableTest varTest = new VariableTest()
                {
                    Descripcion = item.Descripcion,
                    Nombre = item.Nombre,
                    ValorEsperado = item.Valor,
                    EsArreglo = item.EsArreglo,
                    TipoDato = item.TipoDato.ToString(),
                    Contexto = item.Contexto
                };

                foreach (var pos in item.Posiciones)
                {
                    varTest.Posiciones.Add(new PosicionVariableTest() { Posicion = pos.Posicion, Valor = pos.Valor });
                }

                TestGenerado.VariablesSalida.Add(varTest);

                if (item.EsArreglo)
                {
                    for (int j = 0; j < item.Posiciones.Count; j++)
                    {
                        switch (item.TipoDato)
                        {
                            case AplicativoEscritorio.DataAccess.Enums.TipoDato.Texto:
                                strBldrCodigoGarGarSalida.AppendFormat(controladorPosArr, item.Nombre, string.Format("{0}{1}{0}", "'", item.Posiciones[j].Valor), string.Format("{0}{1}{0}",'"', item.Posiciones[j].Valor), item.Posiciones[j].Posicion).AppendLine(); ;
                                break;
                            case AplicativoEscritorio.DataAccess.Enums.TipoDato.Numero:
                                strBldrCodigoGarGarSalida.AppendFormat(controladorPosArr, item.Nombre, item.Posiciones[j].Valor, item.Posiciones[j].Valor, item.Posiciones[j].Posicion).AppendLine();
                                break;
                            case AplicativoEscritorio.DataAccess.Enums.TipoDato.Booleano:
                                string aux = item.Posiciones[j].Valor.ToUpper() == "TRUE" ? "verdadero" : "falso";
                                strBldrCodigoGarGarSalida.AppendFormat(controladorPosArr, item.Nombre, aux, aux, item.Posiciones[j].Posicion).AppendLine(); 
                                break;
                            default:
                                break;
                        }
                    }
                    
                }
                else
                {
                    switch (item.TipoDato)
                    {
                        case AplicativoEscritorio.DataAccess.Enums.TipoDato.Texto:
                            strBldrCodigoGarGarSalida.AppendFormat(controladorVar, item.Nombre, string.Format("{0}{1}{0}", "'", item.Valor), string.Format("{0}{1}{0}",'"', item.Valor)).AppendLine();; 
                            break;
                        case AplicativoEscritorio.DataAccess.Enums.TipoDato.Numero:
                            strBldrCodigoGarGarSalida.AppendFormat(controladorVar, item.Nombre, item.Valor, item.Valor).AppendLine();
                            break;
                        case AplicativoEscritorio.DataAccess.Enums.TipoDato.Booleano:
                            string aux = item.Valor.ToUpper() == "TRUE" ? "verdadero" : "falso";
                            strBldrCodigoGarGarSalida.AppendFormat(controladorVar, item.Nombre, aux, aux ).AppendLine();
                            break;                        
                        default:
                            break;
                    }
                }
            }

            TestGenerado.CodigoGarGarProcSalida = strBldrCodigoGarGarSalida.ToString();

            TestGenerado.Id = RandomManager.RandomStringConPrefijo(40, true);


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

                    try
                    {
                        File.Delete(res.ArchTemporalResultadosEjecucionConRuta);
                    }
                    catch
                    {

                    }
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
