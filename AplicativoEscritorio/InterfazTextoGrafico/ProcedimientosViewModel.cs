using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class ProcedimientosViewModel : ActividadViewModelBase
    {
        public List<ProcedimientoViewModel> Procedimientos { get; set; }

        public override string NombreActividad
        {
            get { return ""; }
        }

        public ProcedimientosViewModel()
        {
            Procedimientos = new List<ProcedimientoViewModel>();
        }

        public override string DescripcionLineas
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();



                return strBldr.ToString();
            }
        }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();


                return strBldr.ToString();
            }
        }


        public override ActividadViewModelBase EncontrarActividadPorLinea(int lineaABuscar)
        {
            return null;
        }

        public override void CalcularLineas(int linea)
        {
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {
        }
    }
}
