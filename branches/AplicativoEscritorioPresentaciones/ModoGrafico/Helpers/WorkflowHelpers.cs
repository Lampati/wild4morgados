using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Presentation.Services;
using System.Activities.Debugger;
using System.Activities.Presentation.View;
using System.ComponentModel.Design;
using LibreriaActividades;
using System.Activities.Presentation.Model;
using System.Windows.Input;

namespace ModoGrafico.Helpers
{
    public static class WorkflowHelpers
    {
        public static Activity GetActivity(WorkflowDesigner workflowDesigner)
        {
            var root = GetRootElement(workflowDesigner);
            var activity = GetRootWorkflow(root);
            return activity;
        }

        private static object GetRootElement(WorkflowDesigner workflowDesigner)
        {
            var modelService = workflowDesigner.Context.Services.GetService<ModelService>();
            if (modelService != null)
            {
                
                return modelService.Root.GetCurrentValue();
            }
            return null;
        }

        public static Activity GetRootWorkflow(object rootModelObject)
        {
            Activity activity;
            var debuggableWorkflowTree = rootModelObject as IDebuggableWorkflowTree;
            
            if (debuggableWorkflowTree != null)
            {
                
                activity = debuggableWorkflowTree.GetWorkflowRoot();
            }
            else
            {
                activity = rootModelObject as Activity;
            }
            return activity;
        }

        public static ActivityDesignerBase GetDesignerViewForActivity(WorkflowDesigner workflowDesigner, ActividadBase actividad)
        {
            var modelService = workflowDesigner.Context.Services.GetService<ModelService>();

            if (modelService != null)
            {
                foreach (var item in modelService.Find(modelService.Root, actividad.GetType()))
                {
                    if (Convert.ToInt64(item.Properties["IdPropio"].Value.ToString()) == actividad.IdPropio)
                    {

                    }
                     
                }
                
            }
            return null;
        }

        public static void GetDesignerView(WorkflowDesigner workflowDesigner)
        {
            var modelService1 = workflowDesigner.Context.Services.GetService<DesignerView>();
            var modelService2 = workflowDesigner.Context.Services.GetService<ModelService>();
            var modelService3 = workflowDesigner.Context.Services.GetService<ViewStateService>();
            var modelService4 = workflowDesigner.Context.Services.GetService<ModelTreeManager>();
        }




        public static void MakeWorkflowViewFitScreen(WorkflowDesigner workflowDesigner)
        {
            DesignerView designer = workflowDesigner.Context.Services.GetService<DesignerView>();
            ((RoutedCommand)DesignerView.FitToScreenCommand).Execute(null, designer);
        }

        public static void MakeWorkflowZoomTo(WorkflowDesigner workflowDesigner)
        {
            DesignerView designer = workflowDesigner.Context.Services.GetService<DesignerView>();
            ((RoutedCommand)DesignerView.ZoomInCommand).Execute(null, designer);
        }

        public static void SelectWorkflow(WorkflowDesigner workflowDesigner)
        {
            DesignerView designer = workflowDesigner.Context.Services.GetService<DesignerView>();
            ((RoutedCommand)DesignerView.SelectAllCommand).Execute(null, designer);
        }


        public static void Bla(WorkflowDesigner workflowDesigner)
        {
            DesignerView view = workflowDesigner.Context.Services.GetService<DesignerView>();

            

            ActivityDesigner bla = new ActivityDesigner();
            bla.BringIntoView();
            //view.MoveFocus(
            

        }
    }
}
