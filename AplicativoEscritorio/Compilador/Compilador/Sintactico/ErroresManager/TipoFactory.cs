﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal static class TipoFactory
    {

        internal static Tipos.TipoBase CrearTipo(List<Terminal> linea, ContextoGlobal contextoGlobal, ContextoLinea tipo, List<Terminal> cadenaEntradaFaltante)
        {
            int fila, col;

            if (cadenaEntradaFaltante.Count > 0)
            {
                fila = cadenaEntradaFaltante[0].Componente.Fila;
                col = cadenaEntradaFaltante[0].Componente.Columna;
            }
            else
            {
                if (linea.Count > 0)
                {
                    fila = linea[linea.Count - 1].Componente.Fila;
                    col = linea[linea.Count - 1].Componente.Columna;
                }
                else
                {
                    fila = -1;
                    col = -1;
                }
            }
            
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
                return Crear(lineaEntera, contextoGlobal, tipo, fila, col);
            }
            else
            {
                //no agarre la linea entera. Error x default??
                return null;
            }
        }

        private static Tipos.TipoBase Crear(List<Terminal> linea, ContextoGlobal contextoGlobal, ContextoLinea tipo, int fila, int col)
        {
            Tipos.TipoBase retorno = null;

            switch (tipo)
            {
                case ContextoLinea.Asignacion:
                    retorno = new Tipos.Asignacion(linea,fila,col);
                    break;
                case ContextoLinea.Leer:
                    retorno = new Tipos.Leer(linea, fila, col);
                    break;
                case ContextoLinea.LlamadaProc:
                    retorno = new Tipos.LlamadoProc(linea, fila, col);
                    break;
                case ContextoLinea.Mientras:
                    retorno = new Tipos.Mientras(linea, fila,col );
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
                    retorno = new Tipos.FinProc(linea, fila, col);  //terminada 16/3/2012
                    break;
                case ContextoLinea.FinMientras:
                    retorno = new Tipos.FinMientras(linea, fila, col);  //terminada 16/3/2012
                    break;
                case ContextoLinea.FinSi:
                    retorno = new Tipos.FinSi(linea, fila, col); //terminada 16/3/2012
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
