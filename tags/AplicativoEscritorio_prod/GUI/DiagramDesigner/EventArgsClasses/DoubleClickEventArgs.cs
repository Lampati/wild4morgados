using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibreriaActividades;

namespace Ragnarok.EventArgsClasses
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

        private string figuraId;
        public string FiguraId
        {
            get
            {
                return figuraId;
            }
        }

 
        private string figuraNombre;
        public string FiguraNombre
        {
            get
            {
                return figuraNombre;
            }
        }

        private string figuraProcedimientoNombre;
        public string FiguraProcedimientoNombre
        {
            get
            {
                return figuraProcedimientoNombre;
            }
        }

        private object actividadReferenciada;
        public object ActividadReferenciada
        {
            get
            {
                return actividadReferenciada;
            }
        }

        public DoubleClickEventArgs(int f, int c, string figId, string figNombre, string figNombreProc, object act)
        {
            fila = f;
            columna = c;
            figuraId = figId;
            figuraNombre = figNombre;
            figuraProcedimientoNombre = figNombreProc;
            actividadReferenciada = act;
        }
    }
}
