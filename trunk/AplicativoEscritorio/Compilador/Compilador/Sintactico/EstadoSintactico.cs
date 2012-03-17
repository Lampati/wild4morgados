using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico
{
    internal enum ContextoGlobal
    {
        Global,
        GlobalDeclaraciones,
        DeclaracionLocal,
        Cuerpo
    }


    internal enum ContextoLinea
    {
        Asignacion,
        Leer,
        LlamadaProc,
        Mientras,
        Si,
        Sino,
        DeclaracionFuncion,
        DeclaracionProc,
        DeclaracionConstante,
        DeclaracionVariable,
        FinFuncion,
        FinProc,
        FinMientras,
        FinSi,
        Mostrar,
        
        Ninguno

    }


    internal enum ElementoPila
    {
        Mientras,
        Si,
        Sino,
        Proc,
        Func
    }

    internal static class EstadoSintactico
    {
        

        private static List<Terminal> listaLeidoHastaAhora = new List<Terminal>();

        private static List<Terminal> listaLineaActual = new List<Terminal>();

        

        private static ContextoGlobal contextoGlobal = Sintactico.ContextoGlobal.Global;
        internal static ContextoGlobal ContextoGlobal
        {
            get { return contextoGlobal; }
            set { contextoGlobal = value; }
        }

        private static ContextoLinea contextoLinea = ContextoLinea.Ninguno;
        internal static ContextoLinea ContextoLinea
        {
            get { return contextoLinea; }
            set { contextoLinea = value; }
        }

        private static Stack<ElementoPila> pila = new Stack<ElementoPila>();

        public static string LineaActual
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();
                foreach (var item in listaLineaActual)
                {
                    strBldr.Append(item.Componente.Lexema).Append(" ");
                }
                return strBldr.ToString();
            }
        }

        public static List<Terminal> ListaLineaActual
        {
            get
            {
                return listaLineaActual;
            }
        }

        internal static void Reiniciar()
        {
            listaLeidoHastaAhora.Clear();
            listaLineaActual.Clear();
            contextoGlobal = Sintactico.ContextoGlobal.Global;
            contextoLinea = Sintactico.ContextoLinea.Ninguno;
        }
            

        internal static void AgregarTerminal(Terminal t)
        {
            if (contextoLinea == ContextoLinea.Ninguno)
            {
                NuevaLinea(t);
            }
            else
            {
                List<Terminal> terminalesQueTerminanLinea = new List<Terminal>();
                switch (contextoLinea)
                {
                    case ContextoLinea.Asignacion:                        
                    case ContextoLinea.Leer:                        
                    case ContextoLinea.LlamadaProc:
                    case ContextoLinea.DeclaracionConstante:
                    case ContextoLinea.DeclaracionVariable:
                    case ContextoLinea.Mostrar:
                    case ContextoLinea.FinProc:
                    case ContextoLinea.FinFuncion:
                    case ContextoLinea.FinSi:
                    case ContextoLinea.FinMientras:
                        terminalesQueTerminanLinea.Add(Terminal.ElementoFinSentencia());
                        break;
                    case ContextoLinea.Mientras:
                        terminalesQueTerminanLinea.Add(Terminal.ElementoHacer());
                        break;
                    case ContextoLinea.Si:
                        terminalesQueTerminanLinea.Add(Terminal.ElementoEntonces());
                        break;                    
                    case ContextoLinea.DeclaracionFuncion:
                        terminalesQueTerminanLinea.Add(Terminal.ElementoTipoBooleano());
                        terminalesQueTerminanLinea.Add(Terminal.ElementoTipoNumero());
                        terminalesQueTerminanLinea.Add(Terminal.ElementoTipoTexto());
                        break;
                    case ContextoLinea.DeclaracionProc:
                        terminalesQueTerminanLinea.Add(Terminal.ElementoParentesisClausura());
                        break;
                        
                }

                if (terminalesQueTerminanLinea.Contains(t))
                {
                    FinLinea(t);
                }
                else
                {
                   
                    AgregarALinea(t);
                }
            }

        }

        private static void AgregarALinea(Terminal t)
        {            
            listaLineaActual.Add(t);
            listaLeidoHastaAhora.Add(t);
        }

        private static void NuevaLinea(Terminal t)
        {
            //ojo con el sino!
            switch (t.Componente.Token)
            {
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Mostrar:
                    contextoLinea = ContextoLinea.Mostrar;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MientrasComienzo:
                    contextoLinea = ContextoLinea.Mientras;
                    pila.Push(ElementoPila.Mientras);
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MientrasFin:
                    contextoLinea = ContextoLinea.FinMientras;
                    pila.Pop();
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiComienzo:
                    contextoLinea = ContextoLinea.Si;
                    pila.Push(ElementoPila.Si);
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiSino:
                    pila.Push(ElementoPila.Sino);
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiFin:
                    contextoLinea = ContextoLinea.FinSi;
                    if (pila.Peek() == ElementoPila.Sino)
                    {
                        pila.Pop();
                    }
                    pila.Pop();
                    
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ProcedimientoComienzo:
                    contextoGlobal = Sintactico.ContextoGlobal.DeclaracionLocal;
                    contextoLinea = ContextoLinea.DeclaracionProc;
                    pila.Push(ElementoPila.Proc);
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ProcedimientoFin:
                    contextoGlobal = Sintactico.ContextoGlobal.Global;
                    contextoLinea = ContextoLinea.FinProc;
                    pila.Pop();
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.FuncionComienzo:
                    contextoGlobal = Sintactico.ContextoGlobal.DeclaracionLocal;
                    contextoLinea = ContextoLinea.DeclaracionFuncion;
                    pila.Push(ElementoPila.Func);
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.FuncionFin:
                    contextoGlobal = Sintactico.ContextoGlobal.Global;
                    contextoLinea = ContextoLinea.FinFuncion;
                    pila.Pop();
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Comenzar:
                    contextoGlobal = ContextoGlobal.Cuerpo;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Constantes:
                    contextoGlobal = Sintactico.ContextoGlobal.GlobalDeclaraciones;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Variables:
                    contextoGlobal = Sintactico.ContextoGlobal.GlobalDeclaraciones;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Const:
                    contextoLinea = ContextoLinea.DeclaracionConstante;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Var:
                    contextoLinea = ContextoLinea.DeclaracionVariable;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Llamar:
                    contextoLinea = ContextoLinea.LlamadaProc;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Leer:
                    contextoLinea = ContextoLinea.Leer;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Identificador:
                    contextoLinea = ContextoLinea.Asignacion;
                    break;
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Principal:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Salida:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Numero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Verdadero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Falso:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.TipoDato:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.TipoNumero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.TipoTexto:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.TipoBooleano:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SiEntonces:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Asignacion:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Concatenacion:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.SumaEntero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.RestaEntero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MultiplicacionEntero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.DivisionEntero:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Mayor:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MayorIgual:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Igual:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MenorIgual:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Menor:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Distinto:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Negacion:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.And:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Or:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ParentesisApertura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ParentesisClausura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.CorcheteApertura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.CorcheteClausura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Literal:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Coma:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.De:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Arreglo:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ComentarioApertura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.ComentarioClausura:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Ninguno:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.Error:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.EOF:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.FinSentencia:
                case CompiladorGargar.Lexicografico.ComponenteLexico.TokenType.MientrasHacer:
                default:
                    contextoLinea = ContextoLinea.Ninguno;
                    break;
            }

            if (contextoLinea != Sintactico.ContextoLinea.Ninguno)
            {
                AgregarALinea(t);
            }
        }

        private static void FinLinea(Terminal t)
        {
            listaLeidoHastaAhora.Add(t);
            contextoLinea = ContextoLinea.Ninguno;
            listaLineaActual.Clear();
        }
        
    }

    
}
