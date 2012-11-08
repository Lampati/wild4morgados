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
using System.Windows.Controls.Primitives;

namespace LibreriaActividades
{
    // Interaction logic for SecuenciaDesigner.xaml
    public partial class SecuenciaDesigner
    {

        // flanzani 8/11/2012
        // IDC_APP_1
        // Agregar tooltips de error al intentar arrastrar
        // Creo el popup
        static Popup popup = new Popup();
        static TextBlock textoPopup = new TextBlock();
        static Border grilla = new Border();

        string textoNoArrastrarActividades = "Solo se pueden arrastrar actividades que no sean declaraciones al espacio de declaraciones";
        string textoNoArrastrarDeclaraciones = "No se pueden arrastrar declaraciones al cuerpo de un procedimiento o función";
        string textoNoArrastrarSecuencia = "No se pueden arrastrar actividades del tipo secuencia";

        public SecuenciaDesigner()
        {
            InitializeComponent();
            base.PreviewDragOver += new DragEventHandler(SecuenciaDesigner_PreviewDragOver);
            base.PreviewDragEnter += new DragEventHandler(SecuenciaDesigner_PreviewDragEnter);


            // flanzani 8/11/2012
            // IDC_APP_1
            // Agregar tooltips de error al intentar arrastrar
            // Agrego estos eventos para poder tener mejor control de cuando mostrar y enconder el popup
            base.PreviewDragLeave += new DragEventHandler(SecuenciaDesigner_PreviewDragLeave);
            base.MouseMove += new MouseEventHandler(SecuenciaDesigner_MouseMove);



            popup.Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse;

            textoPopup = new TextBlock()
            {                
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFE1")),
                Padding = new Thickness(3, 0, 3, 0),
                Foreground = new SolidColorBrush(Colors.Black)              
            };
       
            grilla.Child = new Border()
            {
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(1),
                Child = textoPopup
            };
                     

            


        }

        void SecuenciaDesigner_PreviewDragLeave(object sender, DragEventArgs e)
        {
            popup.IsOpen = false;
        }

        void SecuenciaDesigner_MouseMove(object sender, MouseEventArgs e)
        {
            // flanzani 8/11/2012
            // IDC_APP_1
            // Agregar tooltips de error al intentar arrastrar
            // Escondo el popup en cada movimiento del mouse para que el popup se mueva al ser arrastrado
            popup.IsOpen = false;
        }

        void SecuenciaDesigner_PreviewDragEnter(object sender, DragEventArgs e)
        {
            SecuenciaDesigner_PreviewDragOver(sender, e);
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
                    {
                        // flanzani 8/11/2012
                        // IDC_APP_1
                        // Agregar tooltips de error al intentar arrastrar
                        // Asigno el mensaje correspondiente y muestro el popup
                        textoPopup.Text = textoNoArrastrarActividades;
                        popup.Child = grilla;
                        popup.IsOpen = true;

                        e.Effects = DragDropEffects.None;
                    }
                    else
                    {
                        popup.IsOpen = false;
                    }
                }
                else
                {
                    if (actividad.Contains("DeclaracionVariable") || actividad.Contains("DeclaracionConstante") || actividad.Contains("DeclaracionArreglo"))
                    {
                        // flanzani 8/11/2012
                        // IDC_APP_1
                        // Agregar tooltips de error al intentar arrastrar
                        // Asigno el mensaje correspondiente y muestro el popup
                        textoPopup.Text = textoNoArrastrarDeclaraciones;
                        popup.Child = grilla;
                        popup.IsOpen = true;

                        e.Effects = DragDropEffects.None;
                        
                    }
                    else
                    {
                        popup.IsOpen = false;
                    }
                }
            }
            else
            {

                System.Activities.Presentation.Model.ModelItem resultado = e.Data.GetData(nombre) as System.Activities.Presentation.Model.ModelItem;

                ActividadBase act = resultado.GetCurrentValue() as ActividadBase;

                if (act.GetType() == typeof(Secuencia))
                {
                    // flanzani 8/11/2012
                    // IDC_APP_1
                    // Agregar tooltips de error al intentar arrastrar
                    // Asigno el mensaje correspondiente y muestro el popup
                    textoPopup.Text = textoNoArrastrarSecuencia;
                    popup.Child = grilla;
                    popup.IsOpen = true;

                    e.Effects = DragDropEffects.None;
                }
                else
                {
                    if ((bool)this.ModelItem.Properties["AdmiteDeclaraciones"].ComputedValue)
                    {
                        if (act.GetType() != typeof(DeclaracionVariable) && act.GetType() != typeof(DeclaracionConstante) && act.GetType() != typeof(DeclaracionArreglo))
                        {
                            // flanzani 8/11/2012
                            // IDC_APP_1
                            // Agregar tooltips de error al intentar arrastrar
                            // Asigno el mensaje correspondiente y muestro el popup
                            textoPopup.Text = textoNoArrastrarActividades;
                            popup.Child = grilla;
                            popup.IsOpen = true;

                            e.Effects = DragDropEffects.None;
                        }
                        else
                        {
                            popup.IsOpen = false;
                        }
                    }
                    else
                    {
                        if (act.GetType() == typeof(DeclaracionVariable) || act.GetType() == typeof(DeclaracionConstante) || act.GetType() == typeof(DeclaracionArreglo))
                        {
                            // flanzani 8/11/2012
                            // IDC_APP_1
                            // Agregar tooltips de error al intentar arrastrar
                            // Asigno el mensaje correspondiente y muestro el popup
                            textoPopup.Text = textoNoArrastrarDeclaraciones;
                            popup.Child = grilla;
                            popup.IsOpen = true;                           

                            e.Effects = DragDropEffects.None;

                        }
                        else
                        {
                            popup.IsOpen = false;
                        }
                    }

                }
            }
        }

        protected override void commBindApplicationPaste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            string textoCopiado = Clipboard.GetText();


            //var padreTemplated = ((UserControl)e.OriginalSource).TemplatedParent;
            //var padre = ((UserControl)e.OriginalSource).Parent;

            bool esDeclaracion = textoCopiado.Contains("<l:DeclaracionVariable")
                                || textoCopiado.Contains("<l:DeclaracionConstante")
                                || textoCopiado.Contains("<l:DeclaracionArreglo");

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
            e.Handled = true;
        }

        protected override void commBindApplicationCut_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true;
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
