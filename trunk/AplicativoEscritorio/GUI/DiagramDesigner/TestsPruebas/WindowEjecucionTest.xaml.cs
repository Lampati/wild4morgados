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

namespace DiagramDesigner.TestsPruebas
{
    /// <summary>
    /// Interaction logic for WindowCreacionTest.xaml
    /// </summary>
    public partial class WindowEjecucionTest : Window
    {
        private ArchResultado archResultadoEjecucuion;
        
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

    
    }


   
}
