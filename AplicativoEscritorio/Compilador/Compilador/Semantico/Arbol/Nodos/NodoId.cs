using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Auxiliares;


namespace CompiladorGargar.Semantico.Arbol.Nodos
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
                    List < FirmaProc > firmaFuncion = this.TablaSimbolos.ObtenerFirma(nombre, NodoTablaSimbolos.TipoDeEntrada.Funcion);

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
                            this.ListaFirma = this.hijosNodo[1].ListaFirma;

                            this.EsFuncion = true;
                            this.TipoDato = this.TablaSimbolos.ObtenerTipoFuncion(nombre);
                            //this.Valor = 1;


                            this.Lexema = nombre;

                            if (this.TablaSimbolos.EsVariableGlobal(nombre, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.UsaVariablesGlobales = true;
                            }

                            // flanzani 9/1/2012
                            // Uso de variables globales
                            // Me fijo si se esta usando una variable global como parametro.

                            this.UsaVariablesGlobales = this.hijosNodo[1].UsaVariablesGlobales;
                            
                            
                        }
                        else
                        {
                            List<ErrorSemanticoException> listaExcepciones = new List<ErrorSemanticoException>();

                            strbldr = new StringBuilder();
                            if (firmaFuncion[i - 1].TipoDato != listaFirmaComparar[i - 1].Tipo)
                            {

                                strbldr = new StringBuilder("El parametro ").Append(firmaFuncion[i - 1].Lexema).Append(" pasado a la funcion ");
                                strbldr.Append(nombre).Append(" es de tipo incorrecto. Debe ser de tipo ").Append(EnumUtils.stringValueOf(firmaFuncion[i - 1].TipoDato));
                                listaExcepciones.Add(new ErrorSemanticoException(strbldr.ToString()));
                                
                            }

                             if (firmaFuncion[i - 1].EsArreglo != listaFirmaComparar[i - 1].EsArreglo)
                            {
                                if (firmaFuncion[i - 1].EsArreglo)
                                {
                                    strbldr = new StringBuilder("El parametro ").Append(firmaFuncion[i - 1].Lexema).Append(" pasado a la funcion ");
                                    strbldr.Append(nombre).Append(" debe ser un arreglo, y se paso una variable.");
                                    listaExcepciones.Add(new ErrorSemanticoException(strbldr.ToString()));
                                }
                                else
                                {
                                    strbldr = new StringBuilder("El parametro ").Append(firmaFuncion[i - 1].Lexema).Append(" pasado a la funcion ");
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
                

                if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    this.EsArreglo = true;
                    this.TipoDato = this.TablaSimbolos.ObtenerTipoArreglo(nombre, this.ContextoActual, this.NombreContextoLocal);
                    //this.Valor = this.TablaSimbolos.ObtenerValorPosicionArreglo(nombre, indice);

              

                    this.Lexema = nombre;
                    

                    if (this.TablaSimbolos.EsParametroDeEsteProc(nombre,this.ContextoActual,this.NombreContextoLocal))
                    {
                        this.AsignaParametros = true;
                    }

                    if (this.TablaSimbolos.EsVariableGlobal(nombre, this.ContextoActual, this.NombreContextoLocal))
                    {
                        this.UsaVariablesGlobales = true;
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
                if (this.TablaSimbolos.ExisteVariable(nombre, this.ContextoActual, this.NombreContextoLocal))
                {
                    this.TipoDato = this.TablaSimbolos.ObtenerTipoVariable(nombre,this.ContextoActual,this.NombreContextoLocal);
                    //this.Valor = this.TablaSimbolos.ObtenerValorVariable(nombre);          
                   
                    this.Lexema = nombre;
                        
                    if (this.TablaSimbolos.EsParametroDeEsteProc(nombre,this.ContextoActual,this.NombreContextoLocal))
                    {
                        this.AsignaParametros = true;
                    }

                    if (this.TablaSimbolos.EsVariableGlobal(nombre, this.ContextoActual, this.NombreContextoLocal))
                    {
                        this.UsaVariablesGlobales = true;
                    }
                }
                else
                {
                    // Pq puede ser que haya puesto el arreglo sin el subindice
                    if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.NombreContextoLocal))
                    {
                        if (this.EsPasajeParametrosAProcOFunc)
                        {
                            this.EsArregloEnParametro = true;
                            this.TipoDato = this.TablaSimbolos.ObtenerTipoArreglo(nombre, this.ContextoActual, this.NombreContextoLocal);
                            //this.Valor = this.TablaSimbolos.ObtenerValorPosicionArreglo(nombre, indice);
                            this.Lexema = nombre;

                            if (this.TablaSimbolos.EsParametroDeEsteProc(nombre, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.AsignaParametros = true;
                            }

                            if (this.TablaSimbolos.EsVariableGlobal(nombre, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.UsaVariablesGlobales = true;
                            }
                        }
                        else
                        {
                            strbldr = new StringBuilder("La variable ").Append(nombre).Append(" es un arreglo. Debe usar un indice para asignarle el contenido");
                            strbldr.Append(" a una de sus posiciones. No se puede asignar el contenido total de un arreglo a otro. ");
                            throw new ErrorSemanticoException(strbldr.ToString());
                        }
                    }
                    else
                    {
                        strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
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
