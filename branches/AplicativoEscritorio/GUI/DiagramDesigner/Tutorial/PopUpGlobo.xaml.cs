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
using System.Windows.Media.Animation;

namespace Ragnarok.Tutorial
{
    // flanzani 11/11/2012
    // IDC_APP_5
    // Tutorial para la aplicacion
    // Clase que maneja los globos que se usan a lo largo de los tutoriales

    /// <summary>
    /// Interaction logic for PopUpGlobo.xaml
    /// </summary>
    public partial class PopUpGlobo : Window
    {
        double posXInicial;
        double posYInicial;

		// flanzani 14/11/2012
        // IDC_APP_7
        // Desactivar el tutorial desde el popup
        // Creo el handler y el evento para desactivar el tutorial

        public delegate void TutorialDesativadoEventHandler(object o, EventArgs e);
     
        public static event TutorialDesativadoEventHandler TutorialDesativadoEvent;

        public PopUpGlobo(string mensaje, bool continua, bool muestraDesactivarTutorial)
        {
            InitializeComponent();

            txbMessage.Inlines.Add(new Run(mensaje));
            if (continua)
            {                
                txbMessage.Inlines.Add(new LineBreak());
                txbMessage.Inlines.Add(new Run("Haga click para continuar"));
                
            }
            if (muestraDesactivarTutorial)
            {
                stckPnlCerrarTutorial.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                stckPnlCerrarTutorial.Visibility = System.Windows.Visibility.Collapsed;
            }            
        }

        public PopUpGlobo(string mensaje, bool continua) 
            : this(mensaje, continua, true)
        {

        }

        public void ColocarPosicion(Control controlDondeMostrar, Window ventanaPadre)
        {
            Point punto = controlDondeMostrar.TranslatePoint(new Point(0, 0), ventanaPadre);

            posXInicial = punto.X + ventanaPadre.Left;
            posYInicial = punto.Y + ventanaPadre.Top + controlDondeMostrar.Height - 2;

            Left = posXInicial;
            Top = posYInicial;
        }

        public void ColocarPosicion(Control controlDondeMostrar, Window ventanaPadre, Point puntoVentanaPadre)
        {
            Point punto = controlDondeMostrar.TranslatePoint(new Point(0, 0), ventanaPadre);

            posXInicial = punto.X + puntoVentanaPadre.X;
            posYInicial = punto.Y + puntoVentanaPadre.Y + controlDondeMostrar.Height - 2;

            Left = posXInicial;
            Top = posYInicial;
        }

        public void ColocarPosicion(Point punto)
        {
            posXInicial = punto.X;
            posYInicial = punto.Y;

            Left = posXInicial;
            Top = posYInicial;
        }

        public void ColocarPosicion(Point punto, Window ventanaPadre)
        {
            posXInicial = punto.X + ventanaPadre.Left;
            posYInicial = punto.Y + ventanaPadre.Top;

            Left = posXInicial;
            Top = posYInicial;
        }

        public void ColocarPosicion(Point punto, Point puntoVentanaPadre)
        {
            posXInicial = punto.X + puntoVentanaPadre.X;
            posYInicial = punto.Y + puntoVentanaPadre.Y;

            Left = posXInicial;
            Top = posYInicial;
        }

      

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void popup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= popup_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(1));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, anim);
        }


      	// flanzani 14/11/2012
        // IDC_APP_7
        // Desactivar el tutorial desde el popup
        // Manejo del boton y el evento de desactivar tutorial

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TutorialDesativadoEventFire(sender, new EventArgs());
        }

        private void TutorialDesativadoEventFire(object sender, EventArgs eventArgs)
        {
            if (TutorialDesativadoEvent != null)
            {
                TutorialDesativadoEvent(sender, eventArgs);
            }
        }
    }
}
