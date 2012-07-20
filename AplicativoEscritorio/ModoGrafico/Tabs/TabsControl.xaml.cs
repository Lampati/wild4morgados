namespace ModoGrafico.Tabs
{
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System;
    using ModoGrafico.EventArgsClasses;

    /// <summary>
    /// Interaction logic for FormattedTab.xaml
    /// </summary>
    public partial class TabsControl : UserControl
    {
        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(
            "ItemSource",
            typeof(IEnumerable),
            typeof(TabsControl),
            new FrameworkPropertyMetadata(ItemSourceChangedCallback));

        public TabsControl()
        {
            InitializeComponent();
        }

        public delegate void TipoTabCambiadoEventHandler(object o, TipoTabCambiadoEventArgs e);
        public event TipoTabCambiadoEventHandler CambioTabEvent;

        private void CambioTabEventFire(object sender, TipoTabCambiadoEventArgs e)
        {
            if (CambioTabEvent != null)
            {
                CambioTabEvent(sender, e);
            }
            
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
            TabsControl tab = d as TabsControl;
            if (tab != null)
            {
                IEnumerable value = (IEnumerable)e.NewValue;
                tab.ItemSource = value;
            }
        }

        private void tc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0 && e.AddedItems[0] != null)
            {
                if (e.AddedItems[0].GetType() == typeof(TabItemAgregarProcedimiento))
                {                   

                    Application.Current.Dispatcher.BeginInvoke
                    ((System.Action)delegate
                    {
                        CambioTabEventFire(e.AddedItems[0], new TipoTabCambiadoEventArgs(Enums.TipoTab.TabItemAgregarProcedimiento));

                        if (e.AddedItems.Count > 0)
                        {
                            if (((object[])(e.AddedItems))[0].ToString().Contains("TabItemAgregarProcedimiento"))
                            {
                                tc.SelectedIndex = tc.Items.Count - 3;
                            }
                        }
                    }, System.Windows.Threading.DispatcherPriority.Render, null);
                }
                else if (e.AddedItems[0].GetType() == typeof(TabItemAgregarFuncion))
                {

                    Application.Current.Dispatcher.BeginInvoke
                    ((System.Action)delegate
                    {
                        CambioTabEventFire(e.AddedItems[0], new TipoTabCambiadoEventArgs(Enums.TipoTab.TabItemAgregarFuncion));

                        if (e.AddedItems.Count > 0)
                        {
                            if (((object[])(e.AddedItems))[0].ToString().Contains("TabItemAgregarFuncion"))
                            {
                                tc.SelectedIndex = tc.Items.Count - 3;
                            }
                        }
                    }, System.Windows.Threading.DispatcherPriority.Render, null);
                }
                else if (e.AddedItems[0].GetType() == typeof(TabItemDeclaracionConstante))
                {
                    CambioTabEventFire(e.AddedItems[0], new TipoTabCambiadoEventArgs(Enums.TipoTab.TabItemDeclaracionConstante));
                }
                else if (e.AddedItems[0].GetType() == typeof(TabItemDeclaracionVariable))
                {
                    CambioTabEventFire(e.AddedItems[0], new TipoTabCambiadoEventArgs(Enums.TipoTab.TabItemDeclaracionVariable));
                }
                else if (e.AddedItems[0].GetType() == typeof(TabItemFuncion))
                {
                    CambioTabEventFire(e.AddedItems[0], new TipoTabCambiadoEventArgs(Enums.TipoTab.TabItemFuncion));
                }
                else if (e.AddedItems[0].GetType() == typeof(TabItemProcedimiento))
                {
                    CambioTabEventFire(e.AddedItems[0], new TipoTabCambiadoEventArgs(Enums.TipoTab.TabItemProcedimiento));
                }
                else if (e.AddedItems[0].GetType() == typeof(TabItemPrincipal))
                {
                    CambioTabEventFire(e.AddedItems[0], new TipoTabCambiadoEventArgs(Enums.TipoTab.TabItemPrincipal));
                }
                else if (e.AddedItems[0].GetType() == typeof(TabItemSalida))
                {
                    CambioTabEventFire(e.AddedItems[0], new TipoTabCambiadoEventArgs(Enums.TipoTab.TabItemSalida));
                }
            }
            else
            {
                if (e.RemovedItems != null && e.RemovedItems.Count > 0 && e.RemovedItems[0] != null)
                {
                    tc.SelectedIndex = 0;
                }
            }
        }

        private void tc_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
