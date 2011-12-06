using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compilador.Lexicografico
{

    //Clase que contiene metodos estaticos auxiliares para determinar que tipo de caracter es.
    class CharHelper
    {
        public static bool esCaracterSalteable(char x)
        {
            if ((char.IsWhiteSpace(x))|| esRetornoDeCarro(x) || esNuevaLinea(x))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool esRetornoDeCarro(char x)
        {
            if (x == '\r') 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool esNuevaLinea(char x)
        {
            if (x == '\n')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
