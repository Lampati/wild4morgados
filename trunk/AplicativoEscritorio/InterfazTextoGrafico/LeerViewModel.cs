using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class LeerViewModel : ActividadViewModelBase
    {
        public string Parametro { get; set; }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("leer ( {0} );", this.Parametro);

                return strBldr.ToString();
            }
        }
    }
}
