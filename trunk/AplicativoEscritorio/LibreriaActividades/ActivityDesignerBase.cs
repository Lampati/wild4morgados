using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities.Presentation;
using System.Collections.ObjectModel;

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


        protected bool yaCreada = false;
    }
}
