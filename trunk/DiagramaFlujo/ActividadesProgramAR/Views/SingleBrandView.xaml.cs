namespace UsingWorkflowItemPresenter.Views
{
    using System.Windows.Controls;
    using System.Activities.Presentation;

    /// <summary>
    /// Interaction logic for SingleBrandView.xaml
    /// </summary>
    public partial class SingleBrandView : UserControl
    {
        public SingleBrandView()
        {
            InitializeComponent();
        }

        private void CommandBinding_PreviewCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true; 
        }
    }
}
