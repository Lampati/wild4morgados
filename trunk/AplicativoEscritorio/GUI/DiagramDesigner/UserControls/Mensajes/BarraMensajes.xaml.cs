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
using DiagramDesigner.EventArgsClasses;
using Globales.Enums;
using LibreriaActividades;

namespace DiagramDesigner.UserControls.Mensajes
{
    /// <summary>
    /// Interaction logic for BarraMensajes.xaml
    /// </summary>
    public partial class BarraMensajes : UserControl
    {
       

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

            GridView gridView = (GridView)lstVwMensajesModoTexto.View;
            gridView.AllowsColumnReorder = false;

            gridView = (GridView)lstVwMensajesModoGrafico.View;
            gridView.AllowsColumnReorder = false;
        }

        


        public void AjustarSize()
        {

            double width;

            //if (listview.Width == Double.NaN)
            //{
            //    width = ((DiagramDesigner.BarraMensajes)((System.Windows.Controls.Grid)listview.Parent).Parent).ActualWidth;
            //}
            //else
            //{            
            //    width = listview.Width;
            //}

            width = this.ActualWidth;

            GridView gv = lstVwMensajesModoTexto.View as GridView;
            for (int i = 0; i < gv.Columns.Count -1 ; i++)
            {
                
                    width -= gv.Columns[i].ActualWidth;
            }

            if (width < 100)
            {
                width = 100;
            }

            gv.Columns[2].Width = width - 15;

            width = this.ActualWidth;

            gv = lstVwMensajesModoGrafico.View as GridView;
            for (int i = 0; i < gv.Columns.Count-1; i++)
            {
                    width -= gv.Columns[i].ActualWidth;
            }

            if (width < 100)
            {
                width = 100;
            }

            gv.Columns[3].Width = width - 15;
        }

   


        void lstVwMensajes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            int i = 0;
            ListView listViewSender = (ListView)sender;
            int indice = listViewSender.SelectedIndex;

            if (indice != -1)
            {
                Mensaje mens = mensajes[indice];
                DoubleClickEventFire(new DoubleClickEventArgs(mens.Linea, mens.Columna, mens.FiguraId, mens.Figura, mens.Contexto, mens.ActividadReferenciada));
            }

        }


        public void AgregarErrorModoGrafico(string msg, string figuraId, string figuraProc, string figuraNombre, object act)
        {
            Mensaje m = new Mensaje(msg, Enums.TipoMensaje.Error) { FiguraId = figuraId, Contexto = figuraProc, ActividadReferenciada = act, Figura = figuraNombre };
            AgregarLinea(m);
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

    
}
