using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class ProcedimientosViewModel : ActividadViewModelBase
    {
        public List<ProcedimientoViewModel> Procedimientos { get; set; }

        public ProcedimientosViewModel()
        {
            Procedimientos = new List<ProcedimientoViewModel>();
        }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();


                return strBldr.ToString();
            }
        }
    }
}
