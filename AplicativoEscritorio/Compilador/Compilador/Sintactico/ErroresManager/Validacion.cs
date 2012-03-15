using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal class Validacion
    {
        internal delegate bool ValidacionDelegate(List<Terminal> x);

        private List<Terminal> lista;
        private ValidacionDelegate metodoValidacion;
        private string mensajeError;
        private short importancia;

        public short Importancia
        {
            get { return importancia; }
        }
               

        public bool EsValido
        {
            get { return Validar(); }
        }
        

        /// <summary>
        /// Crear una nueva validacion
        /// </summary>
        /// <param name="terminalesAValidar">Lista de terminales con los que se validara en el metodo Validar()</param>
        /// <param name="mens">Mensaje de Error a mostrar si la validacion no se cumple</param>
        /// <param name="importancia">Importancia de la validacion</param>
        /// <param name="metodoVal">Metodo que contiene el codigo a ejecutar de la validacion</param>
        internal Validacion(List<Terminal> terminalesAValidar, string mens, short p, ValidacionDelegate metodoVal)
        {
            lista = terminalesAValidar;
            mensajeError = mens;
            importancia = p;
            metodoValidacion = metodoVal;
        }

        private bool Validar()
        {
            return metodoValidacion.Invoke(lista);
        }

        internal void ArrojarExcepcion()
        {
            throw new ValidacionException(mensajeError);
        }
    }
}
