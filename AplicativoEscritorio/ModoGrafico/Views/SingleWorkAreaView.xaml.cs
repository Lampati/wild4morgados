namespace ModoGrafico.Views
{
    using System.Windows.Controls;
    using System.Activities.Presentation;
using ModoGrafico.ViewModels;
using System.ComponentModel;
    using System.Windows.Data;
    using System.Windows;
    using LibreriaActividades;
    using System;
using System.Windows.Input;

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

        CommandBinding commBindUndo;
        CommandBinding commBindRedo;
    
        public SingleWorkAreaView()
        {
            InitializeComponent();
            ModoGrafico.Tabs.EditableTabHeaderControl.ClickEvento += new ModoGrafico.Tabs.EditableTabHeaderControl.ClickHandler(EditableTabHeaderControl_ClickEvento);

            ModoGrafico.Views.WorkAreaView.CambioTabStaticEvent += new WorkAreaView.TipoTabCambiadoEventHandler(WorkAreaView_CambioTabStaticEvent);
            ModoGrafico.Views.WorkAreaView.ActualizarParametrosStaticEvent += new WorkAreaView.ActualizarParametrosEventHandler(WorkAreaView_ActualizarParametrosStaticEvent);


            commBindUndo = new CommandBinding();
            commBindUndo.Command = ApplicationCommands.Undo;
            commBindUndo.CanExecute += new CanExecuteRoutedEventHandler(commBindApplicationUndo_CanExecute);
            commBindUndo.Executed += new ExecutedRoutedEventHandler(commBindApplicationUndo_Executed);         

            commBindRedo = new CommandBinding();
            commBindRedo.Command = ApplicationCommands.Cut;
            commBindRedo.CanExecute += new CanExecuteRoutedEventHandler(commBindApplicationRedo_CanExecute);
            commBindRedo.Executed += new ExecutedRoutedEventHandler(commBindApplicationRedo_Executed);

            CommandBindings.Add(commBindUndo);
            CommandBindings.Add(commBindRedo);

        }

        protected virtual void commBindApplicationUndo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Activities.Presentation.View.DesignerView.UndoCommand.Execute(null);
        }

        protected virtual void commBindApplicationUndo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = System.Activities.Presentation.View.DesignerView.UndoCommand.CanExecute(null);
        }

        protected virtual void commBindApplicationRedo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Activities.Presentation.View.DesignerView.RedoCommand.Execute(null);
        }

        protected virtual void commBindApplicationRedo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = System.Activities.Presentation.View.DesignerView.RedoCommand.CanExecute(null);
        }

        void WorkAreaView_ActualizarParametrosStaticEvent(object o, EventArgsClasses.ActualizarParametrosEventArgs e)
        {
            this.cboBoxParametros.ItemsSource = e.Parametros;

            PonerPrimerParametroComoActivo();
        }

        void WorkAreaView_CambioTabStaticEvent(object o, EventArgsClasses.TipoTabCambiadoEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, (System.Action)(() =>
            {
                Tab tabCorrespondiente = this.DataContext as Tab;

                if (tabCorrespondiente != null)
                {
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

                            PonerPrimerParametroComoActivo();

                            stackPanelParametros.Visibility = System.Windows.Visibility.Visible;
                            stackPanelRetorno.Visibility = System.Windows.Visibility.Visible;
                            stackPanelTipoRetorno.Visibility = System.Windows.Visibility.Visible;
                            stackPanelPropiedades.Visibility = System.Windows.Visibility.Visible;


                            Grid.SetColumn(this.contentDesignerDeclaraciones, 0);
                            Grid.SetColumn(this.contentDesigner, 1);
                            break;
                        case ModoGrafico.Enums.TipoTab.TabItemProcedimiento:
                        case ModoGrafico.Enums.TipoTab.TabItemSalida:

                            this.grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(350, System.Windows.GridUnitType.Star) });
                            this.grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(850, System.Windows.GridUnitType.Star) });

                            //cboBoxParametros.ItemsSource = tabCorrespondiente.Parametros;    

                            PonerPrimerParametroComoActivo();

                            stackPanelParametros.Visibility = System.Windows.Visibility.Visible;
                            stackPanelRetorno.Visibility = System.Windows.Visibility.Collapsed;
                            stackPanelTipoRetorno.Visibility = System.Windows.Visibility.Collapsed;
                            stackPanelPropiedades.Visibility = System.Windows.Visibility.Visible;

                            Grid.SetColumn(this.contentDesignerDeclaraciones, 0);
                            Grid.SetColumn(this.contentDesigner, 1);
                            break;
                        case ModoGrafico.Enums.TipoTab.TabItemPrincipal:


                            stackPanelParametros.Visibility = System.Windows.Visibility.Collapsed;
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
                }
            }));
        }

        private void PonerPrimerParametroComoActivo()
        {
            if (cboBoxParametros.Items.Count > 0)
            {
                cboBoxParametros.SelectedIndex = 0;
            }
        }

    

        void EditableTabHeaderControl_ClickEvento(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Lo saco pq necesito que sea sincronico

        }

        //private void CommandBinding_PreviewCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        //{
        //    //e.CanExecute = false;
        //    //e.Handled = true; 
        //    ActivityDesignerBase source = e.OriginalSource as ActivityDesignerBase;
            

        //    if (source != null)
        //    {
        //        bool esModificable = Convert.ToBoolean(source.ModelItem.Properties["SePuedeEliminar"].Value.ToString());
        //        e.CanExecute = esModificable;
        //    }

        //}

        //private void CommandBindingPaste_PreviewCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        //{

        //    Type tipo = e.OriginalSource.GetType();
            
        //    //lo ideal seria hacer esto pero no puedo pq es privada la clase
        //    //e.CanExecute = tipo == typeof(System.Activities.Core.Presentation.VerticalConnector) || tipo == typeof(TextBox);

        //    string textoCopiado = Clipboard.GetText();

        //    bool esActividad = textoCopiado.Contains("xmlns:l=\"clr-namespace:LibreriaActividades;assembly=LibreriaActividades");

        //    //var padreTemplated = ((UserControl)e.OriginalSource).TemplatedParent;
        //    //var padre = ((UserControl)e.OriginalSource).Parent;

        //    bool esDeclaracion = textoCopiado.Contains("<l:DeclaracionVariable)")
        //                        || textoCopiado.Contains("<l:DeclaracionConstante)")
        //                        || textoCopiado.Contains("<l:DeclaracionArreglo)");


            

        //    e.CanExecute = tipo.FullName.Equals("System.Activities.Core.Presentation.VerticalConnector") || tipo == typeof(TextBox);
        //}

      

      

    }
}
