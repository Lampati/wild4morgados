using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades.XML;

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

        public ProgramaViewModel(string xml)
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

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("ProgramaViewModel");

            if (!Object.Equals(this.ConstantesGlobales, null))
            {
                xml.AddElement();
                xml.SetTitle("ConstantesGlobales");
                this.ConstantesGlobales.ToXML(xml);               
                xml.LevelUp();
            }

            if (!Object.Equals(this.VariablesGlobales, null))
            {
                xml.AddElement();
                xml.SetTitle("VariablesGlobales");
                this.VariablesGlobales.ToXML(xml);    
                xml.LevelUp();
            }
   
            if (!Object.Equals(this.Procedimientos, null))
            {
                xml.AddElement();
                xml.SetTitle("Procedimientos");
                foreach (ProcedimientoViewModel proc in this.Procedimientos)
                {
                    proc.ToXML(xml);
                }
                xml.LevelUp();
            }

            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {
            if (Object.Equals(xmlElem, null))
                throw new NullReferenceException("El XML para el ProgramaViewModel se encuentra nulo.");

           
            XMLElement xmlConstantes = xmlElem.FindFirst("ConstantesGlobales");
            if (!Object.Equals(xmlConstantes, null))
            {
                SecuenciaViewModel constantes = new SecuenciaViewModel();
                constantes.FromXML(xmlConstantes);
                ConstantesGlobales = constantes;
            }

            XMLElement xmlVariables = xmlElem.FindFirst("VariablesGlobales");
            if (!Object.Equals(xmlVariables, null))
            {
                SecuenciaViewModel variables = new SecuenciaViewModel();
                variables.FromXML(xmlVariables);
                VariablesGlobales = variables;
            }

            XMLElement xmlProcs = xmlElem.FindFirst("Procedimientos");
            if (!Object.Equals(xmlProcs, null))
            {
                List<ProcedimientoViewModel> procedimientos = new List<ProcedimientoViewModel>();

                foreach (XMLElement xmlProc in xmlProcs.childs)
                {
                    ProcedimientoViewModel tp = new ProcedimientoViewModel();
                    tp.FromXML(xmlProc);
                    procedimientos.Add(tp);
                }

                Procedimientos = procedimientos;

            }
            
            
        }
    }
}
