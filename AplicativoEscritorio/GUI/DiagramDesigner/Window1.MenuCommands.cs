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
using AplicativoEscritorio.DataAccess.Entidades;
using DiagramDesigner.Helpers;
using EJEKOR;
using CompiladorGargar.Resultado;
using DataAccess.Entidades;
using DiagramDesigner.DialogWindows;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using System.Collections.ObjectModel;
using DiagramDesigner.TestsPruebas;
using System.IO;
using AplicativoEscritorio.DataAccess;
using DataAccess;

namespace DiagramDesigner
{
    public partial class Window1 : RibbonWindow
    {
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                try
                {
                    bool continuar = SalvarSiUsuarioQuiere();

                    if (continuar)
                    {
                        string path;
                        switch (Convert.ToInt32(e.Parameter))
                        {
                            case 1:

                                path = FileDialogManager.ElegirUbicacionNuevoEjercicio(this, "Elegir nombre y ubicación para el nuevo ejercicio", ConfiguracionAplicacion.DirectorioEjerciciosCreados);

                                if (!string.IsNullOrWhiteSpace(path))
                                {

                                    Ejercicio ej = new Ejercicio();
                                    string pathTemplate = Path.Combine(Environment.CurrentDirectory, "ModoTexto", "Configuracion", "Template.txt").ToLower();
                                    if (pathTemplate.Contains(@"\bin\debug") || pathTemplate.Contains(@"\bin\release"))
                                        pathTemplate = pathTemplate.Replace(@"bin\debug\", "").Replace(@"bin\release\", "");
                                    if (File.Exists(pathTemplate))
                                        ej.Abrir(new FileInfo(pathTemplate));

                                    ej.UltimoModoGuardado = (AplicativoEscritorio.DataAccess.Enums.ModoVisual)ModoVisual.Texto;
                                    ej.Modo = AplicativoEscritorio.DataAccess.Enums.ModoEjercicio.Normal;
                                    ej.ModificadoDesdeUltimoGuardado = false;
                                    ej.PathGuardadoActual = path;

                                    ej.Guardar(ej.PathGuardadoActual);
                                    ArchCargado = ej;
                                    //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                                    ej.ModificadoDesdeUltimoGuardado = false;
                                }
                                break;
                            case 2:

                                string pathEj = FileDialogManager.ElegirUbicacionNuevoEjercicio(this, "Elegir ejercicio a resolver", ConfiguracionAplicacion.DirectorioEjerciciosDescargados);

                                if (!string.IsNullOrWhiteSpace(pathEj))
                                {

                                    //chequeo de tipo de archivo
                                    if (pathEj.ToLower().EndsWith(string.Format(".{0}", AplicativoEscritorio.DataAccess.Entidades.Ejercicio.EXTENSION_EJERCICIO)))
                                    {

                                        Ejercicio ej = new Ejercicio();
                                        ej.PathGuardadoActual = pathEj;
                                        ej.Abrir(new System.IO.FileInfo(ej.PathGuardadoActual));
                                        //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                                        ej.ModificadoDesdeUltimoGuardado = false;

                                        path = FileDialogManager.ElegirUbicacionNuevaResolucion(this, string.Format("Elegir nombre y ubicación para la nueva resolucón del ej {0}", ej.Nombre), ConfiguracionAplicacion.DirectorioResolucionesEjercicios);

                                        if (!string.IsNullOrWhiteSpace(path))
                                        {

                                            ResolucionEjercicio res = new ResolucionEjercicio(ej);

                                            res.UltimoModoGuardado = (AplicativoEscritorio.DataAccess.Enums.ModoVisual)ModoVisual.Texto;
                                            res.Modo = AplicativoEscritorio.DataAccess.Enums.ModoEjercicio.Normal;
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
                catch (AplicativoEscritorio.DataAccess.Excepciones.ExcepcionCriptografia)
                {
                    MessageBox.Show("El contenido del archivo no puede ser leído por Program.AR", "Error Apertura", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (AplicativoEscritorio.DataAccess.Excepciones.ExcepcionHashNoConcuerda)
                {
                    MessageBox.Show("El contenido del archivo ha sido alterado del original y no se puede abrir", "Hash Incoherente", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error inesperado de apertura", "Error Inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } //fin si
        }



        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                try
                {
                    bool continuar = SalvarSiUsuarioQuiere();

                    if (continuar)
                    {
                        string path = FileDialogManager.ElegirArchivoParaAbrir(this, "Elija el archivo a abrir", ConfiguracionAplicacion.DirectorioAbrirDefault);

                        if (!string.IsNullOrWhiteSpace(path))
                        {
                            //chequeo de tipo de archivo
                            if (path.ToLower().EndsWith(string.Format(".{0}", AplicativoEscritorio.DataAccess.Entidades.Ejercicio.EXTENSION_EJERCICIO)))
                            {
                                Ejercicio ej = new Ejercicio();
                                ej.PathGuardadoActual = path;
                                ej.Abrir(new System.IO.FileInfo(ej.PathGuardadoActual));
                                ArchCargado = ej;
                                //Se lo coloco despues la modificacion pq despues de cargar modifica el texto
                                ej.ModificadoDesdeUltimoGuardado = false;
                            }
                            else if (path.ToLower().EndsWith(string.Format(".{0}", AplicativoEscritorio.DataAccess.Entidades.ResolucionEjercicio.EXTENSION_RESOLUCION)))
                            {
                                ResolucionEjercicio res = new ResolucionEjercicio();
                                res.PathGuardadoActual = path;
                                res.Abrir(new System.IO.FileInfo(res.PathGuardadoActual));
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
                catch (AplicativoEscritorio.DataAccess.Excepciones.ExcepcionCriptografia)
                {
                    MessageBox.Show("El contenido del archivo no puede ser leído por Program.AR", "Error Apertura", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (AplicativoEscritorio.DataAccess.Excepciones.ExcepcionHashNoConcuerda)
                {
                    MessageBox.Show("El contenido del archivo ha sido alterado del original y no se puede abrir", "Hash Incoherente", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error inesperado de apertura", "Error Inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                GuardarADiscoFormatoNormal();
            }
        }



        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string path;
            //Pregunto si no es un RibbonButton, pq esto es un error del framework, que dispara 2 veces el evento
            if (e.OriginalSource.GetType() != typeof(RibbonButton))
            {
                switch (Convert.ToInt32(e.Parameter))
                {
                    case 1:
                        path = FileDialogManager.ElegirGuardarComo(this, "Elija el nombre del ejercicio", ConfiguracionAplicacion.DirectorioAbrirDefault);

                        if (path != string.Empty)
                        {
                                        
                            GuardarADiscoFormatoNormal(path);
                        }
                        break;

                    case 2:
                        //Valido tambien que tenga enunciado, solucion y nivel
                        //Lo que tengo que hacer es Validar una Ejecucion Satisfactoria y luego todos sus tests.

                        if (archCargado.NivelDificultad == 0 || string.IsNullOrWhiteSpace(archCargado.Enunciado) || string.IsNullOrWhiteSpace(archCargado.SolucionTexto))
                        {
                            StringBuilder strBldr = new StringBuilder("Es necesario cargar todos los detalles del ejercicio para poder subirlo a la web.").AppendLine();

                            if (archCargado.NivelDificultad == 0)
                            {
                                strBldr.AppendLine("Falta cargar el nivel de dificultad del ejercicio");
                            }

                            if (string.IsNullOrWhiteSpace(archCargado.Enunciado))
                            {
                                strBldr.AppendLine("Falta cargar el enunciado del ejercicio");
                            }

                            if (string.IsNullOrWhiteSpace(archCargado.SolucionTexto))
                            {
                                strBldr.AppendLine("Falta cargar la solución en modo texto del ejercicio");
                            }

                            MessageBox.Show(strBldr.ToString(), "ProgramAR", MessageBoxButton.OK);
                        }
                        else
                        {

                            MessageBox.Show("Es necesario ejecutar el programa para ver que sea correcto. Presione OK para ejecutar el programa.", "ProgramAR", MessageBoxButton.OK);

                            ResultadoEjecucion res = Ejecutar();

                            if (res.ResEjecucion != null)
                            {
                                if (res.ResEjecucion.EsCorrectaEjecucion)
                                {
                                    bool aptoParaGuardar = true;

                                    if (archCargado.TestsPrueba.Count > 0)
                                    {
                                        MessageBox.Show("Es necesario validar todos los test de prueba del programa. Presione OK para validar los test de prueba.", "ProgramAR", MessageBoxButton.OK);

                                        List<NodoTablaSimbolos> aux = res.ResCompilacion.TablaSimbolos.ObtenerVariablesDelProcPrincipal();
                                        aux.AddRange(res.ResCompilacion.TablaSimbolos.ObtenerVariablesGlobales());



                                        ObservableCollection<Variable> listaVariablesEntrada = TransformarAVariables(aux);
                                        ObservableCollection<Variable> listaVariablesSalida = TransformarAVariables(res.ResCompilacion.TablaSimbolos.ObtenerParametrosDelProcSalida());


                                        bool todosValidos = true;
                                        StringBuilder strErrores = new StringBuilder();
                                        foreach (TestPrueba test in this.archCargado.TestsPrueba)
                                        {
                                            todosValidos &= test.ValidarVariablesEntrada(listaVariablesEntrada.ToList()) && test.ValidarVariablesSalida(listaVariablesSalida.ToList());
                                        }

                                        if (todosValidos)
                                        {
                                            //MessageBox.Show("Es necesario ejecutar todos los test de prueba del programa. Presione OK para ejecutar uno por uno los test de prueba.", "ProgramAR", MessageBoxButton.OK);

                                            ////PONER ACA LA EJECUCION


                                            //foreach (TestPrueba test in this.archCargado.TestsPrueba)
                                            //{
                                            //    todosValidos &= test.EjecutarConVariablesPredeterminadas;
                                            //}
                                        }
                                        else
                                        {
                                            //Abro la consulta, para que vea cuales son
                                            aptoParaGuardar = false;

                                            WindowConsultaTests testWindow = new WindowConsultaTests(true);
                                            testWindow.VariablesEntrada = listaVariablesEntrada;
                                            testWindow.VariablesSalida = listaVariablesSalida;
                                            testWindow.TestPruebas = new ObservableCollection<TestPrueba>(archCargado.TestsPrueba);
                                            testWindow.Validar();

                                            testWindow.Title = "Tests de Prueba - Mis Tests";

                                            testWindow.ShowDialog();
                                        }
                                    }

                                    if (aptoParaGuardar)
                                    {
                                        path = FileDialogManager.ElegirGuardarComo(this, "Elija el nombre del ejercicio", ConfiguracionAplicacion.DirectorioAbrirDefault);
                                        if (path != string.Empty)
                                        {
                                            GuardarADiscoFormatoExportar(path);
                                        }
                                    }

                                }
                                else
                                {
                                    //Muestro cual fue el error pq termino mal
                                    ResultadoEjecucionDialog resultadosDialog = new ResultadoEjecucionDialog(res.ResEjecucion);
                                    resultadosDialog.ShowDialog();

                                    try
                                    {
                                        File.Delete(res.ResCompilacion.ArchTemporalResultadosEjecucionConRuta);
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                        
                        break;
                }
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
                    //printDialog.PrintVisual(Esquema.MyDesigner, "WPF Diagram");
                }
            }
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Salir();
        }

        public void Salir()
        {
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
                        GuardarADiscoFormatoNormal();
                        break;
                }
            }

            return retorno;
        }


        private void GuardarADiscoFormatoNormal()
        {
            GuardarADiscoFormatoNormal(ArchCargado.PathGuardadoActual);
        }

        private void GuardarADiscoFormatoNormal(string path)
        {
            if (archCargado.GetType() == typeof(Ejercicio))
            {
                ((Ejercicio)archCargado).EsValidoSubirWeb = false;
            }

            GuardarADisco(path);
        }

        private void GuardarADiscoFormatoExportar()
        {
            GuardarADiscoFormatoExportar(ArchCargado.PathGuardadoActual);
        }

        private void GuardarADiscoFormatoExportar(string path)
        {
            if (archCargado.GetType() == typeof(Ejercicio))
            {
                ((Ejercicio)archCargado).EsValidoSubirWeb = true;
            }

            GuardarADisco(path);
        }

        private void GuardarADisco(string pathNuevo)
        {
            ArchCargado.PathGuardadoActual = pathNuevo;            

            ArchCargado.Guardar(ArchCargado.PathGuardadoActual);
            ArchCargado.ModificadoDesdeUltimoGuardado = false;
        }

        

    }
}
