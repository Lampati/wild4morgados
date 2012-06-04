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

namespace LibreriaActividades
{
    // Interaction logic for DeclaracionDesigner.xaml
    public partial class DeclaracionConstanteDesigner
    {
        System.Windows.Visibility visible;

        public System.Windows.Visibility Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }

        public DeclaracionConstanteDesigner()
        {
            InitializeComponent();
        }

        protected override void OnModelItemChanged(Object newItem)
        {
            DeclaracionVariable.Attach(ModelItem);
            base.OnModelItemChanged(newItem);
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (e.AddedItems.Count > 0)
            {
                ComboBoxItem ci = (ComboBoxItem)e.AddedItems[0];
                if (ci.Content.ToString() == "Vector")
                    ModelItem.Properties["Visible"].SetValue(System.Windows.Visibility.Visible);
                else
                    ModelItem.Properties["Visible"].SetValue(System.Windows.Visibility.Hidden);
            }
        }
    }
}
