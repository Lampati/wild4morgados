using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace TesterCompilador
{
    internal class InfoArchivo
    {
        public MensajeError MensajeErr { get; set; }
        public int Linea { get; set; }
        public int CodigoGlobal { get; set; }

        public bool Valido { get; set; }
    }
}
