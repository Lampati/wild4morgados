using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using ModoGrafico.Views;
using System.Diagnostics;
using SplashScreen;
using System.Threading;

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread()]
        static void Main()
        {
            Splasher.Splash = new SplashScreen.SplashScreen();
            Splasher.ShowSplash();

            //MessageListener.Instance.ReceiveMessage(string.Format("Cargando interfaz de usuario..."));

            for (int i = 0; i < 1000; i++)
            {
                MessageListener.Instance.ReceiveMessage(string.Format("Cargando Ragnarok, por favor espere..."));
                Thread.Sleep(1);
            }

            new App();
        }
        /// <summary>
        /// 
        /// </summary>
        public App()
        {
            
            StartupUri = new System.Uri("Window1.xaml", UriKind.Relative);

            
            InitializeComponent();

            //MessageListener.Instance.ReceiveMessage(string.Format("Cargando compilador..."));

            Run();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Global exception handling  
            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);
        }

        void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            int i = 0;

            if (Debugger.IsAttached)   // In debug mode do not custom-handle the exception, let Visual Studio handle it
            {
                e.Handled = false;
            }
            else
            {
                ShowUnhandeledException(e);
            }
        }

        void ShowUnhandeledException(DispatcherUnhandledExceptionEventArgs e)
        {

            ErrorWindow errorWindow = new ErrorWindow();
            errorWindow.ErrorDetalles = e.Exception.Message;
            errorWindow.ShowActivated = true;
            errorWindow.ShowInTaskbar = false;
            errorWindow.ShowDialog();

            Application.Current.Shutdown();

            // Prevent default unhandled exception processing
            e.Handled = true;
        }

    }
     
    
}
