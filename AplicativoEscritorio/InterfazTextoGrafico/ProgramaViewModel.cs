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

        public override string NombreActividad
        {
            get { return "Programa"; }
        }

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
                string key2 = key.Substring(0, key.LastIndexOf("_"));
                Regex regex = new Regex(key2 + "[\\s]*[(].*[)]", RegexOptions.IgnoreCase);
                int ixComienzo = codigo.ToUpper().LastIndexOf("COMENZAR");
                if (ixComienzo < 0)
                    throw new Exception("No se encontró la sentencia COMENZAR, error grave."); //no debería pasar jamás.

                if (regex.IsMatch(codigo, ixComienzo))
                {
                    this.detector.AgregarLlamada(invocador, key2);
                    this.ConstruirOrdenRecursivo(codigosFuncProc, sl, codigosFuncProc[key], ref orden, key2);
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

        public void OrdenarProcedimientos()
        {
            SortedList sl = new SortedList();
            Dictionary<string, ProcedimientoViewModel> codigosFuncProc = new Dictionary<string, ProcedimientoViewModel>();
            this.detector = new DetectorLoops();
            int i = 0;

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
                        if (proc.Nombre != null)
                        {
                            codigosFuncProc.Add(proc.Nombre + "_" + i.ToString(), proc);
                        }
                        break;
                }
                i++;
            }

            int orden = 10;
            foreach (string key in codigosFuncProc.Keys)
            {
                //ET (11/11/2012): No se quitaba el "_" lo que hacía que muestre mal la llamada circular cuando era anidada entre 3 o mas procedimientos
                string key_aux = String.Empty;
                if (key.Contains("_"))
                    key_aux = key.Substring(0, key.IndexOf("_"));
                else
                    key_aux = key;
                //FIN CAMBIO
                this.ConstruirOrdenRecursivo(codigosFuncProc, sl, codigosFuncProc[key], ref orden, key_aux);
            }

            if (!Object.Equals(salida, null))
                sl.Add(int.MaxValue, principal);

            if (!Object.Equals(principal, null))
                sl.Add(int.MaxValue - 1, salida);

            this.Procedimientos = sl.Values.Cast<ProcedimientoViewModel>().ToList();
        }

        public void ValidarRepetidos()
        {
            //La manera más sencilla de ver si hay repetidos es agregarlos a una hash y ver si da clave duplicada
            string nombre = String.Empty;
            try
            {
                System.Collections.Hashtable hash = new System.Collections.Hashtable();
                foreach (ProcedimientoViewModel proc in this.Procedimientos)
                {
                    nombre = proc.Nombre.ToLower();
                    hash.Add(nombre, null);
                }
            }
            catch (ArgumentException)
            {
                //dio clave duplicada, hay procedimientos/funciones repetidos, informamos cuál
                throw new Exception(String.Format("Se ha encontrado un nombre repetido en las funciones/procedimientos ({0})", nombre));
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
                    strBldr.Append(ConstantesGlobales.Gargar);
                }

                if (VariablesGlobales != null && VariablesGlobales.ListaActividades.Count > 0)
                {
                    strBldr.AppendLine("variables");
                    strBldr.Append(VariablesGlobales.Gargar);
                }

                //Hacer el sort correctamente por orden de uso de los procedimientos

                foreach (var item in Procedimientos)
                {
                    strBldr.Append(item.Gargar);
                }


                return strBldr.ToString();
            }
        }
        public override void CalcularLineasYAsignarContextoAHijos(int linea, string nombreContexto)        
        {
            lineaComienzo = linea;

            contexto = nombreContexto;

            int lineaAux = lineaComienzo;

            if (ConstantesGlobales != null && ConstantesGlobales.ListaActividades != null && ConstantesGlobales.ListaActividades.Count > 0)
            {
                //Por la linea constantes
                lineaAux++;
                ConstantesGlobales.CalcularLineasYAsignarContextoAHijos(lineaAux, "CONSTANTES");
                lineaAux = ConstantesGlobales.LineaFinal + 1;
            }

            if (VariablesGlobales != null && VariablesGlobales.ListaActividades != null && VariablesGlobales.ListaActividades.Count > 0)
            {
                //Por la linea variables
                lineaAux++;
                VariablesGlobales.CalcularLineasYAsignarContextoAHijos(lineaAux, "VARIABLES");
                lineaAux = VariablesGlobales.LineaFinal + 1;
            }

            foreach (ProcedimientoViewModel proc in Procedimientos)
            {
                proc.CalcularLineasYAsignarContextoAHijos(lineaAux, proc.Nombre);
                lineaAux = proc.LineaFinal + 1;
                //lineaAux = proc.LineaFinal;
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

                if (ConstantesGlobales != null)
                {
                    strBldr.AppendLine(ConstantesGlobales.DescripcionLineas);
                }

                if (VariablesGlobales != null)
                {
                    strBldr.AppendLine(VariablesGlobales.DescripcionLineas);
                }

                foreach (ProcedimientoViewModel proc in Procedimientos)
                {
                    strBldr.AppendLine(proc.DescripcionLineas);
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

        public override ActividadViewModelBase EncontrarActividadPorLinea(int lineaABuscar)
        {
            ActividadViewModelBase retorno = null;

            if (ConstantesGlobales != null)
            {
                if (ConstantesGlobales.LineaComienzo <= lineaABuscar && lineaABuscar <= ConstantesGlobales.LineaFinal)
                {
                    retorno = ConstantesGlobales.EncontrarActividadPorLinea(lineaABuscar);
                }
            }

            if (VariablesGlobales != null)
            {
                if (VariablesGlobales.LineaComienzo <= lineaABuscar && lineaABuscar <= VariablesGlobales.LineaFinal)
                {
                    retorno = VariablesGlobales.EncontrarActividadPorLinea(lineaABuscar);
                }
            }

            foreach (ProcedimientoViewModel proc in Procedimientos)
            {
                if (proc.LineaComienzo <= lineaABuscar && lineaABuscar <= proc.LineaFinal)
                {
                    retorno = proc.EncontrarActividadPorLinea(lineaABuscar);
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
