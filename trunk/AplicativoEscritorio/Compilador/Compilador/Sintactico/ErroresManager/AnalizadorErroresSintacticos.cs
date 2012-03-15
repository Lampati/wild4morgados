using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal class AnalizadorErroresSintacticos
    {

        private Tipos.TipoBase tipoError;

        public AnalizadorErroresSintacticos(List<Terminal> lineaHastaAhora, ContextoGlobal contextoGlobal, ContextoLinea contextoLinea, List<Terminal> cadenaEntradaFaltante)
        {

            tipoError = TipoFactory.CrearTipo(lineaHastaAhora, contextoGlobal, contextoLinea, cadenaEntradaFaltante);
           
        
        }

        internal void Validar()
        {
            if (tipoError != null)
            {
                tipoError.Validar();
            }
        }
    }
}
