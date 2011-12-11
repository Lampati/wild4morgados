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

            if (this.hijosNodo.Count > 2)
            {                
                this.TipoDato = this.hijosNodo[0].TipoDato;
                this.Valor = this.hijosNodo[2].Valor;               

                //this.Temporal = this.hijosNodo[2].Temporal;

                //Pq estoy cambiando de entero a natural, o al reves, deberia guardar el resultado en un temporal acorde
                if (this.hijosNodo[0].TipoDato == this.hijosNodo[2].TipoDato)
                {
                    this.Lugar = this.hijosNodo[2].Lugar;
                }
                else
                {
                    
                    this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString(), this.Lexema);
                    this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, this.hijosNodo[0].TipoDato);

                    this.Lugar = this.Temporal.Nombre;
                    
                }
            }
            else
            {
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
                                this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
                                this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, NodoTablaSimbolos.TipoDeDato.Numero);

                                this.Lugar = this.Temporal.Nombre;
                            }
                            else
                            {
                                strbldr = new StringBuilder("El parametro ").Append(listaFirmaComparar[i - 1].Lexema).Append(" pasado a la funcion ");
                                strbldr.Append(nombre).Append(" es de tipo incorrecto.");
                                throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                            }
                            
                        }
                        else
                        {
                            strbldr = new StringBuilder("La cantidad de parametros para la funcion ").Append(nombre).Append(" es incorrecta.");
                            throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                        }
                    }
                    else
                    {
                        strbldr = new StringBuilder("La funcion ").Append(nombre).Append(" no esta declarada.");
                        throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                    }

                }
                else if (esArreglo)
                {
                    int indice = this.hijosNodo[1].IndiceArreglo;

                    if (this.TablaSimbolos.ExisteArreglo(nombre, this.ContextoActual, this.nombreContextoLocal))
                    {
                        this.EsArreglo = true;
                        this.TipoDato = this.TablaSimbolos.ObtenerTipoArreglo(nombre);
                        //this.Valor = this.TablaSimbolos.ObtenerValorPosicionArreglo(nombre, indice);

                        strbldr = new StringBuilder("Uso de arreglo Global ");
                        strbldr.Append(nombre).Append(" en parte derecha");
                        this.TextoParaImprimirArbol = strbldr.ToString();

                        this.Lexema = nombre;
                        this.Temporal = ManagerTemporales.Instance.CrearNuevoTemporal(this.nombreContextoLocal, this.ToString());
                        this.TablaSimbolos.AgregarTemporal(this.Temporal.Nombre, NodoTablaSimbolos.TipoDeDato.Numero);

                        this.Lugar = this.Temporal.Nombre;
                    }
                    else
                    {
                        strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");
                        throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                    }
                }
                else
                {
                    if (this.TablaSimbolos.ExisteVariable(nombre))
                    {
                        this.TipoDato = this.TablaSimbolos.ObtenerTipoVariable(nombre,this.ContextoActual,this.nombreContextoLocal);
                        //this.Valor = this.TablaSimbolos.ObtenerValorVariable(nombre);          
                        strbldr = new StringBuilder("Uso de variable ");
                        strbldr.Append(EnumUtils.stringValueOf(this.TablaSimbolos.ObtenerContextoVariable(nombre, this.ContextoActual, this.nombreContextoLocal)));
                        strbldr.Append(" ").Append(nombre).Append(" en parte derecha");
                        this.TextoParaImprimirArbol = strbldr.ToString();

                        this.Lexema = nombre;
                        this.Temporal = null;

                        string nombreContexto = this.TablaSimbolos.ObtenerNombreContextoVariable(this.Lexema, this.ContextoActual, this.nombreContextoLocal);

                        this.Lugar = new StringBuilder(nombreContexto).Append(this.Lexema).ToString();
                        
                    }
                    else
                    {
                        strbldr = new StringBuilder("La variable ").Append(nombre).Append(" no esta declarada.");
                        throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
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
            this.Valor = 1;

            return this;
        }


        public override void CalcularCodigo()
        {

            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 2)
            {
                if (this.hijosNodo[0].Lexema.ToUpper() == "ANATURAL")
                {
                    strBldr.Append(this.hijosNodo[2].Codigo);

                    strBldr.Append(GeneracionCodigoHelpers.GenerarPush("Ax"));

                    strBldr.Append(GeneracionCodigoHelpers.GenerarMovHaciaAx(this.hijosNodo[2].Lugar));
                    strBldr.AppendLine(String.Format("ADD\t{0}, {1}", "AX", "0"));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarJump("labelErrorNaturalMenorCero", TipoComparacion.LessThanZero));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMovDesdeAx(this.Lugar));

                    strBldr.Append(GeneracionCodigoHelpers.GenerarPop("Ax"));
                }
                else
                {
                    strBldr.Append(this.hijosNodo[2].Codigo);
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMovHaciaAx(this.hijosNodo[2].Lugar));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMovDesdeAx(this.Lugar));
                }
            }
            else
            {
                if (this.EsFuncion)
                {
                    strBldr.Append(this.hijosNodo[1].Codigo);

                    strBldr.Append(GeneracionCodigoHelpers.GenerarPush("Ax"));
                    List<string> nombresParametros = this.TablaSimbolos.ObtenerParametros(this.Lexema);


                    for (int i = 0; i < this.ListaFirma.Count; i++)
                    {
                        string nombreContexto = this.TablaSimbolos.ObtenerNombreContextoVariable(nombresParametros[i], this.ContextoActual, this.nombreContextoLocal);

                        if (this.ListaFirma[i].Lexema == null || this.ListaFirma[i].Lexema.Equals(string.Empty))
                        {
                            strBldr.Append(GeneracionCodigoHelpers.GenerarMovHaciaAx(this.ListaFirma[i].Valor.ToString()));
                            strBldr.Append(GeneracionCodigoHelpers.GenerarMovDesdeAx(nombreContexto + nombresParametros[i]));
                            //strbldr.Append(GeneracionCodigoHelpers.GenerarMov(nombresParametros[i], this.ListaFirma[i].Valor.ToString()));
                        }
                        else
                        {
                            //strbldr.Append(GeneracionCodigoHelpers.GenerarMov(nombresParametros[i], this.ListaFirma[i].Lexema));
                            strBldr.Append(GeneracionCodigoHelpers.GenerarMovHaciaAx(this.ListaFirma[i].Lexema));
                            strBldr.Append(GeneracionCodigoHelpers.GenerarMovDesdeAx(nombreContexto + nombresParametros[i]));
                        }
                    }
                    

                    strBldr.Append(GeneracionCodigoHelpers.GenerarCall(this.Lexema));

                    strBldr.Append(GeneracionCodigoHelpers.GenerarMovDesdeAx(this.Lugar));

                    strBldr.Append(GeneracionCodigoHelpers.GenerarPop("Ax"));

                }
                else if (this.EsArreglo)
                {
                    strBldr.Append(this.hijosNodo[1].ObtenerHijo(1).Codigo);

                    strBldr.Append(GeneracionCodigoHelpers.GenerarPush("Ax"));  

                    //string nombreContexto = this.TablaSimbolos.ObtenerNombreContextoVariable(this.Lexema, this.ContextoActual, this.nombreContextoLocal);

                    string nombreArreglo = new StringBuilder("Global").Append(this.Lexema).ToString();

                    string posicion = this.hijosNodo[1].ObtenerHijo(1).Lugar;

                    int tamanio = this.TablaSimbolos.ObtenerTamanioArreglo(nombreArreglo);
                                        
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMov("AX", posicion));

                    /*Control OutOfBound*/
                    strBldr.Append(GeneracionCodigoHelpers.GenerarCmp("AX", tamanio.ToString()));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarJump("labelArregloFueraLimites", TipoComparacion.AboveOrEquals));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarCmp("AX", "0"));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarJump("labelArregloFueraLimites", TipoComparacion.Below));

                    strBldr.Append(GeneracionCodigoHelpers.GenerarMov("BX", "2"));
                    strBldr.AppendLine(String.Format("MUL\t{0}", "BX"));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMov("DI", "AX"));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMov("AX", String.Format("{0}[{1}]", nombreArreglo, "DI")));
                    strBldr.Append(GeneracionCodigoHelpers.GenerarMov(this.Lugar, "AX"));

                    strBldr.Append(GeneracionCodigoHelpers.GenerarPop("Ax"));
                }
                else
                {

                }
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
