﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class LlamadoProc : TipoBase
    {

        public LlamadoProc(List<Terminal> lista, int fila, int col) 
            : base(fila,col)
        {
            listaLineaEntera = lista;
            AgregarValidacionPorDefault();
        }

        private void AgregarValidacionPorDefault()
        {
            string mensajeError = "La llamada al procedimiento contiene un error sintactico.";
            short importancia = 1;


            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarFalso, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }
    }
}