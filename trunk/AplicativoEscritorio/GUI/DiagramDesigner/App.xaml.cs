using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using ModoGrafico.Views;

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Global exception handling  
            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);
        }

        void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
#if DEBUG   // In debug mode do not custom-handle the exception, let Visual Studio handle it

            e.Handled = false;

#else

            ShowUnhandeledException(e);    

#endif
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
