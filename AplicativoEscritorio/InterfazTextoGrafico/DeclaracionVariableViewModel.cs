﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class DeclaracionVariableViewModel  : ActividadViewModelBase
    {
        public string Nombre { get; set; }
        public InterfazTextoGrafico.Enums.TipoDato Tipo { get; set; }
    }
}
