using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using BibliotecaMails.Procesamiento;

namespace WebProgramAR.ServicioMails
{
    public partial class Service1 : ServiceBase
    {
        private BandejaMails bandeja;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            bandeja = new BandejaMails();
            bandeja.Comenzar();
        }

        protected override void OnStop()
        {
            bandeja.Detener();
        }
    }
}
