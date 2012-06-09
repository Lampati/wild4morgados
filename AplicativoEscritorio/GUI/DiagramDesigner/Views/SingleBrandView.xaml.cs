namespace DiagramDesigner.Views
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
            FormattedTabControl.EditableTabHeaderControl.ClickEvento += new FormattedTabControl.EditableTabHeaderControl.ClickHandler(EditableTabHeaderControl_ClickEvento);
        }

        void EditableTabHeaderControl_ClickEvento(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, (System.Action)(() =>
            {
                this.grd.RowDefinitions.Clear();
                if (this.contentDesignerDeclaraciones.Content == null)
                {
                    this.grd.RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(600, System.Windows.GridUnitType.Star) });
                    Grid.SetRow(this.contentDesigner, 0);
                }
                else
                {
                    this.grd.RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(200, System.Windows.GridUnitType.Star) });
                    this.grd.RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(500, System.Windows.GridUnitType.Star) });
                    Grid.SetRow(this.contentDesignerDeclaraciones, 0);
                    Grid.SetRow(this.contentDesigner, 1);
                }
            }));
        }

        private void CommandBinding_PreviewCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true; 
        }
    }
}
