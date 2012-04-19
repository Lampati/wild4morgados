using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DiagramDesigner.EventArgsClasses;

namespace DiagramDesigner.UserControls.Mensajes
{
    public partial class BarraMensajes : UserControl
    {
        public delegate void DobleClickEnBarraMensajesEventHandler(object o, DoubleClickEventArgs e);

        public event DobleClickEnBarraMensajesEventHandler DoubleClickEvent;


        private void DoubleClickEventFire(DoubleClickEventArgs e)
        {
            if (DoubleClickEvent != null)
            {
                DoubleClickEvent(this, e);
            }
        }
    }
}
