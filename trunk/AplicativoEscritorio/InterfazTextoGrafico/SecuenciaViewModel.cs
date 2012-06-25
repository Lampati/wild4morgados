using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades.XML;

namespace InterfazTextoGrafico
{
    public class SecuenciaViewModel : ActividadViewModelBase
    {
        public List<ActividadViewModelBase> ListaActividades { get; set; }

        public SecuenciaViewModel()
        {
            ListaActividades = new List<ActividadViewModelBase>();
        }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                foreach (var item in ListaActividades)
                {
                    strBldr.AppendLine(item.Gargar);
                }

                return strBldr.ToString();
            }
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("SecuenciaViewModel");

            foreach (ActividadViewModelBase actividad in ListaActividades)
            {
                actividad.ToXML(xml);
            }

            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {
            XMLElement xmlSecuencia = xmlElem.FindFirst("SecuenciaViewModel");

            if (!Object.Equals(xmlSecuencia, null))
            {
                List<ActividadViewModelBase> acts = new List<ActividadViewModelBase>();

                foreach (XMLElement xmlProc in xmlSecuencia.childs)
                {
                    ActividadViewModelBase tp = ActividadViewModelFactory.CrearActividadViewModel(xmlElem.FindFirst("NombreTipo").value);
                    tp.FromXML(xmlProc);
                    acts.Add(tp);
                }

                ListaActividades = acts;
            }
        }
    }
}
