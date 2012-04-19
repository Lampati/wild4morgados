using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sincronizacion.Eventos
{
    public class Handler
    {
        public delegate void GuardarEjercicioHandler(object barra, object lbl, int cant, int total);
        public delegate void ConectandoHandler(string url, object lbl);
        public delegate void ConectadoHandler(string url, object lbl);
        public delegate void FinalizadoHandler(string texto, object lbl);
        public delegate void InvocandoMetodoHandler(string texto, object lbl);
        public delegate void ErrorConexionHandler(string texto, object lbl);

        public static event GuardarEjercicioHandler GuardarEjercicioEvent;
        public static event ConectandoHandler ConectandoEvent;
        public static event ConectadoHandler ConectadoEvent;
        public static event FinalizadoHandler FinalizadoEvent;
        public static event InvocandoMetodoHandler InvocandoMetodoEvent;
        public static event ErrorConexionHandler ErrorConexionEvent;

        public static void GuardarEjercicioEventFire(object barra, object lbl, int cant, int total)
        {
            if (GuardarEjercicioEvent != null)
            {
                GuardarEjercicioEvent(barra, lbl, cant, total);
            }
        }

        public static void ConectandoEventFire(string url, object lbl)
        {
            if (ConectandoEvent != null)
            {
                ConectandoEvent(url, lbl);
            }
        }

        public static void ConectadoEventFire(string url, object lbl)
        {
            if (ConectadoEvent != null)
            {
                ConectadoEvent(url, lbl);
            }
        }

        public static void FinalizadoEventFire(string text, object lbl)
        {
            if (FinalizadoEvent != null)
            {
                FinalizadoEvent(text, lbl);
            }
        }

        public static void InvocandoMetodoEventFire(string text, object lbl)
        {
            if (InvocandoMetodoEvent != null)
            {
                InvocandoMetodoEvent(text, lbl);
            }
        }

        public static void ErrorConexionEventFire(string text, object lbl)
        {
            if (ErrorConexionEvent != null)
            {
                ErrorConexionEvent(text, lbl);
            }
        }
    }
}
