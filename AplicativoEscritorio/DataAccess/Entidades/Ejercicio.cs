using System;
using System.Collections.Generic;
using DataAccess.Enums;
using DataAccess.Interfases;
using Utilidades.XML;
using System.Text;
using Utilidades.Criptografia;
using System.IO;
using DataAccess.Excepciones;
using Globales.Enums;

namespace DataAccess.Entidades
{
    public class Ejercicio : EntidadBase
    {
        #region Atributos
        private ModoEjercicio modo;
        private string enunciado;
        private NivelDificultad nivelDificultad;
        private string solucionTexto;
        private string solucionGargar;
        private List<TestPrueba> testsPrueba;
        #endregion

        #region Propiedades

        public override bool ModificadoDesdeUltimoGuardado
        {
            get
            {
                return modificadoDesdeUltimoGuardado;
            }
            set
            {
                modificadoDesdeUltimoGuardado = value;
            }
        }

        public override string PathGuardadoActual
        {
            get { return this.pathGuardadoActual; }
            set { this.pathGuardadoActual = value; }
        }

        public override string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }


        public override ModoVisual UltimoModoGuardado
        {
            get { return this.ultimoModoGuardado; }
            set { this.ultimoModoGuardado = value; }
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

        public override NivelDificultad NivelDificultad
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
            set { this.solucionGargar = value; }
        }

        public override List<TestPrueba> TestsPrueba
        {
            get { return this.testsPrueba; }
            
        }

        public override string Gargar
        {
            get { return this.gargar; }
            set { this.gargar = value; }
        }


        #endregion

        #region Constructores
        public Ejercicio() { }
        #endregion

        #region Métodos
        protected override string Hash
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
        protected override string ToXML()
        {
            XMLCreator xml = new XMLCreator();
            xml.AddElement();
            xml.SetTitle("EjercicioProgramAr");
            xml.AddElement();
            xml.SetTitle("UltimoModoGuardado");
            xml.SetValue(((int)this.ultimoModoGuardado).ToString());
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

        protected override void FromXML(string plainXml)
        {
            XMLReader xmlReader = new XMLReader();
            XMLElement xmlElem = xmlReader.Read(plainXml);
            xmlElem = xmlElem.FindFirst("EjercicioProgramAr");

            this.Enunciado = xmlElem.FindFirst("Enunciado").value;
            this.NivelDificultad = (NivelDificultad)int.Parse(xmlElem.FindFirst("NivelDificultad").value);
            this.SolucionGargar = xmlElem.FindFirst("SolucionGargar").value;
            this.Gargar = xmlElem.FindFirst("Gargar").value;
            this.SolucionTexto = xmlElem.FindFirst("SolucionTexto").value;
            this.UltimoModoGuardado = (ModoVisual)int.Parse(xmlElem.FindFirst("UltimoModoGuardado").value);
            this.Modo = (ModoEjercicio)int.Parse(xmlElem.FindFirst("Modo").value);
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
    }
}
