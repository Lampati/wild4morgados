﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Semantico.TablaDeSimbolos
{


    public class NodoTablaSimbolos
    {
        public enum TipoDeDato
        {
            [DescriptionAttribute("Texto")]
            Texto,
            [DescriptionAttribute("Numero")]
            Numero,
            [DescriptionAttribute("Booleano")]
            Booleano,
            [DescriptionAttribute("Ninguno")]
            Ninguno
        }

        public enum TipoDeEntrada
        {      
            [DescriptionAttribute("Parametro")]
            Parametro,
            [DescriptionAttribute("Variable")]
            Variable,
            [DescriptionAttribute("Funcion")]
            Funcion,
            [DescriptionAttribute("Procedimiento")]
            Procedimiento,
            [DescriptionAttribute("AuxiliarCodigoIntermedio")]
            AuxiliarCodigoIntermedio
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

        
        public string NombreParaCodigo
        {
            get { return string.Format("ProgramArVariable__00__{0}",nombre); }
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

        private List<FirmaProc> firma;
        public List<FirmaProc> Firma
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

    
       
        private string nombreContextoLocal;
        public string NombreContextoLocal
        {
            get { return nombreContextoLocal; }
            set { nombreContextoLocal = value; }
        }

        private int valorInt;
        public int ValorInt
        {
            get { return valorInt; }
            set { valorInt = value; }
        }



        internal NodoTablaSimbolos(string nom, TipoDeEntrada entrada, TipoDeDato tdato, List<FirmaProc> firma)
        {
            this.nombre = nom;
            this.tipoEntrada = entrada;
            this.tipoDato = tdato;
            this.firma = new List<FirmaProc>(firma);
            this.contexto = TipoContexto.Global;
            this.nombreContextoLocal = string.Empty;
            this.valorInt = 0;
        }



        internal NodoTablaSimbolos(string nom, TipoDeEntrada entrada, TipoDeDato tdato,
             bool esArreglo,  bool esConst, TipoContexto cont, string nombreProc)
        {
            this.nombre = nom;
            this.tipoEntrada = entrada;
            this.tipoDato = tdato;
            this.firma = null;
            this.contexto = cont;
            this.esArreglo = esArreglo;
            this.esConstante = esConst;
            this.nombreContextoLocal = nombreProc;
            this.valorInt = 0;
            
        }

        internal NodoTablaSimbolos(string nom, TipoDeEntrada entrada, TipoDeDato tdato, 
            bool esConst, TipoContexto cont, string nombreProc)
        {
            this.nombre = nom;
            this.tipoEntrada = entrada;
            this.tipoDato = tdato;
            this.firma = null;
            this.esConstante = esConst;
            this.contexto = cont;
            this.nombreContextoLocal = nombreProc;
            this.valorInt = 0;
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

                case TipoDeEntrada.Parametro:
                case TipoDeEntrada.Variable:

                    strBldr.Append(Nombre).Append(", ");
                    strBldr.Append(EnumUtils.stringValueOf(Contexto)).Append(", ");
                    strBldr.Append(NombreContextoLocal).Append(", ");
                    strBldr.Append(EnumUtils.stringValueOf(TipoDato)).Append(", ");
                    

                    break;

                case TipoDeEntrada.Funcion:
                case TipoDeEntrada.Procedimiento:

                    strBldr.Append(Nombre).Append(", ");
                    strBldr.Append(EnumUtils.stringValueOf(TipoDato));

                    break;

            }

            return strBldr.ToString();
        }

    }

    
}