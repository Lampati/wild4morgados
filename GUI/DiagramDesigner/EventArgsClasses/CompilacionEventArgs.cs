using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.EventArgsClasses
{
    public class CompilacionEventArgs
    {
        private bool esEjecucion;
        public bool EsEjecucion
        {
            get
            {
                return esEjecucion;
            }
        }

        public CompilacionEventArgs(bool esEjec)
        {
            esEjecucion = esEjec;
        }
    }
}
