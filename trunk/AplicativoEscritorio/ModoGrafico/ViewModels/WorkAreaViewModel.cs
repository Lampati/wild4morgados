namespace ModoGrafico.ViewModels
{
    using System.Collections.ObjectModel;
    using Microsoft.Practices.Prism.Commands;
    using ModoGrafico.Views;
    using System;
    using ModoGrafico.Enums;
    using ModoGrafico.Tabs;
using InterfazTextoGrafico;
    using ModoGrafico.EventArgsClasses;
    using System.Collections.Generic;

    public class WorkAreaViewModel : BaseViewModel
    {
        public delegate void WorkflowChangedEventHandler(object o, WorkflowChangedEventArgs args);
        public event WorkflowChangedEventHandler WorkflowChangedEvent;

        private DelegateCommand<Tab> deleteTab;
        private ObservableCollection<Tab> tabs;
        private int cant = 1;

        public WorkAreaViewModel()
            : base()
        {
            ModoGrafico.Tabs.EditableTabHeaderControl.ClickEvento += new ModoGrafico.Tabs.EditableTabHeaderControl.ClickHandler(EditableTabHeaderControl_ClickEvento);
            ModoGrafico.Tabs.EditableTabHeaderControl.PropertiesClickEvento += new EditableTabHeaderControl.HeaderPropertiesClickedHandler(EditableTabHeaderControl_PropertiesClickEvento);
        }

        void EditableTabHeaderControl_PropertiesClickEvento(object sender, HeaderPropertiesClickedEventArgs e)
        {
            Tab tabElegido = ObtenerTab(e.NombreContexto);
        }

        public void AgregarNuevo(string nombre, TipoTab tipoTab)
        {
            this.ExecuteAddTab(nombre, true, tipoTab);
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
         
            Tab t = null;
            switch (tipo)
            {
                case TipoTab.TabItemAgregarProcedimiento:
                    t = new TabItemAgregarProcedimiento();
                    break;
                case TipoTab.TabItemAgregarFuncion:
                    t = new TabItemAgregarFuncion();
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
            {
                this.Tabs.Add(t);
            }
            else
            { 
                this.Tabs.Insert(this.Tabs.Count - 2, t); 
            }
            return t;
            

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
                        t.WorkflowChangedEvent += new Tab.WorkflowChangedEventHandler(t_WorkflowChangedEvent);
                        break;
                    case TipoTab.TabItemProcedimiento:
                        t = new TabItemProcedimiento(proc);
                        t.WorkflowChangedEvent += new Tab.WorkflowChangedEventHandler(t_WorkflowChangedEvent);
                        break;
                    case TipoTab.TabItemPrincipal:
                        t = new TabItemPrincipal(proc);
                        t.WorkflowChangedEvent += new Tab.WorkflowChangedEventHandler(t_WorkflowChangedEvent);
                        break;
                    case TipoTab.TabItemSalida:
                        t = new TabItemSalida(proc);
                        t.WorkflowChangedEvent += new Tab.WorkflowChangedEventHandler(t_WorkflowChangedEvent);
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
                    t.WorkflowChangedEvent += new Tab.WorkflowChangedEventHandler(t_WorkflowChangedEvent);
                }
                else if (tipoTab == TipoTab.TabItemDeclaracionVariable)
	            {
                    t = new TabItemDeclaracionVariable(secuenciaViewModel);
                    t.WorkflowChangedEvent += new Tab.WorkflowChangedEventHandler(t_WorkflowChangedEvent);
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

        void t_WorkflowChangedEvent(object o, WorkflowChangedEventArgs args)
        {
            WorkflowChangedEventFire(this, new WorkflowChangedEventArgs());
        }
        
        private void WorkflowChangedEventFire(object tab, WorkflowChangedEventArgs workflowChangedEventArgs)
        {
            if (WorkflowChangedEvent != null)
            {
                WorkflowChangedEvent(tab, workflowChangedEventArgs);
            }
        }

        internal Tab ObtenerTab(string procedimiento)
        {
            Tab tabElegido;
            List<Tab> lista = new List<Tab>(Tabs);

            if (procedimiento.ToUpper().Trim().Equals("PRINCIPAL"))
            {
                tabElegido = lista.Find(x => x.GetType() == typeof(TabItemPrincipal));
            }
            else if (procedimiento.ToUpper().Trim().Equals("SALIDA"))
            {
                tabElegido = lista.Find(x => x.GetType() == typeof(TabItemSalida));
            }
            else if (procedimiento.ToUpper().Trim().Equals("DECLARACIONCONSTANTE"))
            {
                tabElegido = lista.Find(x => x.GetType() == typeof(TabItemDeclaracionConstante));
            }
            else if (procedimiento.ToUpper().Trim().Equals("DECLARACIONVARIABLE"))
            {
                tabElegido = lista.Find(x => x.GetType() == typeof(TabItemDeclaracionVariable));
            }
            else
            {
                tabElegido = lista.Find(x => x.Header.ToUpper().Trim().Equals(procedimiento.ToUpper().Trim()));
            }

            return tabElegido;
        }
    }
}
