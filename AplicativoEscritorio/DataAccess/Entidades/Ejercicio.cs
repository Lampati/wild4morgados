using System;
using System.Collections.Generic;
using AplicativoEscritorio.DataAccess.Enums;
using AplicativoEscritorio.DataAccess.Interfases;
using Utilidades.XML;
using System.Text;
using Utilidades.Criptografia;
using System.IO;
using AplicativoEscritorio.DataAccess.Excepciones;
using Globales.Enums;

namespace AplicativoEscritorio.DataAccess.Entidades
{
    public class Ejercicio : EntidadBase
    {
        #region Atributos
        private ModoEjercicio modo;
        private string enunciado;
        private short nivelDificultad;
        private string solucionTexto;
        private string solucionGargar;
        private List<TestPrueba> testsPrueba;
        private bool esValidoSubirWeb;
        #endregion

        #region Propiedades

        public bool EsValidoSubirWeb
        {
            get { return this.esValidoSubirWeb; }
            set { this.esValidoSubirWeb = value; }
        }

        public override ModoEjercicio Modo
        {
            get { return this.modo; }
            set { this.modo = value; }
        }

        public override string Enunciado
        {
            get { return this.enunciado; }
            set { this.enunciado = value; }
        }

        public override short NivelDificultad
        {
            get { return this.nivelDificultad; }
            set { this.nivelDificultad = value; }
        }

        public override string SolucionTexto
        {
            get { return this.solucionTexto; }
            set { this.solucionTexto = value; }
        }

        public override string SolucionGargar
        {
            get { return this.solucionGargar; }
        }

        public override List<TestPrueba> TestsPrueba
        {
            get { return this.testsPrueba; }
            
        }

        public override string Gargar
        {
            get { return this.gargar; }
            set 
            { 
                this.gargar = value;
                this.solucionGargar = value;
            }
        }

        public override string Extension
        {
            get { return Globales.ConstantesGlobales.EXTENSION_EJERCICIO; }
        }
        #endregion

        #region Constructores
        public Ejercicio() { }

        #endregion

        #region Métodos
        /// <summary>
        /// Genera el XML del ejercicio con todas sus propiedades. Si se agregan atributos a la clase, deben ser colocados
        /// aquí.
        /// </summary>
        /// <returns></returns>
        public override void ToXML(XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("EjercicioProgramAr");
            xml.AddElement();
            xml.SetTitle("UltimoModoGuardado");
            xml.SetValue(((int)this.ultimoModoGuardado).ToString());
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("EsValidoSubirWeb");
            xml.SetValue(((bool)this.esValidoSubirWeb).ToString());
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("Modo");
            xml.SetValue(((int)this.modo).ToString());
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("Enunciado");
            xml.SetValue(this.enunciado);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("NivelDificultad");
            xml.SetValue(((int)this.nivelDificultad).ToString());
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("SolucionTexto");
            xml.SetValue(this.solucionTexto);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("SolucionGargar");
            xml.SetValue(this.solucionGargar);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("Gargar");
            xml.SetValue(this.gargar);
            xml.LevelUp();
            if (!Object.Equals(this.testsPrueba, null))
            {
                xml.AddElement();
                xml.SetTitle("TestsPrueba");
                foreach (TestPrueba test in this.testsPrueba)
                    test.ToXML(xml);
                xml.LevelUp();
            }
            xml.AddElement();
            xml.SetTitle("HashEjercicio");
            xml.SetValue(this.Hash);
            xml.LevelUp();
            xml.LevelUp();
        }

        public override void FromXML(XMLElement xmlElem)
        {
            if (Object.Equals(xmlElem, null))
                throw new NullReferenceException("El XML para el Ejercicio se encuentra nulo.");

            xmlElem = xmlElem.FindFirst("EjercicioProgramAr");
            if (Object.Equals(xmlElem, null))
                throw new NullReferenceException("El XML no contiene el tag <EjercicioProgramAr>");

            this.EsValidoSubirWeb = bool.Parse(xmlElem.FindFirst("EsValidoSubirWeb").value);
            this.Enunciado = xmlElem.FindFirst("Enunciado").value;
            this.NivelDificultad = short.Parse(xmlElem.FindFirst("NivelDificultad").value);
            this.solucionGargar = xmlElem.FindFirst("SolucionGargar").value;
            this.gargar = xmlElem.FindFirst("Gargar").value;
            this.SolucionTexto = xmlElem.FindFirst("SolucionTexto").value;
            this.UltimoModoGuardado = (ModoVisual)int.Parse(xmlElem.FindFirst("UltimoModoGuardado").value);
            this.Modo = (ModoEjercicio)int.Parse(xmlElem.FindFirst("Modo").value);
            XMLElement xmlTests = xmlElem.FindFirst("TestsPrueba");
            if (!Object.Equals(xmlTests, null))
            {
                List<TestPrueba> pruebas = new List<TestPrueba>();
                foreach (XMLElement xmlTest in xmlTests.childs)
                {
                    TestPrueba tp = new TestPrueba();
                    tp.FromXML(xmlTest);
                    pruebas.Add(tp);
                }

                this.testsPrueba = pruebas;
            }

            string hashXml = xmlElem.FindFirst("HashEjercicio").value;
            string hashEj = this.Hash;

            if (hashXml != hashEj)
                throw new ExcepcionHashNoConcuerda(hashXml, hashEj);
        }

        public void AgregarTestPrueba(TestPrueba tPrueba)
        {
            if (Object.Equals(this.testsPrueba, null))
                this.testsPrueba = new List<TestPrueba>();

            if (!this.testsPrueba.Contains(tPrueba))
                this.testsPrueba.Add(tPrueba);
        }

        public static Ejercicio EjercicioProxy(string enunciado, short nivelDificultad, string solucionTexto,
            string solucionGargar, List<TestPrueba> testsPrueba, bool esValidoSubirWeb)
        {
            Ejercicio ej = new Ejercicio();
            ej.Modo = ModoEjercicio.Normal;
            ej.Enunciado = enunciado;
            ej.NivelDificultad = nivelDificultad;
            ej.SolucionTexto = solucionTexto;
            ej.solucionGargar = solucionGargar;
            ej.testsPrueba = testsPrueba;
            ej.EsValidoSubirWeb = esValidoSubirWeb;
            return ej;
        }
        #endregion

        #region Object Members
        public override string ToString()
        {
            string ultimoModo = ((int)this.ultimoModoGuardado).ToString();
            string nivelDificultad = ((int)this.nivelDificultad).ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append(ultimoModo);
            sb.Append(this.enunciado);
            sb.Append(nivelDificultad);
            sb.Append(this.solucionTexto);
            sb.Append(this.solucionGargar);
            if (!Object.Equals(this.testsPrueba, null))
                foreach (TestPrueba test in this.testsPrueba)
                    sb.Append(test.ToString());

            return sb.ToString();
        }
        #endregion
    }
}
