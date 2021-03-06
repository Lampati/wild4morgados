﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LibreriaActividades;
using System.Activities.Presentation;
using System.Activities.Presentation.Toolbox;
using System.Activities.Core.Presentation;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using ModoGrafico.ViewModels;
using ModoGrafico.Enums;
using InterfazTextoGrafico;
using ModoGrafico.EventArgsClasses;
using ModoGrafico.Tabs;
using System.Text.RegularExpressions;
using ModoGrafico.Helpers;

namespace ModoGrafico
{
    /// <summary>
    /// Interaction logic for RehostingTabsCustom.xaml
    /// </summary>
    public partial class ProgramadorGrafico : UserControl
    {
        public delegate void ModoGraficoModificadoEventHandler(object o, ModoGraficoModificadoEventArgs e);
        public event ModoGraficoModificadoEventHandler ModoGraficoModificadoEvent;

        public Window Owner { get; set; }

        public static void ReiniciarContadoresId()
        {
            ActividadViewModelBase.ReiniciarContadorGlobal();
        }

        public ProgramadorGrafico()
        {
            InitializeComponent();
            this.WorkArea.CambioTabEvent += new Views.WorkAreaView.TipoTabCambiadoEventHandler(WorkArea_CambioTabEvent);
            this.WorkArea.WorkflowChangedEvent += new Views.WorkAreaView.WorkflowChangedEventHandler(WorkArea_WorkflowChangedEvent);

            this.WorkArea.Owner = this.Owner;

            this.Loaded += new RoutedEventHandler(ProgramadorGrafico_Loaded);
            //ModoGrafico.Tabs.EditableTabHeaderControl.ClickEvento += new ModoGrafico.Tabs.EditableTabHeaderControl.ClickHandler(EditableTabHeaderControl_ClickEvento);
        }

        void ProgramadorGrafico_Loaded(object sender, RoutedEventArgs e)
        {
            // hide the search box
            TextBox searchBox = Toolbox.Template.FindName("PART_SearchBox", Toolbox) as TextBox;
            searchBox.Visibility = System.Windows.Visibility.Collapsed;

            // hide the hint text, which is a TextBlock
            Grid grid = VisualTreeHelper.GetParent(searchBox) as Grid;
            foreach (UIElement item in grid.Children)
            {
                if (item is TextBlock)
                {
                    item.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                }
            }
        }

        void WorkArea_WorkflowChangedEvent(object o, WorkflowChangedEventArgs args)
        {
            ModoGraficoModificadoEventFire(this, new ModoGraficoModificadoEventArgs());
        }

        private void ModoGraficoModificadoEventFire(object sender, ModoGraficoModificadoEventArgs e)
        {
            if (ModoGraficoModificadoEvent != null)
            {
                ModoGraficoModificadoEvent(sender, e);
            }
        }

        public void CargarProgramaEnModoGrafico(ProgramaViewModel programa)
        {
            if (programa != null)
            {
                this.WorkArea.CargarPrograma(programa);

                //TabsControl.PreloadTabs(this.WorkArea.tab.tc);

                TabsControl.PreloadTabsSimple(this.WorkArea.tab.tc);
                
            }
        }

        public ProgramaViewModel ObtenerProgramaEnModoGrafico()
        {
            CambiarFocoActualParaTomarCambios();

          

            ProgramaViewModel programa = this.WorkArea.ObtenerProgramaDiagramado();
            programa.OrdenarProcedimientos();
            programa.CalcularLineasYAsignarContextoAHijos(1, "PROGRAMA");

            return programa;
        }

        private void CambiarFocoActualParaTomarCambios()
        {
            // Gets the element with keyboard focus.
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            // Change keyboard focus.
            if (elementWithFocus != null)
            {
                elementWithFocus.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            }
        }

        void WorkArea_CambioTabEvent(object sender, EventArgsClasses.TipoTabCambiadoEventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(
                new System.Threading.ThreadStart(
                  delegate()
                  {
                      Application.Current.Dispatcher.Invoke(
                        System.Windows.Threading.DispatcherPriority.Normal,
                        new Action(
                          delegate()
                          {
                              this.ActualizarToolbox(sender, e.Tipo );
                          }
                      ));
                  }
              ));

            thread.Start();

        }

   

        private void ActualizarToolbox(object sender, TipoTab tipoDeTab)
        {
           

            Toolbox.Categories[0].Tools.Clear();
            if (tipoDeTab == TipoTab.TabItemDeclaracionVariable)
            {
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(DeclaracionVariable)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(DeclaracionArreglo)));
            }
            else if (tipoDeTab == TipoTab.TabItemDeclaracionConstante)
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(DeclaracionConstante)));
            else
            {
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(DeclaracionVariable)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(DeclaracionArreglo)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Si)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mostrar)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mientras)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Asignacion)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(LlamarProcedimiento)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Leer)));
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            // register metadata
            (new DesignerMetadata()).Register();
            RegisterCustomMetadata();
            // add custom activity to toolbox
            Toolbox.Categories.Add(new ToolboxCategory("Actividades GarGar"));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(DeclaracionVariable)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(DeclaracionArreglo)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Si)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mostrar)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mientras)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Asignacion)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(LlamarProcedimiento)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Leer)));


            //pruebas transformacion
            //SecuenciaViewModel secuenciaOrig = new SecuenciaViewModel();
            //secuenciaOrig.ListaActividades.Add(new MientrasViewModel() { Condicion = "buenos dias", Cuerpo = null });

            //Secuencia sec = new Secuencia();
            //sec.AsignarDatos(secuenciaOrig);

            //int i = 0;
        }

        private WorkflowDesigner CrearDesigner()
        {
            WorkflowDesigner w = new WorkflowDesigner();
            Secuencia sec = new Secuencia();
            w.Load(sec);
            return w;
        }

        void RegisterCustomMetadata()
        {
            AttributeTableBuilder builder = new AttributeTableBuilder();
            builder.AddCustomAttributes(typeof(Si), new DesignerAttribute(typeof(SiDesigner)));
            builder.AddCustomAttributes(typeof(Mostrar), new DesignerAttribute(typeof(MostrarDesigner)));
            builder.AddCustomAttributes(typeof(Mientras), new DesignerAttribute(typeof(MientrasDesigner)));
            builder.AddCustomAttributes(typeof(Asignacion), new DesignerAttribute(typeof(AsignacionDesigner)));
            builder.AddCustomAttributes(typeof(LlamarProcedimiento), new DesignerAttribute(typeof(LlamarProcedimientoDesigner)));
            builder.AddCustomAttributes(typeof(Secuencia), new DesignerAttribute(typeof(SecuenciaDesigner)));
            builder.AddCustomAttributes(typeof(DeclaracionVariable), new DesignerAttribute(typeof(DeclaracionVariableDesigner)));
            builder.AddCustomAttributes(typeof(DeclaracionArreglo), new DesignerAttribute(typeof(DeclaracionArregloDesigner)));
            builder.AddCustomAttributes(typeof(DeclaracionConstante), new DesignerAttribute(typeof(DeclaracionConstanteDesigner)));
            builder.AddCustomAttributes(typeof(Leer), new DesignerAttribute(typeof(LeerDesigner)));
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        public void PonerFocoEnActividad(string procedimiento, object actividadBase)
        {
            WorkArea.PonerFocoEnActividad(procedimiento, actividadBase as ActividadBase);
        }


        public void PonerFocoEnTab(string procedimiento)
        {
            WorkArea.PonerFocoEnTab(procedimiento);
        }
    }
}
