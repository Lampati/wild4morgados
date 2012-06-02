namespace UsingWorkflowItemPresenter.ViewModels
{
    using System.Collections.ObjectModel;
    using Microsoft.Practices.Prism.Commands;
using UsingWorkflowItemPresenter.Views;
    using System;

    public class BrandsViewModel : BaseViewModel
    {
        private DelegateCommand<Tab> deleteBrand;
        private ObservableCollection<Tab> brands;
        private int cant = 1;

        public BrandsViewModel()
            : base()
        {
            FormattedTabControl.EditableTabHeaderControl.ClickEvento += new FormattedTabControl.EditableTabHeaderControl.ClickHandler(EditableTabHeaderControl_ClickEvento);
        }

        void EditableTabHeaderControl_ClickEvento(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FormattedTabControl.EditableTabHeaderControl t = sender as FormattedTabControl.EditableTabHeaderControl;
            if (Object.Equals(t, null))
                return;

            if (t.Content.ToString() == " + ")
            {
                this.ExecuteAddBrand("Proc/Func " + cant.ToString(), true, TipoTab.TabItemFuncionProcedimiento);
                cant++;
            }
        }

        public ObservableCollection<Tab> Brands
        {
            get
            {
                return this.brands ?? (this.brands = new ObservableCollection<Tab>());
            }

            set
            {
                if (value != null)
                {
                    this.brands = value;
                    NotifyPropertyChanged("Brands");
                }
            }
        }

        public DelegateCommand<Tab> DeleteBrand
        {
            get
            {
                return this.deleteBrand ?? (this.deleteBrand = new DelegateCommand<Tab>(
                                                                                 this.ExecuteDeleteBrand,
                                                                                 (arg) => true));
            }
        }

        //public DelegateCommand<string> AddBrand
        //{
        //    get
        //    {
        //        return this.addBrand ?? (this.addBrand = new DelegateCommand<string>(
        //                                                     this.ExecuteAddBrand,
        //                                                     (arg) => true));
        //    }
        //}

        public void ExecuteAddBrand(string obj, bool acomodar, TipoTab tipo)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                Tab t = null;
                switch (tipo)
                {
                    case TipoTab.TabItemAgregar:
                        t = new TabItemAgregar();
                        break;
                    case TipoTab.TabItemDeclaracion:
                        t = new TabItemDeclaracion();
                        break;
                    case TipoTab.TabItemFuncionProcedimiento:
                        t = new TabItemFuncionProcedimiento();
                        break;
                    case TipoTab.TabItemPrincipal:
                        t = new TabItemPrincipal();
                        break;
                }
                t.Header = obj;
                if (this.Brands.Count == 0 || !acomodar)
                    this.Brands.Add(t);
                else
                    this.Brands.Insert(this.Brands.Count - 1, t);
            }
        }

        private void ExecuteDeleteBrand(Tab obj)
        {
            if (this.Brands.Contains(obj))
            {
                this.Brands.Remove(obj);
                obj = null;
            }
        }
    }
}
