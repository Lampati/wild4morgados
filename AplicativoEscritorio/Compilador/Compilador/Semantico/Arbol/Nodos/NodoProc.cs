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
            

            if (esFuncion)
            {
                if (!this.TablaSimbolos.ExisteFuncion(nombre))
                {
                    if (devolucion == this.hijosNodo[12].TipoDato)
                    {

                        this.TablaSimbolos.AgregarFuncion(nombre, listaFirmas, devolucion, ManagerTemporales.Instance.CantidadTemporalesParaProc(nombre));
                        this.TextoParaImprimirArbol = nombre;
                    }
                    else
                    {

                        throw new ErrorSemanticoException(new StringBuilder("La funcion ").Append(nombre).Append(" esta declarada como ").Append(EnumUtils.stringValueOf(devolucion)).Append(" pero la expresion que devuelve es ").Append(EnumUtils.stringValueOf(this.hijosNodo[12].TipoDato)).ToString(),
                        t.Componente.Fila, t.Componente.Columna);
                        
                    }
                }
                else
                {
                    throw new ErrorSemanticoException(new StringBuilder("Ya existe la funcion ").Append(nombre).ToString(),
                        t.Componente.Fila, t.Componente.Columna);
                }
            }
            else
            {


                if (nombre.ToLower().Equals("principal"))
                {


                    if (!this.ProcPrincipalYaCreadoyCorrecto && this.ProcPrincipalCrearUnaVez)
                    {
                        this.ProcPrincipalYaCreadoyCorrecto = true;
                        this.ProcPrincipalCrearUnaVez = false;
                    }
                }

                if (!this.TablaSimbolos.ExisteProcedimiento(nombre))
                {
                    this.TablaSimbolos.AgregarProcedimiento(nombre, listaFirmas, ManagerTemporales.Instance.CantidadTemporalesParaProc(nombre));

                    this.TextoParaImprimirArbol = nombre;
                }
                else
                {                   

                    throw new ErrorSemanticoException(new StringBuilder("Ya existe el procedimiento ").Append(nombre).ToString(),
                    t.Componente.Fila, t.Componente.Columna);
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

            hijoAHeredar.nombreContextoLocal = this.nombreContextoLocal;

            
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

            //Para debuguear nomas... SACAR
            //strBldr.AppendLine(this.Lexema);


            if (esFuncion)
            {
                strBldr.Append(this.Lexema).Append(" ").AppendLine("PROC NEAR");
                strBldr.Append(this.hijosNodo[10].Codigo);
                strBldr.Append(this.hijosNodo[12].Codigo);
                strBldr.Append("\t").Append(GeneracionCodigoHelpers.GenerarMovHaciaAx(this.hijosNodo[12].Lugar));
                strBldr.AppendLine("\tRET");
                strBldr.Append(this.Lexema).Append(" ").AppendLine("ENDP");
            }
            else
            {
                if (this.Lexema.ToUpper() == "PRINCIPAL")
                {
                    strBldr.Append(GeneracionCodigoHelpers.GenerarLabel("labelInicial"));
                }
                strBldr.Append(this.Lexema).Append(" ").AppendLine("PROC NEAR");
                strBldr.Append(this.hijosNodo[8].Codigo);

                if (this.Lexema.ToUpper() != "PRINCIPAL")
                {
                    strBldr.AppendLine("\tRET");
                }
                else
                {
                    strBldr.AppendLine("\tINT\t21h");
                }
                strBldr.Append(this.Lexema).Append(" ").AppendLine("ENDP");
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
