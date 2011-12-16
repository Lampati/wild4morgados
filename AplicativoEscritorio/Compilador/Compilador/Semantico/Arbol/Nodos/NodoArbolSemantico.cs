﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;
using System.Reflection;
using System.Collections;
using Compilador.Semantico.Arbol.Nodos.Auxiliares;
using System.Diagnostics;
using System.Windows.Forms;
using Compilador.Semantico.Arbol.Temporales;
using Compilador.Semantico.Arbol.Labels;
using System.ComponentModel;

namespace Compilador.Semantico.Arbol.Nodos
{
    abstract class NodoArbolSemantico
    {
        public enum TipoOperatoria
        {
            Suma,
            Resta,
            Multiplicacion,
            Division,
            Concatenacion,
            And,
            Or,
            Ninguna
        }

        public enum TipoComparacion
        {
            //Entero
            Mayor,
            MayorIgual,            
            Menor,
            MenorIgual,            
            Igual,
            Distinto,            
            None
        }

        public enum TipoDeclaracionesPermitidas
        {
            //Entero
            Variables,
            Constantes,
            Ninguno
        }

    

        public TablaSimbolos TablaSimbolos { get; set; }
        public int Valor { get; set; }
        public NodoTablaSimbolos.TipoDeDato TipoDato { get; set; }
        public bool EsConstante { get; set; }
        public NodoTablaSimbolos.TipoContexto ContextoActual { get; set; }
        public bool ProcPrincipalCrearUnaVez { get; set; }
        public bool ProcPrincipalYaCreadoyCorrecto {get; set;}

        public bool ProcSalidaCrearUnaVez { get; set; }
        public bool ProcSalidaYaCreadoyCorrecto { get; set; }
        public string Lexema { get; set; }
        public string nombreContextoLocal { get; set; }
        public bool EsArreglo { get; set; }
        public bool EsFuncion { get; set; }
        public int IndiceArreglo { get; set; }
        public List<Variable> VariablesACrear { get; set; }
        public List<Firma> ListaFirma { get; set; }
        public TipoOperatoria Operacion { get; set; }
        public string TextoParaImprimirArbol { get; set; }

        public bool EsProcSalida { get; set; }

        public TipoDeclaracionesPermitidas DeclaracionesPermitidas { get; set; }

        //Entrega 4

        //sintetizado
        public bool NecesitaTemporal { get; set; }

        public List<string> ListaElementosVisualizar { get; set; }
        public TipoComparacion Comparacion { get; set; }
        public bool EsSino { get; set; }

        public Temporal Temporal { get; set; }

        public CodeLabel LabelFin { get; set; }
        public CodeLabel LabelVerdadero { get; set; }
        public CodeLabel LabelFalso { get; set; }
        //Para labels auxiliares que tenga que crear el nodo
        public List<CodeLabel> ListaLabel { get; set; }

        public string Codigo { get; set; }
        public string Lugar { get; set; }

        public string LugarExp { get; set; }
        public string LugarMul { get; set; }

        public bool EsOtraExpresion { get; set; }


        protected ElementoGramatica elemento;

        protected NodoArbolSemantico padreNodo;
        public NodoArbolSemantico PadreNodo
        {
            get
            {
                return padreNodo;
            }
        }

        protected List<NodoArbolSemantico> hijosNodo;


        protected bool inicializado;
        public bool CalculadoAtributosHijos
        {
            get
            {
                return AtributosCalculadosEnNodo() && inicializado;
            }

        }

        public bool CalculadoTemporalesHijos
        {
            get
            {
                return ChequeoDeCalculadosEnNodo(temporalesCalculados);
            }

        }

        public bool CalculadoLabelsHijos
        {
            get
            {
                return ChequeoDeCalculadosEnNodo(labelsCalculados);
            }

        }

        public bool CalculadoCodigoHijos
        {
            get
            {
                return ChequeoDeCalculadosEnNodo(codigoCalculado);
            }

        }

        //public bool ChequeoDeCalculadosEnNodo

        protected bool[] atributosCalculados;
        protected bool[] temporalesCalculados;
        protected bool[] labelsCalculados;
        protected bool[] codigoCalculado;

        public int ObtenerCantidadHijos()
        {
            return this.hijosNodo.Count();
        }

        public NodoArbolSemantico ObtenerHijo(int i)
        {
            if (i < this.hijosNodo.Count)
            {
                return this.hijosNodo[i];
            }
            else
            {
                return null;
            }
        }

        internal void CrearTablaSimbolos()
        {
            this.TablaSimbolos = new TablaSimbolos();
        }

        public NodoArbolSemantico(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
        {
            this.elemento = elem;
            this.padreNodo = nodoPadre;
            this.hijosNodo = new List<NodoArbolSemantico>();

            inicializado = false;

            //Sintetizados
            this.Valor = int.MinValue;
            this.TipoDato = NodoTablaSimbolos.TipoDeDato.Ninguno;
            this.Comparacion = TipoComparacion.None;
            
            this.EsFuncion = false;
            this.EsArreglo = false;
            this.Operacion = TipoOperatoria.Ninguna;
            this.TextoParaImprimirArbol = string.Empty;
            this.Codigo = string.Empty;
            this.Lugar = string.Empty;

            this.ListaLabel = new List<CodeLabel>();
            this.ListaElementosVisualizar = new List<string>();

            //Heredados
            this.DeclaracionesPermitidas = TipoDeclaracionesPermitidas.Ninguno;
            this.EsProcSalida = false;

            if (nodoPadre != null)
            {
                this.EsConstante = nodoPadre.EsConstante;
                this.ContextoActual = nodoPadre.ContextoActual;
                this.TablaSimbolos = nodoPadre.TablaSimbolos;
                this.nombreContextoLocal = nodoPadre.nombreContextoLocal;
                this.ProcPrincipalYaCreadoyCorrecto = nodoPadre.ProcPrincipalYaCreadoyCorrecto;
                this.ProcPrincipalCrearUnaVez = nodoPadre.ProcPrincipalCrearUnaVez;
                this.ProcSalidaYaCreadoyCorrecto = nodoPadre.ProcSalidaYaCreadoyCorrecto;
                this.ProcSalidaCrearUnaVez = nodoPadre.ProcSalidaCrearUnaVez;
                this.DeclaracionesPermitidas = nodoPadre.DeclaracionesPermitidas;
                this.EsProcSalida = nodoPadre.EsProcSalida;
            }
        }

        public void AgregarHijos(Produccion prod)
        {
            atributosCalculados = new bool[prod.Der.Count];

            inicializado = true;

            for (int i = 0; i < prod.Der.Count; i++)
            {
                //atributosCalculados[i] = (prod.Der[i].GetType() == typeof(Terminal)) ? true : false;
                atributosCalculados[i] = false;

                if (prod.Der[i].GetType() == typeof(Terminal))
                {
                    this.hijosNodo.Add(new NodoTerminal(this,(Terminal)prod.Der[i]));
                }
                else
                {
                    try
                    {
                        Type t = Type.GetType("Compilador.Semantico.Arbol.Nodos.Nodo" + prod.Der[i].ToString(), true, true);

                        ConstructorInfo ci = t.GetConstructor(new Type[] { typeof(NodoArbolSemantico), typeof(ElementoGramatica) });

                        this.hijosNodo.Add((NodoArbolSemantico)ci.Invoke(new object[] { this, prod.Der[i] }));
                        //this.hijosNodo.Add(new NodoPasaManos(this, prod.Der[i]));
                    }
                    catch (Exception ex)
                    {
                        Utils.Log.AddError(ex.Message);
                        this.hijosNodo.Add((NodoArbolSemantico)new NodoPasaManos(this, prod.Der[i]));
                    }
                }

                
            }
        }

        #region Controladores para calcular atributos, temporales, labels y codigo

        private bool AtributosCalculadosEnNodo()
        {
            bool retorno = true;
            int i = 0;
            while (i < this.hijosNodo.Count && retorno == true)
            {
                retorno = atributosCalculados[i];
                i++;
            }
            return retorno;
        }

        private bool ChequeoDeCalculadosEnNodo(bool[] arregloAControlar)
        {
            bool retorno = true;
            int i = 0;
            while (i < this.hijosNodo.Count && retorno == true)
            {
                retorno = arregloAControlar[i];
                i++;
            }
            return retorno;
        }

        #endregion




        internal NodoArbolSemantico ObtenerPrimerHijo()
        {
            this.hijosNodo[0].TablaSimbolos = this.TablaSimbolos;
            return this.hijosNodo[0];
        }
    

        internal void ActualizarAtributos(NodoArbolSemantico nodo)
        {
            this.TablaSimbolos = nodo.TablaSimbolos;
            this.nombreContextoLocal = nodo.nombreContextoLocal;
            this.ProcPrincipalYaCreadoyCorrecto = nodo.ProcPrincipalYaCreadoyCorrecto;
            this.ProcPrincipalCrearUnaVez = nodo.ProcPrincipalCrearUnaVez;
            this.ProcSalidaYaCreadoyCorrecto = nodo.ProcSalidaYaCreadoyCorrecto;
            this.ProcSalidaCrearUnaVez = nodo.ProcSalidaCrearUnaVez;


            //PRUEBA
            //this.Lugar = nodo.Lugar;

 	        int i = this.hijosNodo.IndexOf(nodo);

            this.atributosCalculados[i] = true;

            this.hijosNodo[i] = nodo;
        }
    
        internal NodoArbolSemantico ProximoNodoACalcular()
        {
            bool retorno = true;
            int i = 0;
            while (i < this.hijosNodo.Count && retorno == true)
            {
                if (atributosCalculados[i] == false)
                {
                    retorno = false;
                }
                else
                {
                    i++;
                }
            }

            if (i < this.hijosNodo.Count)
            {
                this.HeredarAtributosANodo(this.hijosNodo[i]);
                
                return this.hijosNodo[i];
            }
            else
            {
                return this;
            }
 	       
        }


        public virtual NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            foreach (NodoArbolSemantico nodo in this.hijosNodo)
            {
                int i = this.hijosNodo.IndexOf(nodo);

                if (this.atributosCalculados[i])
                {
                    //poner los sintetizados aca
                    this.SintetizarAtributosANodo(this.hijosNodo[i]);
                    
                }
            }
            return this;
        }


        public abstract void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar);

        public abstract void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar);

        public abstract void ChequearAtributos(Terminal t);
        
        public abstract NodoArbolSemantico SalvarAtributosParaContinuar();



        public override string ToString()
        {
            return this.elemento.ToString();
        }

        public string RecorrerArbolYDibujar(int n)
        {                       
            StringBuilder strbldr = new StringBuilder();



            for (int t = 0; t < n; t++)
            {
                strbldr.Append("  ");
            }

            if (this.ToString().Equals("ASIGNACION"))
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
            }

            if (!this.TextoParaImprimirArbol.Equals(string.Empty))
            {
                for (int i = 0; i < this.hijosNodo.Count; i++)
                {
                    strbldr.AppendLine(this.hijosNodo[i].Dibujar());
                    
                    strbldr.Append(this.hijosNodo[i].RecorrerArbolYDibujar(n + 1));
                }
            }
            else
            {
                for (int i = 0; i < this.hijosNodo.Count; i++)
                {
                    strbldr.Append(this.hijosNodo[i].RecorrerArbolYDibujar(n));
                }
            }

            return strbldr.ToString();
        }

        public virtual string Dibujar()
        {
            return this.TextoParaImprimirArbol;
        }



        //Entrega 4 -- Generacion de codigo


        public virtual void CalcularCodigo()
        {
            
        }


        public virtual void CalcularExpresiones()
        {
            
        }
    }
}
