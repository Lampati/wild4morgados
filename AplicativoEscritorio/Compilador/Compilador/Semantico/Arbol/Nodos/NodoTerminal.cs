using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Interfases;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Lexicografico;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using System.Diagnostics;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoTerminal : NodoArbolSemantico, IValorizable, ITipificable
    {
        public NodoTerminal(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            inicializado = true;            
        }

        #region IValorizable Members

        public int ObtenerValor(Terminal t)
        {
            switch (t.Componente.Token)
            {
                case ComponenteLexico.TokenType.Numero:
                    return Convert.ToInt32(t.Componente.Lexema);                    

        
                default:
                    return int.MinValue;
            }
            
        }

        #endregion

        #region ITipificable Members

        public NodoTablaSimbolos.TipoDeDato ObtenerTipo(Terminal t)
        {
            switch (t.Componente.Token)
            {
                case ComponenteLexico.TokenType.Numero:
                    return NodoTablaSimbolos.TipoDeDato.Numero;

                case ComponenteLexico.TokenType.Verdadero:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;

                case ComponenteLexico.TokenType.Falso:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;

                case ComponenteLexico.TokenType.Literal:
                    return NodoTablaSimbolos.TipoDeDato.String;

                case ComponenteLexico.TokenType.TipoNumero:
                    return NodoTablaSimbolos.TipoDeDato.Numero;

                case ComponenteLexico.TokenType.TipoBooleano:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;
         
                case ComponenteLexico.TokenType.TipoTexto:
                    return NodoTablaSimbolos.TipoDeDato.String;  


                case ComponenteLexico.TokenType.SumaEntero:
                    return NodoTablaSimbolos.TipoDeDato.Numero;              



                case ComponenteLexico.TokenType.RestaEntero:
                    return NodoTablaSimbolos.TipoDeDato.Numero;

               

                case ComponenteLexico.TokenType.MultiplicacionEntero:
                    return NodoTablaSimbolos.TipoDeDato.Numero;

                

                case ComponenteLexico.TokenType.DivisionEntero:
                    return NodoTablaSimbolos.TipoDeDato.Numero;


                case ComponenteLexico.TokenType.Concatenacion:
                    return NodoTablaSimbolos.TipoDeDato.String;

                case ComponenteLexico.TokenType.And:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;
                case ComponenteLexico.TokenType.Or:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;

                case ComponenteLexico.TokenType.Mayor:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;
                case ComponenteLexico.TokenType.MayorIgual:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;
                case ComponenteLexico.TokenType.Menor:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;
                case ComponenteLexico.TokenType.MenorIgual:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;
                case ComponenteLexico.TokenType.Igual:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;
                case ComponenteLexico.TokenType.Distinto:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;
                case ComponenteLexico.TokenType.Negacion:
                    return NodoTablaSimbolos.TipoDeDato.Booleano;

                default:
                    return NodoTablaSimbolos.TipoDeDato.Ninguno;
            }
        }
        #endregion

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            this.TipoDato = this.ObtenerTipo(t);
            this.Lexema = t.Componente.Lexema;

            switch (this.Lexema)
            {
                case "=":
                    this.Comparacion = TipoComparacion.Igual;
                    break;
                case "<>":
                    this.Comparacion = TipoComparacion.Distinto;
                    break;
                case "<=":
                    this.Comparacion = TipoComparacion.MenorIgual;
                    break;
                case "<":
                    this.Comparacion = TipoComparacion.Menor;
                    break;
                case ">=":
                    this.Comparacion = TipoComparacion.MayorIgual;
                    break;
                case ">":
                    this.Comparacion = TipoComparacion.Mayor;
                    break;
                default:
                    this.Comparacion = TipoComparacion.None;
                    break;
            }

            this.VariablesACrear = new List<Variable>();
            this.ListaFirma = new List<Firma>();

            if (t.Componente.Token == ComponenteLexico.TokenType.Numero)
            {
                this.ValorConstanteNumerica = Convert.ToInt32(t.Componente.Lexema);
            }

            return this;
        }

        public override void ChequearAtributos(Terminal t)
        {

        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
        {

        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
        {

        }
    }
}
