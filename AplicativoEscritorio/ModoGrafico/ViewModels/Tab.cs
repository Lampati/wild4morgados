﻿namespace ModoGrafico.ViewModels
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
    using System.Collections.Generic;
    using ModoGrafico.Tabs;
using InterfazTextoGrafico;
using ModoGrafico.Enums;
    using ModoGrafico.EventArgsClasses;
    using ModoGrafico.Helpers;
using ModoGrafico.Interfaces;
    using InterfazTextoGrafico.Enums;
    using System.Activities.Presentation.Services;
    using System.Activities.Presentation.Model;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using System.Activities.Presentation.View;

    public abstract class Tab : BaseViewModel, IPropiedadesContexto 
    {
        public static int _generadorId = 0;

       

        public delegate void WorkflowChangedEventHandler(object o, WorkflowChangedEventArgs args);
        public event WorkflowChangedEventHandler WorkflowChangedEvent;

        private WorkflowDesigner wd;
        private WorkflowDesigner wdDecl;
        protected string header;
        internal Secuencia SecuenciaInicialProcedimiento {get; set;}
        internal Secuencia SecuenciaInicialDeclaraciones {get; set;}
        AutoResetEvent syncEvent = new AutoResetEvent(false);
        internal ActividadViewModelBase actividadViewModel {get; set;}
        internal TipoTab Tipo { get; set; }

        private string nombre;
        private TipoDato tipoRetorno;
        private string retorno;
        private ObservableCollection<ParametroViewModel> parametros = new ObservableCollection<ParametroViewModel>();

        private int tabId;
        public int TabId
        {
            get
            {
                return tabId;
            }
        }

            


        public string Nombre
        {
            get
            {
                return header;
            }
            set
            {

                if (!string.IsNullOrEmpty(value))
                {
                    this.header = value;
                    NotifyPropertyChanged("Header");
                    NotifyPropertyChanged("Nombre");
                }
            }
        }

        public TipoDato TipoRetorno
        {
            get
            {
                return tipoRetorno;
            }
            set
            {
                tipoRetorno = value;

                NotifyPropertyChanged("TipoRetorno");
            }
        }

        public string Retorno
        {
            get
            {
                return retorno;
            }
            set
            {
               
                this.retorno = value;
                NotifyPropertyChanged("Retorno");
                
            }
        }

        public ObservableCollection<ParametroViewModel> Parametros
        {
            get
            {
                return parametros;
            }
            set
            {
                parametros = value;
            }
        }
        

        public Tab()
        {
            
            parametros = new ObservableCollection<ParametroViewModel>();
            tabId = ++_generadorId;

           
        }

        ~Tab()
        {
            this.wd = null;
            this.wdDecl = null;
        }

        public WorkflowDesigner WorkflowDesigner
        {
            get
            {
                return wd;
            }
        }

        public WorkflowDesigner WorkflowDesignerDeclaraciones
        {
            get
            {
                return wdDecl;
            }
        }

        public UIElement WF
        {
            get 
            {
                if (this is TabItemAgregarProcedimiento || this is TabItemAgregarFuncion)
                    return null;

                if (Object.Equals(wd, null))
                {
                    wd = new WorkflowDesigner();
                    wd.ModelChanged += new EventHandler(wd_ModelChanged);
                    
                    
                    if (this is TabItemPrincipal)
                    {
                        //init = new Secuencia() { DisplayName = "Secuencia Principal", AdmiteDeclaraciones = false };
                        //init.Activities.Add(new LlamarProcedimiento() { NombreProcedimiento = "SALIDA", DisplayName = "Fin Ejecución", SePuedeEliminar = false });

                        if (actividadViewModel != null)
                        {
                            ProcedimientoViewModel procViewModel = actividadViewModel as ProcedimientoViewModel;

                            Secuencia aux = new Secuencia() { DisplayName = procViewModel.Nombre, AdmiteDeclaraciones = false };
                            aux.AsignarDatos(procViewModel.Cuerpo);

                            SecuenciaInicialProcedimiento = aux;
                        }
                        else
                        {
                            SecuenciaInicialProcedimiento = new Secuencia() { AdmiteDeclaraciones = false };
                            LlamarProcedimiento llamadaSalida = new LlamarProcedimiento() { NombreProcedimiento = "SALIDA" };
                            llamadaSalida.ReasignarId();
                            SecuenciaInicialProcedimiento.Activities.Add(llamadaSalida);
                        }
                    }
                    else if (this is TabItemSalida)
                    {
                        //init = new Secuencia() { DisplayName = "Secuencia Principal", AdmiteDeclaraciones = false };
                        //init.Activities.Add(new LlamarProcedimiento() { NombreProcedimiento = "SALIDA", DisplayName = "Fin Ejecución", SePuedeEliminar = false });

                        if (actividadViewModel != null)
                        {
                            ProcedimientoViewModel procViewModel = actividadViewModel as ProcedimientoViewModel;

                            Secuencia aux = new Secuencia() { DisplayName = procViewModel.Nombre, AdmiteDeclaraciones = false, SePuedeEliminar = false };
                            aux.AsignarDatos(procViewModel.Cuerpo);
                            
                            SecuenciaInicialProcedimiento = aux;
                        }
                        else
                        {
                            SecuenciaInicialProcedimiento = new Secuencia() { AdmiteDeclaraciones = false, SePuedeEliminar = false };
                        }
                    }
                    else if (this is TabItemProcedimiento || this is TabItemFuncion)
                    {
                        if (actividadViewModel != null)
                        {
                            ProcedimientoViewModel procViewModel = actividadViewModel as ProcedimientoViewModel;

                            Secuencia aux = new Secuencia() { DisplayName = procViewModel.Nombre, AdmiteDeclaraciones = false, SePuedeEliminar = false };
                            aux.AsignarDatos(procViewModel.Cuerpo);

                            SecuenciaInicialProcedimiento = aux;
                        }
                        else
                        {
                            SecuenciaInicialProcedimiento = new Secuencia() { DisplayName = this.header, AdmiteDeclaraciones = false, SePuedeEliminar = false };
                        }
                    }
                    else if (this is TabItemDeclaracionConstante || this is TabItemDeclaracionVariable)
                    {
                        if (actividadViewModel != null)
                        {
                            SecuenciaViewModel secViewModel = actividadViewModel as SecuenciaViewModel;

                            Secuencia aux = new Secuencia() { DisplayName = "DECLARACIONES " + this.Header, AdmiteDeclaraciones = true };
                            aux.AsignarDatos(secViewModel);

                            SecuenciaInicialProcedimiento = aux;
                        }
                        else
                        {
                            string display = "SECUENCIA";
                            bool admiteDeclaraciones = false;
                            if (this is TabItemDeclaracionVariable || this is TabItemDeclaracionConstante)
                            {
                                display = "DECLARACIONES " + this.Header;
                                admiteDeclaraciones = true;
                            }

                            SecuenciaInicialProcedimiento = new Secuencia() { DisplayName = display, AdmiteDeclaraciones = admiteDeclaraciones };
                        }
                    }

                    System.Threading.Thread thread = new System.Threading.Thread(
                        new System.Threading.ThreadStart(
                          delegate()
                          {
                              Application.Current.Dispatcher.Invoke(
                                System.Windows.Threading.DispatcherPriority.Normal,
                                new Action(delegate()
                                    {
                                        wd.Load(SecuenciaInicialProcedimiento);
                                        wd.Flush();

                                        ModelService ms = wd.Context.Services.GetService<ModelService>();
                                        if (ms != null)
                                        {
                                            ms.ModelChanged += new EventHandler<ModelChangedEventArgs>(wdModel_ModelChanged);
                                        }

                                        this.ReconstruirContextMenu(wd);
                                        if (((Grid)wd.View).Children.Count > 0)
                                        {
                                            System.Activities.Presentation.View.DesignerView dv = ((Grid)wd.View).Children[0] as System.Activities.Presentation.View.DesignerView;
                                            dv.WorkflowShellBarItemVisibility = System.Activities.Presentation.View.ShellBarItemVisibility.Zoom;

                                        }

                                    })

                              );
                          }
                      ));
                    thread.Start();                    
                }
                
                return wd.View;
            }
        }

      
        

        public UIElement WFDeclaraciones
        {
            get
            {
                if (this is TabItemDeclaracionConstante || this is TabItemDeclaracionVariable || this is TabItemAgregarProcedimiento || this is TabItemAgregarFuncion)
                    return null;

                if (Object.Equals(wdDecl, null))
                {
                    wdDecl = new WorkflowDesigner();
                    wdDecl.ModelChanged += new EventHandler(wd_ModelChanged);

                  

                    if (actividadViewModel != null)
                    {
                        ProcedimientoViewModel procViewModel = actividadViewModel as ProcedimientoViewModel;

                        Secuencia aux = new Secuencia() { DisplayName = "Variables Locales", AdmiteDeclaraciones = true, SePuedeEliminar = false };
                        if (!Object.Equals(procViewModel.VariablesLocales, null))
                            aux.AsignarDatos(procViewModel.VariablesLocales);

                        SecuenciaInicialDeclaraciones = aux;
                    }
                    else
                    {
                        SecuenciaInicialDeclaraciones = new Secuencia() { AdmiteDeclaraciones = true, DisplayName = "Variables Locales", SePuedeEliminar = false };
                    }

                    System.Threading.Thread thread = new System.Threading.Thread(
                        new System.Threading.ThreadStart(
                          delegate()
                          {
                              Application.Current.Dispatcher.Invoke(
                                System.Windows.Threading.DispatcherPriority.Normal,
                                new Action(delegate()
                                    {
                                        wdDecl.Load(SecuenciaInicialDeclaraciones);
                                        wdDecl.Flush();

                                        ModelService ms = wdDecl.Context.Services.GetService<ModelService>();
                                        if (ms != null)
                                        {
                                            ms.ModelChanged += new EventHandler<ModelChangedEventArgs>(wdModel_ModelChanged);
                                        }

                                        this.ReconstruirContextMenu(wdDecl);
                                        if (((Grid)wdDecl.View).Children.Count > 0)
                                        {
                                            System.Activities.Presentation.View.DesignerView dv = ((Grid)wdDecl.View).Children[0] as System.Activities.Presentation.View.DesignerView;
                                            dv.WorkflowShellBarItemVisibility = System.Activities.Presentation.View.ShellBarItemVisibility.Zoom;
                                        }
                                    })
                                 );
                          }
                      ));
                     thread.Start();    
                }

                

                return wdDecl.View;
            }
        }

        private List<long> listaIdRemovidos = new List<long>();

        void wdModel_ModelChanged(object sender, ModelChangedEventArgs e)
        {
            List<ModelItem> listaAgregados = new List<ModelItem>();
            if (e.ItemsAdded != null)
            {
                listaAgregados.AddRange(e.ItemsAdded);
            }

            List<ModelItem> listaRemovidos = new List<ModelItem>();
            if (e.ItemsRemoved != null)
            {
                listaRemovidos.AddRange(e.ItemsRemoved);
            }

            if (listaAgregados.Count > 0)
            {
                foreach (var item in listaAgregados)
                {
                    ActividadBase actividad = item.GetCurrentValue() as ActividadBase;

                    //Si la actividad tiene 0 como id, significa que es nueva
                    //Si la actividad esta contenida en esa lista de removidos es pq o lo estoy moviendo, o lo corte
                    if (actividad.IdPropio > 0 && !listaIdRemovidos.Contains(actividad.IdPropio))
                    {
                        actividad.ReasignarId();
                    }
                }
            }

            listaIdRemovidos.Clear();

            if (listaRemovidos.Count > 0)
            {
                foreach (var item in listaRemovidos)
                {
                    ActividadBase actividad = item.GetCurrentValue() as ActividadBase;
                    listaIdRemovidos.Add(actividad.IdPropio);
                }
            }

        }

        void wd_ModelChanged(object sender, EventArgs e)
        {
            WorkflowChangedEventFire(this, new WorkflowChangedEventArgs());

        }

        private void WorkflowChangedEventFire(Tab tab, WorkflowChangedEventArgs workflowChangedEventArgs)
        {
            if (WorkflowChangedEvent != null)
            {
                WorkflowChangedEvent(tab, workflowChangedEventArgs);
            }
        }

        void ReconstruirContextMenu(WorkflowDesigner wd)
        {
            MenuItem borrar = null;
            MenuItem cortar = null;
            MenuItem copiar = null;
            MenuItem pegar = null;
            MenuItem salvarComoImagen = null;
            MenuItem copiarComoImagen = null;

            if (wd.ContextMenu.Items.Count > 0)
            {
                StringBuilder strBldr = new StringBuilder();
                for (int i = 0; i < wd.ContextMenu.Items.Count; i++)
                {
                    MenuItem mi = wd.ContextMenu.Items[i] as MenuItem;

                    if (mi != null && mi.Command == DesignerView.PasteCommand)
                    {
                        pegar = (MenuItem)wd.ContextMenu.Items[i];
                    }

                    if (mi != null && mi.Command == DesignerView.CopyCommand)
                    {
                        copiar = (MenuItem)wd.ContextMenu.Items[i];
                    }

                    if (mi != null && mi.Command == DesignerView.CutCommand)
                    {
                        cortar = (MenuItem)wd.ContextMenu.Items[i];
                    }

                    if (mi != null && mi.Command == DesignerView.SaveAsImageCommand)
                    {
                        salvarComoImagen = (MenuItem)wd.ContextMenu.Items[i];
                    }

                    if (mi != null && mi.Command == DesignerView.CopyAsImageCommand)
                    {
                        copiarComoImagen = (MenuItem)wd.ContextMenu.Items[i];
                    }

                    if (mi != null && (mi.InputGestureText == "Del" || mi.InputGestureText == "Supr"))
                    {
                        borrar = (MenuItem)wd.ContextMenu.Items[i];
                    }



                    //if (mi != null && mi.InputGestureText == "Ctrl+X")
                    //{
                    //    cortar = (MenuItem)wd.ContextMenu.Items[i];
                    //}
                    //if (mi != null && mi.InputGestureText == "Ctrl+C")
                    //{
                    //    copiar = (MenuItem)wd.ContextMenu.Items[i];
                    //}
                    //if (mi != null && mi.InputGestureText == "Ctrl+V")
                    //{
                    //    pegar = (MenuItem)wd.ContextMenu.Items[i];
                    //}
                }

                wd.ContextMenu.Items.Clear();

                MenuItem m2 = new MenuItem();

                //Uri uri = new Uri("Resources/Cut.png", UriKind.Relative);
                //BitmapImage imagen = new BitmapImage(uri);
                //System.Windows.Media.Imaging.CachedBitmap bitmap = new CachedBitmap( imagen, BitmapCreateOptions.None, BitmapCacheOption.Default);
                //m2.Icon = new System.Windows.Controls.Image()
                //{
                //    Source = bitmap
                //};
                m2.Icon = cortar.Icon;
                m2.Header = "Cortar";
                m2.Command = ApplicationCommands.Cut;
                wd.ContextMenu.Items.Add(m2);

                MenuItem m3 = new MenuItem();
                m3.Icon = copiar.Icon;
                m3.Header = "Copiar";
                m3.Command = ApplicationCommands.Copy;
                wd.ContextMenu.Items.Add(m3);

                MenuItem m4 = new MenuItem();
                m4.Icon = pegar.Icon;
                m4.Header = "Pegar";
                m4.Command = ApplicationCommands.Paste;
                wd.ContextMenu.Items.Add(m4);

                Separator s = new Separator();
                wd.ContextMenu.Items.Add(s);

                MenuItem m = new MenuItem();
                m.Icon = borrar.Icon;
                m.Header = "Eliminar";
                m.Command = ApplicationCommands.Delete;
                wd.ContextMenu.Items.Add(m);

                s = new Separator();
                wd.ContextMenu.Items.Add(s);

                m = new MenuItem();
                m.Header = "Copiar imagen";
                m.Command = DesignerView.CopyAsImageCommand;
                wd.ContextMenu.Items.Add(m);

                m = new MenuItem();
                m.Header = "Salvar como imagen...";
                m.Command = DesignerView.SaveAsImageCommand;
                wd.ContextMenu.Items.Add(m);

            }
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
                    NotifyPropertyChanged("Nombre");
                }
            }
        }

        protected virtual void RecrearWorkflowDesigner()
        {
            if (Object.Equals(SecuenciaInicialProcedimiento, null))
                return;

            wd = new WorkflowDesigner();
            wd.Load(SecuenciaInicialProcedimiento);
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
            if (Object.Equals(SecuenciaInicialProcedimiento, null))
                return;

            foreach (ActividadBase ab in SecuenciaInicialProcedimiento.Activities)
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

        protected int orden;

        public virtual int Orden
        {
            get { return int.MaxValue; }
            set { this.orden = value; }
        }

       

        

    }
}
