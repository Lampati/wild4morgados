using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class Asignacion : TipoBase
    {
        public Asignacion(List<Terminal> lista, int fila, int col) 
            : base(fila,col)
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

            AgregarValidacionElementosConValorNoContiguosParteIzq();
            AgregarValidacionElementosConValorNoContiguosParteDer();

            AgregarValidacionPorDefault();
        }

        private void AgregarValidacionAsignacionRepetido()
        {
            string mensajeError = "El := esta especificado mas de una vez en la asignacion";
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.AsignacionRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }


        

        private void AgregarValidacionAsignacionFaltante()
        {
            string mensajeError = ":= faltante en la asignacion";
            short importancia = 9;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.AsignacionFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        

        private void AgregarValidacionTerminaCorrectamente()
        {
            string mensajeError = "La asignacion termina incorrectamente";
            short importancia = 8;

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Asignacion);            

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.AsignacionTerminaCorrectamente, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionParteIzqCorrecta()
        {
            string mensajeError = "Error sintactico en la parte izq";
            short importancia = 7;

            List<Terminal> parteIzq = ArmarSubListaIzquierdaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Asignacion); 

            Validacion valRep = new Validacion(parteIzq, mensajeError, importancia, ValidacionesFactory.AsignacionParteIzqCorrecta, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionParentesisBalanceadosParteIzq()
        {
            string mensajeError = "Los parentesis no estan balanceados en la parte izquierda de la asignacion";
            short importancia = 6;

            List<Terminal> parteIzq = ArmarSubListaIzquierdaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Asignacion); 

            Validacion valRep = new Validacion(parteIzq, mensajeError, importancia, ValidacionesFactory.ParentesisBalanceados, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);            
        }

       

        private void AgregarValidacionParentesisBalanceadosParteDer()
        {
            string mensajeError = "Los parentesis no estan balanceados en la parte derecha de la asignacion";
            short importancia = 6;

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Asignacion);            

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.ParentesisBalanceados, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionCorchetesBalanceadosParteIzq()
        {
            string mensajeError = "Los corchetes no estan balanceados en la parte izquierda de la asignacion";
            short importancia = 5;

            List<Terminal> parteIzq = ArmarSubListaIzquierdaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Asignacion); 

            Validacion valRep = new Validacion(parteIzq, mensajeError, importancia, ValidacionesFactory.CorchetesBalanceados, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionCorchetesBalanceadosParteDer()
        {
            string mensajeError = "Los corchetes no estan balanceados en la parte derecha de la asignacion";
            short importancia = 5;

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Asignacion);            

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.CorchetesBalanceados, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionElementosConValorNoContiguosParteIzq()
        {
            string mensajeError = "La asignacion contiene una expresión mal formada en su parte izquierda.";
            short importancia = 4;

            List<Terminal> parteIzq = ArmarSubListaIzquierdaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Asignacion); 

            Validacion valRep = new Validacion(parteIzq, mensajeError, importancia, ValidacionesFactory.ElementosConValorNoContiguos, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionElementosConValorNoContiguosParteDer()
        {
            string mensajeError = "La asignacion contiene una expresión mal formada en su parte derecha.";
            short importancia = 4;

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Asignacion);            

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.ElementosConValorNoContiguos, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }


        private void AgregarValidacionPorDefault()
        {
            string mensajeError = "La asignacion contiene un error sintactico.";
            short importancia = 1;


            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarFalso, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }



       
    }
}
