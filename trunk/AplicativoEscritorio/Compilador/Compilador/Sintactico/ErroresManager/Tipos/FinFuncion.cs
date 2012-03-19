using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class FinFuncion : TipoBase
    {
        public FinFuncion(List<Terminal> lista, int fila, int col) 
            : base(fila,col)
        {
            listaLineaEntera = lista;

            AgregarValidacionFin();

        }


        private void AgregarValidacionFin()
        {
            string mensajeError = "El fin de la declaración de un procedimiento debe especificarse de la siguiente manera: finproc;";
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ValidarFinProc, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }
    }
}
