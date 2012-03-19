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

        public AnalizadorErroresSintacticos(List<Terminal> lineaHastaAhora, ContextoGlobal contextoGlobal, ContextoLinea contextoLinea, List<Terminal> cadenaEntradaFaltante)
        {

            tipoError = TipoFactory.CrearTipo(lineaHastaAhora, contextoGlobal, contextoLinea, cadenaEntradaFaltante);

            if (tipoError == null)
            {
                //significa que
                ConstruirYArrojarExcepcion(cadenaEntradaFaltante[0], contextoGlobal);
            }

        
        }

        private void ConstruirYArrojarExcepcion(Terminal terminal, ContextoGlobal contextoGlobal)
        {

            string mensajeError;

            ContextoLinea cont = EstadoSintactico.ContextoPerteneceTerminal(terminal);

            switch (cont)
            {
                case ContextoLinea.Asignacion:
                    mensajeError = string.Format("La variable {0} no tiene lugar. Una asignacion solo puede ser hecha dentro del cuerpo de un procedimiento o funcion",terminal.Componente.Lexema);
                    break;
                case ContextoLinea.Leer:
                    mensajeError = "La operacion leer no tiene lugar. Solo puede ser hecha dentro del cuerpo de un procedimiento o funcion";
                    break;
                case ContextoLinea.LlamadaProc:
                    mensajeError = "La llamada a un procedimiento no tiene lugar. Solo puede ser hecha dentro del cuerpo de un procedimiento o funcion";
                    break;
                case ContextoLinea.Mientras:
                    mensajeError = "El bloque mientras no tiene lugar. Solo puede ser usado dentro del cuerpo de un procedimiento o funcion";
                    break;
                case ContextoLinea.Si:
                    mensajeError = "El bloque si no tiene lugar. Solo puede ser usado dentro del cuerpo de un procedimiento o funcion";
                    break;
                case ContextoLinea.Sino:
                    mensajeError = "El sino no tiene lugar. No se encontro el bloque si al que pertenece";
                    break;
                case ContextoLinea.DeclaracionFuncion:
                    mensajeError = "No es posible declarar una funcion en este contexto. Las funciones deben ser declaradas en un contexto global, no dentro de procedimientos o funciones";
                    break;
                case ContextoLinea.DeclaracionProc:
                    mensajeError = "No es posible declarar un procedimiento en este contexto. Los procedimientos deben ser declarados en un contexto global, no dentro de procedimientos o funciones";
                    break;
                case ContextoLinea.DeclaracionConstante:
                    switch (contextoGlobal)
	                {
                        case ContextoGlobal.Global:
                            mensajeError = @"No es posible declarar una constante en este contexto. Las constantes solo pueden ser declaradas globalmente (debajo de 'constantes')";
                            break;                       
                        case ContextoGlobal.GlobalDeclaracionesVariables:
                            mensajeError = "No es posible declarar una constante en este contexto. Las constantes solo pueden ser declaradas globalmente (debajo de 'constantes') y no dentro del espacio de declaraciones globales de variables";
                            break;
                        case ContextoGlobal.DeclaracionLocal:
                            mensajeError = "No es posible declarar una constante en este contexto. Las constantes solo pueden ser declaradas globalmente (debajo de 'constantes') y no dentro del espacio de declaraciones local de un procedimiento o función";
                            break;
                        case ContextoGlobal.Cuerpo:
                            mensajeError = "No es posible declarar una constante en este contexto. Las constantes solo pueden ser declaradas globalmente (debajo de 'constantes') y no dentro del cuerpo de un procediminiento o función";
                            break;
                        default:
                            mensajeError = @"No es posible declarar una constante en este contexto. Las constantes solo pueden ser declaradas globalmente (debajo de 'constantes')";
                            break;
	                }                    
                    break;
                case ContextoLinea.DeclaracionVariable:
                    switch (contextoGlobal)
                    {
                        case ContextoGlobal.Global:
                            mensajeError = @"No es posible declarar una variable en este contexto. Las variables solo pueden ser declaradas globalmente (debajo de 'variables') o en el ambito local de un procedimiento o función";
                            break;
                        case ContextoGlobal.GlobalDeclaracionesConstantes:
                            mensajeError = "No es posible declarar una variable en este contexto. Las variables solo pueden ser declaradas globalmente (debajo de 'variables') o en el ambito local de un procedimiento o función y no dentro del espacio de declaraciones globales de constantes";
                            break;                        
                        case ContextoGlobal.Cuerpo:
                            mensajeError = "No es posible declarar una variable en este contexto. Las variables solo pueden ser declaradas globalmente (debajo de 'variables') o en el ambito local de un procedimiento o función y no dentro del cuerpo de un procediminiento o función";
                            break;
                        default:
                            mensajeError = @"No es posible declarar una variable en este contexto. Las variables solo pueden ser declaradas globalmente (debajo de 'variables') o en el ambito local de un procedimiento o función";
                            break;
                    }  
                    break;
                case ContextoLinea.FinFuncion:
                    mensajeError = "El finfunc no tiene lugar. No se encontro la declaracion de funcion al que pertenece";
                    break;
                case ContextoLinea.FinProc:
                    mensajeError = "El finproc no tiene lugar. No se encontro la declaracion de procedimiento al que pertenece";
                    break;
                case ContextoLinea.FinMientras:
                    mensajeError = "El finmientras no tiene lugar. No se encontro el bloque mientras al que pertenece";
                    break;
                case ContextoLinea.FinSi:
                    mensajeError = "El finsi no tiene lugar. No se encontro el bloque si al que pertenece";
                    break;
                case ContextoLinea.Mostrar:
                    mensajeError = "La operación mostrar no tiene lugar. Solo puede ser hecha dentro del cuerpo de un procedimiento o funcion";
                    break;
                case ContextoLinea.Ninguno:
                    mensajeError = string.Format("Error sintactico en {0}. {0} no tiene lugar o la linea comienza incorrectamente.",terminal.Componente.Lexema);
                    break;
                default:
                    mensajeError = string.Format("Error sintactico en {0}. {0} no tiene lugar o la linea comienza incorrectamente.", terminal.Componente.Lexema);
                    break;
            }

            throw new AnalizadorErroresException(mensajeError) 
                    { Fila = terminal.Componente.Fila, Columna = terminal.Componente.Columna, Parar = true };
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
