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

namespace Ragnarok.EjercicioBrowser
{
    /// <summary>
    /// Interaction logic for BrowserMainWindow.xaml
    /// </summary>
    

    // flanzani 22/11/2012
    // IDC_APP_9
    // Repositorio de ejercicios
    // Ventana principal de busqueda de ejercicios
    public partial class BrowserMainWindow : Window
    {
        public BrowserMainWindow()
        {
            InitializeComponent();

            browserCursos.ComienzoCargaDatosEvent += new BrowserCursosUserControl.CargandoDatosEventHandler(browserCursos_ComienzoCargaDatosEvent);
            browserCursos.FinalizadaCargaDatosEvent += new BrowserCursosUserControl.CargandoDatosEventHandler(browserCursos_FinalizadaCargaDatosEvent);

            browserCursos.MensajesEstadoEvent += new BrowserUserControlBase.MensajeEstadoEventHandler(browser_MensajesEstadoEvent);

            browserEjercicios.ComienzoCargaDatosEvent += new BrowserUserControlBase.CargandoDatosEventHandler(browserEjercicios_ComienzoCargaDatosEvent);
            browserEjercicios.FinalizadaCargaDatosEvent += new BrowserUserControlBase.CargandoDatosEventHandler(browserEjercicios_FinalizadaCargaDatosEvent);

            browserCursos.MensajesEstadoEvent += new BrowserUserControlBase.MensajeEstadoEventHandler(browser_MensajesEstadoEvent);
        }

        void browserCursos_ComienzoCargaDatosEvent(object sender, EventArgs e)
        {
            tabItemEjercicios.IsEnabled = false;
        }

        void browserCursos_FinalizadaCargaDatosEvent(object sender, EventArgs e)
        {
            tabItemEjercicios.IsEnabled = true;
        }

        void browserEjercicios_ComienzoCargaDatosEvent(object sender, EventArgs e)
        {
            tabItemCursos.IsEnabled = false;
        }

        void browserEjercicios_FinalizadaCargaDatosEvent(object sender, EventArgs e)
        {
            tabItemCursos.IsEnabled = true;
        }    

        void browser_MensajesEstadoEvent(object sender, EventArgsClasses.MensajeEstadoEventArgs e)
        {
            if (e.Resultado.HasValue)
            {
                if (e.Resultado.Value)
                {
                    imgEstadoActualCorrecta.Visibility = System.Windows.Visibility.Visible;
                    imgEstadoActualError.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    imgEstadoActualCorrecta.Visibility = System.Windows.Visibility.Collapsed;
                    imgEstadoActualError.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                imgEstadoActualCorrecta.Visibility = System.Windows.Visibility.Collapsed;
                imgEstadoActualError.Visibility = System.Windows.Visibility.Collapsed;
            }

            statusBarMensaje.Text = e.Mensaje;
        }

      

 
    }
}
