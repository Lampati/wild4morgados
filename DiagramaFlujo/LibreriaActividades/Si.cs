using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace LibreriaActividades
{
    [Designer(typeof(SiDesigner))]
    public class Si : ActividadBase
    {
        // this property contains an activity that will be scheduled in the execute method
        // the WorkflowItemPresenter in the designer is bound to this to enable editing
        // of the value
        [Browsable(false)]
        public Activity BranchVerdadero { get; set; }
        [Browsable(false)]
        public Activity BranchFalso { get; set; }
        public string Condicion { get; set; }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            metadata.AddChild(BranchVerdadero);
            metadata.AddChild(BranchFalso);
            base.CacheMetadata(metadata);
        }

        protected override void Execute(NativeActivityContext context)
        {
            if (BranchVerdadero == null && BranchFalso == null)
                return;

            Extension.Code.AppendLine(String.Format("SI ({0}) ENTONCES", Condicion));
            if (BranchVerdadero != null)
                BranchVerdadero.Ejecutar(context);
            if (BranchFalso != null)
            {
                Extension.Code.AppendLine("SINO");
                BranchFalso.Ejecutar(context);
            }
            Extension.Code.AppendLine("FINSI;");
        }

        public override void Ejecutar(NativeActivityContext context)
        {
            this.Execute(context);
        }
    }
}
