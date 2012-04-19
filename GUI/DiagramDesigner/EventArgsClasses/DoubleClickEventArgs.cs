using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.EventArgsClasses
{
    public class DoubleClickEventArgs
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

        private string figura;
        public string Figura
        {
            get
            {
                return figura;
            }
        }

        public DoubleClickEventArgs(int f, int c, string fig)
        {
            fila = f;
            columna = c;
            figura = fig;
        }
    }
}
