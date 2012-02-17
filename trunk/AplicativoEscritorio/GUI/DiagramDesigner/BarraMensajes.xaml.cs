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
using DiagramDesigner.DTO;
using DiagramDesigner.Enums;

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for BarraMensajes.xaml
    /// </summary>
    public partial class BarraMensajes : UserControl
    {
        public delegate void DobleClickEnBarraMensajesEventHandler(object o, DoubleClickEventArgs e);

        public event DobleClickEnBarraMensajesEventHandler DoubleClickEvent;

        private List<Mensaje> mensajes;

        private ModoVisual modo;
        public ModoVisual Modo
        {
            get { return this.modo; }
            set
            {
                this.modo = value;
                switch (this.modo)
                {
                    case ModoVisual.Flujo:
                        this.lstVwMensajesModoTexto.Visibility = System.Windows.Visibility.Collapsed;
                        this.lstVwMensajesModoGrafico.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case ModoVisual.Texto:
                        this.lstVwMensajesModoTexto.Visibility = System.Windows.Visibility.Visible;
                        this.lstVwMensajesModoGrafico.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                }
            }
        }

        public BarraMensajes()
        {
            
            InitializeComponent();
            this.mensajes = new List<Mensaje>();

            lstVwMensajesModoTexto.MouseDoubleClick += new MouseButtonEventHandler(lstVwMensajes_MouseDoubleClick);
            lstVwMensajesModoGrafico.MouseDoubleClick += new MouseButtonEventHandler(lstVwMensajes_MouseDoubleClick);
        }

      

        void lstVwMensajes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            int i = 0;
            ListView listViewSender = (ListView)sender;
            int indice = listViewSender.SelectedIndex;

            if (indice != -1)
            {
                Mensaje mens = mensajes[indice];
                DoubleClickEventFire(new DoubleClickEventArgs(mens.Linea, mens.Columna, mens.Figura));
            }

        }

        private void DoubleClickEventFire(DoubleClickEventArgs e)
        {
            if (DoubleClickEvent != null)
            {
                DoubleClickEvent(this, e);
            }
        }

       

        public void AgregarError(string msg, int fila, int columna)
        {
            Mensaje m = new Mensaje(msg, Enums.TipoMensaje.Error) { Linea = fila, Columna = columna };
            AgregarLinea(m);               
        }

        public void AgregarAdvertencia(string msg, int fila, int columna)
        {
            Mensaje m = new Mensaje(msg, Enums.TipoMensaje.Advertencia) { Linea = fila, Columna = columna };
            AgregarLinea(m);
        }

        public void AgregarInformacion(string msg, int fila, int columna)
        {
            Mensaje m = new Mensaje(msg, Enums.TipoMensaje.Informacion) { Linea = fila, Columna = columna };
            AgregarLinea(m);
        }

        private void AgregarLinea(Mensaje men)
        {
            this.mensajes.Add(men);
            if (modo == ModoVisual.Texto)
            {
                this.lstVwMensajesModoTexto.Items.Add(men);
            }
            else
            {
                men.Figura = "prueba";
                this.lstVwMensajesModoGrafico.Items.Add(men);
            }
        }

        public void BorrarTodosMensajes()
        {
           
            this.mensajes.Clear();
            this.lstVwMensajesModoTexto.Items.Clear();
            this.lstVwMensajesModoGrafico.Items.Clear();
            
        }
    }

    public class DoubleClickEventArgs
    {
        private int fila;
        public int Fila
        {
            get
            {
                return fila;
            }
        }

        private int columna;
        public int Columna
        {
            get
            {
                return columna;
            }
        }

        private string figura;
        public string Figura
        {
            get
            {
                return figura;
            }
        }

        public DoubleClickEventArgs(int f, int c, string fig)
        {
            fila = f;
            columna = c;
            figura = fig;
        }
    }
}
