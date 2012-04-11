using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades.XML;
using DataAccess.Entidades;

namespace AplicativoEscritorio.DataAccess.Entidades
{
    public class TestPrueba
    {
        #region Atributos
        private List<VariableTest> variablesEntrada;
        private List<VariableTest> variablesSalida;
        private string codigoGarGarProcSalida;
        private string id;
        private string nombre;
        private string descripcion;
        private string mensajesError;
        private string mensajeErrorEntrada;
        private string mensajeErrorSalida;

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

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string MensajeError
        {
            get { return mensajesError; }
            set { mensajesError = value; }
        }

        public string MensajeErrorEntrada
        {
            get { return mensajeErrorEntrada; }
            set { mensajeErrorEntrada = value; }
        }

        public string MensajeErrorSalida
        {
            get { return mensajeErrorSalida; }
            set { mensajeErrorSalida = value; }
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

        public bool ValidarVariablesEntrada(List<Variable> variablesEntradaActuales)
        {
            return ValidarVariables(variablesEntradaActuales, this.variablesEntrada, this.mensajeErrorEntrada);
        }

        public bool ValidarVariablesSalida(List<Variable> variablesSalidaActuales)
        {
            return ValidarVariables(variablesSalidaActuales, this.variablesSalida, this.mensajeErrorSalida);
        }

        private bool ValidarVariables(List<Variable> variablesActuales, List<VariableTest> variablesContraComparar, string mensaje)
        {
            List<VariableTest> auxiliarEntrada = new List<VariableTest>();

            foreach (var item in variablesActuales)
            {
                VariableTest varTest = new VariableTest()
                {
                    Descripcion = item.Descripcion,
                    Nombre = item.Nombre,
                    ValorEsperado = item.Valor,
                    EsArreglo = item.EsArreglo,
                    TipoDato = item.TipoDato.ToString(),
                    Contexto = item.Contexto
                };

                for (int j = 0; j < item.TopeArr; j++)
                {
                    varTest.Posiciones.Add(new PosicionVariableTest());
                }
               

                auxiliarEntrada.Add(varTest);
            }

            int i = 0;
            bool retorno = true;
            StringBuilder strBldrVarsFaltantes = new StringBuilder();

            while (i < variablesContraComparar.Count)            
            {
                if (!auxiliarEntrada.Contains(variablesContraComparar[i]))
                {
                    strBldrVarsFaltantes.AppendFormat("La variable {0} es requerida por el test, pero no esta declarada de la misma manera dentro de principal ni como variable global.", variablesContraComparar[i].Nombre).AppendLine();
                    retorno = false;
                }
                i++;
            }

            mensaje = strBldrVarsFaltantes.ToString();

            return retorno;
            
        }

        public void ToXML(XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("TestPrueba");
            xml.AddElement();
            xml.SetTitle("Id");
            xml.SetValue(this.Id);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("Nombre");
            xml.SetValue(this.nombre);
            xml.LevelUp();    
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
            this.nombre = xmlElem.FindFirst("Nombre").value;
            this.id = xmlElem.FindFirst("Id").value;

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
            sb.Append(this.nombre);
            sb.Append(this.id);

            return sb.ToString();
        }
        #endregion
    }

    public class VariableTest
    {
        public string Nombre { get; set; }
        public string Contexto { get; set; }
        public string Descripcion { get; set; }
        public string TipoDato { get; set; }
        public string VariableMapeada { get; set; }

        public string ValorEsperado { get; set; }
        public bool EsArreglo { get; set; }
        public List<PosicionVariableTest> Posiciones { get; set; }

        public VariableTest()
        {
            Posiciones = new List<PosicionVariableTest>();
        }

        public void ToXML(XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("VariableTest");
            xml.AddElement();
            xml.SetTitle("Nombre");
            xml.SetValue(this.Nombre);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("Contexto");
            xml.SetValue(this.Contexto);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("EsArreglo");
            xml.SetValue(this.EsArreglo.ToString());
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("TipoDato");
            xml.SetValue(this.TipoDato);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("Descripcion");
            xml.SetValue(this.Descripcion);
            xml.LevelUp();           
            xml.AddElement();
            xml.SetTitle("ValorEsperado");
            xml.SetValue(this.ValorEsperado);
            xml.LevelUp();
            if (!Object.Equals(this.Posiciones, null))
            {
                xml.AddElement();
                xml.SetTitle("Posiciones");
                foreach (PosicionVariableTest pos in this.Posiciones)
                    pos.ToXML(xml);
                xml.LevelUp();
            }
            xml.LevelUp();
        }

        public void FromXML(XMLElement xmlElem)
        {
            if (Object.Equals(xmlElem, null))
                throw new NullReferenceException("El XML para el objeto VariableTest se encuentra nulo.");

            this.Nombre = xmlElem.FindFirst("Nombre").value;
            this.Contexto = xmlElem.FindFirst("Contexto").value;
            this.Descripcion = xmlElem.FindFirst("Descripcion").value;
            this.ValorEsperado = xmlElem.FindFirst("ValorEsperado").value;
            this.TipoDato = xmlElem.FindFirst("TipoDato").value;
            this.Contexto = xmlElem.FindFirst("Contexto").value;
            this.EsArreglo = Convert.ToBoolean(xmlElem.FindFirst("EsArreglo").value);

            XMLElement xmlPosiciones = xmlElem.FindFirst("Posiciones");
            if (!Object.Equals(xmlPosiciones, null))
            {
                List<PosicionVariableTest> varsPos = new List<PosicionVariableTest>();
                foreach (XMLElement xmlPosicion in xmlPosiciones.childs)
                {
                    PosicionVariableTest vt = new PosicionVariableTest();
                    vt.FromXML(xmlPosicion);
                    varsPos.Add(vt);
                }

                this.Posiciones = varsPos;
            }
        }

        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // safe because of the GetType check
            VariableTest variable = (VariableTest)obj;

            // use this pattern to compare reference members
            if (variable.Nombre == this.Nombre
                && variable.TipoDato == this.TipoDato
                && variable.EsArreglo == this.EsArreglo                
                && variable.Posiciones.Count == this.Posiciones.Count )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Object Members
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Nombre);
            sb.Append(this.Contexto);
            sb.Append(this.Descripcion);
            sb.Append(this.TipoDato);
            sb.Append(this.VariableMapeada);
            sb.Append(this.ValorEsperado);
            sb.Append(this.EsArreglo.ToString());

            if (!Object.Equals(this.Posiciones, null))
                foreach (PosicionVariableTest pvt in this.Posiciones)
                    sb.Append(pvt.ToString());

            return sb.ToString();
        }
        #endregion
    }

    public class PosicionVariableTest
    {
        public int Posicion { get; set; }
        public string Valor { get; set; }

        public void ToXML(XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("PosicionVariableTest");
            xml.AddElement();
            xml.SetTitle("Posicion");
            xml.SetValue(this.Posicion.ToString());
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("Valor");
            xml.SetValue(this.Valor);
            xml.LevelUp();            
            xml.LevelUp();
        }

        public void FromXML(XMLElement xmlElem)
        {
            if (Object.Equals(xmlElem, null))
                throw new NullReferenceException("El XML para el objeto PosicionVariableTest se encuentra nulo.");

            this.Posicion = Convert.ToInt32(xmlElem.FindFirst("Posicion").value);
            this.Valor = xmlElem.FindFirst("Valor").value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Posicion);
            sb.Append(this.Valor);
            return sb.ToString();
        }
    }
}
