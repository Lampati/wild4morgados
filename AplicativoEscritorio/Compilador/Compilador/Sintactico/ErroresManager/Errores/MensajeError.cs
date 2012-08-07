﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Sintactico.ErroresManager.Errores
{
    public abstract class MensajeError
    {
        public string MensajeModoTexto { get; set; }
        public string MensajeModoGrafico { get; set; }
        public int CodigoGlobal { get; set; }

        public MensajeError()
        {

        }
    }

    public class ErrorVacio : MensajeError
    {
        public ErrorVacio()
            : base()
        {
            CodigoGlobal = 0;
            MensajeModoTexto = "";
            MensajeModoGrafico = "";
        }
    }

    public class ErrorDeclaracionConstanteGenerico : MensajeError
    {
        public ErrorDeclaracionConstanteGenerico()
            : base()
        {
            CodigoGlobal = 1;
            MensajeModoTexto = "La declaración de la constante contiene un error sintactico.";
            MensajeModoGrafico = "La declaración de la constante contiene un error sintactico.";
        }
    }

    public class ErrorTipoDatoDefRepetido : MensajeError
    {
        public ErrorTipoDatoDefRepetido()
            : base()
        {
            CodigoGlobal = 2;
            MensajeModoTexto = "El : esta especificado mas de una vez en la declaración";
            MensajeModoGrafico = "La declaración contiene un error sintactico.";
        }
    }

    public class ErrorTipoDatoDefFaltante : MensajeError
    {
        public ErrorTipoDatoDefFaltante()
            : base()
        {
            CodigoGlobal = 3;
            MensajeModoTexto = ": faltante en la declaración";
            MensajeModoGrafico = "La declaración contiene un error sintactico.";
        }
    }

    public class ErrorAsignarValorRepetido : MensajeError
    {
        public ErrorAsignarValorRepetido()
            : base()
        {
            CodigoGlobal = 4;
            MensajeModoTexto = "El = esta especificado mas de una vez en la declaración";
            MensajeModoGrafico = "La declaración contiene un error sintactico.";
        }
    }

    public class ErrorAsignarValorFaltante : MensajeError
    {
        public ErrorAsignarValorFaltante()
            : base()
        {
            CodigoGlobal = 5;
            MensajeModoTexto = "= faltante en la declaración";
            MensajeModoGrafico = "La declaración contiene un error sintactico.";
        }
    }

    public class ErrorConstanteTipoDatoSinArreglo : MensajeError
    {
        public ErrorConstanteTipoDatoSinArreglo()
            : base()
        {
            CodigoGlobal = 6;
            MensajeModoTexto = "Las constantes no pueden ser arreglos";
            MensajeModoGrafico = "Las constantes no pueden ser arreglos";
        }
    }

    public class ErrorTipoDatoRepetido : MensajeError
    {
        public ErrorTipoDatoRepetido()
            : base()
        {
            CodigoGlobal = 7;
            MensajeModoTexto = "El tipo de dato esta especificado mas de una vez en la declaración";
            MensajeModoGrafico = "El tipo de dato esta especificado mas de una vez en la declaración";
        }
    }

    public class ErrorTipoDatoFaltante : MensajeError
    {
        public ErrorTipoDatoFaltante()
            : base()
        {
            CodigoGlobal = 8;
            MensajeModoTexto = "Tipo de dato faltante en la declaración";
            MensajeModoGrafico = "Tipo de dato faltante en la declaración";
        }
    }

    public class ErrorConstanteValorRepetido : MensajeError
    {
        public ErrorConstanteValorRepetido()
            : base()
        {
            CodigoGlobal = 9;
            MensajeModoTexto = "El valor de constante esta especificado mas de una vez en la declaración";
            MensajeModoGrafico = "El valor de constante esta especificado mas de una vez en la declaración";
        }
    }

    public class ErrorConstanteValorFaltante : MensajeError
    {
        public ErrorConstanteValorFaltante()
            : base()
        {
            CodigoGlobal = 10;
            MensajeModoTexto = "Valor de constante faltante en la declaración";
            MensajeModoGrafico = "Valor de constante faltante en la declaración";
        }
    }

    public class ErrorConstanteElementoQueSobraErroneo : MensajeError
    {
        public ErrorConstanteElementoQueSobraErroneo(string elementoErroneo)
            : base()
        {
            CodigoGlobal = 11;
            MensajeModoTexto = string.Format("{0} no tiene lugar en una declaración de constante", elementoErroneo);
            MensajeModoGrafico = string.Format("{0} no tiene lugar en una declaración de constante", elementoErroneo);
        }
    }

    public class ErrorAsignacionRepetido : MensajeError
    {
        public ErrorAsignacionRepetido()
            : base()
        {
            CodigoGlobal = 12;
            MensajeModoTexto = "El := esta especificado mas de una vez en la asignacion";
            MensajeModoGrafico = "El := esta especificado mas de una vez en la asignacion";
        }
    }

    public class ErrorAsignacionFaltante : MensajeError
    {
        public ErrorAsignacionFaltante()
            : base()
        {
            CodigoGlobal = 13;
            MensajeModoTexto = ":= faltante en la asignacion";
            MensajeModoGrafico = ":= faltante en la asignacion";
        }
    }

    public class ErrorAsignacionTerminaCorrectamente : MensajeError
    {
        public ErrorAsignacionTerminaCorrectamente()
            : base()
        {
            CodigoGlobal = 14;
            MensajeModoTexto = "La asignacion termina incorrectamente";
            MensajeModoGrafico = "La asignacion termina incorrectamente";
        }
    }

    public class ErrorAsignacionParteIzqCorrecta : MensajeError
    {
        public ErrorAsignacionParteIzqCorrecta()
            : base()
        {
            CodigoGlobal = 15;
            MensajeModoTexto = "Error sintactico en la parte izq de la asignación";
            MensajeModoGrafico = "Error sintactico en la parte izq de la asignación";
        }
    }

    public class ErrorAsignacionParentesisBalanceadosParteIzq : MensajeError
    {
        public ErrorAsignacionParentesisBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 16;
            MensajeModoTexto = "Los parentesis no estan balanceados en la parte izquierda de la asignacion";
            MensajeModoGrafico = "Los parentesis no estan balanceados en la parte izquierda de la asignacion";

        }
    }

    public class ErrorAsignacionParentesisBalanceadosParteDer : MensajeError
    {
        public ErrorAsignacionParentesisBalanceadosParteDer()
            : base()
        {
            CodigoGlobal = 17;
            MensajeModoTexto = "Los parentesis no estan balanceados en la parte derecha de la asignacion";
            MensajeModoGrafico = "Los parentesis no estan balanceados en la parte derecha de la asignacion";
        }
    }

    public class ErrorAsignacionCorchetesBalanceadosParteIzq : MensajeError
    {
        public ErrorAsignacionCorchetesBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 18;
            MensajeModoTexto = "Los corchetes no estan balanceados en la parte izquierda de la asignacion";
            MensajeModoGrafico = "Los corchetes no estan balanceados en la parte izquierda de la asignacion";
        }
    }

    public class ErrorAsignacionCorchetesBalanceadosParteDer : MensajeError
    {
        public ErrorAsignacionCorchetesBalanceadosParteDer()
            : base()
        {
            CodigoGlobal = 19;
            MensajeModoTexto = "Los corchetes no estan balanceados en la parte derecha de la asignacion";
            MensajeModoGrafico = "Los corchetes no estan balanceados en la parte derecha de la asignacion";
        }
    }

    public class ErrorAsignacionElementosConValorNoContiguosParteIzq : MensajeError
    {
        public ErrorAsignacionElementosConValorNoContiguosParteIzq()
            : base()
        {
            CodigoGlobal = 20;
            MensajeModoTexto = "La asignacion contiene una expresión mal formada en su parte izquierda";
            MensajeModoGrafico = "La asignacion contiene una expresión mal formada en su parte izquierda";
        }
    }

    public class ErrorAsignacionElementosConValorNoContiguosParteDer : MensajeError
    {
        public ErrorAsignacionElementosConValorNoContiguosParteDer()
            : base()
        {
            CodigoGlobal = 21;
            MensajeModoTexto = "La asignacion contiene una expresión mal formada en su parte derecha";
            MensajeModoGrafico = "La asignacion contiene una expresión mal formada en su parte derecha";
        }
    }

    public class ErrorAsignacionValidacionPorDefault : MensajeError
    {
        public ErrorAsignacionValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 22;
            MensajeModoTexto = "La asignacion contiene un error sintactico";
            MensajeModoGrafico = "La asignacion contiene un error sintactico";
        }
    }

    public class ErrorDeclaracionFuncionValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionFuncionValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 23;
            MensajeModoTexto = "La declaración de la función contiene un error sintactico";
            MensajeModoGrafico = "La declaración de la función contiene un error sintactico";
        }
    }

    public class ErrorDeclaracionProcedimientoValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionProcedimientoValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 24;
            MensajeModoTexto = "La declaración del procedimiento contiene un error sintactico";
            MensajeModoGrafico = "La declaración del procedimiento contiene un error sintactico";
        }
    }

    public class ErrorDeclaracionVariableValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionVariableValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 25;
            MensajeModoTexto = "La declaración de la variable contiene un error sintactico";
            MensajeModoGrafico = "La declaración de la variable contiene un error sintactico";
        }
    }

    public class ErrorDeclaracionVariableParteIzquierdaValida : MensajeError
    {
        public ErrorDeclaracionVariableParteIzquierdaValida()
            : base()
        {
            CodigoGlobal = 26;
            MensajeModoTexto = "La declaración de variables es incorrecta. Debe ser una lista de identificadores separados por comas o un identificador solo";
            MensajeModoGrafico = "La declaración de variables es incorrecta. Debe ser una lista de identificadores separados por comas o un identificador solo";
        }
    }

    public class ErrorDeclaracionVariableCantArregloNoRepetido : MensajeError
    {
        public ErrorDeclaracionVariableCantArregloNoRepetido()
            : base()
        {
            CodigoGlobal = 27;
            MensajeModoTexto = "El arreglo esta especificado mas de una vez en la declaración";
            MensajeModoGrafico = "El arreglo esta especificado mas de una vez en la declaración";
        }
    }

    public class ErrorDeclaracionVariableCorchetesBalanceadosParteIzq : MensajeError
    {
        public ErrorDeclaracionVariableCorchetesBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 28;
            MensajeModoTexto = "Los corchetes no estan balanceados en la declaracion del arreglo";
            MensajeModoGrafico = "Los corchetes no estan balanceados en la declaracion del arreglo";
        }
    }

    public class ErrorDeclaracionVariableElementoQueSobraErroneo : MensajeError
    {
        public ErrorDeclaracionVariableElementoQueSobraErroneo(string elementoErroneo)
            : base()
        {
            CodigoGlobal = 29;
            MensajeModoTexto = string.Format("Error sintactico: {0} es incorrecto en la declaración de un arreglo. La forma correcta es arreglo [MAX] de TIPO", elementoErroneo);
            MensajeModoGrafico = string.Format("Error sintactico: {0} es incorrecto en la declaración de un arreglo.", elementoErroneo);
        }
    }

    public class ErrorMientrasValidacionPorDefault : MensajeError
    {
        public ErrorMientrasValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 30;
            MensajeModoTexto = "El mientras contiene un error sintactico.";
            MensajeModoGrafico = "El mientras contiene un error sintactico.";
        }
    }

    public class ErrorFinSiValidacionFin : MensajeError
    {
        public ErrorFinSiValidacionFin()
            : base()
        {
            CodigoGlobal = 31;
            MensajeModoTexto = "El fin de un bloque si debe especificarse de la siguiente manera: finsi;";
            MensajeModoGrafico = "El fin de un bloque si debe especificarse de la siguiente manera: finsi;";
        }
    }

    public class ErrorLlamadoProcValidacionPorDefault : MensajeError
    {
        public ErrorLlamadoProcValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 32;
            MensajeModoTexto = "La llamada al procedimiento contiene un error sintactico.";
            MensajeModoGrafico = "La llamada al procedimiento contiene un error sintactico.";
        }
    }

    public class ErrorLeerValidacionPorDefault : MensajeError
    {
        public ErrorLeerValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 33;
            MensajeModoTexto = "La llamada a leer contiene un error sintactico.";
            MensajeModoGrafico = "La llamada a leer contiene un error sintactico.";
        }
    }

    public class ErrorFinMientrasValidacionFin : MensajeError
    {
        public ErrorFinMientrasValidacionFin()
            : base()
        {
            CodigoGlobal = 34;
            MensajeModoTexto = "El fin de un bloque mientras debe especificarse de la siguiente manera: finmientras;";
            MensajeModoGrafico = "El fin de un bloque mientras debe especificarse de la siguiente manera: finmientras;";
        }
    }

    public class ErrorSiValidacionPorDefault : MensajeError
    {
        public ErrorSiValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 35;
            MensajeModoTexto = "La declaracion del bloque si contiene un error sintactico.";
            MensajeModoGrafico = "La declaracion del bloque si contiene un error sintactico.";
        }
    }

    public class ErrorMostrarValidacionPorDefault : MensajeError
    {
        public ErrorMostrarValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 36;
            MensajeModoTexto = "La llamada a mostrar contiene un error sintactico.";
            MensajeModoGrafico = "La llamada a mostrar contiene un error sintactico.";
        }
    }

    public class ErrorFinProcValidacionFin : MensajeError
    {
        public ErrorFinProcValidacionFin()
            : base()
        {
            CodigoGlobal = 37;
            MensajeModoTexto = "El fin de la declaración de un procedimiento debe especificarse de la siguiente manera: finproc;";
            MensajeModoGrafico = "El fin de la declaración de un procedimiento debe especificarse de la siguiente manera: finproc;";
        }
    }

    public class ErrorFinFuncValidacionFin : MensajeError
    {
        public ErrorFinFuncValidacionFin()
            : base()
        {
            CodigoGlobal = 38;
            MensajeModoTexto = "El fin de la declaración de una función debe especificarse de la siguiente manera: finfunc;";
            MensajeModoGrafico = "El fin de la declaración de una función debe especificarse de la siguiente manera: finfunc;";
        }
    }

    public class ErrorFinFuncValidacionPorDefault : MensajeError
    {
        public ErrorFinFuncValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 39;
            MensajeModoTexto = "El fin de la declaracion de la funcion contiene un error sintactico.";
            MensajeModoGrafico = "El fin de la declaracion de la funcion contiene un error sintactico.";
        }
    }

    public class ErrorLeerRepetido : MensajeError
    {
        public ErrorLeerRepetido()
            : base()
        {
            CodigoGlobal = 40;
            MensajeModoTexto = "Hay llamadas a leer repetidas en la misma linea";
            MensajeModoGrafico = "Hay llamadas a leer repetidas en la misma linea";
        }
    }

    public class ErrorLeerSolo : MensajeError
    {
        public ErrorLeerSolo()
            : base()
        {
            CodigoGlobal = 41;
            MensajeModoTexto = "La llamada a leer debe estar acompañada de una variable o una posición de un arreglo";
            MensajeModoGrafico = "La llamada a leer debe estar acompañada de una variable o una posición de un arreglo";
        }
    }

    public class ErrorLeerNoIdentificador : MensajeError
    {
        public ErrorLeerNoIdentificador()
            : base()
        {
            CodigoGlobal = 42;
            MensajeModoTexto = "La llamada a leer puede estar acompañada unicamente de una variable o una posición de un arreglo";
            MensajeModoGrafico = "La llamada a leer puede estar acompañada unicamente de una variable o una posición de un arreglo";
        }
    }
}
