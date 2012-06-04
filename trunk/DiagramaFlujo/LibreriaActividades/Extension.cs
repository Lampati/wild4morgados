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
        private static int ordenTab = 10;

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

        /// <summary>
        /// Este orden aplica unicamente a las Funciones / Procedimientos, el resto va hardcodeado
        /// </summary>
        public static int AsignarOrdenTab()
        {
            ordenTab++;
            return ordenTab;
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

        public static void Ejecutar(this Activity act, StringBuilder sb)
        {
            Sequence seq = act as Sequence;
            if (seq != null)
            {
                foreach (ActividadBase ab in seq.Activities)
                    ab.Ejecutar(sb);
            }
            else
            {
                ActividadBase ab = act as ActividadBase;
                if (ab != null)
                    ab.Ejecutar(sb);
            }
        }
    }
}
