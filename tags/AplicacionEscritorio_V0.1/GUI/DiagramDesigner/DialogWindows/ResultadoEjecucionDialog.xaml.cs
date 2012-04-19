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
using DiagramDesigner.TestsPruebas;
using System.Xml;
using DataAccess.Entidades;

namespace DiagramDesigner.DialogWindows
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
            
           
        }

       
    }

   
}
