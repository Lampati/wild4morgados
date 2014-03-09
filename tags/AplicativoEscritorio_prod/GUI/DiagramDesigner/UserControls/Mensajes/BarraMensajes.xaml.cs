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
using Ragnarok.DTO;
using Ragnarok.Enums;
using Ragnarok.EventArgsClasses;
using Globales.Enums;
using LibreriaActividades;

namespace Ragnarok.UserControls.Mensajes
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



            Loaded += new RoutedEventHandler(BarraMensajes_Loaded);
        }

        void BarraMensajes_Loaded(object sender, RoutedEventArgs e)
        {
            
        }


        private void lstVwMensajesModoTexto_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                GridView view = this.lstVwMensajesModoTexto.View as GridView;
                Decorator border = VisualTreeHelper.GetChild(this.lstVwMensajesModoTexto, 0) as Decorator;
                if (border != null)
                {
                    ScrollViewer scroller = border.Child as ScrollViewer;
                    if (scroller != null)
                    {
                        ItemsPresenter presenter = scroller.Content as ItemsPresenter;
                        if (presenter != null)
                        {
                            

                            view.Columns[2].Width = presenter.ActualWidth;
                            for (int i = 0; i < view.Columns.Count - 1; i++)
                            {
                                

                                if (view.Columns[2].Width > view.Columns[i].ActualWidth)
                                {
                                    view.Columns[2].Width -= view.Columns[i].ActualWidth;
                                }
                                else
                                {
                                    view.Columns[2].Width = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void lstVwMensajesModoGrafico_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                GridView view = this.lstVwMensajesModoGrafico.View as GridView;
                Decorator border = VisualTreeHelper.GetChild(this.lstVwMensajesModoGrafico, 0) as Decorator;
                if (border != null)
                {
                    ScrollViewer scroller = border.Child as ScrollViewer;
                    if (scroller != null)
                    {
                        ItemsPresenter presenter = scroller.Content as ItemsPresenter;
                        if (presenter != null)
                        {
                            view.Columns[3].Width = presenter.ActualWidth;
                            for (int i = 0; i < view.Columns.Count - 1; i++)
                            {
                                if (view.Columns[3].Width > view.Columns[i].ActualWidth)
                                {
                                    view.Columns[3].Width -= view.Columns[i].ActualWidth;
                                }
                                else
                                {
                                    view.Columns[3].Width = 0;
                                }
                            }
                        }
                    }
                }
            }
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
