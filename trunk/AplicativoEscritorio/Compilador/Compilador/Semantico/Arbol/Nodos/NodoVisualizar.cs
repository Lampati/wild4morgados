using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoVisualizar : NodoArbolSemantico
    {
        public bool ConSaltoLinea { get; set; }

        public NodoVisualizar(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
          

            this.TextoParaImprimirArbol = this.ToString();

            this.ListaElementosVisualizar = this.hijosNodo[2].ListaElementosVisualizar;

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
            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder("\t");

            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("------COMIENZO VISUALIZAR-----"));

            strBldr.Append(this.hijosNodo[1].Codigo);

            foreach (string impresion in this.ListaElementosVisualizar)
            {
                if (this.TablaSimbolos.ExisteTemporal(impresion, NodoTablaSimbolos.TipoDeDato.String))
                {
                    string val = this.TablaSimbolos.ObtenerStringTemporal(impresion);
                    NodoTablaSimbolos.TipoDeDato tipo = this.TablaSimbolos.ObtenerTipoTemporal(impresion);

                    int cant = val.Replace("'","").Length;

                    strBldr.AppendFormat("PUSH OFFSET\t{0}",impresion).AppendLine();
                    strBldr.AppendFormat("PUSH\t{0}",cant.ToString()).AppendLine();
                    strBldr.Append(GeneracionCodigoHelpers.GenerarCall("writeSTR"));

                }
                else if (this.TablaSimbolos.ExisteTemporal(impresion, NodoTablaSimbolos.TipoDeDato.Numero)
                    || this.TablaSimbolos.ExisteTemporal(impresion, NodoTablaSimbolos.TipoDeDato.Numero))
                {
                    NodoTablaSimbolos.TipoDeDato tipo = this.TablaSimbolos.ObtenerTipoTemporal(impresion);

                    if (tipo == NodoTablaSimbolos.TipoDeDato.Numero)
                    {
                        strBldr.AppendFormat("PUSH\t0000h").AppendLine();
                    }
                    else if (tipo == NodoTablaSimbolos.TipoDeDato.Numero)
                    {
                        strBldr.AppendFormat("PUSH\t0001h").AppendLine();
                    }

                    strBldr.AppendFormat("PUSH\t{0}",impresion).AppendLine();
                    strBldr.Append(GeneracionCodigoHelpers.GenerarCall("writeNUM"));
                    
                }                
                else if (this.TablaSimbolos.ExisteVariable(impresion))
                {
                    string aux = this.TablaSimbolos.ObtenerNombreContextoVariable(impresion,this.ContextoActual,this.nombreContextoLocal);

                    NodoTablaSimbolos.TipoDeDato tipo = this.TablaSimbolos.ObtenerTipoVariable(impresion, this.ContextoActual, aux);

                    if (tipo == NodoTablaSimbolos.TipoDeDato.Numero)
                    {
                        strBldr.AppendFormat("PUSH\t0000h").AppendLine();
                    }
                    else if (tipo == NodoTablaSimbolos.TipoDeDato.Numero)
                    {
                        strBldr.AppendFormat("PUSH\t0001h").AppendLine();
                    }

                    strBldr.AppendFormat("PUSH\t0001h").AppendLine();
                    strBldr.AppendFormat("PUSH\t{0}{1}", this.nombreContextoLocal, impresion).AppendLine();
                    strBldr.Append(GeneracionCodigoHelpers.GenerarCall("writeNUM"));
                }
                else  //es un valor numerico
                {
                    int entero;
                    bool esNumero = int.TryParse(impresion, out entero);

                    if (esNumero)
                    {
                        if (entero >= 0)
                        {
                            strBldr.AppendFormat("PUSH\t0000h").AppendLine();
                        }
                        else
                        {
                            strBldr.AppendFormat("PUSH\t0001h").AppendLine();
                        }
                    }
                    else
                    {
                        NodoTablaSimbolos.TipoDeDato tipo = this.TablaSimbolos.ObtenerTipoVariableUsandoNombreEntero(impresion);

                        if (tipo == NodoTablaSimbolos.TipoDeDato.Numero)
                        {
                            strBldr.AppendFormat("PUSH\t0000h").AppendLine();
                        }
                        else if (tipo == NodoTablaSimbolos.TipoDeDato.Numero)
                        {
                            strBldr.AppendFormat("PUSH\t0001h").AppendLine();
                        }
                    }

                    strBldr.AppendFormat("PUSH\t{0}", impresion).AppendLine();
                    strBldr.Append(GeneracionCodigoHelpers.GenerarCall("writeNUM"));
                }

            }

            if (this.ConSaltoLinea)
            {
                strBldr.Append(GeneracionCodigoHelpers.GenerarCall("writeCRLF"));
            }

            strBldr.Append(GeneracionCodigoHelpers.GenerarComentario("--------FINAL VISUALIZAR--------"));

            this.Codigo = strBldr.ToString().Replace("\r\n", "\r\n\t").ToString().TrimEnd('\t');
        }
    }
}
