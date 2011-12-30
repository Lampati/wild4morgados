using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol;

namespace CompiladorGargar
{
    public class ResultadoCompilacion
    {
        public bool CompilacionGarGarCorrecta { get; set; }
        public bool GeneracionEjectuableCorrecto { get; set; }
        public string Error { get; set; }
        
        public List<PasoAnalizadorSintactico> ListaErrores { get; set; }
        public List<PasoCompilacion> ListaDebugSintactico { get; set; }
        public ArbolSemantico ArbolSemanticoResultado { get; set; }

        public string CodigoPascal { get; set; }
        public string ArchTemporalCodigoPascal { get; set; }
        public string ArchTemporalCodigoPascalConRuta { get; set; }

        public string ArchEjecutable { get; set; }
        public string ArchEjecutableConRuta { get; set; }

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

    public class PasoDebugTiempos
    {
        public float TiempoAnalizadorSint { get; set; }
        public float TiempoAnalizadorTot { get; set; }
        public int NumPaso { get; set; }
    }

    public class PasoCompilacion
    {


        public PasoCompilacion(string pila, string cadena, CompiladorGargar.Global.TipoError tipoError)
        {
            this.ContenidoPila = pila;
            this.EstadoCadenaEntrada = cadena;

            this.TipoError = tipoError;
           
            
        }

        public string ContenidoPila { get; set; }
        public string EstadoCadenaEntrada { get; set; }        
        public CompiladorGargar.Global.TipoError TipoError { get; set; }
    }

    public class PasoAnalizadorSintactico
    {


        public PasoAnalizadorSintactico()
        {           

        }

        public PasoAnalizadorSintactico(string desc, Global.TipoError tipo, int f, int c, bool parar)
        {
            this.Descripcion = desc;
            this.TipoError = tipo;
            this.Fila = f;
            this.Columna = c;
            this.PararCompilacion = parar;
        }

        public PasoAnalizadorSintactico(string desc, Global.TipoError tipo, int f, int c)
        {
            this.Descripcion = desc;
            this.TipoError = tipo;
            this.Fila = f;
            this.Columna = c;
            this.PararCompilacion = false;
        }        

        public string Descripcion { get; set; }
        public bool PararCompilacion { get; set; }

        public int Fila { get; set; }
        public int Columna { get; set; }

        public CompiladorGargar.Global.TipoError TipoError { get; set; }
    }
}
