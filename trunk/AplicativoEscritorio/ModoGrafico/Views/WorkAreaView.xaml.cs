namespace ModoGrafico.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using ModoGrafico.ViewModels;
    using ModoGrafico.Enums;
    using ModoGrafico.EventArgsClasses;
    using InterfazTextoGrafico;
    using System.Collections.Generic;
    using ModoGrafico.Tabs;
    using ModoGrafico.Helpers;
    using System;
    using LibreriaActividades;
    using System.Activities;

    /// <summary>
    /// Interaction logic for BrandView.xaml
    /// </summary>
    public partial class WorkAreaView : UserControl
    {
        public delegate void TipoTabCambiadoEventHandler(object o, TipoTabCambiadoEventArgs e);
        public event TipoTabCambiadoEventHandler CambioTabEvent;

        public delegate void WorkflowChangedEventHandler(object o, WorkflowChangedEventArgs args);
        public event WorkflowChangedEventHandler WorkflowChangedEvent;

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
            ((WorkAreaViewModel)this.DataContext).WorkflowChangedEvent += new WorkAreaViewModel.WorkflowChangedEventHandler(WorkAreaView_WorkflowChangedEvent);

            ProcedimientoViewModel procPrincipal = programa.Procedimientos.Find(y => y.Tipo == InterfazTextoGrafico.Enums.TipoRutina.Principal);
            ProcedimientoViewModel procSalida = programa.Procedimientos.Find(y => y.Tipo == InterfazTextoGrafico.Enums.TipoRutina.Salida);
            List<ProcedimientoViewModel> listaProcs = programa.Procedimientos.FindAll(y => y.Tipo != InterfazTextoGrafico.Enums.TipoRutina.Principal
                                                      && y.Tipo != InterfazTextoGrafico.Enums.TipoRutina.Salida);

            ((WorkAreaViewModel)this.DataContext).ExecuteAddProcedimiento("PRINCIPAL", false, TipoTab.TabItemPrincipal, procPrincipal);
            ((WorkAreaViewModel)this.DataContext).ExecuteAddProcedimiento("SALIDA", false, TipoTab.TabItemSalida, procSalida);
            ((WorkAreaViewModel)this.DataContext).ExecuteAddGlobales("CONSTANTES", false, TipoTab.TabItemDeclaracionConstante, programa.ConstantesGlobales);
            ((WorkAreaViewModel)this.DataContext).ExecuteAddGlobales("VARIABLES", false, TipoTab.TabItemDeclaracionVariable, programa.VariablesGlobales);

            foreach (var item in listaProcs)
            {
                ((WorkAreaViewModel)this.DataContext).ExecuteAddProcedimiento(item.Nombre.ToUpper(), false, TipoTab.TabItemProcedimiento, item);
            }

            ((WorkAreaViewModel)this.DataContext).ExecuteAddTab("     +     ", false, TipoTab.TabItemAgregar);
            
        }

        void WorkAreaView_WorkflowChangedEvent(object o, WorkflowChangedEventArgs args)
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

        public ProgramaViewModel ObtenerProgramaDiagramado()
        {
            WorkAreaViewModel workArea = this.DataContext as WorkAreaViewModel;

            ProgramaViewModel programa = new ProgramaViewModel();

            if (workArea != null)
            {
                foreach (Tab item in workArea.Tabs)
                {
                    switch (item.Tipo)
                    {
                        case TipoTab.TabItemPrincipal:
                            ProcedimientoViewModel activ = new ProcedimientoViewModel();
                            activ.Nombre = item.Header;
                            activ.Tipo = InterfazTextoGrafico.Enums.TipoRutina.Principal;
                            if (item.SecuenciaInicialProcedimiento != null)
                            {
                                activ.VariablesLocales = item.SecuenciaInicialDeclaraciones.Datos as SecuenciaViewModel;
                                activ.Cuerpo = item.SecuenciaInicialProcedimiento.Datos as SecuenciaViewModel;
                            }
                            else
                            {
                                if (item.actividadViewModel != null)
                                {
                                    activ.VariablesLocales = ((ProcedimientoViewModel)item.actividadViewModel).VariablesLocales;
                                    activ.Cuerpo = ((ProcedimientoViewModel)item.actividadViewModel).Cuerpo;
                                }
                                else
                                {
                                    activ.VariablesLocales = new SecuenciaViewModel();
                                    activ.Cuerpo = new SecuenciaViewModel();
                                }
                            }
                            programa.Procedimientos.Add(activ as ProcedimientoViewModel);
                            break;
                        case TipoTab.TabItemDeclaracionVariable:
                            if (item.SecuenciaInicialProcedimiento != null)
                            {
                                programa.VariablesGlobales = item.SecuenciaInicialProcedimiento.Datos as SecuenciaViewModel;
                            }
                            else
                            {
                                programa.VariablesGlobales = item.actividadViewModel as SecuenciaViewModel;
                            }
                            break;
                        case TipoTab.TabItemDeclaracionConstante:
                            if (item.SecuenciaInicialProcedimiento != null)
                            {
                                programa.ConstantesGlobales = item.SecuenciaInicialProcedimiento.Datos as SecuenciaViewModel;
                            }
                            else
                            {
                                programa.ConstantesGlobales = item.actividadViewModel as SecuenciaViewModel;
                            }
                            break;
                        case TipoTab.TabItemFuncion:
                            activ = new ProcedimientoViewModel();
                            activ.Nombre = item.Header;
                            activ.Tipo = InterfazTextoGrafico.Enums.TipoRutina.Funcion;
                            if (item.SecuenciaInicialProcedimiento != null)
                            {
                                activ.VariablesLocales = item.SecuenciaInicialDeclaraciones.Datos as SecuenciaViewModel;
                                activ.Cuerpo = item.SecuenciaInicialProcedimiento.Datos as SecuenciaViewModel;
                            }
                            else
                            {
                                if (item.actividadViewModel != null)
                                {
                                    activ.VariablesLocales = ((ProcedimientoViewModel)item.actividadViewModel).VariablesLocales;
                                    activ.Cuerpo = ((ProcedimientoViewModel)item.actividadViewModel).Cuerpo;
                                }
                                else
                                {
                                    activ.VariablesLocales = new SecuenciaViewModel();
                                    activ.Cuerpo = new SecuenciaViewModel();
                                }
                            }
                            programa.Procedimientos.Add(activ as ProcedimientoViewModel);
                            break;
                        case TipoTab.TabItemProcedimiento:
                            activ = new ProcedimientoViewModel();
                            activ.Nombre = item.Header;
                            activ.Tipo = InterfazTextoGrafico.Enums.TipoRutina.Procedimiento;
                            if (item.SecuenciaInicialProcedimiento != null)
                            {
                                activ.VariablesLocales = item.SecuenciaInicialDeclaraciones.Datos as SecuenciaViewModel;
                                activ.Cuerpo = item.SecuenciaInicialProcedimiento.Datos as SecuenciaViewModel;
                            }
                            else
                            {
                                if (item.actividadViewModel != null)
                                {
                                    activ.VariablesLocales = ((ProcedimientoViewModel)item.actividadViewModel).VariablesLocales;
                                    activ.Cuerpo = ((ProcedimientoViewModel)item.actividadViewModel).Cuerpo;
                                }
                                else
                                {
                                    activ.VariablesLocales = new SecuenciaViewModel();
                                    activ.Cuerpo = new SecuenciaViewModel();
                                }
                            }
                            programa.Procedimientos.Add(activ as ProcedimientoViewModel);
                            break;
                        case TipoTab.TabItemSalida:
                            activ = new ProcedimientoViewModel();
                            activ.Nombre = item.Header;
                            activ.Tipo = InterfazTextoGrafico.Enums.TipoRutina.Salida;
                            if (item.SecuenciaInicialProcedimiento != null)
                            {
                                activ.VariablesLocales = item.SecuenciaInicialDeclaraciones.Datos as SecuenciaViewModel;
                                activ.Cuerpo = item.SecuenciaInicialProcedimiento.Datos as SecuenciaViewModel;
                            }
                            else
                            {
                                if (item.actividadViewModel != null)
                                {
                                    activ.VariablesLocales = ((ProcedimientoViewModel)item.actividadViewModel).VariablesLocales;
                                    activ.Cuerpo = ((ProcedimientoViewModel)item.actividadViewModel).Cuerpo;
                                }
                                else
                                {
                                    activ.VariablesLocales = new SecuenciaViewModel();
                                    activ.Cuerpo = new SecuenciaViewModel();
                                }
                            }
                            programa.Procedimientos.Add(activ as ProcedimientoViewModel);
                            break;
                        case TipoTab.TabItemAgregar:
                            break;
                        default:
                            break;
                    }
                }
            }

            return programa;
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

        internal void PonerFocoEnActividad(string procedimiento, LibreriaActividades.ActividadBase actividad)
        {
            WorkAreaViewModel workAreaVM = this.DataContext as WorkAreaViewModel;
            List<Tab> lista = new List<Tab>(workAreaVM.Tabs);
            Tab tabElegido;

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

            Type tipoActividad = actividad.GetType();
            Activity actividadRoot;

            if (tipoActividad == typeof(DeclaracionVariable)
                || tipoActividad == typeof(DeclaracionConstante)
                || tipoActividad == typeof(DeclaracionArreglo)
                )
            {
                actividadRoot = WorkflowHelpers.GetActivity(tabElegido.WorkflowDesignerDeclaraciones);
            }
            else
            {
                actividadRoot = WorkflowHelpers.GetActivity(tabElegido.WorkflowDesigner);
            }

            //no anda bien pq no se generan siempre los ID de una
            //Activity actAPonerFoco = (WorkflowInspectionServices.Resolve(actividadRoot, actividad.Id));

            
            

            List<Activity> listaActividades = new List<Activity>((WorkflowInspectionServices.GetActivities(actividadRoot)));

            
        }
    }
}
