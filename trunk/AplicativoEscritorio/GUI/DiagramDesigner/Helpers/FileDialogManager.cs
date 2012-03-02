using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace DiagramDesigner.Helpers
{
    public static class FileDialogManager
    {
        public static string ElegirUbicacionNuevoArchivo(Window padre, string titulo, string dirInicial)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.CheckFileExists = false;
            file.CheckPathExists = false;
            file.Multiselect = false;
            file.InitialDirectory = dirInicial;
            file.Title = titulo;

            file.ShowDialog(padre);

            return file.FileName;
            
        }

        internal static string ElegirArchivoParaAbrir(Window1 padre, string titulo, string dirInicial)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.CheckFileExists = true;
            file.CheckPathExists = true;
            file.Multiselect = false;
            file.InitialDirectory = dirInicial;
            file.Title = titulo;

            file.ShowDialog(padre);
            

            return file.FileName;
        }
    }
}
