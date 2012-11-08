using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities.Presentation;

namespace LibreriaActividades
{
    public static class GlobalActivityStore
    {
        public static List<ActivityDesignerBase> _actividades = new List<ActivityDesignerBase>();

        public static void AgregarSiNoExiste(ActivityDesignerBase act)
        {
            if (_actividades == null)
            {
                _actividades = new List<ActivityDesignerBase>();
            }

            _actividades.Add(act);
        }

        public static void Vaciar()
        {
            _actividades.Clear();
        }

    }
}
