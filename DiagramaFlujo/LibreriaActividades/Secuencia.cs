using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Activities.Statements;
using System.Collections.ObjectModel;
using System.Drawing;

namespace LibreriaActividades
{
    //[Designer("System.Activities.Core.Presentation.SequenceDesigner, System.Activities.Core.Presentation")]
    [ToolboxBitmap(typeof(Secuencia), "Resources.Secuencia.png")]
    public class Secuencia : ActividadBase
    {
        [Browsable(false)]
        public Collection<Activity> Activities { get; set; }

        public Secuencia()
        {
            Activities = new Collection<Activity>();
        }


        protected override void CacheMetadata(NativeActivityMetadata metadata) {
            metadata.SetChildrenCollection(Activities);
        }

        protected override void Execute(NativeActivityContext context)
        {
            
        }

        public override void Ejecutar(StringBuilder sb)
        {
            if (Activities != null)
            {
                foreach (ActividadBase a in this.Activities)
                    a.Ejecutar(sb);
            }
        }
    }
}
