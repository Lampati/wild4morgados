using System;
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
using UsingWorkflowItemPresenter.ViewModels;

namespace UsingWorkflowItemPresenter
{
    /// <summary>
    /// Interaction logic for RehostingTabsCustom.xaml
    /// </summary>
    public partial class RehostingTabsCustom : Window
    {
        Secuencia init;
        WorkflowDesigner wd;

        public RehostingTabsCustom()
        {
            InitializeComponent();
            FormattedTabControl.EditableTabHeaderControl.ClickEvento += new FormattedTabControl.EditableTabHeaderControl.ClickHandler(EditableTabHeaderControl_ClickEvento);
        }

        void EditableTabHeaderControl_ClickEvento(object sender, MouseButtonEventArgs e)
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
                              this.ActualizarToolbox(sender);
                          }
                      ));
                  }
              ));

            thread.Start();
        }

        private void ActualizarToolbox(object sender)
        {
            FormattedTabControl.EditableTabHeaderControl t = sender as FormattedTabControl.EditableTabHeaderControl;
            if (Object.Equals(t, null))
                return;

            Toolbox.Categories[0].Tools.Clear();
            if (t.Content.ToString() == "VARIABLES")
            {
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(DeclaracionVariable)));
            }
            else if (t.Content.ToString() == "CONSTANTES")
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(DeclaracionConstante)));
            else
            {
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(DeclaracionVariable)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Si)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mostrar)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mientras)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Asignacion)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(LlamarProcedimiento)));
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
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Si)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mostrar)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mientras)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Asignacion)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(LlamarProcedimiento)));
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
            builder.AddCustomAttributes(typeof(DeclaracionConstante), new DesignerAttribute(typeof(DeclaracionConstanteDesigner)));
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            System.Collections.SortedList sl = new System.Collections.SortedList();

            foreach (Tab t in this.Tabs.tab.ItemSource)
                sl.Add(t.Orden, t);

            foreach (Tab t in sl.Values)
                t.Ejecutar(sb);

            MessageBox.Show(sb.ToString());
        }
    }
}
