using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Utilidades
{
    public class EjecucionManager
    {

        public static void EjecutarConVentana(string archConRuta)
        {
            Ejecutar(archConRuta, new List<string>(), ProcessWindowStyle.Normal);   
        }

        public static void EjecutarSinVentana(string archConRuta, List<string> argumentos)
        {
            Ejecutar(archConRuta, argumentos, ProcessWindowStyle.Hidden);   
        }



        private static void Ejecutar(string archConRuta, List<string> argumentos, ProcessWindowStyle ventana)
        {
            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.CreateNoWindow = false;
            procInfo.FileName = archConRuta;
            procInfo.WindowStyle = ventana;
            procInfo.Arguments = string.Join(" ", argumentos.ToArray());

            Process proc = new Process();

            proc.StartInfo = procInfo;


            
            proc.Start();
            proc.WaitForExit();
        }
    }
}
