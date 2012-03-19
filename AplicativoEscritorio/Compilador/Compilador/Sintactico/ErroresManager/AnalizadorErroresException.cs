﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal class AnalizadorErroresException : Exception
    {
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Parar { get; set; }

        public AnalizadorErroresException(string m)
            : base(m)
        {
          
        }
    }
}
