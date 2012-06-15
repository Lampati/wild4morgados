using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class MientrasViewModel : ActividadViewModelBase
    {
        public SecuenciaViewModel Cuerpo { get; set; }
        public string Condicion { get; set; }
    }
}
