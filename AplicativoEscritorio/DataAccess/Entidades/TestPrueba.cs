using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades.XML;

namespace AplicativoEscritorio.DataAccess.Entidades
{
    public class TestPrueba
    {
        #region Atributos
        private List<VariableTest> variablesEntrada;
        private List<VariableTest> variablesSalida;
        private string codigoGarGarProcSalida;
        private string descripcion;

        #endregion

        #region Propiedades
        public List<VariableTest> VariablesEntrada
        {
            get { return variablesEntrada; }
            set { variablesEntrada = value; }
        }

        public List<VariableTest> VariablesSalida
        {
            get { return variablesSalida; }
            set { variablesSalida = value; }
        }

        public string CodigoGarGarProcSalida
        {
            get { return codigoGarGarProcSalida; }
            set { codigoGarGarProcSalida = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        #endregion

        #region Constructores
        public TestPrueba() 
        {
            VariablesEntrada = new List<VariableTest>();
            variablesSalida = new List<VariableTest>();
        
        }
        #endregion

        #region Métodos
        public void ToXML(XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("TestPrueba");
            xml.AddElement();
            xml.SetTitle("CodigoGarGarProcSalida");
            xml.SetValue(this.codigoGarGarProcSalida);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("Descripcion");
            xml.SetValue(this.Descripcion);
            xml.LevelUp();
            if (!Object.Equals(this.variablesEntrada, null))
            {
                xml.AddElement();
                xml.SetTitle("VariablesEntrada");
                foreach (VariableTest vt in this.variablesEntrada)
                    vt.ToXML(xml);
                xml.LevelUp();
            }
            if (!Object.Equals(this.variablesSalida, null))
            {
                xml.AddElement();
                xml.SetTitle("VariablesSalida");
                foreach (VariableTest vt in this.variablesSalida)
                    vt.ToXML(xml);
                xml.LevelUp();
            }

            xml.LevelUp();
        }

        public void FromXML(XMLElement xmlElem)
        {
            if (Object.Equals(xmlElem, null))
                throw new NullReferenceException("El XML para el Test de Prueba se encuentra nulo.");

            this.codigoGarGarProcSalida = xmlElem.FindFirst("CodigoGarGarProcSalida").value;
            this.descripcion = xmlElem.FindFirst("Descripcion").value;

            XMLElement xmlEntradas = xmlElem.FindFirst("VariablesEntrada");
            if (!Object.Equals(xmlEntradas, null))
            {
                List<VariableTest> varsEntrada = new List<VariableTest>();
                foreach (XMLElement xmlEntrada in xmlEntradas.childs)
                {
                    VariableTest vt = new VariableTest();
                    vt.FromXML(xmlEntrada);
                    varsEntrada.Add(vt);
                }

                this.variablesEntrada = varsEntrada;
            }

            XMLElement xmlSalidas = xmlElem.FindFirst("VariablesSalida");
            if (!Object.Equals(xmlSalidas, null))
            {
                List<VariableTest> varsSalida = new List<VariableTest>();
                foreach (XMLElement xmlSalida in xmlSalidas.childs)
                {
                    VariableTest vt = new VariableTest();
                    vt.FromXML(xmlSalida);
                    varsSalida.Add(vt);
                }

                this.variablesSalida = varsSalida;
            }
        }
        #endregion

        #region Object Members
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            if (!Object.Equals(this.variablesEntrada, null))
                foreach (VariableTest vt in this.variablesEntrada)
                    sb.Append(vt.ToString());

            if (!Object.Equals(this.variablesSalida, null))
                foreach (VariableTest vt in this.variablesSalida)
                    sb.Append(vt.ToString());

            sb.Append(this.codigoGarGarProcSalida);
            sb.Append(this.descripcion);

            return sb.ToString();
        }
        #endregion
    }

    public class VariableTest
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string VariableMapeada { get; set; }

        public string ValorEsperado { get; set; }

        public void ToXML(XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("VariableTest");
            xml.AddElement();
            xml.SetTitle("Nombre");
            xml.SetValue(this.Nombre);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("Descripcion");
            xml.SetValue(this.Descripcion);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("VariableMapeada");
            xml.SetValue(this.VariableMapeada);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("ValorEsperado");
            xml.SetValue(this.ValorEsperado);
            xml.LevelUp();
            xml.LevelUp();
        }

        public void FromXML(XMLElement xmlElem)
        {
            if (Object.Equals(xmlElem, null))
                throw new NullReferenceException("El XML para el objeto VariableTest se encuentra nulo.");

            this.Nombre = xmlElem.FindFirst("Nombre").value;
            this.Descripcion = xmlElem.FindFirst("Descripcion").value;
            this.VariableMapeada = xmlElem.FindFirst("VariableMapeada").value;
            this.ValorEsperado = xmlElem.FindFirst("ValorEsperado").value;
        }

        #region Object Members
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Nombre);
            sb.Append(this.Descripcion);
            sb.Append(this.VariableMapeada);
            sb.Append(this.ValorEsperado);
            return sb.ToString();
        }
        #endregion
    }
}
