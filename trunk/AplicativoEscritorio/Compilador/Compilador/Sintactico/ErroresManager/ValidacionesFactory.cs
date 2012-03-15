using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal static class ValidacionesFactory
    {

        internal static bool AsignacionRepetido(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion).Count;

            return cantidad < 2;
        }

        internal static bool AsignacionFaltante(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion).Count;

            return cantidad > 0;
        }

        internal static bool AsignacionTerminaCorrectamente(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion).Count;

            return cantidad > 0;
        }

        internal static bool AsignacionParteIzqCorrecta(List<Terminal> lista)
        {
            bool res = lista.Exists(x => 
                                x.Componente.Token != Lexicografico.ComponenteLexico.TokenType.Identificador
                                && x.Componente.Token != Lexicografico.ComponenteLexico.TokenType.CorcheteApertura                                
                            );

            return res;
        }

        internal static bool ParentesisBalanceados(List<Terminal> lista)
        {
            int cantidadAbiertos = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisApertura).Count;
            int cantidadCerrados = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisClausura).Count;

            return cantidadAbiertos == cantidadCerrados;
        }

        internal static bool CorchetesBalanceados(List<Terminal> lista)
        {
            int cantidadAbiertos = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.CorcheteApertura).Count;
            int cantidadCerrados = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.CorcheteClausura).Count;

            return cantidadAbiertos == cantidadCerrados;
        }
    }
}
