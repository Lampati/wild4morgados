using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal static class TipoFactory
    {

        internal static Tipos.TipoBase CrearTipo(List<Terminal> linea, ContextoLinea tipo)
        {
            Tipos.TipoBase retorno = null;

            switch (tipo)
            {
                case ContextoLinea.Asignacion:
                    break;
                case ContextoLinea.Leer:
                    break;
                case ContextoLinea.LlamadaProc:
                    break;
                case ContextoLinea.Mientras:
                    retorno = new Tipos.Mientras(linea);
                    break;
                case ContextoLinea.Si:
                    break;
                case ContextoLinea.Sino:
                    break;
                case ContextoLinea.DeclaracionFuncion:
                    break;
                case ContextoLinea.DeclaracionProc:
                    break;
                case ContextoLinea.DeclaracionConstante:
                    break;
                case ContextoLinea.DeclaracionVariable:
                    break;
                case ContextoLinea.FinFuncion:
                    break;
                case ContextoLinea.FinProc:
                    break;
                case ContextoLinea.FinMientras:
                    break;
                case ContextoLinea.FinSi:
                    break;
                case ContextoLinea.Mostrar:
                    break;
                case ContextoLinea.Ninguno:
                    break;
                default:
                    break;
            }



            return retorno;
        }
    }
}
