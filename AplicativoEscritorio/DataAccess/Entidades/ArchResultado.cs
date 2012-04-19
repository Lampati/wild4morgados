using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections.ObjectModel;

namespace DataAccess.Entidades
{
    public class ArchResultado
    {
        public ObservableCollection<Variable> VariablesSalida {get;set;}
        public ObservableCollection<ErrorEjecucion> Errores { get; set; }
        public ObservableCollection<Entrada> Entradas {get;set;}

        
         

        public bool EsCorrectaEjecucion { get; set; }

        public ArchResultado(string xmlFileName)
        {
            VariablesSalida = new ObservableCollection<Variable>();
            Errores = new ObservableCollection<ErrorEjecucion>();
            Entradas = new ObservableCollection<Entrada>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFileName);

                XmlNode nodoResFinal = xmlDoc.GetElementsByTagName("resultadoFinal")[0];

                foreach (XmlNode nodo in nodoResFinal.ChildNodes)
                {

                    if (nodo.Name == "variables")
                    {
                        foreach (XmlNode item in nodo.ChildNodes)
                        {
                            string nombre = item.Attributes["Nombre"].Value;
                            string contexto = item.Attributes["Contexto"].Value;
                            string tipo = ConvertirATipoGarGar(item.Attributes["Tipo"].Value);
                            bool esArreglo = Convert.ToBoolean(item.Attributes["EsArreglo"].Value);
                            string valor = string.Empty;
                            List<PosicionArreglo> listaPosiciones = new List<PosicionArreglo>();
                            string tipoVar;

                            if (esArreglo)
                            {
                                int tope = Convert.ToInt32(item.Attributes["Tope"].Value);
                                tipoVar = string.Format("Arreglo con tope {0}", tope);

                                foreach (XmlNode pos in item.ChildNodes)
                                {
                                    int posicion = Convert.ToInt32(pos.Attributes["posicion"].Value);
                                    string val = pos.InnerText;
                                    listaPosiciones.Add(new PosicionArreglo(posicion, val));
                                }
                            }
                            else
                            {
                                valor = item.FirstChild.InnerText;
                                tipoVar = "Variable";
                            }

                            VariablesSalida.Add(new Variable(nombre, contexto, tipo, tipoVar, esArreglo, valor, listaPosiciones));

                        }

                        
                    }
                    else if (nodo.Name == "res")
                    {
                        EsCorrectaEjecucion = Convert.ToBoolean(nodo.InnerText);

                    }
                }


                XmlNode nodoErrores = xmlDoc.GetElementsByTagName("errores")[0];

                if (nodoErrores.HasChildNodes)
                {
                    foreach (XmlNode item in nodoErrores.ChildNodes)
                    {
                        string desc = item.InnerText;
                        string tipo = item.Attributes["tipo"].Value;
                        int linea = Convert.ToInt32(item.Attributes["linea"].Value);

                        Errores.Add(new ErrorEjecucion() { Descripcion = desc, Linea = linea, TipoError = tipo });
                    }
                }

                XmlNode nodoEntradas = xmlDoc.GetElementsByTagName("entradasParciales")[0];

                if (nodoEntradas.HasChildNodes)
                {
                    foreach (XmlNode entrada in nodoEntradas.ChildNodes)
                    {
                        int linea = Convert.ToInt32(entrada.Attributes["linea"].Value);

                        ObservableCollection<Variable> variablesEntrada = new ObservableCollection<Variable>();

                        foreach (XmlNode item in entrada.ChildNodes)
                        {
                            string nombre = item.Attributes["Nombre"].Value;
                            string contexto = item.Attributes["Contexto"].Value;
                            string tipo = ConvertirATipoGarGar(item.Attributes["Tipo"].Value);
                            bool esArreglo = Convert.ToBoolean(item.Attributes["EsArreglo"].Value);
                            string valor = string.Empty;
                            List<PosicionArreglo> listaPosiciones = new List<PosicionArreglo>();
                            string tipoVar;

                            if (esArreglo)
                            {
                                int tope = Convert.ToInt32(item.Attributes["Tope"].Value);
                                tipoVar = string.Format("Arreglo con tope {0}", tope);

                                foreach (XmlNode pos in item.ChildNodes)
                                {
                                    int posicion = Convert.ToInt32(pos.Attributes["posicion"].Value);
                                    string val = pos.InnerText;
                                    listaPosiciones.Add(new PosicionArreglo(posicion, val));
                                }
                            }
                            else
                            {
                                valor = item.FirstChild.InnerText;
                                tipoVar = "Variable";
                            }

                            variablesEntrada.Add(new Variable(nombre, contexto, tipo, tipoVar, esArreglo, valor, listaPosiciones));
                        }

                        Entradas.Add(new Entrada() { Linea = linea, Variables = variablesEntrada });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string ConvertirATipoGarGar(string p)
        {
            string retorno = string.Empty;
            switch (p.ToUpper())
            {
                case "INTEGER":
                case "REAL":
                    retorno = "Numero";
                    break;
                case "STRING":
                    retorno = "Texto";
                    break;
                case "BOOLEAN":
                    retorno = "Booleano";
                    break;
                default:
                    break;
            }

            return retorno;
        }
    }

    public class ErrorEjecucion
    {
        public string TipoError { get; set; }
        public string Descripcion { get; set; }
        public int Linea { get; set; }

    }

    public class Entrada
    {
        public int Linea { get; set; }
        public ObservableCollection<Variable> Variables { get; set; }
    }


}
