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
using Ragnarok.ModoTexto.Configuracion;

namespace Ragnarok.DialogWindows
{
    /// <summary>
    /// Interaction logic for TextEditionWindow.xaml
    /// </summary>
    public partial class TextEditionWindow : Window
    {
        public string Titulo
        {
            get
            {
                return Title;
            }
            set
            {
                Title = value;
            }
        }

        public string Copete
        {
            get
            {
                return (string)txtBlockCopete.Text;
            }
            set
            {
                txtBlockCopete.Text = value;
            }
        }

        public string Texto
        {
            get
            {
                return textEditor.Text;
            }
            set
            {
                textEditor.Text = value;
            }
        }

        private bool esEditable;
        public bool EsEditable
        {
            get
            {
                return esEditable;
            }
            set
            {
                esEditable = value;

                if (esEditable)
                {
                    bttnCancelar.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    bttnCancelar.Visibility = System.Windows.Visibility.Collapsed;
                }

                textEditor.IsReadOnly = !esEditable;
            }
        }

        private bool esContenidoGarGar;
        public bool EsContenidoGarGar
        {
            get
            {
                return esContenidoGarGar;
            }
            set
            {
                esContenidoGarGar = value;
                ModoTextoConfiguracion.ConfigurarAvalonEdit(textEditor);
            }
        }

        public TextEditionWindow()
        {
            InitializeComponent();

            esContenidoGarGar = false;
            esEditable = true;
        }

        private void bttnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void bttnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
