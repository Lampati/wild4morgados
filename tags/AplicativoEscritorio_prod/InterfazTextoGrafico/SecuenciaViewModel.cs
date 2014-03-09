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


        public int Count
        {
            get
            {
                return ListaActividades.Count;
            }
        }

         public SecuenciaViewModel(long id)
            : base(id)
        {
            ListaActividades = new List<ActividadViewModelBase>();
        }

         public SecuenciaViewModel()
            : base()
        {
            ListaActividades = new List<ActividadViewModelBase>();
        }

        public override string NombreActividad
        {
            get { return "Secuencia"; }
        }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                foreach (var item in ListaActividades)
                {
                    strBldr.Append(item.Gargar);
                }

                return strBldr.ToString();
            }
        }

        public override void CalcularLineasYAsignarContextoAHijos(int linea, string nombreContexto)
        {
            lineaComienzo = linea;
            int lineaAux = linea;

            if (ListaActividades.Count > 0)
            {
                foreach (ActividadViewModelBase act in ListaActividades)
                {
                    act.CalcularLineasYAsignarContextoAHijos(lineaAux, nombreContexto);
                    lineaAux = act.LineaFinal + 1;
                }
                //Le resto uno pq siempre termino con un incremento mas
                lineaAux--;
            }

            lineaFinal = lineaAux;
        }

        public override string DescripcionLineas
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendLine(string.Format("{0} - {1} a {2}",
                    this.GetType().ToString(),
                    this.lineaComienzo,
                    this.lineaFinal
                ));

                foreach (ActividadViewModelBase act in ListaActividades)
                {
                    strBldr.AppendLine(act.DescripcionLineas);
                }

                return strBldr.ToString();
            }
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle(string.Format("Secuencia{0}",InterfazTextoGrafico.Auxiliares.GlobalXMLTags.Instance.CantSecuencias));


            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("SecuenciaViewModel");
            xml.LevelUp();          

            foreach (ActividadViewModelBase actividad in ListaActividades)
            {
                actividad.ToXML(xml);
            }
  


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

        public override ActividadViewModelBase EncontrarActividadPorLinea(int lineaABuscar)
        {
            ActividadViewModelBase retorno = null;

            foreach (ActividadViewModelBase act in ListaActividades)
            {
                if (act.LineaComienzo <= lineaABuscar && lineaABuscar <= act.LineaFinal)
                {
                    return act.EncontrarActividadPorLinea(lineaABuscar);
                }
            }

            if (retorno == null)
            {
                retorno = this;
            }

            return retorno;
        }
    }
}
