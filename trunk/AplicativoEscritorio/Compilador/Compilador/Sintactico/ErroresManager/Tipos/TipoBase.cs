﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    internal abstract class TipoBase
    {
        protected List<Validacion> listaValidaciones = new List<Validacion>();

        protected List<Terminal> listaLineaEntera = new List<Terminal>();
        public List<Terminal> ListaLineaEntera
        {
            get
            {
                return listaLineaEntera;
            }            
        }

        protected int filaDelError;
        public int FilaDelError
        {
            get { return filaDelError; }
            set { filaDelError = value; }
        }

        protected int columnaDelError;
        public int ColumnaDelError
        {
            get { return columnaDelError; }
            set { columnaDelError = value; }
        }

        public TipoBase(int f, int c)
        {
            filaDelError = f;
            columnaDelError = c;

        }

        public void Validar()
        {
            if (listaValidaciones.Count > 0)
            {
                listaValidaciones.Sort(CompareValidaciones);
                listaValidaciones.Reverse();

                int i = 0;

                while (i < listaValidaciones.Count && listaValidaciones[i].EsValido)
                {
                    i++;
                }

                if (i < listaValidaciones.Count)
                {
                    //significa que una validacion dio mal
                    listaValidaciones[i].ArrojarExcepcion();
                }
            }           
        }

        private static int CompareValidaciones(Validacion x, Validacion y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    return x.Importancia.CompareTo(y.Importancia);
                }
            }
        }
    }
}