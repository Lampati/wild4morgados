﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;


namespace Ragnarok.Helpers
{
    public static class FileDialogManager
    {
        internal static string ElegirUbicacionNuevoEjercicio(Window padre, string titulo, string dirInicial)
        {
            return ElegirUbicacionNuevoArchivo(padre, titulo, dirInicial, AplicativoEscritorio.DataAccess.Entidades.Ejercicio.EXTENSION_EJERCICIO,
                string.Format("Archivos de Ejercicio (*.{0})|*.{0}|Todos los archivos (*.*)|*.*", AplicativoEscritorio.DataAccess.Entidades.Ejercicio.EXTENSION_EJERCICIO));
        }

        internal static string ElegirUbicacionNuevaResolucion(Window padre, string titulo, string dirInicial)
        {
            return ElegirUbicacionNuevoArchivo(padre, titulo, dirInicial, AplicativoEscritorio.DataAccess.Entidades.ResolucionEjercicio.EXTENSION_RESOLUCION,
                string.Format("Archivos de Resolución (*.{0})|*.{0}|Todos los archivos (*.*)|*.*", AplicativoEscritorio.DataAccess.Entidades.ResolucionEjercicio.EXTENSION_RESOLUCION));
        }


        private static string ElegirUbicacionNuevoArchivo(Window padre, string titulo, string dirInicial, string filtro, string descFiltros)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.CheckFileExists = false;
            file.CheckPathExists = false;
            file.InitialDirectory = dirInicial;
            file.Title = titulo;
            file.Filter = descFiltros;
            file.DefaultExt = string.Format(".{0}",filtro);
            file.AddExtension = true;

            file.ShowDialog(padre);

            return file.FileName;
            
        }


        internal static string ElegirGuardarComo(Window padre, string titulo, string dirInicial)
        {
            return ElegirArchivoGuardar(padre, titulo, dirInicial,
                string.Format("Archivos de Ejercicio (*.{0})|*.{0}",
                AplicativoEscritorio.DataAccess.Entidades.Ejercicio.EXTENSION_EJERCICIO)
                );
        }


        internal static string ElegirEjercicioParaResolucion(Window padre, string titulo, string dirInicial)
        {
            return ElegirArchivoAbrir (padre, titulo, dirInicial, 
                //string.Format("Archivos de Ejercicio (*.{0})|*.{0}|Todos los archivos (*.*)|*.*",
                string.Format("Archivos de Ejercicio (*.{0})|*.{0}",
                AplicativoEscritorio.DataAccess.Entidades.Ejercicio.EXTENSION_EJERCICIO)
                );
        }

        internal static string ElegirArchivoParaAbrir(Window padre, string titulo, string dirInicial)
        {
            return ElegirArchivoAbrir (padre, titulo, dirInicial,
                string.Format("Archivos de GarGar Dev (*.{1} o *.{0})|*.{1};*.{0}|Archivos de Resolución (*.{1})|*.{1}|Archivos de Ejercicio (*.{0})|*.{0}|Todos los archivos (*.*)|*.*",
                AplicativoEscritorio.DataAccess.Entidades.Ejercicio.EXTENSION_EJERCICIO,
                AplicativoEscritorio.DataAccess.Entidades.ResolucionEjercicio.EXTENSION_RESOLUCION)
                );
        }

        internal static string ElegirArchivoAbrir(Window padre, string titulo, string dirInicial, string filtros)
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

        internal static string ElegirArchivoGuardar(Window padre, string titulo, string dirInicial, string filtros)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.InitialDirectory = dirInicial;
            file.Title = titulo;
            file.Filter = filtros;
            file.AddExtension = true;

            file.ShowDialog(padre);


            return file.FileName;
        } 
    }
}
