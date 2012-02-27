using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.EventArgsClasses
{
    public class ModoTextoCambiarPosicionEventArgs
    {
        private int fila;
        public int Fila
        {
            get
            {
                return fila;
            }
        }

        private int columna;
        public int Columna
        {
            get
            {
                return columna;
            }
        }

        public ModoTextoCambiarPosicionEventArgs(int f, int c)
        {
            fila = f;
            columna = c;
        }
    }
}
