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
using System.Reflection;

namespace Ragnarok
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
            for (int i = 0; i < 2000; i++)
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
            
            StartupUri = new System.Uri("RagnarokWindow.xaml", UriKind.Relative);

            
            InitializeComponent();

            //MessageListener.Instance.ReceiveMessage(string.Format("Cargando compilador..."));

            Run();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Global exception handling  
            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        //System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        //{
        //     String resourceName = "AssemblyLoadingAndReflection." +

        //     new AssemblyName(args.Name).Name + ".dll";

        //     using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        //     {
        //         Byte[] assemblyData = new Byte[stream.Length];

        //         stream.Read(assemblyData, 0, assemblyData.Length);

        //         return Assembly.Load(assemblyData);
        //     }
        //}

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
