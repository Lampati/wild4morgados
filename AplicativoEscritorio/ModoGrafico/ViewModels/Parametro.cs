using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.Enums;

namespace ModoGrafico.ViewModels
{
    public class Parametro
    {
        public string Nombre { get; set; }
        public TipoDato Tipo { get; set; }
        public bool EsArreglo { get; set; }
        public string TopeArreglo { get; set; }
    }
}
