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

        public AnalizadorErroresSintacticos(List<Terminal> lineaHastaAhora, ContextoLinea contextoLinea, List<Terminal> cadenaEntradaFaltante)
        {
            List<Terminal> lineaEntera = new List<Terminal>(lineaHastaAhora);

            List<Terminal> terminalesQueTerminanLinea = new List<Terminal>();
            switch (contextoLinea)
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

                tipoError = TipoFactory.CrearTipo(lineaEntera, contextoLinea);

            }
            else
            {
                tipoError = null;
                //no agarre la linea entera. Error x default??
            }
        
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
