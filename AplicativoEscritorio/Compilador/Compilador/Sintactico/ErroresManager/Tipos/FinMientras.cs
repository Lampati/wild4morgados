using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class FinMientras : TipoBase
    {
        public FinMientras(List<Terminal> lista, int fila, int col) 
            : base(fila,col)
        {
            listaLineaEntera = lista;

            AgregarValidacionFin();
        }


        private void AgregarValidacionFin()
        {
            string mensajeError = "El fin de un bloque mientras debe especificarse de la siguiente manera: finmientras;";
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ValidarFinMientras, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }
    }
}
