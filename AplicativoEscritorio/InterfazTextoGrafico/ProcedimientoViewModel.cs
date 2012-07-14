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

      

        public override string NombreActividad
        {
            get { return "Rutina"; }
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
                        strBldr.AppendFormat("funcion {0}()", Nombre).AppendLine();
                        fin = "finfunc;";
                        break;
                    case TipoRutina.Procedimiento:
                        strBldr.AppendFormat("procedimiento {0}()",Nombre).AppendLine();                        
                        fin = "finproc;";
                        break;
                    case TipoRutina.Salida:
                        strBldr.AppendLine("procedimiento salida()");
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
            lineaComienzo = linea;

            int lineaAux = lineaComienzo;

            //aumento 1 por la cabecera
            lineaAux++;

            if (VariablesLocales != null)
            {
                VariablesLocales.CalcularLineasYAsignarContextoAHijos(lineaAux, nombreContexto);
                lineaAux = VariablesLocales.LineaFinal + 1;
            }

            //aumento 1 por la linea del comenzar
            lineaAux++;

            if (Cuerpo != null)
            {
                Cuerpo.CalcularLineasYAsignarContextoAHijos(lineaAux, nombreContexto);
                lineaAux = Cuerpo.LineaFinal + 1;
                //No hace falta aumentar uno cuando termine.
            }
            else
            {
                //aumento 1 por la linea del finproc
                lineaAux++;
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
            xml.SetTitle("Nombre");
            xml.SetValue(this.Nombre);            
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Tipo");
            xml.SetValue(((int)this.Tipo).ToString());
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Orden");
            xml.SetValue(this.Orden.ToString());
            xml.LevelUp();

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

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("ProcedimientoViewModel");
            xml.LevelUp();            


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

            this.Nombre = xmlElem.FindFirst("Nombre").value;
            this.Tipo = (TipoRutina)int.Parse(xmlElem.FindFirst("Tipo").value);
            this.Orden = short.Parse(xmlElem.FindFirst("Orden").value);
        }

        public override ActividadViewModelBase EncontrarActividadPorLinea(int lineaABuscar)
        {
            if (VariablesLocales != null)
            {
                if (VariablesLocales.LineaComienzo <= lineaABuscar && lineaABuscar <= VariablesLocales.LineaFinal)
                {
                    return VariablesLocales.EncontrarActividadPorLinea(lineaABuscar);
                }
            }

            if (Cuerpo != null)
            {
                if (Cuerpo.LineaComienzo <= lineaABuscar && lineaABuscar <= Cuerpo.LineaFinal)
                {
                    return Cuerpo.EncontrarActividadPorLinea(lineaABuscar);
                }
            }            

            //Si no era ninguno de sus subhijos es pq es algo de esta actividad sin subHijos
            return this;
        }
        
    }
}
