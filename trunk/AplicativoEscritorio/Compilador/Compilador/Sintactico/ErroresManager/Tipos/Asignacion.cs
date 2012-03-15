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
            AgregarValidacionCorchetesBalanceadosParteDer();
            AgregarValidacionCorchetesBalanceadosParteIzq();
            AgregarValidacionParentesisBalanceadosParteDer();
            AgregarValidacionParentesisBalanceadosParteIzq();
            //AgregarValidacionParteIzqCorrecta();
            AgregarValidacionTerminaCorrectamente();
        }

        private void AgregarValidacionAsignacionRepetido()
        {
            string mensajeError = "El := esta especificado mas de una vez en la asignacion";
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.AsignacionRepetido);

            listaValidaciones.Add(valRep);
        }


        

        private void AgregarValidacionAsignacionFaltante()
        {
            string mensajeError = ":= faltante en la asignacion";
            short importancia = 9;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.AsignacionFaltante);

            listaValidaciones.Add(valRep);
        }

        

        private void AgregarValidacionTerminaCorrectamente()
        {
            string mensajeError = "La asignacion termina incorrectamente";
            short importancia = 8;

            int i = listaLineaEntera.FindIndex(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion);

            List<Terminal> parteDer;

            if (i > 0)
            {
                parteDer = listaLineaEntera.GetRange(i, listaLineaEntera.Count - i);
            }
            else
            {
                parteDer = listaLineaEntera;
            }

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.AsignacionTerminaCorrectamente);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionParteIzqCorrecta()
        {
            string mensajeError = "Error sintactico en la parte izq";
            short importancia = 7;

            int i = listaLineaEntera.FindIndex(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion);

            List<Terminal> parteIzq;

            if (i > 0)
            {
                parteIzq = listaLineaEntera.GetRange(0, i);
            }
            else
            {
                parteIzq = listaLineaEntera;
            }

            Validacion valRep = new Validacion(parteIzq, mensajeError, importancia, ValidacionesFactory.AsignacionParteIzqCorrecta);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionParentesisBalanceadosParteIzq()
        {
            string mensajeError = "Los parentesis no estan balanceados en la parte izquierda de la asignacion";
            short importancia = 6;

            int i = listaLineaEntera.FindIndex(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion);

            List<Terminal> parteIzq;

            if (i > 0)
            {
                parteIzq = listaLineaEntera.GetRange(0, i);
            }
            else
            {
                parteIzq = listaLineaEntera;
            }

            Validacion valRep = new Validacion(parteIzq, mensajeError, importancia, ValidacionesFactory.ParentesisBalanceados);           
            listaValidaciones.Add(valRep);            
        }

        private void AgregarValidacionParentesisBalanceadosParteDer()
        {
            string mensajeError = "Los parentesis no estan balanceados en la parte derecha de la asignacion";
            short importancia = 6;

            int i = listaLineaEntera.FindIndex(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion);

            List<Terminal> parteDer;

            if (i > 0)
            {
                parteDer = listaLineaEntera.GetRange(i, listaLineaEntera.Count - i);
            }
            else
            {
                parteDer = listaLineaEntera;
            }

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.ParentesisBalanceados);
            listaValidaciones.Add(valRep);
        }


        private void AgregarValidacionCorchetesBalanceadosParteIzq()
        {
            string mensajeError = "Los corchetes no estan balanceados en la parte izquierda de la asignacion";
            short importancia = 5;

            int i = listaLineaEntera.FindIndex(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion);

            List<Terminal> parteIzq;

            if (i > 0)
            {                
                parteIzq = listaLineaEntera.GetRange(0, i);
            }
            else
            {
                parteIzq = listaLineaEntera;
            }

            Validacion valRep = new Validacion(parteIzq, mensajeError, importancia, ValidacionesFactory.CorchetesBalanceados);
            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionCorchetesBalanceadosParteDer()
        {
            string mensajeError = "Los corchetes no estan balanceados en la parte derecha de la asignacion";
            short importancia = 5;

            int i = listaLineaEntera.FindIndex(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion);

            List<Terminal> parteDer;

            if (i > 0)
            {
                parteDer = listaLineaEntera.GetRange(i, listaLineaEntera.Count - i);
            }
            else
            {
                parteDer = listaLineaEntera;
            }

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.CorchetesBalanceados);
            listaValidaciones.Add(valRep);
        }
    }
}
