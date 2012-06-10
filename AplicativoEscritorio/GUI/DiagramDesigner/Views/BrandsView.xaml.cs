namespace DiagramDesigner.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using DiagramDesigner.ViewModels;
    using DiagramDesigner.Enums;

    /// <summary>
    /// Interaction logic for BrandView.xaml
    /// </summary>
    public partial class BrandsView : UserControl
    {
        public BrandsView()
        {
            InitializeComponent();
            this.FindAndApplyResources();
            this.DataContext = new BrandsViewModel();
            ((BrandsViewModel)this.DataContext).ExecuteAddBrand("    PRINCIPAL    ", false, eTipoTab.TabItemPrincipal);
            ((BrandsViewModel)this.DataContext).ExecuteAddBrand("    CONSTANTES    ", false, eTipoTab.TabItemDeclaracionConstante);
            ((BrandsViewModel)this.DataContext).ExecuteAddBrand("    VARIABLES    ", false, eTipoTab.TabItemDeclaracionVariable);
            ((BrandsViewModel)this.DataContext).ExecuteAddBrand("     +     ", false, eTipoTab.TabItemAgregar);                    
        }

        private void FindAndApplyResources()
        {
            var singleBrandKey = new DataTemplateKey(typeof(Tab));
            var productKey = new DataTemplateKey(typeof(ProductViewModel));
            var singleBrandTemplate = this.TryFindResource(singleBrandKey);
            var productTemplate = this.TryFindResource(productKey);
            if (singleBrandTemplate != null && productTemplate != null)
            {
                this.tab.AddResource(singleBrandKey, singleBrandTemplate);
                this.tab.AddResource(productKey, productTemplate);
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
