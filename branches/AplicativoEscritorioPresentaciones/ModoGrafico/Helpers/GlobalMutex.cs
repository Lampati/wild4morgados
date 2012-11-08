using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ModoGrafico.Helpers
{
    public static class GlobalMutex
    {
        public static object padlock = new object();

        public static bool SePuedeEntrar
        {
            get
            {
                return Monitor.TryEnter(padlock);
            }
        }

        public static void Liberar()
        {
            Monitor.Exit(padlock);
        }

        public static void Ocupar()
        {
            Monitor.Enter(padlock);
        }
    }
}
