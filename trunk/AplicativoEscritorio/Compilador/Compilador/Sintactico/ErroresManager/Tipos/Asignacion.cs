using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class Asignacion : TipoBase
    {
        public Asignacion(List<Terminal> lista)
        {
            listaLineaEntera = lista;



        }
    }
}
