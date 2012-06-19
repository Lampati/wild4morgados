using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class SiViewModel : ActividadViewModelBase
    {
        public SecuenciaViewModel BranchVerdadero { get; set; }
        public SecuenciaViewModel BranchFalso { get; set; }
        public string Condicion { get; set; }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("si ( {0} ) entonces",Condicion).AppendLine();
                strBldr.AppendLine(BranchVerdadero.Gargar);
                if (BranchFalso != null)
                {
                    strBldr.AppendLine(BranchFalso.Gargar);
                }

                strBldr.AppendLine("finsi;");

                return strBldr.ToString();
            }
        }
    }
}
