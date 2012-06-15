using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class ProgramaViewModel : ActividadViewModelBase
    {
        public List<ProcedimientoViewModel> Procedimientos { get; set; }
        public SecuenciaViewModel ConstantesGlobales { get; set; }
        public SecuenciaViewModel VariablesGlobales { get; set; }


        public ProgramaViewModel()
        {
            Procedimientos = new List<ProcedimientoViewModel>();
        }
    }
}
