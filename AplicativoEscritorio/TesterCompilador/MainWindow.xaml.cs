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

namespace TesterCompilador
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<ArchivoConError> listaArchivosCompilar = new List<ArchivoConError>();

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
            DirectoryInfo dirInfo = new DirectoryInfo(textBoxDir.Text);

            List<FileInfo> listFiles = dirInfo.GetFiles("*.gej").ToList();


            foreach (var file in listFiles)
            {
                int codigoGlobal = -1;
                int linea = -1;

                int.TryParse(file.Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[0], out codigoGlobal);
                int.TryParse(file.Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[1], out linea);

                if (codigoGlobal != -1 && linea != -1)
                {
                    MensajeError mensajePrevisto = MensajeErrorFactory.ObtenerMensajeError(codigoGlobal);

                    if (mensajePrevisto != null)
                    {

                        Ejercicio ej = new Ejercicio();
                        ej.Abrir(file);


                    }
                }
            }
        }
    }
}
