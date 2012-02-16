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

        public BarraMensajes()
        {
            InitializeComponent();
            this.mensajes = new List<Mensaje>();

            lstVwMensajes.MouseDoubleClick += new MouseButtonEventHandler(lstVwMensajes_MouseDoubleClick);
        }

        void lstVwMensajes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            int i = 0;
            ListView listViewSender = (ListView)sender;
            int indice = listViewSender.SelectedIndex;

            if (indice != -1)
            {
                Mensaje mens = mensajes[indice];
                DoubleClickEventFire(new DoubleClickEventArgs(mens.Linea, mens.Columna));
            }

        }

        private void DoubleClickEventFire(DoubleClickEventArgs e)
        {
            if (DoubleClickEvent != null)
            {
                DoubleClickEvent(this, e);
            }
        }

        public void AgregarMensaje(string msg)
        {
            if (!String.IsNullOrEmpty(msg))
            {
                Mensaje m = new Mensaje(msg);
                this.mensajes.Add(m);
                this.lstVwMensajes.Items.Add(m);
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
            this.lstVwMensajes.Items.Add(men);
        }

        public void BorrarTodosMensajes()
        {
           
            this.mensajes.Clear();
            this.lstVwMensajes.Items.Clear();
            
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

        public DoubleClickEventArgs(int f, int c)
        {
            fila = f;
            columna = c;
        }
    }
}
