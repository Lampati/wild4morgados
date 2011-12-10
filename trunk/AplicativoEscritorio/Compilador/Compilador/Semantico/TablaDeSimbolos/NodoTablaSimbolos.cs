using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Compilador.Auxiliares;

namespace Compilador.Semantico.TablaDeSimbolos
{
    class NodoTablaSimbolos
    {
        public enum TipoDeDato
        {
            [DescriptionAttribute("String")]
            String,
            [DescriptionAttribute("Numero")]
            Numero,
            [DescriptionAttribute("Booleano")]
            Booleano,
            [DescriptionAttribute("Ninguno")]
            Ninguno
        }

        public enum TipoDeEntrada
        {
            [DescriptionAttribute("Temporal")]
            Temporal,
            [DescriptionAttribute("Parametro")]
            Parametro,
            [DescriptionAttribute("Variable")]
            Variable,
            [DescriptionAttribute("Funcion")]
            Funcion,
            [DescriptionAttribute("Procedimiento")]
            Procedimiento
        }

        public enum TipoContexto
        {
            [DescriptionAttribute("Global")]
            Global,
            [DescriptionAttribute("Local")]
            Local
        }

        private string nombre;
        public string Nombre 
        {
            get { return nombre; }
        }

        private TipoContexto contexto;
        public TipoContexto Contexto
        {
            get { return contexto; }
        }

        private TipoDeEntrada tipoEntrada;
        public TipoDeEntrada TipoEntrada
        {
            get { return tipoEntrada; }
        }

        private TipoDeDato tipoDato;
        public TipoDeDato TipoDato
        {
            get { return tipoDato; }
        }

        private List<TipoDeDato> firma;
        public List<TipoDeDato> Firma
        {
            get { return firma; }
        }

        private bool esArreglo;
        public bool EsArreglo
        {
            get { return esArreglo; }
        }

        private bool esConstante;
        public bool EsConstante
        {
            get { return esConstante; }
        }

        private int indice;
        public int Indice
        {
            get { return indice; }
        }

        private int valor;
        public int Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        private int cantTemporalesRequeridos;
        public int CantTemporalesRequeridos
        {
            get { return cantTemporalesRequeridos; }
            set { cantTemporalesRequeridos = value; }
        }

        private string nombreContextoLocal;
        public string NombreContextoLocal
        {
            get { return nombreContextoLocal; }
            set { nombreContextoLocal = value; }
        }


        private int tamanio;
        public int Tamanio
        {
            get { return tamanio; }
            set { tamanio = value; }
        }

        private int desplazamiento;
        public int Desplazamiento
        {
            get { return desplazamiento; }
            set { desplazamiento = value; }
        }

        private string valorString;
        public string ValorString
        {
            get { return valorString; }
            set { valorString = value; }
        }

        public NodoTablaSimbolos(string nom, TipoDeEntrada entrada, TipoDeDato tdato, List<TipoDeDato> firma, int cantTemps)
        {
            this.nombre = nom;
            this.tipoEntrada = entrada;
            this.tipoDato = tdato;
            this.valor = int.MinValue;
            this.firma = new List<TipoDeDato>(firma);
            this.contexto = TipoContexto.Global;
            this.nombreContextoLocal = string.Empty;
            this.cantTemporalesRequeridos = cantTemps;
        }



        public NodoTablaSimbolos(string nom, TipoDeEntrada entrada, TipoDeDato tdato,
            Int32 valor, bool esArreglo, int indice, bool esConst, TipoContexto cont, string nombreProc,
            int tam, int desp)
        {
            this.nombre = nom;
            this.tipoEntrada = entrada;
            this.tipoDato = tdato;
            this.valor = valor;
            this.firma = null;
            this.contexto = cont;
            this.esArreglo = esArreglo;
            this.esConstante = esConst;
            this.nombreContextoLocal = nombreProc;
            this.tamanio = tam;
            this.desplazamiento = desp;
        }

        public NodoTablaSimbolos(string nom, TipoDeEntrada entrada, TipoDeDato tdato, Int32 valor,
            bool esConst, TipoContexto cont, string nombreProc, int tam, int desp)
        {
            this.nombre = nom;
            this.tipoEntrada = entrada;
            this.tipoDato = tdato;
            this.valor = valor;
            this.firma = null;
            this.esConstante = esConst;
            this.contexto = cont;
            this.nombreContextoLocal = nombreProc;
            this.tamanio = tam;
            this.desplazamiento = desp;
        }

        public NodoTablaSimbolos(string nom, TipoDeEntrada entrada, TipoDeDato tdato, string valor,
            bool esConst, TipoContexto cont, string nombreProc, int tam, int desp)
        {
            this.nombre = nom;
            this.tipoEntrada = entrada;
            this.tipoDato = tdato;
            this.valorString = valor;
            this.firma = null;
            this.esConstante = esConst;
            this.contexto = cont;
            this.nombreContextoLocal = nombreProc;
            this.tamanio = tam;
            this.desplazamiento = desp;
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
            NodoTablaSimbolos nodo = (NodoTablaSimbolos)obj;

            // use this pattern to compare reference members
            if (Nombre.Equals(nodo.Nombre) && TipoEntrada.Equals(nodo.TipoEntrada))
            {
                return true;
            }
            else
            {
                return false;
            }   
        }

        public override string ToString()
        {
            StringBuilder strBldr = new StringBuilder(string.Empty);

            strBldr.Append(EnumUtils.stringValueOf(TipoEntrada)).Append(", ");

            switch (TipoEntrada)
            {
                case TipoDeEntrada.Temporal:
                    strBldr.Append(Nombre).Append(", ");                    
                    strBldr.Append(EnumUtils.stringValueOf(TipoDato)).Append(", ");
                    strBldr.Append(tamanio.ToString()).Append(", ");
                    strBldr.Append(desplazamiento.ToString()).Append(", ");
                    strBldr.Append(valorString == string.Empty ? string.Empty : valorString);
                    break;

                case TipoDeEntrada.Parametro:
                case TipoDeEntrada.Variable:

                    strBldr.Append(Nombre).Append(", ");
                    strBldr.Append(EnumUtils.stringValueOf(Contexto)).Append(", ");
                    strBldr.Append(NombreContextoLocal).Append(", ");
                    strBldr.Append(EnumUtils.stringValueOf(TipoDato)).Append(", ");
                    strBldr.Append(tamanio.ToString()).Append(", ");
                    strBldr.Append(desplazamiento.ToString());

                    break;

                case TipoDeEntrada.Funcion:
                case TipoDeEntrada.Procedimiento:

                    strBldr.Append(Nombre).Append(", ");
                    strBldr.Append(EnumUtils.stringValueOf(TipoDato)).Append(", temporalesReq:");
                    strBldr.Append(cantTemporalesRequeridos.ToString());

                    break;

            }

            return strBldr.ToString();
        }

    }
}
