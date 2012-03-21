using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Auxiliares;


using System.IO;
using System.Configuration;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoStart : NodoArbolSemantico 
    {
        public NodoStart(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ContextoActual = NodoTablaSimbolos.TipoContexto.Global;
            this.NombreContextoLocal = EnumUtils.stringValueOf(NodoTablaSimbolos.TipoContexto.Global);
            this.ProcPrincipalYaCreadoyCorrecto = false;
            this.ProcPrincipalCrearUnaVez = true;

            this.ProcSalidaYaCreadoyCorrecto = false;
            this.ProcSalidaCrearUnaVez = true;
        }

        public string MemoriaGlobal { get; set; }

        public override void ChequearAtributos(Terminal t)
        {
            
            if (!this.ProcPrincipalYaCreadoyCorrecto)
            {
                StringBuilder strbldr = new StringBuilder("Error en el procedimiento principal: Debe haber unicamente un procedimiento principal y debe ser el ultimo.");
                throw new ErrorSemanticoException(strbldr.ToString());
            }

            if (!this.ProcSalidaYaCreadoyCorrecto)
            {
                StringBuilder strbldr = new StringBuilder("Error en el procedimiento salida: Debe haber unicamente un procedimiento salida");
                throw new ErrorSemanticoException(strbldr.ToString());
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            if (!this.ProcPrincipalYaCreadoyCorrecto)
            {
                this.ProcPrincipalYaCreadoyCorrecto = true;
            }

            if (!this.ProcSalidaYaCreadoyCorrecto)
            {
                this.ProcSalidaYaCreadoyCorrecto = true;
            }

            return this;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
        {
            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
        {
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine("program temporal;");
            strBldr.AppendLine("uses crt, Sysutils, ArchResultadoManager;");
            strBldr.AppendLine("");

            strBldr.AppendLine("Const");
            strBldr.AppendLine(this.hijosNodo[0].ConstantesGlobales);
            strBldr.AppendLine("Type");
            strBldr.AppendLine(ArmarTiposDeArreglo(this.TablaSimbolos.ListaTiposArreglos));
            strBldr.AppendLine("Var");
            strBldr.AppendLine(string.Format("{0} : integer;",GeneracionCodigoHelpers.VariableContadoraLineas));
            strBldr.AppendLine(this.hijosNodo[0].VariablesGlobales);
            //strBldr.AppendLine(this.hijosNodo[1].VariablesProcPrincipal);
            strBldr.AppendLine("");
            strBldr.AppendLine(GeneracionCodigoHelpers.DefinirFuncionesBasicas());
            strBldr.AppendLine(this.hijosNodo[1].Codigo);

            strBldr.AppendLine("begin");
            strBldr.Append(GeneracionCodigoHelpers.InicializarVariablesGlobales(this.TablaSimbolos));
            strBldr.AppendLine("try");       
            strBldr.Append("\t").AppendLine(string.Format("{0}PRINCIPAL();",GlobalesCompilador.PREFIJO_VARIABLES));            
            strBldr.AppendLine("except");
            strBldr.AppendLine("on E: SysUtils.EDivByZero do");
            strBldr.AppendLine(string.Format("WriteLn('Error Fatal: Se intento dividir por cero en la linea ',{0});",GeneracionCodigoHelpers.VariableContadoraLineas));
            strBldr.AppendLine("on ETotal: Exception do");
            strBldr.AppendLine("end;");
            strBldr.AppendLine(GeneracionCodigoHelpers.PausarHastaEntradaTeclado());
            strBldr.AppendLine("end.");
            

            
            
            this.Codigo = strBldr.ToString();
        }

        private string ArmarTiposDeArreglo(List<NodoTipoArreglo> list)
        {
            StringBuilder strBlder = new StringBuilder();
            foreach (NodoTipoArreglo item in list)
            {
                strBlder.Append(item.Nombre).Append(" = ").Append("Array [1..").Append(item.Rango).Append("] of ");

                string tipo;
                switch (item.TipoDato)
                {
                    case NodoTablaSimbolos.TipoDeDato.String:
                        tipo = "string";
                        break;
                    case NodoTablaSimbolos.TipoDeDato.Numero:
                        tipo = "integer";
                        break;
                    case NodoTablaSimbolos.TipoDeDato.Booleano:
                        tipo = "boolean";
                        break;
                    case NodoTablaSimbolos.TipoDeDato.Ninguno:               
                    default:
                        tipo = string.Empty;
                        break;
                }

                strBlder.Append(tipo).AppendLine(";");
                
            }

            return strBlder.ToString();
        }
    }
}
