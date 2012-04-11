using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sincronizacion.Eventos
{
    public class Handler
    {
        public delegate void GuardarEjercicioHandler(object barra, object lbl, int cant, int total);

        public static event GuardarEjercicioHandler GuardarEjercicioEvent;

        public static void GuardarEjercicioEventFire(object barra, object lbl, int cant, int total)
        {
            if (GuardarEjercicioEvent != null)
            {
                GuardarEjercicioEvent(barra, lbl, cant, total);
            }

        }
    }
}
