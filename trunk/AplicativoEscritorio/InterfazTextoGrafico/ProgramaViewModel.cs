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

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                if (ConstantesGlobales != null && ConstantesGlobales.ListaActividades.Count > 0)
                {
                    strBldr.AppendLine("constantes");
                    strBldr.AppendLine(ConstantesGlobales.Gargar);
                }

                if (VariablesGlobales != null && VariablesGlobales.ListaActividades.Count > 0)
                {
                    strBldr.AppendLine("variables");
                    strBldr.AppendLine(VariablesGlobales.Gargar);
                }

                //Hacer el sort correctamente por orden de uso de los procedimientos

                foreach (var item in Procedimientos)
                {
                    strBldr.AppendLine(item.Gargar);
                }


                return strBldr.ToString();
            }
        }
    }
}
