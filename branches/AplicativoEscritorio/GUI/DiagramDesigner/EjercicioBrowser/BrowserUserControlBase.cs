using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DataAccess;
using System.ComponentModel;
using Ragnarok.EjercicioBrowser.EventArgsClasses;

namespace Ragnarok.EjercicioBrowser
{
    // flanzani 22/11/2012
    // IDC_APP_9
    // Repositorio de ejercicios
    // Clase padre para los UserControl de los browsers
    public abstract class BrowserUserControlBase : UserControl
    {
        private readonly BackgroundWorker workerRefrescar = new BackgroundWorker();
        private readonly BackgroundWorker workerSincronizacion = new BackgroundWorker();
        private readonly BackgroundWorker workerDetalles = new BackgroundWorker();


        public delegate void CargandoDatosEventHandler(object sender, EventArgs e);
        public event CargandoDatosEventHandler ComienzoCargaDatosEvent;
        public event CargandoDatosEventHandler FinalizadaCargaDatosEvent;

        public delegate void MensajeEstadoEventHandler(object sender, MensajeEstadoEventArgs e);
        public event MensajeEstadoEventHandler MensajesEstadoEvent;

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

        public BrowserUserControlBase()
        {
            workerRefrescar.DoWork += new DoWorkEventHandler(worker_DoWork);
            workerRefrescar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            workerSincronizacion.DoWork += new DoWorkEventHandler(workerSincronizacion_DoWork);
            workerSincronizacion.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerSincronizacion_RunWorkerCompleted);

            workerDetalles.DoWork += new DoWorkEventHandler(workerDetalles_DoWork);
            workerDetalles.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerDetalles_RunWorkerCompleted);
        }

    

        void workerDetalles_DoWork(object sender, DoWorkEventArgs e)
        {
            ConsultarDetalle();
        }

        void workerDetalles_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                FinalizadaConsultarDetalle();

                BloquearPantalla(false, true);

                FinalizadaCargaDatosEventFire();
            }
        }

        void workerSincronizacion_DoWork(object sender, DoWorkEventArgs e)
        {
            Descargar();
        }

        void workerSincronizacion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FinalizadaCargaDatosEventFire();

            if (!e.Cancelled)
            {
                BloquearPantalla(false, true);

                if (e.Cancelled)
                {
                    MensajesEstadoEventFire("Descarga fallida", false);
                }
                else
                {
                    if (e.Error != null)
                    {
                        MensajesEstadoEventFire("Ocurrio un error con la descarga. Por favor reintente mas tarde", false);
                    }
                    else
                    {
                        FinalizadaDescarga();
                        
                    }
                }

                
            }
        }




        public void ComienzoCargaDatosEventFire()
        {
            if (ComienzoCargaDatosEvent != null)
            {
                ComienzoCargaDatosEvent(this, new EventArgs());
            }
        }



        public void FinalizadaCargaDatosEventFire()
        {
            if (FinalizadaCargaDatosEvent != null)
            {
                FinalizadaCargaDatosEvent(this, new EventArgs());
            }
        }

        public void MensajesEstadoEventFire(string mensaje, bool? resultado)
        {
            if (MensajesEstadoEvent != null)
            {
                MensajesEstadoEvent(this, new MensajeEstadoEventArgs() { Mensaje = mensaje , Resultado= resultado} );
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // run all background tasks here
            //llenar el objeto de la sincronizacion

            TraerDatos();
        }

        private void worker_RunWorkerCompleted(object sender,
                                             RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                CargarDatosEnPantalla();

                BloquearPantalla(false, true);

                FinalizadaCargaDatosEventFire();
            }
        }

        protected void Refrescar()
        {
            ComienzoCargaDatosEventFire();

            BloquearPantalla(true,true);

            workerRefrescar.RunWorkerAsync();

        }

        protected void ComenzarConsultarDetalle()
        {
            ComienzoCargaDatosEventFire();

            BloquearPantalla(true, false);

            workerDetalles.RunWorkerAsync();
        }

        protected void ComenzarDescarga(string mensajeEstado)
        {
            ComienzoCargaDatosEventFire();
            BloquearPantalla(true, false);

            MensajesEstadoEventFire(mensajeEstado,null);
            workerSincronizacion.RunWorkerAsync();
        }


        protected abstract void BloquearPantalla(bool bloquear, bool esconderGrilla);

        protected abstract void CargarDatosEnPantalla();

        protected abstract void TraerDatos();

        protected abstract void Descargar();

        protected abstract void FinalizadaDescarga();

        protected abstract void ConsultarDetalle();

        protected abstract void FinalizadaConsultarDetalle();
    }
}
