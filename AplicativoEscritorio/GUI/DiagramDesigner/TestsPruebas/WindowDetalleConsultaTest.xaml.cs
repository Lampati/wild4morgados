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
using System.Collections.ObjectModel;
using DataAccess.Entidades;
using AplicativoEscritorio.DataAccess.Entidades;

namespace Ragnarok.TestsPruebas
{
    /// <summary>
    /// Interaction logic for WindowDetalleConsultaTest.xaml
    /// </summary>
    public partial class WindowDetalleConsultaTest : Window
    {

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

        private TestPrueba testPrueba;
        public TestPrueba TestPrueba
        {
            get
            {
                return testPrueba;
            }

            set
            {
                testPrueba = value;

                ObservableCollection<Variable> auxListaEntrada = new ObservableCollection<Variable>();

                foreach (var item in testPrueba.VariablesEntrada)
                {
                    Variable variable = new Variable(item.Nombre, item.Contexto, item.TipoDato, string.Empty, item.EsArreglo, item.ValorEsperado, null);
                    variable.Posiciones = new List<PosicionArreglo>();

                    foreach (var pos in item.Posiciones)
                    {
                        variable.Posiciones.Add(new PosicionArreglo(pos.Posicion, pos.Valor));
                    }

                    variable.SetearDatosArregloPostCargadoPosiciones();

                    variable.EsValida = variablesEntrada.SingleOrDefault(x => x.Contexto == variable.Contexto
                                                                && x.Nombre == variable.Nombre
                                                                && x.EsArreglo == variable.EsArreglo
                                                                && (!x.EsArreglo || (x.EsArreglo && x.TopeArr == variable.TopeArr))
                                                                && x.TipoDato == variable.TipoDato) != null;
                  

                    auxListaEntrada.Add(variable);

                }

                ObservableCollection<Variable> auxListaSalida = new ObservableCollection<Variable>();

                foreach (var item in testPrueba.VariablesSalida)
                {
                    Variable variable = new Variable(item.Nombre, item.Contexto, item.TipoDato, string.Empty, item.EsArreglo, item.ValorEsperado, null);
                    variable.Posiciones = new List<PosicionArreglo>();

                    foreach (var pos in item.Posiciones)
                    {
                        variable.Posiciones.Add(new PosicionArreglo(pos.Posicion, pos.Valor));
                    }

                    variable.SetearDatosArregloPostCargadoPosiciones();

                    variable.EsValida = variablesSalida.SingleOrDefault(x => x.Contexto == variable.Contexto
                                                                && x.Nombre == variable.Nombre
                                                                && x.EsArreglo == variable.EsArreglo
                                                                && (!x.EsArreglo || (x.EsArreglo && x.TopeArr == variable.TopeArr))
                                                                && x.TipoDato == variable.TipoDato) != null;

                    auxListaSalida.Add(variable);

                }

                dataVarsEntrada.Variables = auxListaEntrada;
                dataVarsSalida.Variables = auxListaSalida;
            }
        }

        private void buttonAceptar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public WindowDetalleConsultaTest()
        {
            InitializeComponent();
        }
    }
}
