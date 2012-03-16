using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class DeclaracionConstante: TipoBase
    {
        public DeclaracionConstante(List<Terminal> lista, int fila, int col) 
            : base(fila,col)
        {
            listaLineaEntera = lista;

            

        }


        private void AgregarValidacionAsignacionRepetido()
        {
            string mensajeError = "El : esta especificado mas de una vez en la declaración";
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ValidarDefTipoDatoRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionAsignacionFaltante()
        {
            string mensajeError = ": faltante en la declaración";
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ValidarDefTipoDatoFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }



        private void AgregarValidacionAsignarValorRepetido()
        {
            string mensajeError = "El = esta especificado mas de una vez en la declaración";
            short importancia = 9;

            int i = listaLineaEntera.FindIndex(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoDato);

            List<Terminal> parteDer;

            if (i > 0)
            {
                parteDer = listaLineaEntera.GetRange(i, listaLineaEntera.Count - i + 1);
            }
            else
            {
                parteDer = listaLineaEntera;
            }

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.ValidarAsignacionValorConstanteRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionAsignarValorFaltante()
        {
            string mensajeError = "= faltante en la declaración";
            short importancia = 9;

            int i = listaLineaEntera.FindIndex(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoDato);

            List<Terminal> parteDer;

            if (i > 0)
            {
                parteDer = listaLineaEntera.GetRange(i, listaLineaEntera.Count - i + 1);
            }
            else
            {
                parteDer = listaLineaEntera;
            }

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.ValidarAsignacionValorConstanteFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }




    }
}
