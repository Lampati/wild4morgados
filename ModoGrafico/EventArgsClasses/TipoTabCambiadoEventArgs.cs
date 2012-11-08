using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.Enums;

namespace ModoGrafico.EventArgsClasses
{
    public class TipoTabCambiadoEventArgs
    {
        

        private TipoTab tipo;
        public TipoTab Tipo
        {
            get
            {
                return tipo;
            }
        }


        public TipoTabCambiadoEventArgs(TipoTab t)
        {
            tipo = t;
        }
    }
}
