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
        private static int _contadorGlobalAct = 0;

        protected string id;
        protected long idPropio;

        protected object actividadReferenciada;

        protected int lineaComienzo;
        protected int lineaFinal;

        public abstract string Gargar { get; }
        public abstract string DescripcionLineas { get; }
        public abstract string NombreActividad { get; }

        protected string contexto;

        public long IdPropio
        {
            get
            {
                if (idPropio == 0)
                {
                    idPropio = ++_contadorGlobalAct;
                }

                return idPropio;
            }        

        }

        public string Contexto
        {
            get
            {
                return contexto;
            }

        }

        public string Id
        {
            get
            {
                return id;
            }
             set
            {
                id = value;
            }
        }

        public object ActividadReferenciada
        {
            get
            {
                return actividadReferenciada;
            }
            set
            {
                actividadReferenciada = value;
            }
        }

        

        public int LineaComienzo
        {
            get
            {
                return lineaComienzo;
            }
        }

        public int LineaFinal
        {
            get
            {
                return lineaFinal;
            }
        }

        public ActividadViewModelBase(long id)
        {
            if (id == long.MinValue)
            {
                idPropio = ++_contadorGlobalAct;
            }
            else
            {
                idPropio = id;
            }
        }

        public ActividadViewModelBase()
        {
            idPropio = ++_contadorGlobalAct;
        }

        public static void ReiniciarContadorGlobal()
        {
            _contadorGlobalAct = 0;
        }

     

        public abstract void ToXML(XMLCreator xml);
        public abstract void FromXML(XMLElement xmlElem);

        public abstract void CalcularLineasYAsignarContextoAHijos(int lineaAnterior, string nombreContexto);

        public abstract ActividadViewModelBase EncontrarActividadPorLinea(int lineaABuscar);


    }
}
