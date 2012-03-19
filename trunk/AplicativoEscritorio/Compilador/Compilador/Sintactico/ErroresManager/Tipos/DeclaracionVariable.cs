using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class DeclaracionVariable: TipoBase
    {
        public DeclaracionVariable(List<Terminal> lista, int fila, int col) 
            : base(fila,col)
        {
            listaLineaEntera = lista;

            AgregarValidacionAsignacionFaltante();
            AgregarValidacionAsignacionRepetido();
            AgregarValidacionAsignarValorFaltante();
            AgregarValidacionAsignarValorRepetido();
            AgregarValidacionTipoDatoFaltante();
            AgregarValidacionTipoDatoRepetido();
            AgregarValidacionTipoDatoSinArreglo();
            AgregarValidacionValorFaltante();
            AgregarValidacionValorRepetido();
            AgregarValidacionElementoQueSobraErroneo();

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

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.ValidarAsignacionConstanteRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionAsignarValorFaltante()
        {
            string mensajeError = "= faltante en la declaración";
            short importancia = 9;

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera,Lexicografico.ComponenteLexico.TokenType.TipoDato);

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.ValidarAsignacionConstanteFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionTipoDatoSinArreglo()
        {
            string mensajeError = "Las constantes no pueden ser arreglos";
            short importancia = 7;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaIzquierdaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarDefTipoDatoSinArreglo, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionTipoDatoRepetido()
        {
            string mensajeError = "El tipo de dato esta especificado mas de una vez en la declaración";
            short importancia = 7;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaIzquierdaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarTipoDatoRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionTipoDatoFaltante()
        {
            string mensajeError = "Tipo de dato faltante en la declaración";
            short importancia = 7;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaIzquierdaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarTipoDatoFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionValorRepetido()
        {
            string mensajeError = "El valor de constante esta especificado mas de una vez en la declaración";
            short importancia = 6;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaDerechaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarAsignacionValorConstanteRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionValorFaltante()
        {
            string mensajeError = "Valor de constante faltante en la declaración";
            short importancia = 6;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaDerechaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarAsignacionValorConstanteFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionElementoQueSobraErroneo()
        {
            
            short importancia = 5;

            int i = 0;
            Terminal terminalErroneo = null;

            while (i < listaLineaEntera.Count && terminalErroneo == null)
            {
                switch (i)
                {
                    case 0:
                        if (listaLineaEntera[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.Const)
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 1:
                        if (listaLineaEntera[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.Identificador)
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 2:
                        if (listaLineaEntera[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.TipoDato)
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 3:
                        if   ( ! EsTipoDeDato(listaLineaEntera[i]))
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 4:
                        if (listaLineaEntera[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.Igual)
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 5:
                        if (!EsTerminalConValorConstante(listaLineaEntera[i]))
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 6:
                        if (listaLineaEntera[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.FinSentencia)
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    default:
                        terminalErroneo = listaLineaEntera[i];
                        break;
                }

                i++;
            }

            string mensajeError = "{0} no tiene lugar en una declaración de constante";
            Validacion valRep;

            if (i < listaLineaEntera.Count)
            {
                mensajeError = string.Format(mensajeError, terminalErroneo.Componente.Lexema);
                valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarFalso, FilaDelError, ColumnaDelError);
            }
            else
            {
                valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarVerdadero, FilaDelError, ColumnaDelError);
            }

            listaValidaciones.Add(valRep);
        }


        private static bool EsTerminalConValorConstante(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Numero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Literal
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Verdadero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Falso
                 );
        }

        private static bool EsTipoDeDato(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoNumero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoNumero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoTexto
                 );
        }

    }
}
