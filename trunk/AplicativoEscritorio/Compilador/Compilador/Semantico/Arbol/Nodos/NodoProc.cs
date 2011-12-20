using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.Arbol.Nodos.Auxiliares;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Auxiliares;
using Compilador.Semantico.Arbol.Temporales;
using Compilador.Semantico.Arbol.Labels;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoProc : NodoArbolSemantico
    {

        public NodoProc(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            bool esFuncion = (this.hijosNodo[0].Lexema.ToUpper() == "FUNCION");
            string nombre = this.hijosNodo[1].Lexema;
            this.Lexema = nombre;
            List<NodoTablaSimbolos.TipoDeDato> listaFirmas = new List<NodoTablaSimbolos.TipoDeDato>();
            NodoTablaSimbolos.TipoDeDato devolucion = NodoTablaSimbolos.TipoDeDato.Ninguno;

            if (esFuncion)
            {
                devolucion = this.hijosNodo[6].TipoDato;
            }

            foreach (Firma f in this.hijosNodo[3].ListaFirma)
            {
                listaFirmas.Add(f.Tipo);
            }

            if (!this.ProcPrincipalCrearUnaVez)
            {
                this.ProcPrincipalYaCreadoyCorrecto = false;
            }

            //if (!this.ProcSalidaCrearUnaVez)
            //{
            //    this.ProcSalidaYaCreadoyCorrecto = false;
            //}
            

            if (esFuncion)
            {

                if (!nombre.ToLower().Equals(Global.NOMBRE_PROC_PRINCIPAL.ToLower()))
                {
                    if (!nombre.ToLower().Equals(Global.NOMBRE_PROC_SALIDA.ToLower()))
                    {
                        if (!this.TablaSimbolos.ExisteFuncion(nombre))
                        {
                            if (devolucion == this.hijosNodo[11].TipoDato)
                            {

                                this.TablaSimbolos.AgregarFuncion(nombre, listaFirmas, devolucion, ManagerTemporales.Instance.CantidadTemporalesParaProc(nombre));
                                this.TextoParaImprimirArbol = nombre;

                                if (this.hijosNodo[9].LlamaProcSalida)
                                {
                                    throw new ErrorSemanticoException(new StringBuilder("Solo el procedimiento principal puede tener una llamada al procedimiento salida ").ToString());
                                }
                            }
                            else
                            {

                                throw new ErrorSemanticoException(new StringBuilder("La funcion ").Append(nombre).Append(" esta declarada como ").Append(EnumUtils.stringValueOf(devolucion)).Append(" pero la expresion que devuelve es ").Append(EnumUtils.stringValueOf(this.hijosNodo[12].TipoDato)).ToString());

                            }
                        }
                        else
                        {
                            throw new ErrorSemanticoException(new StringBuilder("Ya existe la funcion ").Append(nombre).ToString());
                        }
                    }
                    else
                    {
                        throw new ErrorSemanticoException(new StringBuilder("Salida no puede ser una funcion. Debe ser forzosamente un procedimiento").ToString());
                    }
                }
                else
                {
                    throw new ErrorSemanticoException(new StringBuilder("Principal no puede ser una funcion. Debe ser forzosamente un procedimiento").ToString());
                }
            }
            else
            {
                if (nombre.ToLower().Equals(Global.NOMBRE_PROC_PRINCIPAL.ToLower()))
                {
                    if (!this.ProcPrincipalYaCreadoyCorrecto && this.ProcPrincipalCrearUnaVez)
                    {
                        this.ProcPrincipalYaCreadoyCorrecto = true;
                        this.ProcPrincipalCrearUnaVez = false;
                    }

                    
                }

                if (nombre.ToLower().Equals(Global.NOMBRE_PROC_SALIDA.ToLower()))
                {
                    if (!this.ProcSalidaYaCreadoyCorrecto && this.ProcSalidaCrearUnaVez)
                    {
                        this.ProcSalidaYaCreadoyCorrecto = true;
                        this.ProcSalidaCrearUnaVez = false;
                    }
                }

                if (!this.TablaSimbolos.ExisteProcedimiento(nombre))
                {
                    List<ErrorSemanticoException> listaExcepciones = new List<ErrorSemanticoException>();

                    this.TablaSimbolos.AgregarProcedimiento(nombre, listaFirmas, ManagerTemporales.Instance.CantidadTemporalesParaProc(nombre));

                    //Chequeo de que no se llame a Salida donde no se debe, y que solo principal llame a salida
                    if (!this.hijosNodo[7].LlamaProcSalida && nombre.ToLower().Equals(Global.NOMBRE_PROC_PRINCIPAL.ToLower()))
                    {
                        listaExcepciones.Add( new ErrorSemanticoException(new StringBuilder("El procedimiento principal debe tener una llamada al procedimiento salida ").ToString()));
                    }
                    else
                    {
                        if (this.hijosNodo[7].LlamaProcSalida && !nombre.ToLower().Equals(Global.NOMBRE_PROC_PRINCIPAL.ToLower()))
                        {
                            listaExcepciones.Add( new ErrorSemanticoException(new StringBuilder("Solo el procedimiento principal puede tener una llamada al procedimiento salida ").ToString()));
                        }
                    }

                    if (nombre.ToLower().Equals(Global.NOMBRE_PROC_SALIDA.ToLower()))
                    {
                        

                        if (this.hijosNodo[7].ModificaParametros)
                        {
                            listaExcepciones.Add( new ErrorSemanticoException(new StringBuilder("El procedimiento salida no permite modificaciones en sus parametros").ToString()));
                        }

                        if (this.hijosNodo[7].AsignaParametros)
                        {
                            listaExcepciones.Add( new ErrorSemanticoException(new StringBuilder("El procedimiento salida no permite asignar sus parametros a otras variables").ToString()));
                        }

                        if (this.hijosNodo[7].TieneLecturas)
                        {
                            listaExcepciones.Add( new ErrorSemanticoException(new StringBuilder("El procedimiento salida no permite lecturas").ToString()));
                        }

                        if (this.hijosNodo[7].LlamaProcs)
                        {
                            listaExcepciones.Add( new ErrorSemanticoException(new StringBuilder("El procedimiento salida no permite llamadas a otros procedimientos").ToString()));
                        }

                    }

                    //Arrojo todas las excepciones juntas
                    if (listaExcepciones.Count > 1)
                    {
                        throw new AggregateException(listaExcepciones);

                    }

                    this.TextoParaImprimirArbol = nombre;
                }
                else
                {                   

                    throw new ErrorSemanticoException(new StringBuilder("Ya existe el procedimiento ").Append(nombre).ToString());
                }
            }

           

            //Saco todas las variables locales que creo este procedimiento de la tabla de simbolos
            //this.TablaSimbolos.EliminarVariablesContextoLocal(this.nombreContextoLocal);

            return this;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            hijoAHeredar.ProcPrincipalYaCreadoyCorrecto = this.ProcPrincipalYaCreadoyCorrecto;
            hijoAHeredar.ProcPrincipalCrearUnaVez = this.ProcPrincipalCrearUnaVez;
            hijoAHeredar.ProcSalidaYaCreadoyCorrecto = this.ProcSalidaYaCreadoyCorrecto;
            hijoAHeredar.ProcSalidaCrearUnaVez = this.ProcSalidaCrearUnaVez;
            hijoAHeredar.NombreContextoLocal = this.NombreContextoLocal;

            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            
            
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
            bool esFuncion = (this.hijosNodo[0].Lexema.ToUpper() == "FUNCION");

            StringBuilder strBldr = new StringBuilder();

            
            this.Codigo = strBldr.ToString();
        }
    }
}
