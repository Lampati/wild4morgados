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

namespace Ragnarok.EjercicioBrowser
{
    /// <summary>
    /// Interaction logic for CursoDetallesWindow.xaml
    /// </summary>
    /// 

    // flanzani 22/11/2012
    // IDC_APP_9
    // Repositorio de ejercicios
    // Creado de ventana para detalle de cursos
    public partial class CursoDetallesWindow : Window
    {
        private readonly BackgroundWorker workerSincronizacion = new BackgroundWorker();
        private readonly BackgroundWorker workerDetalles = new BackgroundWorker();

        private int idDescarga;
        private bool descargarEsteCurso;

        static EjercicioDetalleDTO ejercicioDetalleSincronizado;
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

        private CursoDetalleDTO curso;
        public CursoDetalleDTO Curso
        {
            get
            {
                return curso;
                
            }
            set
            {
                curso = value;

                lblIdContenido.Content = curso.CursoId;
                lblNombreContenido.Content = curso.Nombre;
                lblCreadorContenido.Content = curso.Creador;
           

                if (curso.Ejercicios.Count > 0)
                {
                    if (curso.LoTieneLocal)
                    {
                        lblCursoYaDescargado.Visibility = System.Windows.Visibility.Visible;
                        bttnDescargar.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    else
                    {
                        lblCursoYaDescargado.Visibility = System.Windows.Visibility.Collapsed;
                        bttnDescargar.Visibility = System.Windows.Visibility.Visible;
                    }
                }
                else
                {
                    lblCursoYaDescargado.Visibility = System.Windows.Visibility.Collapsed;
                    bttnDescargar.Visibility = System.Windows.Visibility.Collapsed;
                }

                lstEjercicios.ItemsSource = curso.Ejercicios;
                lstEjercicios.Items.Refresh();
            }

        }

        public CursoDetallesWindow()
        {
            InitializeComponent();

            workerSincronizacion.DoWork += new DoWorkEventHandler(workerSincronizacion_DoWork);
            workerSincronizacion.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerSincronizacion_RunWorkerCompleted);

            workerDetalles.DoWork += new DoWorkEventHandler(workerDetalles_DoWork);
            workerDetalles.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerDetalles_RunWorkerCompleted);

        }


        void workerDetalles_DoWork(object sender, DoWorkEventArgs e)
        {
            ConsultarDetalle();
        }

        private void ConsultarDetalle()
        {
            ejercicioDetalleSincronizado = Servicio.EjercicioDetalle(idDescarga);
        }

        void workerDetalles_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FinalizadaConsultarDetalle();

            lstEjercicios.IsEnabled = true;
            bttnDescargar.IsEnabled = true;
            
        }

        private void FinalizadaConsultarDetalle()
        {

            EjercicioDetallesWindow windowEj = new EjercicioDetallesWindow();
            windowEj.Ejercicio = ejercicioDetalleSincronizado;

            windowEj.ShowDialog();
        }

        void workerSincronizacion_DoWork(object sender, DoWorkEventArgs e)
        {
            Descargar();
        }

        private void Descargar()
        {
            if (descargarEsteCurso)
            {
                resultadoSincro = Servicio.EjerciciosPorCurso(curso.CursoId);
            }
            else
            {
                resultadoSincro = Servicio.EjerciciosPorId(idDescarga);
            }
            
        }

        void workerSincronizacion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            lstEjercicios.IsEnabled = true;
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

                        if (descargarEsteCurso)
                        {
                            
                            statusBarMensaje.Text = "El curso que se intento descargar no existe más";
                        }
                        else
                        {
                            statusBarMensaje.Text = "El ejercicio que se intento descargar no existe más";
                        }
                    }
                }
            }
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

        private void ComenzarConsultarDetalle()
        {
            lstEjercicios.IsEnabled = false;
            bttnDescargar.IsEnabled = false;

            workerDetalles.RunWorkerAsync();
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
                descargarEsteCurso = false;
                ComenzarDescarga(string.Format("Comenzando la descarga del ejercicio número {0}, por favor espere...", idDescarga));
            }
        }

        private void ComenzarDescarga(string mens)
        {
            lstEjercicios.IsEnabled = false;
            bttnDescargar.IsEnabled = false;

            imgEstadoActualCorrecta.Visibility = System.Windows.Visibility.Collapsed;
            imgEstadoActualError.Visibility = System.Windows.Visibility.Collapsed;

            statusBarMensaje.Text = mens;

            workerSincronizacion.RunWorkerAsync();
        }

        private void bttnDescargar_Click(object sender, RoutedEventArgs e)
        {
            descargarEsteCurso = true;

            ComenzarDescarga(string.Format("Comenzando la descarga del curso número {0}, por favor espere...", curso.CursoId));
        }
    }
}
