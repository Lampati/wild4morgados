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
using Ragnarok.TestsPruebas;
using System.Xml;
using DataAccess.Entidades;

namespace Ragnarok.DialogWindows
{
    /// <summary>
    /// Interaction logic for ResultadoEjecucionDialog.xaml
    /// </summary>
    public partial class ResultadoEjecucionDialog : Window
    {
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

                dataVariables.Variables = variablesSalida;
                
            }
        }


        private ObservableCollection<ErrorEjecucion> errores;
        public ObservableCollection<ErrorEjecucion> Errores
        {
            get
            {
                return errores;
            }

            set
            {
                errores = value;


                dataGridErrores.ItemsSource = errores;
                dataGridErrores.Items.Refresh();
                //dataVariables.Variables = variablesSalida;

            }
        }

        // flanzani 22/11/2012
        // IDC_APP_8
        // Agregar el tiempo de ejecucion 
        // Agrego la propiedad para el tiempo
        public double TiempoEjecucion
        {
            set
            {
                txtBlckTiempoEjecucion.Text = string.Format("Tiempo de ejecución: {0} segundos",value.ToString("0.##"));
            }

        }

        private bool esCorrectaEjecucion;
        public bool EsCorrectaEjecucion
        {
            get { return esCorrectaEjecucion; }
            set 
            { 
                esCorrectaEjecucion = value;

                if (esCorrectaEjecucion)
                {
                    panelEjecucionIncorrecta.Visibility = System.Windows.Visibility.Collapsed;
                    panelEjecucionCorrecta.Visibility = System.Windows.Visibility.Visible;

                    panelErrores.Visibility = System.Windows.Visibility.Collapsed;
                    panelVariablesSalida.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    panelEjecucionIncorrecta.Visibility = System.Windows.Visibility.Visible;
                    panelEjecucionCorrecta.Visibility = System.Windows.Visibility.Collapsed;

                    panelErrores.Visibility = System.Windows.Visibility.Visible;
                    panelVariablesSalida.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

         bool hayErrores = false;

       

        public ResultadoEjecucionDialog()
        {
            InitializeComponent();

          
        }

        public ResultadoEjecucionDialog(ArchResultado archRes)
        {
            InitializeComponent();

            variablesSalida = new ObservableCollection<Variable>();
            errores = new ObservableCollection<ErrorEjecucion>();

            VariablesSalida = archRes.VariablesSalida;
            Errores = archRes.Errores;
            EsCorrectaEjecucion = archRes.EsCorrectaEjecucion;

            if (VariablesSalida.Count == 0)
            {
                dataVariables.Visibility = System.Windows.Visibility.Collapsed;
                txtBlockSinVariablesSalida.Visibility = System.Windows.Visibility.Visible;
            }

            if (Errores.Count == 0)
            {

                panelErrores.Visibility = System.Windows.Visibility.Collapsed;
            }


           
        }

        private void bttnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       
    }

   
}
