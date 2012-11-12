using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Globales
{
    public class ConstantesGlobales
    {
        public const string NOMBRE_ARCH_COMPILADOR_PASCAL = @"Configuracion\CompiladorPascal\Compilador\bin\fpc\ppc386.exe";
        public const string NOMBRE_DIR_UNITS_PASCAL = @"Configuracion\CompiladorPascal\Compilador\extra";
        public const string NOMBRE_ARCH_CONFIG_APLICACION = @"configApp.xml";
        public const string NOMBRE_ARCH_RUTINASPREDEF_APLICACION = @"rutinasPredef.xml";

        public const string NOMBRE_APLICACION = @"Ragnarok";

        //public const string EXTENSION_EJERCICIO = @"gej";
        //public const string EXTENSION_RESOLUCION = @"gres";

        private static string pathEjecucionAplicacion = null;
        public static string PathEjecucionAplicacion
        {
            get
            {
                if (pathEjecucionAplicacion == null)
                {
                    pathEjecucionAplicacion = AppDomain.CurrentDomain.BaseDirectory;
                }
                return pathEjecucionAplicacion;
            }
        }
    }
}
