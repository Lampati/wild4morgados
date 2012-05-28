using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.Statements;

namespace LibreriaActividades
{
    public static class Extension
    {
        private static StringBuilder code;
        private static int profundidadIdentacion = 0;

        public static StringBuilder Code
        {
            get {
                if (Object.Equals(code, null))
                    code = new StringBuilder();

                return code;
            }
            set
            {
                code = value;
            }
        }

        public static int ProfundidadIdentacion
        {
            get { return profundidadIdentacion; }
            set { profundidadIdentacion = value; }
        }

        public static string Tabs
        {
            get
            {
                string tabs = String.Empty;
                for (int i = 0; i < Extension.ProfundidadIdentacion; i++)
                    tabs += "\t";

                return tabs;
            }
        }

        public static void Ejecutar(this Activity act, NativeActivityContext context)
        {
            Sequence seq = act as Sequence;
            if (seq != null)
            {
                foreach (ActividadBase ab in seq.Activities)
                    ab.Ejecutar(context);
            }
            else
            {
                ActividadBase ab = act as ActividadBase;
                if (ab != null)
                    ab.Ejecutar(context);
            }
        }
    }
}
