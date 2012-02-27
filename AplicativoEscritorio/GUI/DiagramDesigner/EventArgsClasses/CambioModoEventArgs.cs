using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner.Enums;

namespace DiagramDesigner.EventArgsClasses
{
    public class CambioModoEventArgs
    {
        private ModoVisual modoSeleccionado;
        public ModoVisual ModoSeleccionado
        {
            get
            {
                return modoSeleccionado;
            }
        }

        public CambioModoEventArgs(ModoVisual modo)
        {
            modoSeleccionado = modo;
        }
    }
}
