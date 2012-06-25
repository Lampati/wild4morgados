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
        private DelegateCommand<Tab> deleteTab;
        private ObservableCollection<Tab> tabs;
        private int cant = 1;

        public WorkAreaViewModel()
            : base()
        {
            ModoGrafico.Tabs.EditableTabHeaderControl.ClickEvento += new ModoGrafico.Tabs.EditableTabHeaderControl.ClickHandler(EditableTabHeaderControl_ClickEvento);
        }

        public void AgregarNuevo()
        {
            this.ExecuteAddTab("Proc/Func" + cant.ToString(), true, TipoTab.TabItemProcedimiento);
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

        public ObservableCollection<Tab> Tabs
        {
            get
            {
                return this.tabs ?? (this.tabs = new ObservableCollection<Tab>());
            }

            set
            {
                if (value != null)
                {
                    this.tabs = value;
                    NotifyPropertyChanged("Tabs");
                }
            }
        }

        public DelegateCommand<Tab> DeleteTab
        {
            get
            {
                return this.deleteTab ?? (this.deleteTab = new DelegateCommand<Tab>(
                                                                                 this.ExecuteDeleteTab,
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

        public Tab ExecuteAddTab(string obj, bool acomodar, TipoTab tipo)
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
                    case TipoTab.TabItemSalida:
                        t = new TabItemSalida();
                        break;
                    case TipoTab.TabItemPrincipal:
                        t = new TabItemPrincipal();
                        break;
                }
                t.Header = obj;
                if (this.Tabs.Count == 0 || !acomodar)
                    this.Tabs.Add(t);
                else
                    this.Tabs.Insert(this.Tabs.Count - 1, t);
                return t;
            }

            return null;
        }

        private void ExecuteDeleteTab(Tab obj)
        {
            if (this.Tabs.Contains(obj))
            {
                this.Tabs.Remove(obj);
                //if (this.Brands.Count == 4)
                //{
                //    ModoGrafico.Tabs.EditableTabHeaderControl etc = new ModoGrafico.Tabs.EditableTabHeaderControl();
                //    etc.Content = this.Brands[0].Header;
                //    ModoGrafico.Tabs.EditableTabHeaderControl.ClickEventoFire(etc, null);
                //}

                
                ModoGrafico.Tabs.EditableTabHeaderControl etc = new ModoGrafico.Tabs.EditableTabHeaderControl();
                etc.Content = this.Tabs[0].Header;
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
                        t = new TabItemSalida(proc);
                        break;
                }           
                t.Header = obj;
                if (this.Tabs.Count == 0 || !acomodar)
                    this.Tabs.Add(t);
                else
                    this.Tabs.Insert(this.Tabs.Count - 1, t);
                return t;
            }

            return null;
        }






        internal Tab ExecuteAddGlobales(string obj, bool acomodar, TipoTab tipoTab, SecuenciaViewModel secuenciaViewModel)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                Tab t = null;
                if (tipoTab == TipoTab.TabItemDeclaracionConstante)
                {
                    t = new TabItemDeclaracionConstante(secuenciaViewModel);
                }
                else if (tipoTab == TipoTab.TabItemDeclaracionVariable)
	            {
                    t = new TabItemDeclaracionVariable(secuenciaViewModel);
	            }

                t.Header = obj;

                if (this.Tabs.Count == 0 || !acomodar)
                    this.Tabs.Add(t);
                else
                    this.Tabs.Insert(this.Tabs.Count - 1, t);
                return t;    
            }
            else
            {
                return null;
            }
        }

        internal void ExecuteAddVariablesGlobales(string obj, bool acomodar, SecuenciaViewModel secuenciaViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
