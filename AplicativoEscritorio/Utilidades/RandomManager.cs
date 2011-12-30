using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilidades
{
    public static class RandomManager
    {

        public static string RandomStringConPrefijo(string prefijo, int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder(prefijo);
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static string RandomString(int size, bool lowerCase)
        {
            return RandomStringConPrefijo(string.Empty, size, lowerCase);
        }
    }
}
