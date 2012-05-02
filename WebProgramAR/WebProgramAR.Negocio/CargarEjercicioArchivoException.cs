using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebProgramAR.Negocio
{
    public class CargarEjercicioArchivoException : Exception
    {
        public CargarEjercicioArchivoException(string message)
            : base(message)
        {

        }
    }
}
