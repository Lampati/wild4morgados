using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Windows.Controls.Ribbon;
using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Controls;
using Globales.Enums;
using DataAccess.Entidades;
using DiagramDesigner.Helpers;

namespace DiagramDesigner
{
    public partial class Window1 : RibbonWindow
    {
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                bool continuar = SalvarSiUsuarioQuiere();

                if (continuar)
                {
                    string path;
                    switch (Convert.ToInt32(e.Parameter))
                    {
                        case 1:

                            path = FileDialogManager.ElegirUbicacionNuevoEjercicio(this, "Elegir nombre y ubicación para el nuevo ejercicio", configApp.DirectorioEjerciciosCreados);

                            if (!string.IsNullOrWhiteSpace(path))
                            {

                                Ejercicio ej = new Ejercicio();
                                ej.UltimoModoGuardado = ModoVisual.Texto;
                                ej.Modo = DataAccess.Enums.ModoEjercicio.Normal;
                                ej.ModificadoDesdeUltimoGuardado = false;
                                ej.PathGuardadoActual = path;

                                ej.Guardar(ej.PathGuardadoActual);
                                ArchCargado = ej;
                                //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                                ej.ModificadoDesdeUltimoGuardado = false;
                            }
                            break;
                        case 2:

                            string pathEj = FileDialogManager.ElegirUbicacionNuevoEjercicio(this, "Elegir ejercicio a resolver", configApp.DirectorioEjerciciosDescargados);

                            if (!string.IsNullOrWhiteSpace(pathEj))
                            {

                                //chequeo de tipo de archivo
                                if (pathEj.ToLower().EndsWith(string.Format(".{0}", Globales.ConstantesGlobales.EXTENSION_EJERCICIO)))
                                {

                                    Ejercicio ej = new Ejercicio();
                                    ej.PathGuardadoActual = pathEj;
                                    ej.Abrir(ej.PathGuardadoActual);
                                    //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                                    ej.ModificadoDesdeUltimoGuardado = false;

                                    path = FileDialogManager.ElegirUbicacionNuevaResolucion(this, string.Format("Elegir nombre y ubicación para la nueva resolucón del ej {0}", ej.Nombre), configApp.DirectorioResolucionesEjercicios);

                                    if (!string.IsNullOrWhiteSpace(path))
                                    {

                                        ResolucionEjercicio res = new ResolucionEjercicio(ej);

                                        res.UltimoModoGuardado = ModoVisual.Texto;
                                        res.Modo = DataAccess.Enums.ModoEjercicio.Normal;
                                        res.ModificadoDesdeUltimoGuardado = false;
                                        res.PathGuardadoActual = path;
                                        res.Guardar(res.PathGuardadoActual);
                                        ArchCargado = res;
                                        //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                                        res.ModificadoDesdeUltimoGuardado = false;
                                    }
                                }
                                else
                                {
                                    //Error formato no soportado
                                }
                            }
                            break;
                    }
                }
            }
        }



        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                bool continuar = SalvarSiUsuarioQuiere();

                if (continuar)
                {
                    string path = FileDialogManager.ElegirArchivoParaAbrir(this, "Elija el archivo a abrir", configApp.DirectorioAbrirDefault);

                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        //chequeo de tipo de archivo
                        if (path.ToLower().EndsWith(string.Format(".{0}", Globales.ConstantesGlobales.EXTENSION_EJERCICIO)))
                        {
                            Ejercicio ej = new Ejercicio();
                            ej.PathGuardadoActual = path;
                            ej.Abrir(ej.PathGuardadoActual);
                            ArchCargado = ej;
                            //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                            ej.ModificadoDesdeUltimoGuardado = false;
                        }
                        else if (path.ToLower().EndsWith(string.Format(".{0}", Globales.ConstantesGlobales.EXTENSION_RESOLUCION)))
                        {
                            ResolucionEjercicio res = new ResolucionEjercicio();
                            res.PathGuardadoActual = path;
                            res.Abrir(res.PathGuardadoActual);
                            ArchCargado = res;
                            //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                            res.ModificadoDesdeUltimoGuardado = false;
                        }
                        else
                        {
                            //Error formato no soportado
                        }
                    }
                }
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                GuardarADisco();
            }
        }



        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {

            }
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                if (Modo == ModoVisual.Texto)
                {

                    FlowDocument doc = new FlowDocument(new Paragraph(new Run(Esquema.textEditor.Text)));
                    doc.Name = "GarGar";

                    doc.ColumnWidth = printDialog.PrintableAreaWidth;
                    doc.PagePadding = new Thickness(25);

                    // Create IDocumentPaginatorSource from FlowDocument
                    IDocumentPaginatorSource idpSource = doc;

                    // Call PrintDocument method to send document to printer

                    printDialog.PrintDocument(idpSource.DocumentPaginator, "Documento Gargar");
                }
                else
                {
                    printDialog.PrintVisual(Esquema.MyDesigner, "WPF Diagram");
                }
            }
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int i = 0;

            bool continuar = SalvarSiUsuarioQuiere();

            if (continuar)
            {
                Close();
            }
        }

        private bool SalvarSiUsuarioQuiere()
        {
            bool retorno = true;

            if (ArchCargado != null && ArchCargado.ModificadoDesdeUltimoGuardado)
            {
                MessageBoxResult result = MessageBox.Show("¿Desea guardar los cambios efectuados?", "ProgramAR", MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        retorno = false;
                        break;

                    case MessageBoxResult.No:
                        break;

                    case MessageBoxResult.Yes:
                        GuardarADisco();
                        break;
                }
            }

            return retorno;
        }

        private void GuardarADisco()
        {
            ArchCargado.Guardar(ArchCargado.PathGuardadoActual);
            ArchCargado.ModificadoDesdeUltimoGuardado = false;
        }

    }
}
