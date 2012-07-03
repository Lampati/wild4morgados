using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Presentation.Services;
using System.Activities.Debugger;
using System.Activities.Presentation.View;

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


        public static void Bla(WorkflowDesigner workflowDesigner)
        {
            DesignerView view = workflowDesigner.Context.Services.GetService<DesignerView>();

            

            ActivityDesigner bla = new ActivityDesigner();
            bla.BringIntoView();
            //view.MoveFocus(
            

        }
    }
}
