using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Sintactico.ErroresManager.Errores
{
    public abstract class MensajeError
    {
        public string Mensaje { get; set; }
        public int CodigoGlobal { get; set; }

        public MensajeError()
        {

        }
    }

    public class ErrorDeclaracionConstanteGenerico : MensajeError
    {
        public ErrorDeclaracionConstanteGenerico()
            : base()
        {
            CodigoGlobal = 1;
            Mensaje = "La declaración de la constante contiene un error sintactico.";
        }
    }

    public class ErrorTipoDatoDefRepetido : MensajeError
    {
        public ErrorTipoDatoDefRepetido()
            : base()
        {
            CodigoGlobal = 2;
            Mensaje = "El : esta especificado mas de una vez en la declaración";
        }
    }

    public class ErrorTipoDatoDefFaltante : MensajeError
    {
        public ErrorTipoDatoDefFaltante()
            : base()
        {
            CodigoGlobal = 3;
            Mensaje = ": faltante en la declaración";
        }
    }

    public class ErrorAsignarValorRepetido : MensajeError
    {
        public ErrorAsignarValorRepetido()
            : base()
        {
            CodigoGlobal = 4;
            Mensaje = "El = esta especificado mas de una vez en la declaración";
        }
    }

    public class ErrorAsignarValorFaltante : MensajeError
    {
        public ErrorAsignarValorFaltante()
            : base()
        {
            CodigoGlobal = 5;
            Mensaje = "= faltante en la declaración";
        }
    }

    public class ErrorConstanteTipoDatoSinArreglo : MensajeError
    {
        public ErrorConstanteTipoDatoSinArreglo()
            : base()
        {
            CodigoGlobal = 6;
            Mensaje = "Las constantes no pueden ser arreglos";
        }
    }

    public class ErrorTipoDatoRepetido : MensajeError
    {
        public ErrorTipoDatoRepetido()
            : base()
        {
            CodigoGlobal = 7;
            Mensaje = "El tipo de dato esta especificado mas de una vez en la declaración";
        }
    }

    public class ErrorTipoDatoFaltante : MensajeError
    {
        public ErrorTipoDatoFaltante()
            : base()
        {
            CodigoGlobal = 8;
            Mensaje = "Tipo de dato faltante en la declaración";
        }
    }

    public class ErrorConstanteValorRepetido : MensajeError
    {
        public ErrorConstanteValorRepetido()
            : base()
        {
            CodigoGlobal = 9;
            Mensaje = "El valor de constante esta especificado mas de una vez en la declaración";
        }
    }

    public class ErrorConstanteValorFaltante : MensajeError
    {
        public ErrorConstanteValorFaltante()
            : base()
        {
            CodigoGlobal = 10;
            Mensaje = "Valor de constante faltante en la declaración";
        }
    }

    public class ErrorConstanteElementoQueSobraErroneo : MensajeError
    {
        public ErrorConstanteElementoQueSobraErroneo(string elementoErroneo)
            : base()
        {
            CodigoGlobal = 11;
            Mensaje = string.Format("{0} no tiene lugar en una declaración de constante", elementoErroneo);
        }
    }

    public class ErrorAsignacionRepetido : MensajeError
    {
        public ErrorAsignacionRepetido()
            : base()
        {
            CodigoGlobal = 12;
            Mensaje = "El := esta especificado mas de una vez en la asignacion";
        }
    }

    public class ErrorAsignacionFaltante : MensajeError
    {
        public ErrorAsignacionFaltante()
            : base()
        {
            CodigoGlobal = 13;
            Mensaje = ":= faltante en la asignacion";
        }
    }

    public class ErrorAsignacionTerminaCorrectamente : MensajeError
    {
        public ErrorAsignacionTerminaCorrectamente()
            : base()
        {
            CodigoGlobal = 14;
            Mensaje = "La asignacion termina incorrectamente";
        }
    }

    public class ErrorAsignacionParteIzqCorrecta : MensajeError
    {
        public ErrorAsignacionParteIzqCorrecta()
            : base()
        {
            CodigoGlobal = 15;
            Mensaje = "Error sintactico en la parte izq";
        }
    }

    public class ErrorAsignacionParentesisBalanceadosParteIzq : MensajeError
    {
        public ErrorAsignacionParentesisBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 16;
            Mensaje = "Los parentesis no estan balanceados en la parte izquierda de la asignacion";
        }
    }

    public class ErrorAsignacionParentesisBalanceadosParteDer : MensajeError
    {
        public ErrorAsignacionParentesisBalanceadosParteDer()
            : base()
        {
            CodigoGlobal = 17;
            Mensaje = "Los parentesis no estan balanceados en la parte derecha de la asignacion";
        }
    }

    public class ErrorAsignacionCorchetesBalanceadosParteIzq : MensajeError
    {
        public ErrorAsignacionCorchetesBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 18;
            Mensaje = "Los corchetes no estan balanceados en la parte izquierda de la asignacion";
        }
    }

    public class ErrorAsignacionCorchetesBalanceadosParteDer : MensajeError
    {
        public ErrorAsignacionCorchetesBalanceadosParteDer()
            : base()
        {
            CodigoGlobal = 19;
            Mensaje = "Los corchetes no estan balanceados en la parte derecha de la asignacion";
        }
    }

    public class ErrorAsignacionElementosConValorNoContiguosParteIzq : MensajeError
    {
        public ErrorAsignacionElementosConValorNoContiguosParteIzq()
            : base()
        {
            CodigoGlobal = 20;
            Mensaje = "La asignacion contiene una expresión mal formada en su parte izquierda.";
        }
    }

    public class ErrorAsignacionElementosConValorNoContiguosParteDer : MensajeError
    {
        public ErrorAsignacionElementosConValorNoContiguosParteDer()
            : base()
        {
            CodigoGlobal = 21;
            Mensaje = "La asignacion contiene una expresión mal formada en su parte derecha.";
        }
    }

    public class ErrorAsignacionValidacionPorDefault : MensajeError
    {
        public ErrorAsignacionValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 22;
            Mensaje = "La asignacion contiene un error sintactico.";
        }
    }

    public class ErrorDeclaracionFuncionValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionFuncionValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 23;
            Mensaje = "La declaración de la función contiene un error sintactico.";
        }
    }

    public class ErrorDeclaracionProcedimientoValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionProcedimientoValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 24;
            Mensaje = "La declaración del procedimiento contiene un error sintactico.";
        }
    }

    public class ErrorDeclaracionVariableValidacionPorDefault : MensajeError
    {
        public ErrorDeclaracionVariableValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 25;
            Mensaje = "La declaración de la variable contiene un error sintactico.";
        }
    }

    public class ErrorDeclaracionVariableParteIzquierdaValida : MensajeError
    {
        public ErrorDeclaracionVariableParteIzquierdaValida()
            : base()
        {
            CodigoGlobal = 26;
            Mensaje = "La declaración de variables es incorrecta. Debe ser una lista de identificadores separados por comas o un identificador solo";
        }
    }

    public class ErrorDeclaracionVariableCantArregloNoRepetido : MensajeError
    {
        public ErrorDeclaracionVariableCantArregloNoRepetido()
            : base()
        {
            CodigoGlobal = 27;
            Mensaje = "El arreglo esta especificado mas de una vez en la declaración";
        }
    }

    public class ErrorDeclaracionVariableCorchetesBalanceadosParteIzq : MensajeError
    {
        public ErrorDeclaracionVariableCorchetesBalanceadosParteIzq()
            : base()
        {
            CodigoGlobal = 28;
            Mensaje = "Los corchetes no estan balanceados en la declaracion del arreglo";
        }
    }

    public class ErrorDeclaracionVariableElementoQueSobraErroneo : MensajeError
    {
        public ErrorDeclaracionVariableElementoQueSobraErroneo(string elementoErroneo)
            : base()
        {
            CodigoGlobal = 29;
            Mensaje = string.Format("Error sintactico: {0} es incorrecto en la declaración de un arreglo. La forma correcta es arreglo [MAX] de TIPO", elementoErroneo);
        }
    }

    public class ErrorMientrasValidacionPorDefault : MensajeError
    {
        public ErrorMientrasValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 30;
            Mensaje = "El mientras contiene un error sintactico.";
        }
    }

    public class ErrorFinSiValidacionFin : MensajeError
    {
        public ErrorFinSiValidacionFin()
            : base()
        {
            CodigoGlobal = 31;
            Mensaje = "El fin de un bloque si debe especificarse de la siguiente manera: finsi;";
        }
    }

    public class ErrorLlamadoProcValidacionPorDefault : MensajeError
    {
        public ErrorLlamadoProcValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 32;
            Mensaje = "La llamada al procedimiento contiene un error sintactico.";
        }
    }

    public class ErrorLeerValidacionPorDefault : MensajeError
    {
        public ErrorLeerValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 33;
            Mensaje = "La llamada al procedimiento contiene un error sintactico.";
        }
    }

    public class ErrorFinMientrasValidacionFin : MensajeError
    {
        public ErrorFinMientrasValidacionFin()
            : base()
        {
            CodigoGlobal = 34;
            Mensaje = "El fin de un bloque mientras debe especificarse de la siguiente manera: finmientras;";
        }
    }

    public class ErrorSiValidacionPorDefault : MensajeError
    {
        public ErrorSiValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 35;
            Mensaje = "La declaracion del bloque si contiene un error sintactico.";
        }
    }

    public class ErrorMostrarValidacionPorDefault : MensajeError
    {
        public ErrorMostrarValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 36;
            Mensaje = "La llamada a mostrar contiene un error sintactico.";
        }
    }

    public class ErrorFinProcValidacionFin : MensajeError
    {
        public ErrorFinProcValidacionFin()
            : base()
        {
            CodigoGlobal = 37;
            Mensaje = "El fin de la declaración de un procedimiento debe especificarse de la siguiente manera: finproc;";
        }
    }

    public class ErrorFinFuncValidacionFin : MensajeError
    {
        public ErrorFinFuncValidacionFin()
            : base()
        {
            CodigoGlobal = 38;
            Mensaje = "El fin de la declaración de una función debe especificarse de la siguiente manera: finfunc;";
        }
    }

    public class ErrorFinFuncValidacionPorDefault : MensajeError
    {
        public ErrorFinFuncValidacionPorDefault()
            : base()
        {
            CodigoGlobal = 39;
            Mensaje = "El fin de la declaracion de la funcion contiene un error sintactico.";
        }
    }
}
