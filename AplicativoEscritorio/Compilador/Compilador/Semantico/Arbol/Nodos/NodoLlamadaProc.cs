using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.Arbol.Nodos.Auxiliares;
using Compilador.Semantico.TablaDeSimbolos;

namespace Compilador.Semantico.Arbol.Nodos
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

            StringBuilder strbldr;

            if (this.TablaSimbolos.ExisteProcedimiento(nombre))
            {
                List<NodoTablaSimbolos.TipoDeDato> firmaFuncion = this.TablaSimbolos.ObtenerFirma(nombre, NodoTablaSimbolos.TipoDeEntrada.Procedimiento);

                if (firmaFuncion.Count == listaFirmaComparar.Count)
                {
                    int i = 0;
                    bool igual = true;

                    while (i < firmaFuncion.Count && igual)
                    {
                        igual = firmaFuncion[i] == listaFirmaComparar[i].Tipo;
                        i++;
                    }

                    if (igual)
                    {
                        this.TipoDato = this.TablaSimbolos.ObtenerTipoProcedimiento(nombre);
                        //this.Valor = 1;
                        strbldr = new StringBuilder("Llamada a procedimiento ").Append(nombre);
                        this.TextoParaImprimirArbol = strbldr.ToString();

                        if (nombre.ToLower().Trim().Equals(Global.NOMBRE_PROC_SALIDA))
                        {
                            this.LlamaProcSalida = true;
                        }
                    }
                    else
                    {
                        strbldr = new StringBuilder("El parametro ").Append(listaFirmaComparar[i - 1].Lexema).Append(" pasado a la funcion ");
                        strbldr.Append(nombre).Append(" es de tipo incorrecto.");
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }

                }
                else
                {
                    strbldr = new StringBuilder("La cantidad de parametros para la funcion ").Append(nombre).Append(" es incorrecta.");
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
            StringBuilder strbldr = new StringBuilder("\t");

         

            this.Codigo = strbldr.ToString().Replace("\r\n", "\r\n\t").ToString().TrimEnd('\t');
        }
    }


   
}
