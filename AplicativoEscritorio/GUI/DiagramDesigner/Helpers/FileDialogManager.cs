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
        internal static string ElegirUbicacionNuevoEjercicio(Window padre, string titulo, string dirInicial)
        {
            return ElegirUbicacionNuevoArchivo(padre, titulo, dirInicial, Globales.ConstantesGlobales.EXTENSION_EJERCICIO,
                string.Format("Archivos de Ejercicio (*.{0})|*.{0}|Todos los archivos (*.*)|*.*", Globales.ConstantesGlobales.EXTENSION_EJERCICIO));
        }

        internal static string ElegirUbicacionNuevaResolucion(Window padre, string titulo, string dirInicial)
        {
            return ElegirUbicacionNuevoArchivo(padre, titulo, dirInicial, Globales.ConstantesGlobales.EXTENSION_RESOLUCION,
                string.Format("Archivos de Resolución (*.{0})|*.{0}|Todos los archivos (*.*)|*.*", Globales.ConstantesGlobales.EXTENSION_RESOLUCION));
        }


        private static string ElegirUbicacionNuevoArchivo(Window padre, string titulo, string dirInicial, string filtro, string descFiltros)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.CheckFileExists = false;
            file.CheckPathExists = false;
            file.Multiselect = false;
            file.InitialDirectory = dirInicial;
            file.Title = titulo;
            file.Filter = descFiltros;
            file.DefaultExt = string.Format(".{0}",filtro);
            file.AddExtension = true;

            file.ShowDialog(padre);

            return file.FileName;
            
        }


        internal static string ElegirEjercicioParaResolucion(Window padre, string titulo, string dirInicial)
        {
            return ElegirArchivo (padre, titulo, dirInicial, 
                string.Format("Archivos de Ejercicio (*.{0})|*.{0}|Todos los archivos (*.*)|*.*",
                Globales.ConstantesGlobales.EXTENSION_EJERCICIO)
                );
        }

        internal static string ElegirArchivoParaAbrir(Window padre, string titulo, string dirInicial)
        {
            return ElegirArchivo (padre, titulo, dirInicial,
                string.Format("Archivos de Ragnarok (*.{1} o *.{0})|*.{1};*.{0}|Archivos de Resolución (*.{1})|*.{1}|Archivos de Ejercicio (*.{0})|*.{0}|Todos los archivos (*.*)|*.*",
                Globales.ConstantesGlobales.EXTENSION_EJERCICIO,
                Globales.ConstantesGlobales.EXTENSION_RESOLUCION)
                );
        }

        internal static string ElegirArchivo(Window padre, string titulo, string dirInicial, string filtros)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.CheckFileExists = true;
            file.CheckPathExists = true;
            file.Multiselect = false;
            file.InitialDirectory = dirInicial;
            file.Title = titulo;
            file.Filter = filtros;

            file.ShowDialog(padre);
            

            return file.FileName;
        } 
    }
}
