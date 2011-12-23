using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Semantico.Arbol.Nodos.Auxiliares;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Sintactico.Gramatica;
using Compilador.Auxiliares;
using Compilador.Semantico.Arbol.Temporales;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoId: NodoArbolSemantico
    {
        public NodoId(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {            
            StringBuilder strbldr;           
            
            bool esArreglo = this.hijosNodo[1].EsArreglo;
            bool esFuncion = this.hijosNodo[1].EsFuncion;

            string nombre = this.hijosNodo[0].Lexema;

            if (esFuncion)
            {
                List<Firma> listaFirmaComparar = this.hijosNodo[1].ListaFirma;

                if (this.TablaSimbolos.ExisteFuncion(nombre))
                {
                    List < NodoTablaSimbolos.TipoDeDato > firmaFuncion = this.TablaSimbolos.ObtenerFirma(nombre, NodoTablaSimbolos.TipoDeEntrada.Funcion);

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
                            this.ListaFirma = this.hijosNodo[1].ListaFirma;

                            this.EsFuncion = true;
                            this.TipoDato = this.TablaSimbolos.ObtenerTipoFuncion(nombre);
                            //this.Valor = 1;

                            strbldr = new StringBuilder("Uso de funcion ").Append(nombre).Append(" en parte derecha");
                            this.TextoParaImprimirArbol = strbldr.ToString();

                            this.Lexema = nombre;
                            this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.NombreContextoLocal, this.ToString());
                            this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, NodoTablaSimbolos.TipoDeDato.Numero);

                            this.Lugar = this.Temporal.Nombre;
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
                    strbldr = new StringBuilder("La funcion ").Append(nombre).Append(" no esta declarada.");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }

            }
            else if (esArreglo)
            {
                int indice = this.hijosNodo[1].IndiceArreglo;

                if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    this.EsArreglo = true;
                    this.TipoDato = this.TablaSimbolos.ObtenerTipoArreglo(nombre);
                    //this.Valor = this.TablaSimbolos.ObtenerValorPosicionArreglo(nombre, indice);

                    strbldr = new StringBuilder("Uso de arreglo Global ");
                    strbldr.Append(nombre).Append(" en parte derecha");
                    this.TextoParaImprimirArbol = strbldr.ToString();

                    this.Lexema = nombre;
                    this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.NombreContextoLocal, this.ToString());
                    this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, NodoTablaSimbolos.TipoDeDato.Numero);

                    this.Lugar = this.Temporal.Nombre;

                    if (this.TablaSimbolos.EsParametroDeEsteProc(nombre,this.ContextoActual,this.NombreContextoLocal))
                    {
                        this.AsignaParametros = true;
                    }
                }
                else
                {
                    strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }
            }
            else
            {
                if (this.TablaSimbolos.ExisteVariable(nombre))
                {
                    this.TipoDato = this.TablaSimbolos.ObtenerTipoVariable(nombre,this.ContextoActual,this.NombreContextoLocal);
                    //this.Valor = this.TablaSimbolos.ObtenerValorVariable(nombre);          
                    strbldr = new StringBuilder("Uso de variable ");
                    strbldr.Append(EnumUtils.stringValueOf(this.TablaSimbolos.ObtenerContextoVariable(nombre, this.ContextoActual, this.NombreContextoLocal)));
                    strbldr.Append(" ").Append(nombre).Append(" en parte derecha");
                    this.TextoParaImprimirArbol = strbldr.ToString();

                    this.Lexema = nombre;
                    this.Temporal = null;

                    string nombreContexto = this.TablaSimbolos.ObtenerNombreContextoVariable(this.Lexema, this.ContextoActual, this.NombreContextoLocal);

                    this.Lugar = new StringBuilder(nombreContexto).Append(this.Lexema).ToString();
                        
                    if (this.TablaSimbolos.EsParametroDeEsteProc(nombre,this.ContextoActual,this.NombreContextoLocal))
                    {
                        this.AsignaParametros = true;
                    }
                }
                else
                {
                    strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }
            }
            


            return this;
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            
            
        }

        public override void ChequearAtributos(Terminal t)
        {

        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {            
            //Por defecto coloco entero y un 1 para no alterar demasiado
            this.TipoDato = NodoTablaSimbolos.TipoDeDato.Numero;
            this.Valor = 1;

            return this;
        }


        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();
            strBldr.Append(this.hijosNodo[0].Lexema);
            strBldr.Append(this.hijosNodo[1].Codigo);
            this.Codigo = strBldr.ToString();
        }
    }
}
