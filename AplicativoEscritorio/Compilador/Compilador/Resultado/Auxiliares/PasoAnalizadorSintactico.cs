using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Resultado.Auxiliares
{
    public class PasoAnalizadorSintactico
    {
        public PasoAnalizadorSintactico()
        {

        }

        public PasoAnalizadorSintactico(string desc, GlobalesCompilador.TipoError tipo, int f, int c, bool parar)
        {
            this.Descripcion = desc;
            this.TipoError = tipo;
            this.Fila = f;
            this.Columna = c;
            this.PararCompilacion = parar;
        }

        public PasoAnalizadorSintactico(string desc, GlobalesCompilador.TipoError tipo, int f, int c)
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

        public CompiladorGargar.GlobalesCompilador.TipoError TipoError { get; set; }
    }
}
