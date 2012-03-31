using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol;
using CompiladorGargar.Resultado.Auxiliares;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar.Resultado
{
    public class ResultadoCompilacion
    {
        public bool CompilacionGarGarCorrecta { get; set; }
        public bool GeneracionEjectuableCorrecto { get; set; }
        public string Error { get; set; }

        public List<PasoAnalizadorSintactico> ListaErrores { get; set; }
        public List<PasoCompilacion> ListaDebugSintactico { get; set; }
        internal ArbolSemantico ArbolSemanticoResultado { get; set; }
        public TablaSimbolos TablaSimbolos { get; set; }
        public List<int> ListaLineasValidas { get; set; }

        public string CodigoGarGar { get; set; }
        public string CodigoPascal { get; set; }
        public string ArchTemporalCodigoPascal { get; set; }
        public string ArchTemporalCodigoPascalConRuta { get; set; }

        public string ArchEjecutable { get; set; }
        public string ArchEjecutableConRuta { get; set; }
        public ResultadoCompilacionPascal ResultadoCompPascal { get; set; }

        public float TiempoCompilacionTotal { get; set; }
        public float TiempoGeneracionAnalizadorLexico { get; set; }
        public float TiempoGeneracionAnalizadorSintactico { get; set; }
        public float TiempoGeneracionCodigo { get; set; }
        public float TiempoGeneracionTemporalCodigo { get; set; }
        public float TiempoGeneracionEjecutable { get; set; }

        public ResultadoCompilacion()
        {
            ListaDebugSintactico = new List<PasoCompilacion>();
            ListaErrores = new List<PasoAnalizadorSintactico>();
        }
        
    }

    

   

  
}
