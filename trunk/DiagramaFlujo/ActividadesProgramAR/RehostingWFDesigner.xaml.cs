//-------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved
//-------------------------------------------------------------------

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

namespace Microsoft.Samples.UsingWorkflowItemPresenter
{
    public partial class RehostingWfDesigner : Window
    {
        Secuencia init;
        AutoResetEvent syncEvent = new AutoResetEvent(false);
        WorkflowDesigner wd;

        public RehostingWfDesigner()
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
            //init.Branches.Add(new LlamarProcedimiento() { NombreProcedimiento = "SALIDA", DisplayName = "Fin Ejecución" });

            CreateDesigner();
     
        }

        void CreateDesigner()
        {
            // create the workflow designer
            wd = new WorkflowDesigner();
            wd.Load(init);
            DesignerBorder.Child = wd.View;
            PropertyBorder.Child = wd.PropertyInspectorView;
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

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            LibreriaActividades.Extension.Code = null;
            WorkflowApplication app = new WorkflowApplication(init);

            app.Completed = delegate(WorkflowApplicationCompletedEventArgs ea)
            {
                syncEvent.Set();
            };

            app.Run();
            syncEvent.WaitOne();
            MessageBox.Show(LibreriaActividades.Extension.Code.ToString());
            CreateDesigner();
        }
    }
}
