namespace UsingWorkflowItemPresenter.ViewModels
{
    using System.Collections.ObjectModel;
    using Microsoft.Practices.Prism.Commands;
    using System.Windows;
    using System.Activities.Presentation;
    using System.Activities.Statements;
    using System;
    using LibreriaActividades;
    using System.Activities;
    using System.Threading;

    public class Tab : BaseViewModel
    {
        private WorkflowDesigner wd;
        private string header;
        private Secuencia init;
        AutoResetEvent syncEvent = new AutoResetEvent(false);

        public Tab()
        {
            
        }

        ~Tab()
        {
            this.wd = null;
        }

        public UIElement WF
        {
            get {
                if (this is TabItemAgregar)
                    return null;

                if (Object.Equals(wd, null))
                {
                    wd = new WorkflowDesigner();
                    if (this is TabItemPrincipal)
                    {
                        init = new Secuencia() { DisplayName = "Secuencia Principal" };
                        init.Activities.Add(new LlamarProcedimiento() { NombreProcedimiento = "SALIDA", DisplayName = "Fin Ejecución" });
                    }
                    else
                    {
                        string display = "SECUENCIA";
                        if (this is TabItemDeclaracion)
                            display = "DECLARACION " + this.Header;

                        init = new Secuencia() { DisplayName = display };
                    }

                    System.Threading.Thread thread = new System.Threading.Thread(
                        new System.Threading.ThreadStart(
                          delegate()
                          {
                              Application.Current.Dispatcher.Invoke(
                                System.Windows.Threading.DispatcherPriority.Normal,
                                new Action(
                                  delegate()
                                  {
                                      wd.Load(init);
                                  }
                              ));
                          }
                      ));
                    thread.Start();                    
                }
                return wd.View;
            }
        }

        public string Header
        {
            get
            {
                return this.header;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.header = value;
                    NotifyPropertyChanged("Header");
                }
            }
        }

        protected virtual void RecrearWorkflowDesigner()
        {
            if (Object.Equals(init, null))
                return;

            wd = new WorkflowDesigner();
            wd.Load(init);
        }

        private void RecrearWorkflows()
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
                              this.RecrearWorkflowDesigner();
                          }
                      ));
                  }
              ));

            thread.Start();
        }

        public virtual void Ejecutar()
        {
            if (Object.Equals(init, null))
                return;

            WorkflowApplication app = new WorkflowApplication(init);

            app.Completed = delegate(WorkflowApplicationCompletedEventArgs ea)
            {
                syncEvent.Set();
            };

            app.Run();
            syncEvent.WaitOne();
            RecrearWorkflows();
        }
    }
}
