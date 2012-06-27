using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades.XML;
using System.Collections;
using System.Text.RegularExpressions;
using InterfazTextoGrafico.Auxiliares;

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

        private DetectorLoops detector;

        private void ConstruirOrdenRecursivo(Dictionary<string, ProcedimientoViewModel> codigosFuncProc, System.Collections.SortedList sl, ProcedimientoViewModel proc, ref int orden, string invocador)
        {
            string codigo = proc.Gargar;
            bool encontroLlamado = false;
            foreach (string key in codigosFuncProc.Keys)
            {
                Regex regex = new Regex(key + "[\\s]*[(].*[)]", RegexOptions.IgnoreCase);
                int ixComienzo = codigo.ToUpper().LastIndexOf("COMENZAR");
                if (ixComienzo < 0)
                    throw new Exception("No se encontró la sentencia COMENZAR, error grave."); //no debería pasar jamás.

                if (regex.IsMatch(codigo, ixComienzo))
                {
                    this.detector.AgregarLlamada(invocador, key);
                    this.ConstruirOrdenRecursivo(codigosFuncProc, sl, codigosFuncProc[key], ref orden, key);
                    if (!sl.ContainsValue(proc))
                    {
                        sl.Add(orden++, proc);
                        encontroLlamado = true;
                    }
                }
            }

            if (!encontroLlamado)
                if (!sl.ContainsValue(proc))
                {
                    this.detector.AgregarLlamada(invocador);
                    sl.Add(orden++, proc);
                }
        }

        public List<ProcedimientoViewModel> ProcedimientosOrdenados
        {
            get
            {
                SortedList sl = new SortedList();
                Dictionary<string, ProcedimientoViewModel> codigosFuncProc = new Dictionary<string, ProcedimientoViewModel>();
                this.detector = new DetectorLoops();

                ProcedimientoViewModel salida = null;
                ProcedimientoViewModel principal = null;
                foreach (ProcedimientoViewModel proc in this.Procedimientos)
                {
                    switch (proc.Tipo)
                    {
                        case Enums.TipoRutina.Principal:
                            principal = proc; break;
                        case Enums.TipoRutina.Salida:
                            salida = proc; break;
                        case Enums.TipoRutina.Funcion:
                        case Enums.TipoRutina.Procedimiento:
                            codigosFuncProc.Add(proc.Nombre, proc);
                            break;
                    }
                }

                int orden = 10;
                foreach (string key in codigosFuncProc.Keys)
                {
                    this.ConstruirOrdenRecursivo(codigosFuncProc, sl, codigosFuncProc[key], ref orden, key);
                }

                sl.Add(int.MaxValue, principal);
                sl.Add(int.MaxValue - 1, salida);

                return sl.Values.Cast<ProcedimientoViewModel>().ToList();
            }
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

                foreach (var item in ProcedimientosOrdenados)
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
