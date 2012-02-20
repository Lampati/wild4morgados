using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Globales
{
    public class ConstantesGlobales
    {
        public const string NOMBRE_ARCH_COMPILADOR_PASCAL = @"Configuracion\CompiladorPascal\Compilador\bin\fpc\ppc386.exe";
        public const string NOMBRE_ARCH_CONFIG_APLICACION = @"configApp.xml";

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
