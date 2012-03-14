using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal static class TipoFactory
    {

        internal static Tipos.TipoBase CrearTipo(List<Terminal> linea, ContextoLinea tipo, List<Terminal> cadenaEntradaFaltante)
        {
            
            List<Terminal> lineaEntera = new List<Terminal>(linea);

            List<Terminal> terminalesQueTerminanLinea = new List<Terminal>();
            switch (tipo)
            {
                case ContextoLinea.Asignacion:
                case ContextoLinea.Leer:
                case ContextoLinea.LlamadaProc:
                case ContextoLinea.DeclaracionConstante:
                case ContextoLinea.DeclaracionVariable:
                case ContextoLinea.Mostrar:
                case ContextoLinea.FinProc:
                case ContextoLinea.FinFuncion:
                case ContextoLinea.FinSi:
                case ContextoLinea.FinMientras:
                    terminalesQueTerminanLinea.Add(Terminal.ElementoFinSentencia());
                    break;
                case ContextoLinea.Mientras:
                    terminalesQueTerminanLinea.Add(Terminal.ElementoHacer());
                    break;
                case ContextoLinea.Si:
                    terminalesQueTerminanLinea.Add(Terminal.ElementoEntonces());
                    break;
                case ContextoLinea.DeclaracionFuncion:
                    terminalesQueTerminanLinea.Add(Terminal.ElementoTipoBooleano());
                    terminalesQueTerminanLinea.Add(Terminal.ElementoTipoNumero());
                    terminalesQueTerminanLinea.Add(Terminal.ElementoTipoTexto());
                    break;
                case ContextoLinea.DeclaracionProc:
                    terminalesQueTerminanLinea.Add(Terminal.ElementoParentesisClausura());
                    break;

            }

            int i = 0;
            while (i < cadenaEntradaFaltante.Count && !terminalesQueTerminanLinea.Contains(cadenaEntradaFaltante[i]))
            {
                lineaEntera.Add(cadenaEntradaFaltante[i]);
                i++;
            }

            if (i < cadenaEntradaFaltante.Count)
            {
                //salida normal, agarre la linea entera

                lineaEntera.Add(cadenaEntradaFaltante[i]);
                return Crear(lineaEntera, tipo);
            }
            else
            {
                //no agarre la linea entera. Error x default??
                return null;
            }
        }

        private static Tipos.TipoBase Crear(List<Terminal> linea, ContextoLinea tipo)
        {
            Tipos.TipoBase retorno = null;

            switch (tipo)
            {
                case ContextoLinea.Asignacion:
                    retorno = new Tipos.Asignacion(linea);
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
