namespace ModoGrafico.ViewModels
{
    using System.Collections.ObjectModel;
    using Microsoft.Practices.Prism.Commands;
    using ModoGrafico.Views;
    using System;
    using ModoGrafico.Enums;
    using ModoGrafico.Tabs;
using InterfazTextoGrafico;

    public class WorkAreaViewModel : BaseViewModel
    {
        private DelegateCommand<Tab> deleteBrand;
        private ObservableCollection<Tab> brands;
        private int cant = 1;

        public WorkAreaViewModel()
            : base()
        {
            ModoGrafico.Tabs.EditableTabHeaderControl.ClickEvento += new ModoGrafico.Tabs.EditableTabHeaderControl.ClickHandler(EditableTabHeaderControl_ClickEvento);
        }

        public void AgregarNuevo()
        {
            this.ExecuteAddBrand("Proc/Func" + cant.ToString(), true, TipoTab.TabItemProcedimiento);
            cant++;
        }

        void EditableTabHeaderControl_ClickEvento(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //ModoGrafico.Tabs.EditableTabHeaderControl t = sender as ModoGrafico.Tabs.EditableTabHeaderControl;
            //if (Object.Equals(t, null))
            //    return;

            //if (t.Content.ToString().Trim() == "+")
            //{
            //    //TODO aca viene el wizard para crear funciones / procedimientos!
            //    this.ExecuteAddBrand("Proc/Func" + cant.ToString(), true, eTipoTab.TabItemProcedimiento);
            //    cant++;
            //}
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

        public Tab ExecuteAddBrand(string obj, bool acomodar, TipoTab tipo)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                Tab t = null;
                switch (tipo)
                {
                    case TipoTab.TabItemAgregar:
                        t = new TabItemAgregar();
                        break;
                    case TipoTab.TabItemDeclaracionVariable:
                        t = new TabItemDeclaracionVariable();
                        break;
                    case TipoTab.TabItemDeclaracionConstante:
                        t = new TabItemDeclaracionConstante();
                        break;
                    case TipoTab.TabItemFuncion:
                        t = new TabItemFuncion();
                        break;
                    case TipoTab.TabItemProcedimiento:
                        t = new TabItemProcedimiento();
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
                return t;
            }

            return null;
        }

        private void ExecuteDeleteBrand(Tab obj)
        {
            if (this.Brands.Contains(obj))
            {
                this.Brands.Remove(obj);
                //if (this.Brands.Count == 4)
                //{
                //    ModoGrafico.Tabs.EditableTabHeaderControl etc = new ModoGrafico.Tabs.EditableTabHeaderControl();
                //    etc.Content = this.Brands[0].Header;
                //    ModoGrafico.Tabs.EditableTabHeaderControl.ClickEventoFire(etc, null);
                //}

                
                ModoGrafico.Tabs.EditableTabHeaderControl etc = new ModoGrafico.Tabs.EditableTabHeaderControl();
                etc.Content = this.Brands[0].Header;
                ModoGrafico.Tabs.EditableTabHeaderControl.ClickEventoFire(etc, null);
            
                obj = null;
            }
        }

        public Tab ExecuteAddProcedimiento(string obj, bool acomodar, TipoTab tipo, ProcedimientoViewModel proc)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                Tab t = null;
                switch (tipo)
                {
                    case TipoTab.TabItemFuncion:
                        t = new TabItemFuncion(proc);
                        break;
                    case TipoTab.TabItemProcedimiento:
                        t = new TabItemProcedimiento(proc);
                        break;
                    case TipoTab.TabItemPrincipal:
                        t = new TabItemPrincipal(proc);
                        break;
                    case TipoTab.TabItemSalida:
                        t = new TabItemPrincipal(proc);
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

       



    }
}
