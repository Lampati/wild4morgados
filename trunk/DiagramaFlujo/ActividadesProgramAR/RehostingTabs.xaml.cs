using System;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System.ComponentModel;
using System.Windows;
using LibreriaActividades;
using System.Activities;
using System.Threading;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Microsoft.Samples
{
    /// <summary>
    /// Interaction logic for RehostingTabs.xaml
    /// </summary>
    public partial class RehostingTabs : Window
    {
        Secuencia init;
        WorkflowDesigner wd;

        public RehostingTabs()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            // register metadata
            (new DesignerMetadata()).Register();
            RegisterCustomMetadata();
            // add custom activity to toolbox
            Toolbox.Categories.Add(new ToolboxCategory("Actividades GarGar"));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Si)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mostrar)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mientras)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Asignacion)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(LlamarProcedimiento)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Secuencia)));
            Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Declaracion)));

            init = new Secuencia() { DisplayName = "Secuencia Principal" };
            init.Activities.Add(new LlamarProcedimiento() { NombreProcedimiento = "SALIDA", DisplayName = "Fin Ejecución" });

            CreateDesigner();
        }

        private WorkflowDesigner CrearDesigner()
        {
            WorkflowDesigner w = new WorkflowDesigner();
            Secuencia sec = new Secuencia();
            w.Load(sec);
            return w;
        }

        void CreateDesigner()
        {
            // create the workflow designer
            wd = new WorkflowDesigner();
            wd.Load(init);
            PropertyBorder.Child = wd.PropertyInspectorView;

            TabItemPrincipal tPrincipal = new TabItemPrincipal();
            tPrincipal.Content = wd.View;
            tPrincipal.Header = "Actividad Principal";
            tPrincipal.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(t_MouseLeftButtonUp);
            this.tabActividades.Items.Add(tPrincipal);

            TabItemDeclaracion tConstantes = new TabItemDeclaracion();
            tConstantes.Content = CrearDesigner().View;
            tConstantes.Header = "Constantes";
            tConstantes.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(t_MouseLeftButtonUp);
            this.tabActividades.Items.Add(tConstantes);

            TabItemDeclaracion tVariables = new TabItemDeclaracion();
            tVariables.Content = CrearDesigner().View;
            tVariables.Header = "Variables";
            tVariables.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(t_MouseLeftButtonUp);
            this.tabActividades.Items.Add(tVariables);

            TabItem t = new TabItem();
            t.Header = " + ";
            t.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(t_MouseDown);
            this.tabActividades.Items.Add(t);
        }

        int i = 1;

        void t_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TabItemFuncionProcedimiento t = new TabItemFuncionProcedimiento();
            t.Header = "Func./Proc. " + i.ToString();
            t.Content = CrearDesigner().View;
            t.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(t_MouseLeftButtonUp);
            this.tabActividades.Items.Insert(this.tabActividades.Items.Count - 1, t);
            t.Focus();
            t_MouseLeftButtonUp(t, e);
            i++;
        }

        void t_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TabItemDeclaracion t = sender as TabItemDeclaracion;
            Toolbox.Categories[0].Tools.Clear();
            if (t != null)
            {
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Declaracion)));
            }
            else
            {
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Si)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mostrar)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Mientras)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Asignacion)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(LlamarProcedimiento)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Secuencia)));
                Toolbox.Categories[0].Add(new ToolboxItemWrapper(typeof(Declaracion)));
            }
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
            builder.AddCustomAttributes(typeof(Declaracion), new DesignerAttribute(typeof(DeclaracionDesigner)));
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
