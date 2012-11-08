using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico.Auxiliares
{
    public sealed class GlobalXMLTags
    {
        private static volatile GlobalXMLTags instance;
        private static object syncRoot = new Object();

        private uint cantMientras;
        public uint CantMientras
        {
            get { return ++cantMientras; }
        }

        private uint cantSi;
        public uint CantSi
        {
            get { return ++cantSi; }
        }

        private uint cantSecuencias;
        public uint CantSecuencias
        {
            get { return ++cantSecuencias; }
        }

        private GlobalXMLTags() 
        {
            cantMientras = 0;
            cantSi = 0;
            cantSecuencias = 0;
        }

        public static GlobalXMLTags Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GlobalXMLTags();
                    }
                }

                return instance;
            }
        }


        
    }
}
