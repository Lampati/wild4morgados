using System;
using System.Collections.Generic;
using DataAccess.Enums;
using DataAccess.Interfases;
using Utilidades.XML;
using System.Text;
using Utilidades.Criptografia;
using System.IO;
using DataAccess.Excepciones;

namespace DataAccess.Entidades
{
    public class Ejercicio : IPersistible
    {
        #region Atributos
        private ModoEjercicio ultimoModoGuardado;
        private string enunciado;
        private NivelDificultad nivelDificultad;
        private string solucionTexto;
        private string solucionGargar;
        private List<TestPrueba> testsPrueba;
        #endregion

        #region Propiedades
        public ModoEjercicio UltimoModoGuardado
        {
            get { return this.ultimoModoGuardado; }
            set { this.ultimoModoGuardado = value; }
        }

        public string Enunciado
        {
            get { return this.enunciado; }
            set { this.enunciado = value; }
        }

        public NivelDificultad NivelDificultad
        {
            get { return this.nivelDificultad; }
            set { this.nivelDificultad = value; }
        }

        public string SolucionTexto
        {
            get { return this.solucionTexto; }
            set { this.solucionTexto = value; }
        }

        public string SolucionGargar
        {
            get { return this.solucionGargar; }
            set { this.solucionGargar = value; }
        }
        #endregion

        #region Constructores
        public Ejercicio() { }
        #endregion

        #region Métodos
        private string Hash
        {
            get
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

                return Crypto.ComputarHash(sb.ToString());
            }
        }

        /// <summary>
        /// Genera el XML del ejercicio con todas sus propiedades. Si se agregan atributos a la clase, deben ser colocados
        /// aquí.
        /// </summary>
        /// <returns></returns>
        private string ToXML()
        {
            XMLCreator xml = new XMLCreator();
            xml.AddElement();
            xml.SetTitle("EjercicioProgramAr");
            xml.AddElement();
            xml.SetTitle("UltimoModoGuardado");
            xml.SetValue(((int)this.ultimoModoGuardado).ToString());
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
            if (!Object.Equals(this.testsPrueba, null))
            {
                xml.AddElement();
                xml.SetTitle("TestsPrueba");
                foreach (TestPrueba test in this.testsPrueba)
                {
                    xml.AddElement();
                    xml.SetTitle("TestPrueba");
                    xml.SetValue(test.ToString());
                    xml.LevelUp();
                }
                xml.LevelUp();
            }
            xml.AddElement();
            xml.SetTitle("HashEjercicio");
            xml.SetValue(this.Hash);

            return xml.Get();
        }

        private void FromXML(string xml)
        {
            XMLReader xmlReader = new XMLReader();
            XMLElement xmlElem = xmlReader.Read(xml);

            this.Enunciado = xmlElem.FindFirst("Enunciado").value;
            this.NivelDificultad = (NivelDificultad)int.Parse(xmlElem.FindFirst("NivelDificultad").value);
            this.SolucionGargar = xmlElem.FindFirst("SolucionGargar").value;
            this.SolucionTexto = xmlElem.FindFirst("SolucionTexto").value;
            this.UltimoModoGuardado = (ModoEjercicio)int.Parse(xmlElem.FindFirst("UltimoModoGuardado").value);
            XMLElement xmlTests = xmlElem.FindFirst("TestsPrueba");
            if (!Object.Equals(xmlTests, null))
            {
                List<TestPrueba> pruebas = new List<TestPrueba>();
                foreach (XMLElement xmlTest in xmlTests.childs)
                    pruebas.Add(new TestPrueba(xmlTest.value));

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
        #endregion

        #region IPersistible
        public void Guardar(string path)
        {
            string xml = this.ToXML();
            string xmlEncriptado = Crypto.Encriptar(xml);

            File.WriteAllText(path, xmlEncriptado);
        }

        public void Abrir(string path)
        {
            string xmlEncriptado = File.ReadAllText(path);
            string xmlDesencriptado = Crypto.Desencriptar(xmlEncriptado);
            
            this.FromXML(xmlDesencriptado);
        }
        #endregion
    }
}
