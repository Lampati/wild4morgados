namespace ModoGrafico.Views
{
    using System.Windows.Controls;
    using System.Activities.Presentation;
using ModoGrafico.ViewModels;
using System.ComponentModel;
    using System.Windows.Data;

    /// <summary>
    /// Interaction logic for SingleBrandView.xaml
    /// </summary>
    public partial class SingleWorkAreaView : UserControl , INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    
        public SingleWorkAreaView()
        {
            InitializeComponent();
            ModoGrafico.Tabs.EditableTabHeaderControl.ClickEvento += new ModoGrafico.Tabs.EditableTabHeaderControl.ClickHandler(EditableTabHeaderControl_ClickEvento);

            ModoGrafico.Views.WorkAreaView.CambioTabStaticEvent += new WorkAreaView.TipoTabCambiadoEventHandler(WorkAreaView_CambioTabStaticEvent);
        }

        void WorkAreaView_CambioTabStaticEvent(object o, EventArgsClasses.TipoTabCambiadoEventArgs e)
        {

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, (System.Action)(() =>
            {
                Tab tabCorrespondiente = this.DataContext as Tab;

                this.grd.ColumnDefinitions.Clear();

                switch (tabCorrespondiente.Tipo)
                {

                    case ModoGrafico.Enums.TipoTab.TabItemDeclaracionVariable:
                    case ModoGrafico.Enums.TipoTab.TabItemDeclaracionConstante:
                        stackPanelPropiedades.Visibility = System.Windows.Visibility.Collapsed;
                        this.grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(1050, System.Windows.GridUnitType.Star) });
                        Grid.SetColumn(this.contentDesigner, 0);
                        break;
                    case ModoGrafico.Enums.TipoTab.TabItemFuncion:
                        this.grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(350, System.Windows.GridUnitType.Star) });
                        this.grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(850, System.Windows.GridUnitType.Star) });


                        //if (cboBoxParametros.Items.Count > 0)
                        //{
                        //    cboBoxParametros.SelectedIndex = 0;
                        //}
                        //stackPanelParametros.Visibility = System.Windows.Visibility.Visible;
                        stackPanelRetorno.Visibility = System.Windows.Visibility.Visible;
                        stackPanelTipoRetorno.Visibility = System.Windows.Visibility.Visible;
                        stackPanelPropiedades.Visibility = System.Windows.Visibility.Visible;
                        

                        Grid.SetColumn(this.contentDesignerDeclaraciones, 0);
                        Grid.SetColumn(this.contentDesigner, 1);
                        break;
                    case ModoGrafico.Enums.TipoTab.TabItemProcedimiento:

                        //if (cboBoxParametros.Items.Count > 0)
                        //{
                        //    cboBoxParametros.SelectedIndex = 0;
                        //}
                        //stackPanelParametros.Visibility = System.Windows.Visibility.Visible;
                        stackPanelRetorno.Visibility = System.Windows.Visibility.Collapsed;
                        stackPanelTipoRetorno.Visibility = System.Windows.Visibility.Collapsed;                        
                        stackPanelPropiedades.Visibility = System.Windows.Visibility.Visible;

                        this.grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(350, System.Windows.GridUnitType.Star) });
                        this.grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(850, System.Windows.GridUnitType.Star) });

                       

                        Grid.SetColumn(this.contentDesignerDeclaraciones, 0);
                        Grid.SetColumn(this.contentDesigner, 1);
                        break;
                    case ModoGrafico.Enums.TipoTab.TabItemPrincipal:
                    case ModoGrafico.Enums.TipoTab.TabItemSalida:

                        //stackPanelParametros.Visibility = System.Windows.Visibility.Collapsed;
                        stackPanelRetorno.Visibility = System.Windows.Visibility.Collapsed;
                        stackPanelTipoRetorno.Visibility = System.Windows.Visibility.Collapsed;
                        stackPanelPropiedades.Visibility = System.Windows.Visibility.Visible;

                        this.grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(350, System.Windows.GridUnitType.Star) });
                        this.grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(850, System.Windows.GridUnitType.Star) });

                        Grid.SetColumn(this.contentDesignerDeclaraciones, 0);
                        Grid.SetColumn(this.contentDesigner, 1);
                        break;
                    case ModoGrafico.Enums.TipoTab.TabItemAgregarFuncion:                        
                    case ModoGrafico.Enums.TipoTab.TabItemAgregarProcedimiento:
                        stackPanelPropiedades.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                    default:
                        break;
                }
            }));
        }

        void EditableTabHeaderControl_ClickEvento(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Lo saco pq necesito que sea sincronico

        }

        private void CommandBinding_PreviewCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true; 
        }

      

    }
}
