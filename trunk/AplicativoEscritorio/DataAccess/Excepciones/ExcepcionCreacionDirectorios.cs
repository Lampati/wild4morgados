using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Excepciones
{
    public class ExcepcionCreacionDirectorios : Exception
    {
        public ExcepcionCreacionDirectorios(string mensaje) : base(mensaje) { }

        public ExcepcionCreacionDirectorios(string mensaje, Exception e) : base(mensaje, e) { }
    }
}
