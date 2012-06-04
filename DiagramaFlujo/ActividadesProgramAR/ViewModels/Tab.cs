﻿namespace UsingWorkflowItemPresenter.ViewModels
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
    using System.Text;
    using System.ComponentModel;
    using System.Windows.Controls;

    public abstract class Tab : BaseViewModel
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
                        init.Activities.Add(new LlamarProcedimiento() { NombreProcedimiento = "SALIDA", DisplayName = "Fin Ejecución", SePuedeEliminar = false });
                    }
                    else
                    {
                        string display = "SECUENCIA";
                        if (this is TabItemDeclaracionVariable || this is TabItemDeclaracionConstante)
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
                                      this.ReconstruirContextMenu(wd);
                                  }
                              ));
                          }
                      ));
                    thread.Start();                    
                }

                return wd.View;
            }
        }

        void ReconstruirContextMenu(WorkflowDesigner wd)
        {
            MenuItem borrar = null;
            MenuItem cortar = null;
            MenuItem copiar = null;
            MenuItem pegar = null;
            for (int i = 0; i < wd.ContextMenu.Items.Count; i++)
            {
                MenuItem mi = wd.ContextMenu.Items[i] as MenuItem;
                if (mi != null && mi.InputGestureText == "Del")
                {
                    borrar = (MenuItem)wd.ContextMenu.Items[i];
                }
                if (mi != null && mi.InputGestureText == "Ctrl+X")
                {
                    cortar = (MenuItem)wd.ContextMenu.Items[i];
                }
                if (mi != null && mi.InputGestureText == "Ctrl+C")
                {
                    copiar = (MenuItem)wd.ContextMenu.Items[i];
                }
                if (mi != null && mi.InputGestureText == "Ctrl+V")
                {
                    pegar = (MenuItem)wd.ContextMenu.Items[i];
                }
            }

            wd.ContextMenu.Items.Clear();

            MenuItem m2 = new MenuItem();
            m2.Icon = cortar.Icon;
            m2.Header = "Cortar";
            m2.InputGestureText = "Ctrl+X";
            m2.Click += new RoutedEventHandler(m2_Click);
            comandoCortar = cortar.Command;
            wd.ContextMenu.Items.Add(m2);

            MenuItem m3 = new MenuItem();
            m3.Icon = copiar.Icon;
            m3.Header = "Copiar";
            m3.InputGestureText = "Ctrl+C";
            m3.Click += new RoutedEventHandler(m3_Click);
            comandoCopiar = copiar.Command;
            wd.ContextMenu.Items.Add(m3);

            MenuItem m4 = new MenuItem();
            m4.Icon = pegar.Icon;
            m4.Header = "Pegar";
            m4.InputGestureText = "Ctrl+V";
            m4.Click += new RoutedEventHandler(m4_Click);
            comandoPegar = pegar.Command;
            wd.ContextMenu.Items.Add(m4);

            Separator s = new Separator();
            wd.ContextMenu.Items.Add(s);

            MenuItem m = new MenuItem();
            m.Icon = borrar.Icon;
            m.Header = "Eliminar";
            m.InputGestureText = "Del";
            m.Click += new RoutedEventHandler(m_Click);
            comandoBorrar = borrar.Command;
            wd.ContextMenu.Items.Add(m);
        }

        System.Windows.Input.ICommand comandoBorrar;
        System.Windows.Input.ICommand comandoCortar;
        System.Windows.Input.ICommand comandoCopiar;
        System.Windows.Input.ICommand comandoPegar;

        void m_Click(object sender, RoutedEventArgs e)
        {
            bool elimina = (bool)((System.Activities.Presentation.WorkflowViewElement)(((System.Windows.Controls.MenuItem)(sender)).CommandTarget)).ModelItem.Properties["SePuedeEliminar"].ComputedValue;
            if (!elimina)
                MessageBox.Show("No se puede eliminar esta actividad.", "Error eliminación", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                comandoBorrar.Execute(null);            
        }

        void m2_Click(object sender, RoutedEventArgs e)
        {
            bool elimina = (bool)((System.Activities.Presentation.WorkflowViewElement)(((System.Windows.Controls.MenuItem)(sender)).CommandTarget)).ModelItem.Properties["SePuedeEliminar"].ComputedValue;
            if (!elimina)
                MessageBox.Show("No se puede cortar esta actividad.", "Error cortar", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                comandoCortar.Execute(null);  
        }

        void m3_Click(object sender, RoutedEventArgs e)
        {
            bool elimina = (bool)((System.Activities.Presentation.WorkflowViewElement)(((System.Windows.Controls.MenuItem)(sender)).CommandTarget)).ModelItem.Properties["SePuedeEliminar"].ComputedValue;
            if (!elimina)
                MessageBox.Show("No se puede copiar esta actividad.", "Error copiar", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                comandoCopiar.Execute(null);  
        }

        void m4_Click(object sender, RoutedEventArgs e)
        {
            comandoPegar.Execute(null);  
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

        public virtual void Ejecutar(StringBuilder sb)
        {
            if (Object.Equals(init, null))
                return;

            foreach (ActividadBase ab in init.Activities)
                ab.Ejecutar(sb);

            /*
            WorkflowApplication app = new WorkflowApplication(init);
            app.Completed = delegate(WorkflowApplicationCompletedEventArgs ea)
            {
                syncEvent.Set();
            };

            app.Run();
            syncEvent.WaitOne();
            RecrearWorkflows();*/
        }

        public abstract int Orden { get; }
    }
}
