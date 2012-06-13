using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner.Enums;

namespace DiagramDesigner.EventArgsClasses
{
    public class TipoTabCambiadoEventArgs
    {
        

        private eTipoTab tipo;
        public eTipoTab Tipo
        {
            get
            {
                return tipo;
            }
        }


        public TipoTabCambiadoEventArgs(eTipoTab t)
        {
            tipo = t;
        }
    }
}
