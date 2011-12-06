using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Lexicografico;
using Compilador.Sintactico.Gramatica;
using Compilador.Auxiliares;

namespace Compilador.Sintactico
{
    class CadenaEntrada
    {
        private List<Terminal> cadena;

        public int Count
        {
            get
            {
                return cadena.Count;
            }
        }

        public CadenaEntrada()
        {
            this.cadena = new List<Terminal>();
        }

        public Terminal ObtenerPrimerTerminal()
        {
            return this.cadena.First();
        }

        public void EliminarPrimerTerminal()
        {
            this.cadena.RemoveAt(0);
        }

        public void InsertarTerminal(Terminal t)
        {
            this.cadena.Add(t);
        }

        public override string ToString()
        {
            StringBuilder strBldr = new StringBuilder(string.Empty);

            foreach (Terminal elem in this.cadena)
            {
                strBldr.Append(elem.ToString());
                strBldr.Append(" ");
            }

            return strBldr.ToString();
        }


        internal bool esVacia()
        {
            return this.cadena.Count == 0;
        }

        internal bool esFinDeCadena()
        {
            if (!esVacia())
            {
                return this.ObtenerPrimerTerminal().Componente.Token == ComponenteLexico.TokenType.EOF;
            }
            else
            {
                return false;
            }
        }
    }
}
