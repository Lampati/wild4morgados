namespace ModoGrafico.Tabs
{
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System;
    using ModoGrafico.EventArgsClasses;
    using InterfazTextoGrafico;
    using ModoGrafico.Helpers;

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

        private bool estaCargando = false;

        private static object padlock = new object();

        public TabsControl()
        {
            InitializeComponent();
        }

        //Esto se hace para salvarse del bug del workflow apenas cargas los WD y haces click rapido en los tabs
        public static void PreloadTabsSimple(TabControl tabControl)
        {

            for (var i = 0; i < tabControl.Items.Count-2; i++)
            {
                tabControl.SelectedIndex = i;
                tabControl.UpdateLayout();
            }

        }

        /// <summary>
        /// Preloads tab items of a tab control in sequence.
        /// </summary>
        /// <param name="tabControl">The tab control.</param>
        public static void PreloadTabs(TabControl tabControl)
        {
            // Evaluate
            if (tabControl.Items != null)
            {
                // The first tab is already loaded
                // so, we will start from the second tab.
                if (tabControl.Items.Count > 1)
                {
                    // Hide tabs
                    tabControl.Opacity = 0.0;

                    // Last action
                    Action onComplete = () =>
                    {
                        // Set index to first tab
                        tabControl.SelectedIndex = 0;

                        // Show tabs
                        tabControl.Opacity = 1.0;
                    };

                    // Second tab
                    var firstTab = (tabControl.Items[1] as TabItem);
                    if (firstTab != null)
                    {
                        PreloadTab(tabControl, firstTab, onComplete);
                    }
                }
            }
        }

        /// <summary>
        /// Preloads an individual tab item.
        /// </summary>
        /// <param name="tabControl">The tab control.</param>
        /// <param name="tabItem">The tab item.</param>
        /// <param name="onComplete">The onComplete action.</param>
        private static void PreloadTab(TabControl tabControl, TabItem tabItem, Action onComplete = null)
        {
            // On update complete
            tabItem.Loaded += delegate
            {
                // Update if not the last tab
                if (tabItem != tabControl.Items[tabControl.Items.Count-1])
                {
                    // Get next tab
                    var nextIndex = tabControl.Items.IndexOf(tabItem) + 1;
                    var nextTabItem = tabControl.Items[nextIndex] as TabItem;

                    // Preload
                    if (nextTabItem != null)
                    {
                        PreloadTab(tabControl, nextTabItem, onComplete);
                    }
                }

                else
                {
                    if (onComplete != null)
                    {
                        onComplete();
                    }
                }
            };

            // Set current tab context
            tabControl.SelectedItem = tabItem;
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
            lock (padlock)
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
                        if (e.RemovedItems[0].GetType() != typeof(ParametroViewModel))
                        {
                            tc.SelectedIndex = 0;
                        }
                    }
                }
                                
            }
        }

        private void tc_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
