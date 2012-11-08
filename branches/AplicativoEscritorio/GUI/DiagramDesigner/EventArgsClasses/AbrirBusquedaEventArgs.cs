using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ragnarok.EventArgsClasses
{
    public class AbrirBusquedaEventArgs
    {
        private bool esBuscarYReemplazar;
        public bool EsBuscarYReemplazar
        {
            get
            {
                return esBuscarYReemplazar;
            }
        }

        public AbrirBusquedaEventArgs(bool esReemp)
        {
            esBuscarYReemplazar = esReemp;
        }
    }
}
