using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Resultado;
using DataAccess.Entidades;

namespace Ragnarok
{
    public class ResultadoEjecucion
    {
        public ResultadoCompilacion ResCompilacion { get; set; }
        public ArchResultado ResEjecucion { get; set; }


    }
}
