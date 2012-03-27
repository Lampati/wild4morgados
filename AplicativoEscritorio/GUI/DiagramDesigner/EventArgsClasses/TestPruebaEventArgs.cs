using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner.Enums;
using Globales.Enums;

namespace DiagramDesigner.EventArgsClasses
{
    public class TestPruebaEventArgs
    {
        private bool esCreacion;
        public bool EsCreacion
        {
            get
            {
                return esCreacion;
            }
        }

        public TestPruebaEventArgs(bool modo)
        {
            esCreacion = modo;
        }
    }
}
