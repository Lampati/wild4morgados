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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using WebProgramAR.EntidadesDTO;
using DataAccess;

namespace Ragnarok.EjercicioBrowser
{
    /// <summary>
    /// Interaction logic for BrowserCursosUserControl.xaml
    /// </summary>
    /// 

    // flanzani 22/11/2012
    // IDC_APP_9
    // Repositorio de ejercicios
    // Creado del user control para la parte de filtro de ejercicios
    public partial class BrowserEjerciciosUserControl : BrowserUserControlBase
    {

        private int idDescarga = -1 ;

        private int? idActual = null;
        private string nombreActual = string.Empty;
        private string usuarioActual = string.Empty;
        private int? nivelActual = null;

        static List<EjercicioDTO> ejerciciosSincronizados;



        static EjercicioDetalleDTO ejercicioDetalleSincronizado;
        static bool resultadoSincro;

        private ObservableCollection<EjercicioDTO> listaDatos = new ObservableCollection<EjercicioDTO>();
        public ObservableCollection<EjercicioDTO> ListaDatos
        {
            get
            {
                return listaDatos;
            }

            set
            {
                listaDatos = value;

                lstCursos.ItemsSource = listaDatos;
                lstCursos.Items.Refresh();
            }
        }
    

        public BrowserEjerciciosUserControl()
            : base()
        {
            InitializeComponent();

            cboBxDificultad.SelectedIndex = 0;

        }

        protected override void BloquearPantalla(bool bloquear, bool escondiendoGrilla)
        {
            if (escondiendoGrilla)
            {
                if (bloquear)
                {
                    txtCargando.Visibility = System.Windows.Visibility.Visible;
                    lstCursos.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    txtCargando.Visibility = System.Windows.Visibility.Hidden;
                    lstCursos.Visibility = System.Windows.Visibility.Visible;
                }
            }

            lstCursos.IsEnabled = !bloquear;           
            bttnBuscar.IsEnabled = !bloquear;
            txtBxId.IsEnabled = !bloquear;
            txtBxNombre.IsEnabled = !bloquear;
            txtBxUsuario.IsEnabled = !bloquear;
            cboBxDificultad.IsEnabled = !bloquear;
        }

        protected override void TraerDatos()
        {
            ejerciciosSincronizados = Servicio.Ejercicios(idActual, nombreActual, usuarioActual,nivelActual);
        }

        protected override void CargarDatosEnPantalla()
        {
            ObservableCollection<EjercicioDTO> listaAux = new ObservableCollection<EjercicioDTO>(ejerciciosSincronizados);
            ListaDatos = listaAux;
        }
  
        private void ButtonDetalles_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)e.OriginalSource;

            string id = boton.DataContext.ToString();

            
            idDescarga = -1;
            if (!int.TryParse(id, out idDescarga))
            {
                idDescarga = -1;
            }

            if (idDescarga > 0)
            {              

                ComenzarConsultarDetalle();
            }
        }

        protected override void ConsultarDetalle()
        {
            ejercicioDetalleSincronizado = Servicio.EjercicioDetalle(idDescarga);

        }

        protected override void FinalizadaConsultarDetalle()
        {
            EjercicioDetallesWindow windowEj = new EjercicioDetallesWindow();
            windowEj.Ejercicio = ejercicioDetalleSincronizado;

            windowEj.ShowDialog();
        }
    
        private void ButtonDescargar_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)e.OriginalSource;

            string id = boton.DataContext.ToString();

            idDescarga = -1;
            if (!int.TryParse(id, out idDescarga))
            {
                idDescarga = -1;
            }

            if (idDescarga > 0)
            {
                ComenzarDescarga(string.Format("Comenzando la descarga del ejercicio número {0}, por favor espere...", idDescarga));
            }
        }     

        protected override void Descargar()
        {
            resultadoSincro = servicio.EjerciciosPorId(idDescarga);

        }

        protected override void FinalizadaDescarga()
        {
            if (resultadoSincro)
            {
                MensajesEstadoEventFire("¡Descarga completa!", true);
            }
            else
            {
                MensajesEstadoEventFire("El ejercicio que se intento descargar no existe más", false);
            }
        }

        private void bttnBuscar_Click(object sender, RoutedEventArgs e)
        {
            int x;
            idActual = null;
            if (!string.IsNullOrWhiteSpace(txtBxId.Text) && int.TryParse(txtBxId.Text, out x))
            {
                idActual = x;
            }
          

            nombreActual = txtBxNombre.Text;
            usuarioActual = txtBxUsuario.Text;

            nivelActual = null;
            ComboBoxItem typeItem = (ComboBoxItem)cboBxDificultad.SelectedItem;
            string value = typeItem.Content.ToString();
            if (int.TryParse(value, out x))
            {
                nivelActual = x;
            }

            Refrescar();
        }

        private void txtBxId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsNumber(Convert.ToChar(e.Text));

            base.OnPreviewTextInput(e);
        }
    }
}
