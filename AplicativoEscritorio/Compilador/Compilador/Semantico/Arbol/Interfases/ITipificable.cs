using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Interfases
{
    interface ITipificable
    {
        TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato ObtenerTipo(Terminal t);
    }
}
