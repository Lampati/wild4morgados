using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class Mientras : TipoBase
    {
        public Mientras(List<Terminal> lista, int fila, int col) 
            : base(fila, col)
        {
            listaLineaEntera = lista;
            AgregarValidacionPorDefault();
        }

        private void AgregarValidacionPorDefault()
        {
            string mensajeError = "El mientras contiene un error sintactico.";
            short importancia = 1;


            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarFalso, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }
    }
}
