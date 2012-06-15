namespace ModoGrafico.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using ModoGrafico.ViewModels;
    using ModoGrafico.Enums;
    using ModoGrafico.EventArgsClasses;
using InterfazTextoGrafico;
    using System.Collections.Generic;

    /// <summary>
    /// Interaction logic for BrandView.xaml
    /// </summary>
    public partial class WorkAreaView : UserControl
    {
        public delegate void TipoTabCambiadoEventHandler(object o, TipoTabCambiadoEventArgs e);
        public event TipoTabCambiadoEventHandler CambioTabEvent;


        public WorkAreaView()
        {
            InitializeComponent();

            this.tab.CambioTabEvent += new Tabs.TabsControl.TipoTabCambiadoEventHandler(tab_CambioTabEvent);

            this.FindAndApplyResources();


            //this.DataContext = new WorkAreaViewModel();
            //((WorkAreaViewModel)this.DataContext).ExecuteAddBrand("    PRINCIPAL    ", false, TipoTab.TabItemPrincipal);
            //((WorkAreaViewModel)this.DataContext).ExecuteAddBrand("    CONSTANTES    ", false, TipoTab.TabItemDeclaracionConstante);
            //((WorkAreaViewModel)this.DataContext).ExecuteAddBrand("    VARIABLES    ", false, TipoTab.TabItemDeclaracionVariable);
            //((WorkAreaViewModel)this.DataContext).ExecuteAddBrand("     +     ", false, TipoTab.TabItemAgregar);                    
        }

        public void CargarPrograma(ProgramaViewModel programa)
        {
            
            this.DataContext = new WorkAreaViewModel();

            ProcedimientoViewModel procPrincipal = programa.Procedimientos.Find(y => y.Tipo == InterfazTextoGrafico.Enums.TipoRutina.Principal);
            ProcedimientoViewModel procSalida = programa.Procedimientos.Find(y => y.Tipo == InterfazTextoGrafico.Enums.TipoRutina.Salida);
            List<ProcedimientoViewModel> listaProcs = programa.Procedimientos.FindAll(y => y.Tipo != InterfazTextoGrafico.Enums.TipoRutina.Principal
                                                      && y.Tipo != InterfazTextoGrafico.Enums.TipoRutina.Salida);

            ((WorkAreaViewModel)this.DataContext).ExecuteAddProcedimiento("    PRINCIPAL    ", false, TipoTab.TabItemPrincipal, procPrincipal);
            ((WorkAreaViewModel)this.DataContext).ExecuteAddProcedimiento("    SALIDA    ", false, TipoTab.TabItemSalida, procSalida);
            ((WorkAreaViewModel)this.DataContext).ExecuteAddBrand("    CONSTANTES    ", false, TipoTab.TabItemDeclaracionConstante);
            ((WorkAreaViewModel)this.DataContext).ExecuteAddBrand("    VARIABLES    ", false, TipoTab.TabItemDeclaracionVariable);

            foreach (var item in listaProcs)
            {
                ((WorkAreaViewModel)this.DataContext).ExecuteAddProcedimiento(item.Nombre.ToUpper(), false, TipoTab.TabItemProcedimiento, item);
            }

            ((WorkAreaViewModel)this.DataContext).ExecuteAddBrand("     +     ", false, TipoTab.TabItemAgregar);
            
        }

        void tab_CambioTabEvent(object o, TipoTabCambiadoEventArgs e)
        {
            if (e.Tipo == TipoTab.TabItemAgregar)
            {
                ((WorkAreaViewModel)this.DataContext).AgregarNuevo();
            }
            else
            {
                CambioTabEventFire(o, e);
            }

        }

        private void CambioTabEventFire(object sender, TipoTabCambiadoEventArgs e)
        {
            if (CambioTabEvent != null)
            {
                CambioTabEvent(sender, e);
            }

        }

        private void FindAndApplyResources()
        {
            var singleBrandKey = new DataTemplateKey(typeof(Tab));
            var singleBrandTemplate = this.TryFindResource(singleBrandKey);
            if (singleBrandTemplate != null )
            {
                this.tab.AddResource(singleBrandKey, singleBrandTemplate);
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
