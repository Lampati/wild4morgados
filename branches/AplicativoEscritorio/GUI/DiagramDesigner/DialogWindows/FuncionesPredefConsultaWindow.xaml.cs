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
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using Ragnarok.DialogWindows.FuncionesPredef;
using Ragnarok.ModoTexto.Configuracion;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Ragnarok.DialogWindows
{
    // flanzani 11/11/2012
    // IDC_APP_4
    // Mostrar las funciones predefinidas por el framework
    // Ventana que muestra de un xml las funciones del framework
    public partial class FuncionesPredefConsultaWindow : Window
    {
        public ObservableCollection<RutinaPredefData> ListaRutinas = new ObservableCollection<RutinaPredefData>();

        public FuncionesPredefConsultaWindow()
        {
            InitializeComponent();

            lblEncabezado.Visibility = System.Windows.Visibility.Hidden;
            lblTipo.Visibility = System.Windows.Visibility.Hidden;
            lblTipoContenido.Visibility = System.Windows.Visibility.Hidden;
            lblDescripcion.Visibility = System.Windows.Visibility.Hidden;
            txtDescripcionContenido.Visibility = System.Windows.Visibility.Hidden;
            lblParametros.Visibility = System.Windows.Visibility.Hidden;
            lstBxParametros.Visibility = System.Windows.Visibility.Hidden;

            separadorHoriz.Visibility = System.Windows.Visibility.Hidden;

            lblDescripcionParam.Visibility = System.Windows.Visibility.Hidden;
            txtDescripcionParamContenido.Visibility = System.Windows.Visibility.Hidden;

            lblUso.Visibility = System.Windows.Visibility.Hidden;
            txtEjemplo.Visibility = System.Windows.Visibility.Hidden;

                     


            this.DataContext = this;

      

            ModoTextoConfiguracion.ConfigurarAvalonEdit(txtEjemplo);


            //using (var stream = new MemoryStream())
            //using (var writer = XmlWriter.Create(stream))
            //{
            //    new XmlSerializer(ListaRutinas.GetType()).Serialize(writer, ListaRutinas);
            //    var xmlEncodedList = Encoding.UTF8.GetString(stream.ToArray());
            //}

            CargarListaRutinas();
        }

        private void CargarListaRutinas()
        {
            try
            {
                var settings = new XmlReaderSettings();
                RutinaPredefData[] obj;
                using (var reader = XmlReader.Create(System.IO.Path.Combine(Globales.ConstantesGlobales.PathEjecucionAplicacion,
                                             Globales.ConstantesGlobales.NOMBRE_ARCH_RUTINASPREDEF_APLICACION), settings))
                {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(RutinaPredefData[]));
                    obj = (RutinaPredefData[])serializer.Deserialize(reader);

                    reader.Close();
                }
                ListaRutinas = new ObservableCollection<RutinaPredefData>(obj);
                lstBxRutinas.ItemsSource = ListaRutinas;
                lstBxRutinas.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Error al cargar el archivo de configuracion de funciones predefinidas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }                      
        }

        public void CargarDefaults()
        {
            RutinaPredefData rut;


            rut = new RutinaPredefData()
            {
                Descripcion = "Función que chequea si la parte entera de un numero es impar",
                Ejemplo = "x = ESIMPAR(3); {Esto da verdadero}\r\nx = ESIMPAR(4.15); {Esto da falso}\r\nx = ESIMPAR(17.14); {Esto da verdadero}",
                Nombre = "ESIMPAR",
                Tipo = "Booleano",
                EsFunc = true
            };
            rut.ListaParams.Add(new ParametroPredefData() { Descripcion = "Número con o sin decimales a chequear si es impar", Texto = "num : numero" });
            ListaRutinas.Add(rut);

            rut = new RutinaPredefData()
            {
                Descripcion = "Función que chequea si la parte entera de un numero es par",
                Ejemplo = "x = ESPAR(2); {Esto da verdadero}\r\nx = ESPAR(3.14); {Esto da falso}\r\nx = ESPAR(4.14); {Esto da verdadero}",
                Nombre = "ESPAR",
                Tipo = "Booleano",
                EsFunc = true
            };
            rut.ListaParams.Add(new ParametroPredefData() { Descripcion = "Número con o sin decimales a chequear si es par", Texto = "num : numero" });
            ListaRutinas.Add(rut);

            rut = new RutinaPredefData()
            {
                Descripcion = "Función que permite realizar calculos de potencia sobre una base y un exponente que se pasan por parametro",
                Ejemplo = "x = POTENCIA(2, 8 ); {Esto da 64}",
                Nombre = "POTENCIA",
                Tipo = "Numero",
                EsFunc = true
            };
            rut.ListaParams.Add(new ParametroPredefData() { Descripcion = "Número base sobre el cual se hace la potencia", Texto = "base : numero" });
            rut.ListaParams.Add(new ParametroPredefData() { Descripcion = "Número exponente el que se eleva la base", Texto = "exp : numero" });
            ListaRutinas.Add(rut);

            rut = new RutinaPredefData()
            {
                Descripcion = "Función que permite realizar calculos de raiz sobre una base y un exponente que se pasan por parametro",
                Ejemplo = "x = RAIZ(9, 2 ); {Esto da 3}",
                Nombre = "RAIZ",
                Tipo = "Numero",
                EsFunc = true

            };
            rut.ListaParams.Add(new ParametroPredefData() { Descripcion = "Número base sobre el cual se hace la raiz", Texto = "base : numero" });
            rut.ListaParams.Add(new ParametroPredefData() { Descripcion = "Número exponente sobre el que se realiza la raiz", Texto = "raiz : numero" });
            ListaRutinas.Add(rut);

            rut = new RutinaPredefData()
            {
                Descripcion = "Función que redondea el numero pasado por parametro",
                Ejemplo = "x = REDONDEAR(3); {Esto devuelve 3}\r\nx = REDONDEAR(4.15); {Esto devuelve 4}\r\nx = TRUNCAR(17.74); {Esto devuelve 18}",
                Nombre = "REDONDEAR",
                Tipo = "Numero",
                EsFunc = true
            };
            rut.ListaParams.Add(new ParametroPredefData() { Descripcion = "Número con o sin decimales a redondear", Texto = "num : numero" });
            ListaRutinas.Add(rut);

            rut = new RutinaPredefData()
            {
                Descripcion = "Función que trunca la parte decimal de un numero, si esta tuviere",
                Ejemplo = "x = TRUNCAR(3); {Esto devuelve 3}\r\nx = TRUNCAR(4.15); {Esto devuelve 4}\r\nx = TRUNCAR(17.14); {Esto devuelve 17}",
                Nombre = "TRUNCAR",
                Tipo = "Booleano",
                EsFunc = true
            };
            rut.ListaParams.Add(new ParametroPredefData() { Descripcion = "Número con o sin decimales a truncar", Texto = "num : numero" });
            ListaRutinas.Add(rut);

            lstBxRutinas.ItemsSource = ListaRutinas;
            lstBxRutinas.Items.Refresh();
        }

        private void bttnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lstBxRutinas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RutinaPredefData rutina = e.AddedItems[0] as RutinaPredefData;

            txtEjemplo.Text = rutina.Ejemplo;

            lblEncabezado.Visibility = System.Windows.Visibility.Visible;
            lblTipo.Visibility = System.Windows.Visibility.Visible;
            lblTipoContenido.Visibility = System.Windows.Visibility.Visible;
            lblDescripcion.Visibility = System.Windows.Visibility.Visible;
            txtDescripcionContenido.Visibility = System.Windows.Visibility.Visible;
            lblParametros.Visibility = System.Windows.Visibility.Visible;
            lstBxParametros.Visibility = System.Windows.Visibility.Visible;

            separadorHoriz.Visibility = System.Windows.Visibility.Visible;

            lblUso.Visibility = System.Windows.Visibility.Visible;
            txtEjemplo.Visibility = System.Windows.Visibility.Visible;

            if (rutina.EsFunc)
            {
                lblTipo.Visibility = System.Windows.Visibility.Visible;
                lblTipoContenido.Visibility = System.Windows.Visibility.Visible;

                lblEncabezado.Content = string.Format("Función {0}", rutina.Nombre);
            }
            else
            {
                lblTipo.Visibility = System.Windows.Visibility.Collapsed;
                lblTipoContenido.Visibility = System.Windows.Visibility.Collapsed;

                lblEncabezado.Content = string.Format("Procedimiento {0}", rutina.Nombre);
            }

            lblDescripcionParam.Visibility = System.Windows.Visibility.Hidden;
            txtDescripcionParamContenido.Visibility = System.Windows.Visibility.Hidden;
        
            
        }

        private void lstBxParametros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ParametroPredefData param = e.AddedItems[0] as ParametroPredefData;

                lblDescripcionParam.Visibility = System.Windows.Visibility.Visible;
                txtDescripcionParamContenido.Visibility = System.Windows.Visibility.Visible;
            }
        }

    }
}
