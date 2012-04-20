﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner.Enums;
using Globales.Enums;

namespace DiagramDesigner.EventArgsClasses
{
    public class TestPruebaEventArgs
    {

        public enum TipoAccion
        {
            Crear,
            Consultar,
            Ejecutar
        }

        private TipoAccion accion;
        public TipoAccion Accion
        {
            get
            {
                return accion;
            }
        }



        public TestPruebaEventArgs(TipoAccion t)
        {
            accion = t;
        }
    }
}