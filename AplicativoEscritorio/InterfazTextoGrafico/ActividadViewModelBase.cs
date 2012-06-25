using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades.XML;

namespace InterfazTextoGrafico
{
    [System.Xml.Serialization.XmlInclude(typeof(AsignacionViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(DeclaracionArregloViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(DeclaracionVariableViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(DeclaracionConstanteViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(DeclaracionesGlobalesViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(LeerViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(LlamarProcedimientoViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(MientrasViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(MostrarViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ProcedimientosViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ProcedimientoViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ProgramaViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(RetornoViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(SecuenciaViewModel))]
    [System.Xml.Serialization.XmlInclude(typeof(SiViewModel))]
    public abstract class ActividadViewModelBase
    {
        public abstract string Gargar { get; }

        public abstract void ToXML(XMLCreator xml);
        public abstract void FromXML(XMLElement xmlElem);

    }
}
