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
        public short Orden { get; set; }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();
                string fin = string.Empty;

                switch (Tipo)
                {
                    case TipoRutina.Principal:
                        strBldr.AppendLine("procedimiento principal()");
                        fin = "finproc;";
                        break;
                    case TipoRutina.Funcion:
                        strBldr.AppendFormat("funcion {0}()", Nombre).AppendLine();
                        fin = "finfunc;";
                        break;
                    case TipoRutina.Procedimiento:
                        strBldr.AppendFormat("procedimiento {0}()",Nombre).AppendLine();                        
                        fin = "finproc;";
                        break;
                    case TipoRutina.Salida:
                        strBldr.AppendLine("procedimiento salida()");
                        fin = "finproc;";
                        break;
            
                }

                if (!Object.Equals(VariablesLocales, null))
                    strBldr.AppendLine(VariablesLocales.Gargar);

                strBldr.AppendLine("comenzar");
                strBldr.AppendLine(Cuerpo.Gargar);
                strBldr.AppendLine(fin);

                return strBldr.ToString();
            }
        }
    }
}
