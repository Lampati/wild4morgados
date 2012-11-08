using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ragnarok.Enums;
using Globales.Enums;
using System.Windows;

namespace Ragnarok.EventArgsClasses
{
    public class CambioModoEventArgs
    {
        private ModoVisual modoSeleccionado;
        public ModoVisual ModoSeleccionado
        {
            get
            {
                return modoSeleccionado;
            }
        }

        private RoutedEventArgs sourceEvent;
        public RoutedEventArgs SourceEvent
        {
            get
            {
                return sourceEvent;
            }
        }

        

        public CambioModoEventArgs(ModoVisual modo, RoutedEventArgs ev)
        {
            modoSeleccionado = modo;
            sourceEvent = ev;
        }
    }
}
