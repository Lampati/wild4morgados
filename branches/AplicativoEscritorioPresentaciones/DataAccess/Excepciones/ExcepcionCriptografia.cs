using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AplicativoEscritorio.DataAccess.Excepciones
{
    public class ExcepcionCriptografia : Exception
    {
        public ExcepcionCriptografia(string mensaje) : base(mensaje) { }

        public ExcepcionCriptografia(string mensaje, Exception e) : base(mensaje, e) { }
    }
}
