using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Lexicografico;
using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Sintactico.Gramatica
{
    internal class Terminal : ElementoGramatica
    {
        #region estaticas EOF, lambda y ERROR

        internal static Terminal ElementoVacio()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.Ninguno;

            return t;
        }

        internal static Terminal ElementoFinSentencia()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.FinSentencia;
            t.componente.Lexema = ";";
            return t;
        }

        internal static Terminal ElementoAsignacion()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.Asignacion;
            t.componente.Lexema = ":=";
            return t;
        }

        internal static Terminal ElementoParentesisClausura()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.ParentesisClausura;
            t.componente.Lexema = ")";
            return t;
        }

        internal static Terminal ElementoEOF()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.EOF;

            return t;
        }

        internal static Terminal ElementoError()
        {
            Terminal t = new Terminal();
            t.componente = new ComponenteLexico();
            t.componente.Token = ComponenteLexico.TokenType.Error;

            return t;
        }
        #endregion

        private ComponenteLexico componente;
        public ComponenteLexico Componente
        {
            get { return componente; }
            set { componente = value; }
        }

        public Terminal()
        {
        }


        public Terminal(string lexema)
        {
            this.componente = new ComponenteLexico();
            this.componente.Token = (ComponenteLexico.TokenType)EnumUtils.enumValueOf(lexema, typeof(ComponenteLexico.TokenType));
            this.componente.Lexema = lexema;
        }


        // override object.Equals
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
            Terminal t = (Terminal)obj;

            // use this pattern to compare reference members
            if (Componente.Equals(t.Componente))
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
            return EnumUtils.stringValueOf(componente.Token);
        }

        internal bool NoEsLambda()
        {
            return (this.componente.Token != ComponenteLexico.TokenType.Ninguno);
        }                      

        public override int ObtenerValor()
        {
            int retorno = 0;
            bool resultado;
            switch (this.Componente.Token)
            {
                case ComponenteLexico.TokenType.Numero:
                    resultado = int.TryParse(this.Componente.Lexema, out retorno);
                    break;

                

                case ComponenteLexico.TokenType.Identificador:
                    //Buscarlo en la tabla de simbolos
                    break;

                default:

                    break;
                    //Error?
            }

            return retorno;
        }
    }
}
