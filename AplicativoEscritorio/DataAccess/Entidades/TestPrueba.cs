using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades.XML;

namespace DataAccess.Entidades
{
    public class TestPrueba
    {
        #region Atributos
        private string propiedadA;
        #endregion

        #region Propiedades
        public string PropiedadA
        {
            get { return this.propiedadA; }
            set { this.propiedadA = value; }
        }
        #endregion

        #region Constructores
        public TestPrueba() { }
        #endregion

        #region Métodos
        public void ToXml(XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("TestPrueba");
            xml.AddElement();
            xml.SetTitle("PropiedadA");
            xml.SetValue(this.propiedadA);
            xml.LevelUp();
            xml.LevelUp();
        }

        public void FromXml(Utilidades.XML.XMLElement xmlElem)
        {
            if (Object.Equals(xmlElem, null))
                throw new NullReferenceException("El XML para el Test de Prueba se encuentra nulo.");

            this.propiedadA = xmlElem.FindFirst("PropiedadA").value;
        }
        #endregion

        #region Object Members
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.propiedadA);
            return sb.ToString();
        }
        #endregion
    }
}
