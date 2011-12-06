using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Sintactico.Gramatica;
using Compilador.Auxiliares;
using Compilador.Semantico.Arbol.Temporales;
using Compilador.Semantico.Arbol.Labels;
using System.IO;
using System.Configuration;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoStart : NodoArbolSemantico 
    {
        public NodoStart(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ContextoActual = NodoTablaSimbolos.TipoContexto.Global;
            this.nombreContextoLocal = EnumUtils.stringValueOf(NodoTablaSimbolos.TipoContexto.Global);
            this.ProcPrincipalYaCreadoyCorrecto = false;
            this.ProcPrincipalCrearUnaVez = true;               
        }

        public string MemoriaGlobal { get; set; }

        public override void ChequearAtributos(Terminal t)
        {
            ManagerTemporales inst = ManagerTemporales.Instance;
            if (!this.ProcPrincipalYaCreadoyCorrecto)
            {
                StringBuilder strbldr = new StringBuilder("Error en el procedimiento principal: Debe haber unicamente un procedimiento principal y debe ser el ultimo.");
                throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            if (!this.ProcPrincipalYaCreadoyCorrecto)
            {
                this.ProcPrincipalYaCreadoyCorrecto = true;
            }

            return this;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
        {
            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
        {
            this.TextoParaImprimirArbol = this.ToString();
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();
            strBldr.AppendLine("SEG Segment");
            strBldr.AppendLine("ASSUME CS:SEG,SS:SEG,DS:SEG,ES:SEG");
            strBldr.AppendLine("ORG 0100h");
            strBldr.AppendLine("JMP labelInicial");
            strBldr.Append(this.MemoriaGlobal);            

            //Pongo los errores
            strBldr.Append(GeneracionCodigoHelpers.GenerarError("labelErrorNaturalMenorCero", "ErrorNaturalMenorCero", ArbolSemantico.ERROR_NATURAL_MENOR_CERO.Length - 2));
            strBldr.Append(GeneracionCodigoHelpers.GenerarError("labelArregloFueraLimites", "ArregloFueraLimites", ArbolSemantico.ERROR_ARREGLO_FUERA_LIMITES.Length - 2));
            strBldr.Append(GeneracionCodigoHelpers.GenerarError("labelDivisionPorCero", "DivisionPorCero", ArbolSemantico.ERROR_DIVISION_POR_CERO.Length - 2));
            
            

            //Cargo las rutinas de entrada salida
            strBldr.Append(File.ReadAllText(ConfigurationSettings.AppSettings["archRutinasEentradaSalida"].ToString()));
            strBldr.Append(this.hijosNodo[1].Codigo);            
            
            strBldr.AppendLine("SEG ENDS");
            
            this.Codigo = strBldr.ToString();
        }
    }
}
