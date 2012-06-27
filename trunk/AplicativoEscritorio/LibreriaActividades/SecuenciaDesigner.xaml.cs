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
            object wfAct = e.Data.GetData("WorkflowItemTypeNameFormat") as object;
            if (Object.Equals(wfAct, null))
                return;

            string actividad = e.Data.GetData("WorkflowItemTypeNameFormat").ToString();
            if ((bool)this.ModelItem.Properties["AdmiteDelaraciones"].ComputedValue)
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
    }
}
