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
        private List<Mensaje> mensajes;

        public BarraMensajes()
        {
            InitializeComponent();
            this.mensajes = new List<Mensaje>();
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
    }
}
