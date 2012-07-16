using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades.XML;

namespace InterfazTextoGrafico
{
    public class SiViewModel : ActividadViewModelBase
    {
        public SecuenciaViewModel BranchVerdadero { get; set; }
        public SecuenciaViewModel BranchFalso { get; set; }
        public string Condicion { get; set; }

        public SiViewModel(long id)
            : base(id)
        {

        }

        public SiViewModel()
            : base()
        {

        }

        public override string NombreActividad
        {
            get { return "Si"; }
        }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("si ( {0} ) entonces",Condicion).AppendLine();
                strBldr.Append(BranchVerdadero.Gargar);
                if (BranchFalso != null && BranchFalso.ListaActividades.Count > 0)
                {
                    strBldr.AppendLine("sino");
                    strBldr.Append(BranchFalso.Gargar);
                }

                strBldr.AppendLine("finsi;");

                return strBldr.ToString();
            }
        }

        public override void CalcularLineasYAsignarContextoAHijos(int linea, string nombreContexto)
        {
            lineaComienzo = linea;

            int lineaAux = lineaComienzo;

            contexto = nombreContexto;

            //aumento 1 por la linea de la condicion del si
            lineaAux++;

            if (BranchVerdadero != null && BranchVerdadero.ListaActividades.Count > 0)
            {
                BranchVerdadero.CalcularLineasYAsignarContextoAHijos(lineaAux, nombreContexto);
                lineaAux = BranchVerdadero.LineaFinal + 1;

                if (BranchFalso != null && BranchFalso.ListaActividades.Count > 0)
                {
                    //aumento 1 por la linea del sino
                    lineaAux++;
                    BranchFalso.CalcularLineasYAsignarContextoAHijos(lineaAux, nombreContexto);
                    lineaAux = BranchFalso.LineaFinal;
                }
                else
                {
                    //Le resto una pq me quedo sumada de mas en el primer si
                    lineaAux--;
                }
            }
            else
            {
                if (BranchFalso != null && BranchFalso.ListaActividades.Count > 0)
                {
                    //aumento 1 por la linea del sino
                    lineaAux++;
                    BranchFalso.CalcularLineasYAsignarContextoAHijos(lineaAux, nombreContexto);
                    lineaAux = BranchFalso.LineaFinal;
                }

               
            }

          

            //aumento 1 por la linea del finsi
            lineaAux++;

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

                if (BranchVerdadero != null)
                {
                    strBldr.AppendLine(BranchVerdadero.DescripcionLineas);
                }

                if (BranchFalso != null && BranchFalso.ListaActividades.Count > 0)
                {
                    strBldr.AppendLine(BranchFalso.DescripcionLineas);
                }

                return strBldr.ToString();
            }
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle(string.Format("Si{0}",InterfazTextoGrafico.Auxiliares.GlobalXMLTags.Instance.CantSi));

            xml.AddElement();
            xml.SetTitle("Condicion");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(this.Condicion));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle(string.Format("BranchVerdadero{0}", InterfazTextoGrafico.Auxiliares.GlobalXMLTags.Instance.CantSi));            
            this.BranchVerdadero.ToXML(xml);
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle(string.Format("BranchFalso{0}", InterfazTextoGrafico.Auxiliares.GlobalXMLTags.Instance.CantSi));
            this.BranchFalso.ToXML(xml);
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("SiViewModel");
            xml.LevelUp();            


            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {


            this.Condicion = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("Condicion").value);

            XMLElement xmlBranchVerdadero = xmlElem.FindFirstPrefix("BranchVerdadero");
            if (!Object.Equals(xmlBranchVerdadero, null))
            {
                SecuenciaViewModel cuerpo = new SecuenciaViewModel();
                cuerpo.FromXML(xmlBranchVerdadero);
                BranchVerdadero = cuerpo;
            }

            XMLElement xmlBranchFalso = xmlElem.FindFirstPrefix("BranchFalso");
            if (!Object.Equals(xmlBranchFalso, null))
            {
                SecuenciaViewModel cuerpo = new SecuenciaViewModel();
                cuerpo.FromXML(xmlBranchFalso);
                BranchFalso = cuerpo;
            }
            
        }

        public override ActividadViewModelBase EncontrarActividadPorLinea(int lineaABuscar)
        {
            if (BranchVerdadero != null)
            {
                if (BranchVerdadero.LineaComienzo <= lineaABuscar && lineaABuscar <= BranchVerdadero.LineaFinal)
                {
                    return BranchVerdadero.EncontrarActividadPorLinea(lineaABuscar);
                }
            }

            if (BranchFalso != null && BranchFalso.ListaActividades.Count > 0)
            {
                if (BranchFalso.LineaComienzo <= lineaABuscar && lineaABuscar <= BranchFalso.LineaFinal)
                {
                    return BranchFalso.EncontrarActividadPorLinea(lineaABuscar);
                }
            }

            //Si no era ninguno de sus subhijos es pq es algo de esta actividad sin subHijos
            return this;
        }
    }
}
