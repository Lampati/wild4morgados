namespace FormattedTabControl
{
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for FormattedTab.xaml
    /// </summary>
    public partial class FormattedTab : UserControl
    {
        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(
            "ItemSource",
            typeof(IEnumerable),
            typeof(FormattedTab),
            new FrameworkPropertyMetadata(ItemSourceChangedCallback));

        public FormattedTab()
        {
            InitializeComponent();
        }

        public IEnumerable ItemSource
        {
            get
            {
                return this.tc.ItemsSource;
            }

            set
            {
                this.tc.ItemsSource = value;
                this.tc.SelectedIndex = 0;
            }
        }

        public void AddResource(object key, object value)
        {
            this.Resources.Add(key, value);
        }

        private static void ItemSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FormattedTab tab = d as FormattedTab;
            if (tab != null)
            {
                IEnumerable value = (IEnumerable)e.NewValue;
                tab.ItemSource = value;
            }
        }
    }
}
