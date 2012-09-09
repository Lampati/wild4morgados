using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Ragnarok.EventArgsClasses;

namespace Ragnarok.UserControls.Entorno
{
    public partial class EsquemaCentral : UserControl
    {
        public delegate void ModoTextoCambiarPosicionEventHandler(object o, ModoTextoCambiarPosicionEventArgs e);
        public event ModoTextoCambiarPosicionEventHandler ModoTextoCambiarPosicionEvent;


        private void ModoTextoCambiarPosicionEventFire(ModoTextoCambiarPosicionEventArgs e)
        {
            if (ModoTextoCambiarPosicionEvent != null)
            {
                ModoTextoCambiarPosicionEvent(this, e);
            }
        }

      
    }
}
