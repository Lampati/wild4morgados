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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace TesterCompilador
{
    /// <summary>
    /// Interaction logic for ErroresMensajeDisplayerWindow.xaml
    /// </summary>
    public partial class ErroresMensajeDisplayerWindow : Window
    {
        ObservableCollection<MensajeError> listaCompletaErrores = new ObservableCollection<MensajeError>();

        public ErroresMensajeDisplayerWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 200; i++)
            {
                MensajeError mensAgregar = MensajeErrorFactory.ObtenerMensajeError(i);

                if (mensAgregar != null)
                {
                    listaCompletaErrores.Add(mensAgregar);
                }

                ActualizarLista(listaCompletaErrores);
            }
        }

        private void ActualizarLista(ObservableCollection<MensajeError> lista)
        {
                lstMensajes.ItemsSource = lista;
                lstMensajes.Items.Refresh();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBxFiltroTexto.Text != string.Empty)
            {
                ObservableCollection<MensajeError> listaFiltrada = new ObservableCollection<MensajeError>( listaCompletaErrores.ToList().FindAll(x => x.MensajeModoTexto.ToUpper().Contains(txtBxFiltroTexto.Text.ToUpper())));
                ActualizarLista(listaFiltrada);
            }
            else
            {
                ActualizarLista(listaCompletaErrores);
            }
        }
    }
}
