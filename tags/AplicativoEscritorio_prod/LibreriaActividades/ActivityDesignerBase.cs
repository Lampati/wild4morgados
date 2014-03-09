using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities.Presentation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Activities.Presentation.Model;

namespace LibreriaActividades
{
    public class ActivityDesignerBase : ActivityDesigner
    {
        public long IdPropio
        {
            get
            {
                return Convert.ToInt64(this.ModelItem.Properties["IdPropio"].Value.ToString());
            }
        }

        protected CommandBinding commBindPaste;
        protected CommandBinding commBindCopy;
        protected CommandBinding commBindCut;
        protected CommandBinding commBindDelete;
        protected bool yaCreada = false;

        //Esto lo que hace es prevenir que se pueda editar el header de una actividad.
        //Es correcto que quede vacio
        public override void OnApplyTemplate()
        {
            
        }

        public ActivityDesignerBase()
        {
            commBindPaste = new CommandBinding();
            commBindPaste.Command = ApplicationCommands.Paste;
            commBindPaste.CanExecute += new CanExecuteRoutedEventHandler(commBindApplicationPaste_CanExecute);
            commBindPaste.Executed += new ExecutedRoutedEventHandler(commBindApplicationPaste_Executed);

            CommandBindings.Add(commBindPaste);

            commBindCopy = new CommandBinding();
            commBindCopy.Command = ApplicationCommands.Copy;
            commBindCopy.CanExecute += new CanExecuteRoutedEventHandler(commBindApplicationCopy_CanExecute);
            commBindCopy.Executed += new ExecutedRoutedEventHandler(commBindApplicationCopy_Executed);

            CommandBindings.Add(commBindCopy);

            commBindCut = new CommandBinding();
            commBindCut.Command = ApplicationCommands.Cut;
            commBindCut.CanExecute += new CanExecuteRoutedEventHandler(commBindApplicationCut_CanExecute);
            commBindCut.Executed += new ExecutedRoutedEventHandler(commBindApplicationCut_Executed);

            CommandBindings.Add(commBindCut);

            commBindDelete = new CommandBinding();
            commBindDelete.Command = ApplicationCommands.Delete;
            commBindDelete.CanExecute += new CanExecuteRoutedEventHandler(commBindApplicationDelete_CanExecute);
            //commBindDelete.Executed += new ExecutedRoutedEventHandler(commBindApplicationDelete_Executed);
            
            CommandBindings.Add(commBindDelete);

     
        }

       

        protected virtual void commBindApplicationPaste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Activities.Presentation.View.DesignerView.PasteCommand.Execute(null);
        }

        protected virtual void commBindApplicationPaste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = System.Activities.Presentation.View.DesignerView.PasteCommand.CanExecute(null);
        }

        protected virtual void commBindApplicationCopy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Activities.Presentation.View.DesignerView.CopyCommand.Execute(null);
        }

        protected virtual void commBindApplicationCopy_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ModelProperty prop = this.ModelItem.Properties["SePuedeEliminar"];

            if (prop != null)
            {
                bool esModificable = Convert.ToBoolean(this.ModelItem.Properties["SePuedeEliminar"].Value.ToString());
                e.CanExecute = esModificable && System.Activities.Presentation.View.DesignerView.CopyCommand.CanExecute(null);
            }

        }

        protected virtual void commBindApplicationCut_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Activities.Presentation.View.DesignerView.CutCommand.Execute(null);
        }

        protected virtual void commBindApplicationCut_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ModelProperty prop = this.ModelItem.Properties["SePuedeEliminar"];

            if (prop != null)
            {
                bool esModificable = Convert.ToBoolean(this.ModelItem.Properties["SePuedeEliminar"].Value.ToString());
                e.CanExecute = esModificable && System.Activities.Presentation.View.DesignerView.CutCommand.CanExecute(null); 
            }

        }

        //protected virtual void commBindApplicationDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    ActivityDesignerBase act = sender as ActivityDesignerBase;


        //    if (act.IsFocused)
        //    {
        //        ModelProperty prop = this.ModelItem.Properties["SePuedeEliminar"];

        //        if (prop != null)
        //        {
        //            bool esModificable = Convert.ToBoolean(this.ModelItem.Properties["SePuedeEliminar"].Value.ToString());

        //            if (esModificable && ApplicationCommands.Delete.CanExecute(null, act))
        //            {
                       
        //            }
        //            else
        //            {
        //                MessageBox.Show("La actividad seleccionada no puede ser borrada");
        //            }


        //        }
        //    }
        //}

     
     

     

        protected virtual void commBindApplicationDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ActivityDesignerBase act = sender as ActivityDesignerBase;
            ModelProperty prop = this.ModelItem.Properties["SePuedeEliminar"];

            if (prop != null)
            {
                bool esModificable = Convert.ToBoolean(this.ModelItem.Properties["SePuedeEliminar"].Value.ToString());
                e.CanExecute = esModificable && act.IsFocused;                    
            }
            e.Handled = true;
        }

       
    }
}
