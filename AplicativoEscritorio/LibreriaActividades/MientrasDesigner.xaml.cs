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
    // Interaction logic for MientrasDesigner.xaml
    public partial class MientrasDesigner
    {
        public MientrasDesigner()
        {
            InitializeComponent();
        }

        protected override void commBindApplicationCopy_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            MientrasDesigner act = sender as MientrasDesigner;
            
            e.CanExecute = System.Activities.Presentation.View.DesignerView.CopyCommand.CanExecute(null) && act.IsFocused;            

        }

        protected override void commBindApplicationCut_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            MientrasDesigner act = sender as MientrasDesigner;

            e.CanExecute = System.Activities.Presentation.View.DesignerView.CutCommand.CanExecute(null) && act.IsFocused;

        }

        //protected override void commBindApplicationDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    MientrasDesigner act = sender as MientrasDesigner;
        //    e.CanExecute = act.IsFocused;

        //}
    }
}
