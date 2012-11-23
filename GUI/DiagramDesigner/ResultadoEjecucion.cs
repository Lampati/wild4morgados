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

        // flanzani 22/11/2012
        // IDC_APP_8
        // Agregar el tiempo de ejecucion 
        // Agrego el tiempo
        public double Segundos { get; set; }

    }
}
