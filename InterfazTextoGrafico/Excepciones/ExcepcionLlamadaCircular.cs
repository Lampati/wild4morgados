using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico.Excepciones
{
    public class ExcepcionLlamadaCircular : Exception
    {
        public ExcepcionLlamadaCircular(string mensaje) : base(mensaje) { }
    }
}
