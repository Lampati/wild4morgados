using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfazTextoGrafico.Enums;
using Utilidades.XML;

namespace InterfazTextoGrafico
{
    public class ProcedimientoViewModel : ActividadViewModelBase
    {
        public SecuenciaViewModel Cuerpo { get; set; }
        public SecuenciaViewModel VariablesLocales { get; set; }
        public string Nombre { get; set; }
        public TipoRutina Tipo { get; set; }
        public short Orden { get; set; }

        public List<ParametroViewModel> Parametros { get; set; }
        public TipoDato TipoRetorno { get; set; }
        public string Retorno { get; set; }

        private string CadenaParametros
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                if (Parametros.Count > 0)
                {
                    foreach (var item in Parametros)
                    {
                        strBldr.Append(item.Gargar).Append(",");
                    }

                    strBldr.Remove(strBldr.Length - 1, 1);
                }

                return strBldr.ToString();
            }
        }

      
         public ProcedimientoViewModel(long id)
            : base(id)
        {
            Parametros = new List<ParametroViewModel>();
        }

         public ProcedimientoViewModel()
            : base()
        {
            Parametros = new List<ParametroViewModel>();
        }

        public override string NombreActividad
        {
            get 
            {
                if (Tipo == TipoRutina.Funcion)
                {
                    return "Función";
                }
                else
                {

                    return "Procedimiento";
                }
            }
        }

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
                        strBldr.AppendFormat("funcion {0} ( {1} ) : {2}", Nombre, CadenaParametros, TipoRetorno.ToString()).AppendLine();
                        fin = string.Format("finfunc {0} ;", Retorno);
                        break;
                    case TipoRutina.Procedimiento:
                        strBldr.AppendFormat("procedimiento {0} ( {1} )", Nombre, CadenaParametros).AppendLine();                        
                        fin = "finproc;";
                        break;
                    case TipoRutina.Salida:
                        strBldr.AppendFormat("procedimiento SALIDA ( {0} )",  CadenaParametros).AppendLine();   
                        fin = "finproc;";
                        break;
            
                }

                if (!Object.Equals(VariablesLocales, null))
                    strBldr.Append(VariablesLocales.Gargar);

                strBldr.AppendLine("comenzar");
                if (!Object.Equals(Cuerpo, null))
                    strBldr.Append(Cuerpo.Gargar);
                strBldr.AppendLine(fin);

                return strBldr.ToString();
            }
        }


        public override void CalcularLineasYAsignarContextoAHijos(int linea, string nombreContexto)
        {
            contexto = nombreContexto;

            lineaComienzo = linea;

            int lineaAux = lineaComienzo;

            //aumento 1 por la cabecera
            lineaAux++;

            if (VariablesLocales != null && VariablesLocales.Count > 0)
            {
                VariablesLocales.CalcularLineasYAsignarContextoAHijos(lineaAux, nombreContexto);
                lineaAux = VariablesLocales.LineaFinal + 1;
            }

            //aumento 1 por la linea del comenzar
            lineaAux++;

            if (Cuerpo != null && Cuerpo.Count > 0)
            {
                Cuerpo.CalcularLineasYAsignarContextoAHijos(lineaAux, nombreContexto);
                lineaAux = Cuerpo.LineaFinal + 1;
                //No hace falta aumentar uno cuando termine.
            }
            else
            {
                //aumento 1 por la linea del finproc
                //lineaAux++;
            }
            ////aumento 1 por la linea del finproc
            ////lineaAux++;

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

                if (VariablesLocales != null)
                {
                    strBldr.AppendLine(VariablesLocales.DescripcionLineas);
                }

                if (Cuerpo != null)
                {
                    strBldr.AppendLine(Cuerpo.DescripcionLineas);
                }

                return strBldr.ToString();
            }
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("Procedimiento");

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("ProcedimientoViewModel");
            xml.LevelUp();      

            xml.AddElement();
            xml.SetTitle("Nombre");
            xml.SetValue(this.Nombre);            
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Tipo");
            xml.SetValue(((int)this.Tipo).ToString());
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("TipoRetorno");
            xml.SetValue(((int)this.TipoRetorno).ToString());
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Retorno");
            xml.SetValue(this.Retorno);
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Orden");
            xml.SetValue(this.Orden.ToString());
            xml.LevelUp();

            if (!Object.Equals(this.Parametros, null))
            {
                xml.AddElement();
                xml.SetTitle("Parametros");
                foreach (var item in Parametros)
                {
                    item.ToXML(xml);
                }              
                xml.LevelUp();
            }

            if (!Object.Equals(this.VariablesLocales, null))
            {
                xml.AddElement();
                xml.SetTitle("VariablesLocales");
                this.VariablesLocales.ToXML(xml);
                xml.LevelUp();
            }

            if (!Object.Equals(this.Cuerpo, null))
            {
                xml.AddElement();
                xml.SetTitle("Cuerpo");
                this.Cuerpo.ToXML(xml);
                xml.LevelUp();
            }

                 


            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {

            XMLElement xmlvars = xmlElem.FindFirst("VariablesLocales");
            if (!Object.Equals(xmlvars, null))
            {
                SecuenciaViewModel vars = new SecuenciaViewModel();
                vars.FromXML(xmlvars);
                VariablesLocales = vars;
            }

            XMLElement xmlCuerpo = xmlElem.FindFirst("Cuerpo");
            if (!Object.Equals(xmlCuerpo, null))
            {
                SecuenciaViewModel cuerpo = new SecuenciaViewModel();
                cuerpo.FromXML(xmlCuerpo);
                Cuerpo = cuerpo;
            }

            Parametros = new List<ParametroViewModel>();
            XMLElement xmlParams = xmlElem.FindFirst("Parametros");
            if (!Object.Equals(xmlParams, null))
            {
                foreach (XMLElement xmlParam in xmlParams.childs)
                {
                    ParametroViewModel param = new ParametroViewModel();
                    param.FromXML(xmlParam);

                    Parametros.Add(param);
                }
            }

            this.Nombre = xmlElem.FindFirst("Nombre").value;
            this.Tipo = (TipoRutina)int.Parse(xmlElem.FindFirst("Tipo").value);
            this.Orden = short.Parse(xmlElem.FindFirst("Orden").value);
            this.Retorno = xmlElem.FindFirst("Retorno").value;
            this.TipoRetorno = (TipoDato)int.Parse(xmlElem.FindFirst("TipoRetorno").value);
        }

        public override ActividadViewModelBase EncontrarActividadPorLinea(int lineaABuscar)
        {

            ActividadViewModelBase retorno = null;

            if (VariablesLocales != null)
            {
                if (VariablesLocales.LineaComienzo <= lineaABuscar && lineaABuscar <= VariablesLocales.LineaFinal)
                {
                    retorno = VariablesLocales.EncontrarActividadPorLinea(lineaABuscar);
                }
            }

            if (Cuerpo != null)
            {
                if (Cuerpo.LineaComienzo <= lineaABuscar && lineaABuscar <= Cuerpo.LineaFinal)
                {
                    retorno = Cuerpo.EncontrarActividadPorLinea(lineaABuscar);
                }
            }

            if (retorno == null)
            {
                retorno = this;
            }

            //Si no era ninguno de sus subhijos es pq es algo de esta actividad sin subHijos
            return retorno;
        }
        
    }
}
