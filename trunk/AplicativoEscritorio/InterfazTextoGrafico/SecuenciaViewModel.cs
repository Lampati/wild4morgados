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
            xml.SetTitle(string.Format("Secuencia{0}",InterfazTextoGrafico.Auxiliares.GlobalXMLTags.Instance.CantSecuencias));

            foreach (ActividadViewModelBase actividad in ListaActividades)
            {
                actividad.ToXML(xml);
            }

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("SecuenciaViewModel");
            xml.LevelUp();            


            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {
            XMLElement xmlSecuencia = xmlElem.FindFirstPrefix("Secuencia");

            if (!Object.Equals(xmlSecuencia, null))
            {
                List<ActividadViewModelBase> acts = new List<ActividadViewModelBase>();

                foreach (XMLElement xmlProc in xmlSecuencia.childs)
                {
                    try
                    {
                        if (xmlProc.title != "NombreTipo")
                        {
                            string nombre = xmlProc.FindFirst("NombreTipo").value;

                            ActividadViewModelBase tp = ActividadViewModelFactory.CrearActividadViewModel(nombre);
                            tp.FromXML(xmlProc);
                            acts.Add(tp);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }

                ListaActividades = acts;
            }
        }
    }
}
