using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace InterfazTextoGrafico
{
    public class DeclaracionConstanteViewModel : ActividadViewModelBase
    {
        public string Nombre {get; set;}
        public InterfazTextoGrafico.Enums.TipoDato Tipo {get; set;}
        public string Valor {get; set;}
    }
}
