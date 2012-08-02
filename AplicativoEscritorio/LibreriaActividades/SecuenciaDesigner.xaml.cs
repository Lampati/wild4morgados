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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Activities.Presentation.Model;

namespace LibreriaActividades
{
    // Interaction logic for SecuenciaDesigner.xaml
    public partial class SecuenciaDesigner
    {
        public SecuenciaDesigner()
        {
            InitializeComponent();
            base.PreviewDragOver += new DragEventHandler(SecuenciaDesigner_PreviewDragOver);
            
        }

        void SecuenciaDesigner_PreviewDragOver(object sender, DragEventArgs e)
        {
            string nombre = e.Data.GetFormats().SingleOrDefault(x => x.StartsWith("ModelItemFormat"));
            if (string.IsNullOrEmpty(nombre))
            {

                //Es una actividad que arrastro
                object wfAct = e.Data.GetData("WorkflowItemTypeNameFormat") as object;
                if (Object.Equals(wfAct, null))
                    return;

                string actividad = e.Data.GetData("WorkflowItemTypeNameFormat").ToString();
                if ((bool)this.ModelItem.Properties["AdmiteDeclaraciones"].ComputedValue)
                {
                    if (!actividad.Contains("DeclaracionVariable") && !actividad.Contains("DeclaracionConstante") && !actividad.Contains("DeclaracionArreglo"))
                        e.Effects = DragDropEffects.None;
                }
                else
                {
                    if (actividad.Contains("DeclaracionVariable") || actividad.Contains("DeclaracionConstante") || actividad.Contains("DeclaracionArreglo"))
                        e.Effects = DragDropEffects.None;
                }
                
            }
            else
            {

                System.Activities.Presentation.Model.ModelItem resultado = e.Data.GetData(nombre) as System.Activities.Presentation.Model.ModelItem;

                ActividadBase act = resultado.GetCurrentValue() as ActividadBase;

                if (act.GetType() == typeof(Secuencia))
                {
                    e.Effects = DragDropEffects.None;
                }
            }
        }

        protected override void commBindApplicationPaste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            string textoCopiado = Clipboard.GetText();


            //var padreTemplated = ((UserControl)e.OriginalSource).TemplatedParent;
            //var padre = ((UserControl)e.OriginalSource).Parent;

            bool esDeclaracion = textoCopiado.Contains("<l:DeclaracionVariable)")
                                || textoCopiado.Contains("<l:DeclaracionConstante)")
                                || textoCopiado.Contains("<l:DeclaracionArreglo)");

            ModelProperty prop = this.ModelItem.Properties["AdmiteDeclaraciones"];

            if (prop != null)
            {
                bool admiteDeclaraciones = Convert.ToBoolean(prop.Value.ToString());

                e.CanExecute = (admiteDeclaraciones && esDeclaracion || !admiteDeclaraciones && !esDeclaracion) && System.Activities.Presentation.View.DesignerView.PasteCommand.CanExecute(null);
            }
            else
            {
                e.CanExecute =  System.Activities.Presentation.View.DesignerView.PasteCommand.CanExecute(null);
            }
          

        }

        protected override void commBindApplicationCopy_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        protected override void commBindApplicationCut_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        //protected override void commBindApplicationDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    //e.CanExecute = false;
        //    e.CanExecute = true;
        //}

        //protected override void commBindApplicationDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    e.Handled = true;
        //}

        //protected override void commBindApplicationPaste_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    string textoCopiado = Clipboard.GetText();


        //    //var padreTemplated = ((UserControl)e.OriginalSource).TemplatedParent;
        //    //var padre = ((UserControl)e.OriginalSource).Parent;

        //    bool esDeclaracion = textoCopiado.Contains("<l:DeclaracionVariable)")
        //                        || textoCopiado.Contains("<l:DeclaracionConstante)")
        //                        || textoCopiado.Contains("<l:DeclaracionArreglo)");

        //    ModelProperty prop = this.ModelItem.Properties["AdmiteDeclaraciones"];

        //    if (prop != null)
        //    {
        //        bool admiteDeclaraciones = Convert.ToBoolean(prop.Value.ToString());

        //        e.Handled = (admiteDeclaraciones && esDeclaracion || !admiteDeclaraciones && !esDeclaracion) && System.Activities.Presentation.View.DesignerView.PasteCommand.CanExecute(null);
        //    }
        //    else
        //    {
        //        e.Handled = System.Activities.Presentation.View.DesignerView.PasteCommand.CanExecute(null);
        //    }
        //}
    }
}
