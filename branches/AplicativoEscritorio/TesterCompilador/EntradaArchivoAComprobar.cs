using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesterCompilador
{
    public class EntradaArchivoAComprobar
    {
        public bool CompilacionCorrecta { get; set; }

        public string NombreArch  { get; set; }
        public string Mensaje { get; set; }

        public virtual bool EsValido
        {
            get
            {
                return CompilacionCorrecta;

            }
        }

       

    }

    public class ArchivoSintacticoComprobar : EntradaArchivoAComprobar
    {
        public int CodigoGlobalEsperado { get; set; }
        public int CodigoGlobalReal { get; set; }

        public int LineaEsperado { get; set; }
        public int LineaReal { get; set; }

        public bool EsValidaLinea
        {
            get
            {
                return LineaReal == LineaEsperado;
            }

        }

        public bool EsValidoCodigoGlobal
        {
            get
            {
                return CodigoGlobalReal == CodigoGlobalEsperado;
            }

        }

        public override bool EsValido
        {
            get
            {
                return EsValidaLinea && EsValidoCodigoGlobal;

            }
        }

    }
    
}
