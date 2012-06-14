using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModoGrafico.Datos
{
    public class SiViewModel : ActividadViewModelBase
    {
        public SecuenciaViewModel BranchVerdadero { get; set; }
        public SecuenciaViewModel BranchFalso { get; set; }
        public string Condicion { get; set; }
    }
}
