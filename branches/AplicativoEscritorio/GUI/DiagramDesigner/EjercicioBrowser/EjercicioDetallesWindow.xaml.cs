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
using WebProgramAR.EntidadesDTO;
using System.ComponentModel;
using DataAccess;
using Ragnarok.ModoTexto.Configuracion;

namespace Ragnarok.EjercicioBrowser
{
    /// <summary>
    /// Interaction logic for CursoDetallesWindow.xaml
    /// </summary>

    // flanzani 22/11/2012
    // IDC_APP_9
    // Repositorio de ejercicios
    // Creado de ventana para detalle de ejercicios
    public partial class EjercicioDetallesWindow : Window
    {
        private readonly BackgroundWorker workerSincronizacion = new BackgroundWorker();

        private int idDescarga;

        static bool resultadoSincro;


        protected Sincronizacion.Servicio servicio;
        protected Sincronizacion.Servicio Servicio
        {
            get
            {
                if (Object.Equals(this.servicio, null))
                    this.servicio = new Sincronizacion.Servicio();

                List<string> wsdls = null;
                if (!Object.Equals(ConfiguracionAplicacion.UrlsDescargaEjercicios, null))
                    wsdls = new List<string>(ConfiguracionAplicacion.UrlsDescargaEjercicios.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
                this.servicio.Urls = wsdls;
                this.servicio.Directorio = ConfiguracionAplicacion.DirectorioEjerciciosDescargados;

                return this.servicio;
            }
        }

        private EjercicioDetalleDTO ejercicio;
        public EjercicioDetalleDTO Ejercicio
        {
            get
            {
                return ejercicio;
            }
            set
            {
                ejercicio = value;

                lblIdContenido.Content = ejercicio.EjercicioId;
                lblNombreContenido.Content = ejercicio.Nombre;
                lblCreadorContenido.Content = ejercicio.Usuario;
                lblFechaContenido.Content = ejercicio.FechaAlta.ToString("dd/MM/yyyy");
                lblGlobalContenido.Content = ejercicio.Global ? "Si" : "No";
                lblDificultadContenido.Content = ejercicio.NivelDificultad.ToString();

                txtBxEnunciado.Text = ejercicio.Enunciado;
                txtBxSolucionExplicada.Text = ejercicio.SolucionTexto;
                txtBxSolucionGarGar.Text = ejercicio.SolucionGarGar;

                if (!ejercicio.LoTieneLocal)
                {
                    bttnDescargar.Visibility = System.Windows.Visibility.Visible;
                    lblEjercicioYaDescargado.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    bttnDescargar.Visibility = System.Windows.Visibility.Collapsed;
                    lblEjercicioYaDescargado.Visibility = System.Windows.Visibility.Visible;
                }
            }

        }

        public EjercicioDetallesWindow()
        {
            InitializeComponent();

            workerSincronizacion.DoWork += new DoWorkEventHandler(workerSincronizacion_DoWork);
            workerSincronizacion.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerSincronizacion_RunWorkerCompleted);

            ModoTextoConfiguracion.ConfigurarAvalonEdit(txtBxSolucionGarGar);

        }


        void workerSincronizacion_DoWork(object sender, DoWorkEventArgs e)
        {
            Descargar();
        }

        private void Descargar()
        {           
            resultadoSincro = Servicio.EjerciciosPorId(ejercicio.EjercicioId);
        }

        void workerSincronizacion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bttnDescargar.IsEnabled = true;

            if (e.Cancelled)
            {
                imgEstadoActualCorrecta.Visibility = System.Windows.Visibility.Collapsed;
                imgEstadoActualError.Visibility = System.Windows.Visibility.Visible;

                statusBarMensaje.Text = "Descarga fallida";
                
            }
            else
            {
                if (e.Error != null)
                {
                    imgEstadoActualCorrecta.Visibility = System.Windows.Visibility.Collapsed;
                    imgEstadoActualError.Visibility = System.Windows.Visibility.Visible;

                    statusBarMensaje.Text = "Ocurrio un error con la descarga. Por favor reintente mas tarde";
                }
                else
                {
                    if (resultadoSincro)
                    {
                        imgEstadoActualCorrecta.Visibility = System.Windows.Visibility.Visible;
                        imgEstadoActualError.Visibility = System.Windows.Visibility.Collapsed;

                        statusBarMensaje.Text = "¡Descarga completa!";
                    }
                    else
                    {
                        
                        imgEstadoActualCorrecta.Visibility = System.Windows.Visibility.Collapsed;
                        imgEstadoActualError.Visibility = System.Windows.Visibility.Visible;

                        statusBarMensaje.Text = "El ejercicio que se intento descargar no existe más";
                    }
                }
            }
        }

        private void ComenzarDescarga(string mens)
        {
            bttnDescargar.IsEnabled = false;

            imgEstadoActualCorrecta.Visibility = System.Windows.Visibility.Collapsed;
            imgEstadoActualError.Visibility = System.Windows.Visibility.Collapsed;

            statusBarMensaje.Text = mens;

            workerSincronizacion.RunWorkerAsync();
        }

        private void bttnDescargar_Click(object sender, RoutedEventArgs e)
        {
            ComenzarDescarga(string.Format("Comenzando la descarga del ejercicio número {0}, por favor espere...", ejercicio.EjercicioId));
        }
    }
}
