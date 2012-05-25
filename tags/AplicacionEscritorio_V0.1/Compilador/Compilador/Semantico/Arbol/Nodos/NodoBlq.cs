﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoBlq: NodoArbolSemantico
    {
        public NodoBlq(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {

        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                //Si no tiene valor, es pq es un lambda
                if (this.hijosNodo[1].LlamaProcSalida.HasValue)
                {
                    this.LlamaProcSalida = this.hijosNodo[0].LlamaProcSalida.Value || this.hijosNodo[1].LlamaProcSalida.Value;

                    if ((this.hijosNodo[0].LlamaProcSalida.Value && this.hijosNodo[1].LlamaProcSalida.Value)
                        || this.hijosNodo[1].ProcSalidaLlamadoMasDeUnaVez)
                    {
                        this.ProcSalidaLlamadoMasDeUnaVez = true;
                    }

                  
                    this.ProcSalidaUltimaLinea = this.hijosNodo[1].ProcSalidaUltimaLinea;
                  
                }
                else
                {
                    this.LlamaProcSalida = this.hijosNodo[0].LlamaProcSalida.Value;
                    this.ProcSalidaLlamadoMasDeUnaVez = false;
                    this.ProcSalidaUltimaLinea = this.hijosNodo[0].LlamaProcSalida.Value;
                }

                this.TieneLecturas = this.hijosNodo[0].TieneLecturas || this.hijosNodo[1].TieneLecturas;
                this.LlamaProcs = this.hijosNodo[0].LlamaProcs || this.hijosNodo[1].LlamaProcs;
                this.ModificaParametros = this.hijosNodo[0].ModificaParametros || this.hijosNodo[1].ModificaParametros;
                this.AsignaParametros = this.hijosNodo[0].AsignaParametros || this.hijosNodo[1].AsignaParametros;
                this.UsaVariablesGlobales = this.hijosNodo[0].UsaVariablesGlobales || this.hijosNodo[1].UsaVariablesGlobales;
            }
            else
            {
                this.LlamaProcSalida = null;
            }

            return this;
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

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
                strBldr.Append(this.hijosNodo[1].Codigo);

                this.Codigo = strBldr.ToString();
            }
            else
            {
                this.Codigo = string.Empty;
            }
        }

    }
}