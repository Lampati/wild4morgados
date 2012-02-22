using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoLlamadaProc: NodoArbolSemantico
    {
        public NodoLlamadaProc(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ListaFirma = new List<Firma>();
            this.LlamaProcs = true;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
    
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {

            List<Firma> listaFirmaComparar = this.hijosNodo[3].ListaFirma;
            this.ListaFirma = listaFirmaComparar;
            string nombre = this.hijosNodo[1].Lexema;
            this.Lexema = nombre;

            LineaCorrespondiente = GlobalesCompilador.UltFila;

            StringBuilder strbldr;

            if (this.TablaSimbolos.ExisteProcedimiento(nombre))
            {
                List<FirmaProc> firmaFuncion = this.TablaSimbolos.ObtenerFirma(nombre, NodoTablaSimbolos.TipoDeEntrada.Procedimiento);

                if (firmaFuncion.Count == listaFirmaComparar.Count)
                {
                    int i = 0;
                    bool igual = true;

                    while (i < firmaFuncion.Count && igual)
                    {
                        igual = firmaFuncion[i].TipoDato == listaFirmaComparar[i].Tipo
                            && firmaFuncion[i].EsArreglo == listaFirmaComparar[i].EsArreglo;
                        i++;
                    }

                    if (igual)
                    {
                        this.TipoDato = this.TablaSimbolos.ObtenerTipoProcedimiento(nombre);
                        //this.Valor = 1;
                       
                        if (nombre.ToLower().Trim().Equals(GlobalesCompilador.NOMBRE_PROC_SALIDA))
                        {
                            this.LlamaProcSalida = true;
                        }
                    }
                    else
                    {
                        List<ErrorSemanticoException> listaExcepciones = new List<ErrorSemanticoException>();

                        strbldr = new StringBuilder();
                        if (firmaFuncion[i - 1].TipoDato != listaFirmaComparar[i - 1].Tipo)
                        {

                            strbldr = new StringBuilder("El parametro ").Append(firmaFuncion[i - 1].Lexema).Append(" pasado al procedimiento ");
                            strbldr.Append(nombre).Append(" es de tipo incorrecto. Debe ser de tipo ").Append(EnumUtils.stringValueOf(firmaFuncion[i - 1].TipoDato));
                            listaExcepciones.Add(new ErrorSemanticoException(strbldr.ToString()));
                            

                        }

                        if (firmaFuncion[i - 1].EsArreglo != listaFirmaComparar[i - 1].EsArreglo)
                        {
                            if (firmaFuncion[i - 1].EsArreglo)
                            {
                                strbldr = new StringBuilder("El parametro ").Append(firmaFuncion[i - 1].Lexema).Append(" pasado a la procedimiento ");
                                strbldr.Append(nombre).Append(" debe ser un arreglo, y se paso una variable.");
                                listaExcepciones.Add(new ErrorSemanticoException(strbldr.ToString()));
                            }
                            else
                            {
                                strbldr = new StringBuilder("El parametro ").Append(firmaFuncion[i - 1].Lexema).Append(" pasado a la procedimiento ");
                                strbldr.Append(nombre).Append(" debe ser una variable, y se paso una arreglo.");
                                listaExcepciones.Add(new ErrorSemanticoException(strbldr.ToString()));

                            }
                        }

                        if (listaExcepciones.Count > 0)
                        {
                            throw new AggregateException(listaExcepciones);
                        }
                    }

                }
                else
                {
                    strbldr = new StringBuilder("La cantidad de parametros para el procedimiento ").Append(nombre).Append(" es incorrecta.");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }
            }
            else
            {
                strbldr = new StringBuilder("El procedimiento ").Append(nombre).Append(" no esta declarado.");
                throw new ErrorSemanticoException(strbldr.ToString());
            }

            return this;
        }


        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.ListaFirma.AddRange(hijoASintetizar.ListaFirma);
            
        }

        public override void ChequearAtributos(Terminal t)
        {
            
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine(GeneracionCodigoHelpers.AsignarLinea(LineaCorrespondiente));

            strBldr.Append(this.hijosNodo[1].Codigo);
            strBldr.Append("(");
            strBldr.Append(this.hijosNodo[3].Codigo);
            strBldr.Append(")");
            strBldr.Append(";");

            this.Codigo = strBldr.ToString();

        }
    }


   
}
