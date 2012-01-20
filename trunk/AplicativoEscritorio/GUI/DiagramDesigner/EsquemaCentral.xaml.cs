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
using DiagramDesigner.Enums;

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for EsquemaCentral.xaml
    /// </summary>
    public partial class EsquemaCentral : UserControl
    {
        private ModoVisual modo;

        public EsquemaCentral()
        {
            InitializeComponent();
            this.Modo = ModoVisual.Flujo;
        }

        public ModoVisual Modo
        {
            get { return this.modo; }
            set
            {                
                this.modo = value;
                switch (this.modo)
                {
                    case ModoVisual.Flujo:
                        this.grdVisual.Visibility = System.Windows.Visibility.Visible;
                        this.grdTexto.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                    case ModoVisual.Texto:
                        this.grdVisual.Visibility = System.Windows.Visibility.Collapsed;
                        this.grdTexto.Visibility = System.Windows.Visibility.Visible;
                        break;
                }
            }
        }
    }
}
