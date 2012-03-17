using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal static class ValidacionesFactory
    {
        private delegate bool ChequeosTerminalesDelegate(Terminal x);


        #region Declaraciones de constantes y variables
        internal static bool ValidarDefTipoDatoFaltante(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoDato).Count;

            return cantidad > 0;
        }

        internal static bool ValidarDefTipoDatoRepetido(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoDato).Count;

            return cantidad < 2;
        }

        internal static bool ValidarTipoDatoFaltante(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => EsTipoDeDato(x)).Count;

            return cantidad > 0;
        }

        internal static bool ValidarTipoDatoRepetido(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => EsTipoDeDato(x)).Count;

            return cantidad < 2;
        }

        internal static bool ValidarAsignacionValorConstanteFaltante(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Igual).Count;

            return cantidad > 0;
        }

        internal static bool ValidarAsignacionValorConstanteRepetido(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Igual).Count;

            return cantidad < 2;
        }

        internal static bool ValidarDefTipoDatoSinArreglo(List<Terminal> lista)
        {
            bool retorno = true;

            if (lista.Count > 0)
            {
                retorno = lista[0].Componente.Token == Lexicografico.ComponenteLexico.TokenType.Arreglo;
            }

            return retorno;
        }

        internal static bool ValidarEstarDefiniendoSoloUnID(List<Terminal> lista)
        {

            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoDato).Count;

            return cantidad < 2;

        }


        #endregion



        internal static bool ValidarFinMientras(List<Terminal> lista)
        {
            bool retorno = lista.Count == 2 
                && lista[0].Componente.Token == Lexicografico.ComponenteLexico.TokenType.MientrasFin
                && lista[1].Componente.Token == Lexicografico.ComponenteLexico.TokenType.FinSentencia;        

            return retorno;
        }

        internal static bool ValidarFinSi(List<Terminal> lista)
        {
            bool retorno = lista.Count == 2
                && lista[0].Componente.Token == Lexicografico.ComponenteLexico.TokenType.SiFin
                && lista[1].Componente.Token == Lexicografico.ComponenteLexico.TokenType.FinSentencia;

            return retorno;
        }


        internal static bool ValidarFinProc(List<Terminal> lista)
        {
            bool retorno = lista.Count == 2
                && lista[0].Componente.Token == Lexicografico.ComponenteLexico.TokenType.ProcedimientoFin
                && lista[1].Componente.Token == Lexicografico.ComponenteLexico.TokenType.FinSentencia;

            return retorno;
        }

        internal static bool AsignacionRepetido(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion).Count;

            return cantidad < 2;
        }

        internal static bool AsignacionFaltante(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion).Count;

            return cantidad > 0;
        }

        internal static bool AsignacionTerminaCorrectamente(List<Terminal> lista)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Asignacion).Count;

            return cantidad > 0;
        }

        internal static bool AsignacionParteIzqCorrecta(List<Terminal> lista)
        {
            bool res = lista.Exists(x => 
                                x.Componente.Token != Lexicografico.ComponenteLexico.TokenType.Identificador
                                && x.Componente.Token != Lexicografico.ComponenteLexico.TokenType.CorcheteApertura                                
                            );

            return res;
        }

        internal static bool ParentesisBalanceados(List<Terminal> lista)
        {
            int cantidadAbiertos = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisApertura).Count;
            int cantidadCerrados = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisClausura).Count;

            return cantidadAbiertos == cantidadCerrados;
        }

        internal static bool CorchetesBalanceados(List<Terminal> lista)
        {
            int cantidadAbiertos = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.CorcheteApertura).Count;
            int cantidadCerrados = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.CorcheteClausura).Count;

            return cantidadAbiertos == cantidadCerrados;
        }

        internal static bool ElementosConValorNoContiguos(List<Terminal> lista)
        {           
            List<Terminal> listaElementosConValorContiguos = lista.FindAll(x => EsTerminalConValor(x));

            return ChequeoContiguosIguales(lista, listaElementosConValorContiguos, EsTerminalConValor);
        }

        internal static bool ElementosOperadoresNoContiguos(List<Terminal> lista)
        {
            List<Terminal> listaElementosOperadoresContiguos = lista.FindAll(x => EsOperador(x));

            return ChequeoContiguosIguales(lista, listaElementosOperadoresContiguos, EsOperador);
        }

        internal static bool NegacionesCorrectas(List<Terminal> lista)
        {
            bool retorno = true;

            List<Terminal> listaElementos = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Negacion);

            foreach (Terminal item in listaElementos)
            {
                int indice = lista.IndexOf(item);
                if (indice < lista.Count - 1)
                {
                    retorno = !(lista[indice + 1].Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisApertura);
                }
                else
                {
                    //es el ultimo elemento. esta mal.
                    retorno = false;
                }

                if (!retorno)
                {
                    break;
                }
            }
            return retorno;
        }




        private static bool ChequeoContiguosIguales(List<Terminal> lista, List<Terminal> listaElementosConValorContiguos, ChequeosTerminalesDelegate del)
        {
            bool retorno = true;

            foreach (Terminal item in listaElementosConValorContiguos)
            {
                int indice = lista.IndexOf(item);
                if (indice > 0 && indice < lista.Count - 1)
                {
                    retorno = !(del.Invoke(lista[indice - 1]) || del.Invoke(lista[indice + 1]));
                }
                else
                {
                    if (indice > 0)
                    {
                        retorno = !(del.Invoke(lista[indice - 1]));
                    }
                    else
                    {
                        if (indice < lista.Count - 1)
                        {
                            retorno = !(del.Invoke(lista[indice + 1]));
                        }
                        else
                        {
                            //Tiene un solo elemento. Cumple la validacion
                            retorno = true;
                        }
                    }
                }

                if (!retorno)
                {
                    break;
                }
            }
            return retorno;
        }

        private static bool EsParentesis(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisApertura
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisClausura
                 );
        }


        private static bool EsOperador(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.SumaEntero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.RestaEntero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.MultiplicacionEntero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.DivisionEntero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.And
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Or
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Concatenacion
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Menor
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.MenorIgual
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Igual
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Distinto
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Mayor
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.MayorIgual
                 );
        }


        private static bool EsTerminalConValor(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Numero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Literal
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Verdadero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Falso
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Identificador
                 );
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
