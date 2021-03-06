﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar
{
    internal class ErrorCompilacionException : Exception
    {
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }

        public ErrorCompilacionException(string desc, int f, int c )
        {
            this.Fila = f;
            this.Columna = c;
            this.Descripcion = desc;
        }

        public ErrorCompilacionException(string desc)
        {          
            this.Descripcion = desc;
        }
    }
}
