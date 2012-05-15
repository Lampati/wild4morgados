using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using AplicativoEscritorio.DataAccess.Entidades;
using CompiladorGargar.Sintactico.ErroresManager.Errores;
using CompiladorGargar;
using System.Diagnostics;
using CompiladorGargar.Resultado;
using CompiladorGargar.Resultado.Auxiliares;
using System.Collections.ObjectModel;

namespace TesterCompilador
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TesterBatchWindow : Window
    {
        Compilador compilador;
        ModoTest modoTest;


        public TesterBatchWindow(ModoTest modo)
        {
            InitializeComponent();

            modoTest = modo;

            switch (modoTest)
            {
                case ModoTest.ErroresSintacticos:
                    lblTipoTest.Content = "Verificador Errores Sintacticos Correctos";
                    lstTestsCorrectos.Visibility = System.Windows.Visibility.Collapsed;
                    lstTestsSintacticos.Visibility = System.Windows.Visibility.Visible;
                    break;
                case ModoTest.EjerciciosCorrectos:
                    lblTipoTest.Content = "Verificador Ejercicios Correctos";
                    lstTestsCorrectos.Visibility = System.Windows.Visibility.Visible;
                    lstTestsSintacticos.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }

        private void bttnDirTemporales_Click(object sender, RoutedEventArgs e)
        {
            FolderPickerLib.FolderPickerDialog fd = new FolderPickerLib.FolderPickerDialog();
            fd.InitialPath = textBoxDir.Text;
            fd.Title = "Elija la carpeta que se visualizara por defecto al elegir Abrir";

            if (fd.ShowDialog().Value == true)
            {
                textBoxDir.Text = fd.SelectedPath;

                bttnCompilarTodo.IsEnabled = true;
            }
            else
            {
                bttnCompilarTodo.IsEnabled = false;
            }
        }

        private void bttnCompilarTodo_Click(object sender, RoutedEventArgs e)
        {
            ActualizarLista(ObtenerListaArchivosComprobados());
        }

        private ObservableCollection<EntradaArchivoAComprobar> ObtenerListaArchivosComprobados()
        {
            ObservableCollection<EntradaArchivoAComprobar> listaRetorno = new ObservableCollection<EntradaArchivoAComprobar>();

            List<FileInfo> listFiles = ObtenerListadoArchivosEjercicio(textBoxDir.Text);

            foreach (var file in listFiles)
            {

                Ejercicio ej = new Ejercicio();
                ej.Abrir(file);

                ConfigurarCompilador();
                string programa = ej.Gargar;

                ResultadoCompilacion res = this.compilador.Compilar(programa);

                if (modoTest == ModoTest.EjerciciosCorrectos)
                {
                    EntradaArchivoAComprobar entrada = new EntradaArchivoAComprobar();
                    entrada.NombreArch = file.Name;
                    entrada.CompilacionCorrecta = res.CompilacionGarGarCorrecta && res.ResultadoCompPascal != null && res.ResultadoCompPascal.CompilacionPascalCorrecta;

                    listaRetorno.Add(entrada);
                }
                else
                {
                    InfoArchivo infoArch = ObtenerMensajePrevistoDeArchivo(System.IO.Path.GetFileNameWithoutExtension(file.Name));

                    if (infoArch.Valido)
                    {                      
                        if (res.ListaErrores.Count == 1)
                        {
                            PasoAnalizadorSintactico error = res.ListaErrores[0];

                            if (error.MensajeError != null)
                            {
                                ArchivoSintacticoComprobar entrada = new ArchivoSintacticoComprobar();

                                entrada.NombreArch = file.Name;

                                entrada.LineaEsperado = infoArch.Linea;
                                entrada.CodigoGlobalEsperado = infoArch.CodigoGlobal;
                                entrada.LineaReal = error.Fila;
                                entrada.CodigoGlobalReal = error.MensajeError.CodigoGlobal;

                                entrada.Mensaje = error.MensajeError.Mensaje;

                                listaRetorno.Add(entrada);
                            }
                        }
                    }
                }
            }

            return listaRetorno;
        }

        private InfoArchivo ObtenerMensajePrevistoDeArchivo(string nombreArch)
        {
            int codigoGlobal = -1;
            int linea = -1;
            InfoArchivo infoArch = new InfoArchivo();

            int.TryParse(nombreArch.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[0], out codigoGlobal);
            int.TryParse(nombreArch.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[1], out linea);

            if (codigoGlobal != -1 && linea != -1)
            {
                infoArch.CodigoGlobal = codigoGlobal;
                infoArch.Linea = linea;
                MensajeError mensajePrevisto = MensajeErrorFactory.ObtenerMensajeError(codigoGlobal);

                infoArch.Valido = mensajePrevisto != null;
            }
            else
            {
                infoArch.Valido = false;
            }    

            return infoArch;
        }

        private List<FileInfo> ObtenerListadoArchivosEjercicio(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            return dirInfo.GetFiles("*.gej").ToList();
        }

        private void ConfigurarCompilador()
        {

            bool modoDebug = Debugger.IsAttached;

            string directorioActual = Globales.ConstantesGlobales.PathEjecucionAplicacion;

            compilador = new Compilador(modoDebug, directorioActual, directorioActual, "prueba");
        }

        private void ActualizarLista(ObservableCollection<EntradaArchivoAComprobar> lista)
        {
            if (modoTest == ModoTest.ErroresSintacticos)
            {
                lstTestsCorrectos.Visibility = System.Windows.Visibility.Collapsed;
                lstTestsSintacticos.ItemsSource = lista;
                lstTestsSintacticos.Items.Refresh();
                lstTestsSintacticos.Visibility = System.Windows.Visibility.Visible;
            }
            else if (modoTest == ModoTest.EjerciciosCorrectos)
            {
                lstTestsSintacticos.Visibility = System.Windows.Visibility.Collapsed;
                lstTestsCorrectos.ItemsSource = lista;
                lstTestsCorrectos.Items.Refresh();
                lstTestsCorrectos.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
