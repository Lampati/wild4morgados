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
using Globales.Enums;
using AplicativoEscritorio.DataAccess.Entidades;

namespace Ragnarok.UserControls.Estado
{
    /// <summary>
    /// Interaction logic for BarraMensajes.xaml
    /// </summary>
    public partial class BarraEstado : UserControl
    {

        private EntidadBase archCargado = null;
        public EntidadBase ArchCargado
        {
            get { return archCargado; }
            set
            {
                archCargado = value;

                if (archCargado != null)
                {
                    Archivo.Text = archCargado.PathGuardadoActual;
                }
            }
        }

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
                        this.LineaBarraEstado.Visibility = System.Windows.Visibility.Hidden;
                        this.ColumnaBarraEstado.Visibility = System.Windows.Visibility.Hidden;
                        break;
                    case ModoVisual.Texto:
                        this.LineaBarraEstado.Visibility = System.Windows.Visibility.Visible;
                        this.ColumnaBarraEstado.Visibility = System.Windows.Visibility.Visible;                        
                        break;
                }
            }
        }

      

        public string Estado
        {
            get
            {
                return EstadoActual.Text;
            }

            set
            {
                EstadoActual.Text = value;
            }
        }

        public BarraEstado()
        {
            InitializeComponent();
           
        }

        public void RefrescarNombre()
        {
            if (archCargado != null)
            {
                Archivo.Text = archCargado.PathGuardadoActual;
            }
        }

        public void ColocarResCompilacion(bool res)
        {
            if (res)
            {
                this.imgEstadoActualCorrecta.Visibility = System.Windows.Visibility.Visible;
                this.imgEstadoActualError.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.imgEstadoActualCorrecta.Visibility = System.Windows.Visibility.Collapsed;
                this.imgEstadoActualError.Visibility = System.Windows.Visibility.Visible;
            }
        }


        public void ModificarPosicionModoTexto(int f, int c)
        {
            this.LineaBarraEstado.Text = string.Format("Linea {0}", f);
            this.ColumnaBarraEstado.Text = string.Format("Columna {0}", c);

        }

       

    }
}
