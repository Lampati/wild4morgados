using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfazTextoGrafico.Enums;

namespace InterfazTextoGrafico
{
    public class ProcedimientoViewModel : ActividadViewModelBase
    {
        public SecuenciaViewModel Cuerpo { get; set; }
        public SecuenciaViewModel VariablesLocales { get; set; }
        public string Nombre { get; set; }
        public TipoRutina Tipo { get; set; }
    }
}
