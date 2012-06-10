namespace DiagramDesigner.ViewModels
{
    using System.Collections.ObjectModel;
    using Microsoft.Practices.Prism.Commands;
    using DiagramDesigner.Views;
    using System;
    using DiagramDesigner.Enums;
    using DiagramDesigner.Tabs;

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

            if (t.Content.ToString().Trim() == "+")
            {
                //TODO aca viene el wizard para crear funciones / procedimientos!
                this.ExecuteAddBrand("Proc/Func" + cant.ToString(), true, eTipoTab.TabItemProcedimiento);
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

        public Tab ExecuteAddBrand(string obj, bool acomodar, eTipoTab tipo)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                Tab t = null;
                switch (tipo)
                {
                    case eTipoTab.TabItemAgregar:
                        t = new TabItemAgregar();
                        break;
                    case eTipoTab.TabItemDeclaracionVariable:
                        t = new TabItemDeclaracionVariable();
                        break;
                    case eTipoTab.TabItemDeclaracionConstante:
                        t = new TabItemDeclaracionConstante();
                        break;
                    case eTipoTab.TabItemFuncion:
                        t = new TabItemFuncion();
                        break;
                    case eTipoTab.TabItemProcedimiento:
                        t = new TabItemProcedimiento();
                        break;
                    case eTipoTab.TabItemPrincipal:
                        t = new TabItemPrincipal();
                        break;
                }
                t.Header = obj;
                if (this.Brands.Count == 0 || !acomodar)
                    this.Brands.Add(t);
                else
                    this.Brands.Insert(this.Brands.Count - 1, t);
                return t;
            }

            return null;
        }

        private void ExecuteDeleteBrand(Tab obj)
        {
            if (this.Brands.Contains(obj))
            {
                this.Brands.Remove(obj);
                if (this.Brands.Count == 4)
                {
                    FormattedTabControl.EditableTabHeaderControl etc = new FormattedTabControl.EditableTabHeaderControl();
                    etc.Content = this.Brands[0].Header;
                    FormattedTabControl.EditableTabHeaderControl.ClickEventoFire(etc, null);
                }
                obj = null;
            }
        }
    }
}
