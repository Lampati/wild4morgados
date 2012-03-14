using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class Asignacion : TipoBase
    {
        public Asignacion(List<Terminal> lista)
        {
            listaLineaEntera = lista;

            AgregarValidacionAsignacionRepetido();
            AgregarValidacionAsignacionFaltante();
        }

        private void AgregarValidacionAsignacionRepetido()
        {
            string mensajeError = "";
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionAsignacionRepetido);

            listaValidaciones.Add(valRep);
        }


        public bool ValidacionAsignacionRepetido(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion).Count;

            return cantidad < 2;        
        }

        private void AgregarValidacionAsignacionFaltante()
        {
            string mensajeError = "";
            short importancia = 9;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionAsignacionFaltante);

            listaValidaciones.Add(valRep);
        }

        public bool ValidacionAsignacionFaltante(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion).Count;

            return cantidad > 0;
        }
    }
}
