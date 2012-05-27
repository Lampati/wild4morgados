using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace LibreriaActividades
{
    public abstract class ActividadBase : NativeActivity
    {
        public abstract void Ejecutar(NativeActivityContext context);
    }
}
