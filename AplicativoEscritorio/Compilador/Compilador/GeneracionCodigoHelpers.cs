using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.Arbol.Nodos;
using System.IO;

namespace CompiladorGargar
{

    class GeneracionCodigoHelpers
    {
        public static string DirectorioTemporales { get; set; }

        private static string variableContadoraLineas;
        public static string VariableContadoraLineas
        {
            get
            {
                return variableContadoraLineas;
            }
        }

        private static string archivoTemporalEstaEjecucion;
        public static string ArchivoTemporalEstaEjecucion
        {
            get
            {
                return archivoTemporalEstaEjecucion;
            }
        }

        private static string llamarCrearEntradaEnArchResultado;
        public static string LlamarCrearEntradaEnArchResultado
        {
            get
            {
                return llamarCrearEntradaEnArchResultado;
            }
        }

        private static string llamarCrearResSalidaEnArchResultado;
        public static string LlamarCrearResSalidaEnArchResultado
        {
            get
            {
                return llamarCrearResSalidaEnArchResultado;
            }
        }
        

        public static string DefinirFuncionesBasicas()
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine("function FrameworkProgramArProgramAr0000001EscribirBooleano( x : boolean) : string ;");
            strBldr.AppendLine("VAR ");
            strBldr.AppendLine("retorno : string; ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("if ( x ) then");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\tretorno := 'verdadero'; ");
            strBldr.AppendLine("end ");
            strBldr.AppendLine("else ");
            strBldr.AppendLine("begin ");
            strBldr.AppendLine("\tretorno := 'falso'; ");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine("\tFrameworkProgramArProgramAr0000001EscribirBooleano := retorno; ");
            strBldr.AppendLine("end; ");
            strBldr.AppendLine();

            strBldr.AppendLine(GeneracionCodigoHelpers.DefinirConversionYChequeoIndiceArreglo());


            return strBldr.ToString();
        }

        private static string DefinirConversionYChequeoIndiceArreglo()
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine("function FrameworkProgramArProgramAr0000001ConvertirAEnteroIndiceArreglo( num : real ; arregloAccedido : string ) : integer;");
            strBldr.AppendLine("begin");
            strBldr.AppendLine("if (  num > trunc(num) ) then");
            strBldr.AppendLine("begin");
            strBldr.AppendLine("raise EIndiceArregloInvalido.Create('El arreglo '+ arregloAccedido +' tenia un indice decimal. No se admiten arreglos con indices decimales.');");
            strBldr.AppendLine("end;");

            strBldr.AppendLine("FrameworkProgramArProgramAr0000001ConvertirAEnteroIndiceArreglo := trunc(num);");
            strBldr.AppendLine("end;");

            return strBldr.ToString();
        }

        public static string EscribirValorBooleano(string codigo)
        {
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append("FrameworkProgramArProgramAr0000001EscribirBooleano( ");
            strBldr.Append(codigo);
            strBldr.Append(") ");
            

            return strBldr.ToString();
        }

        public static string PausarHastaEntradaTeclado()
        {

            StringBuilder strBldr = new StringBuilder();

            strBldr.AppendLine(" readkey; ");

            return strBldr.ToString();
        }

        public static string AsignarLinea(int linea)
        {
            return string.Format("{0} := {1};",VariableContadoraLineas,linea);
        }


        internal static void ReiniciarValoresVariablesAleatorias()
        {
            variableContadoraLineas = Utilidades.RandomManager.RandomStringConPrefijo("ProgramAr_ContLineas",20,true);
            archivoTemporalEstaEjecucion = Path.Combine(DirectorioTemporales,string.Format("{0}.xml",Utilidades.RandomManager.RandomStringConPrefijo("ProgramAr_ArchResName", 20, true)));
        }

        internal static string InicializarVariablesGlobales(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos)
        {
            return InicializarVariables(tablaSimbolos, Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoContexto.Global, string.Empty);
        }

        internal static string InicializarVariablesProc(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos, string nombreProc)
        {
            return InicializarVariables(tablaSimbolos, Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoContexto.Local, nombreProc);
        }

        private static string InicializarVariables(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos, Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoContexto cont, string nombreProc)
        {
            StringBuilder strBldr = new StringBuilder();            

            List<CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos> listaVars = tablaSimbolos.ObtenerVariablesDeclaradasEnProcedimiento(cont, nombreProc);

            foreach (CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos item in listaVars)
            {
        
                switch (item.TipoDato)
                {
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Texto:
                        if (item.EsArreglo)
                        {
                            for (int i = 1; i <= item.Valor; i++)
                            {
                                strBldr.AppendLine(string.Format("{0}[{1}] := '';", item.NombreParaCodigo,i));
                            }
                        }
                        else
                        {
                            if (!item.EsConstante)
                            {
                                strBldr.AppendLine(string.Format("{0} := '';", item.NombreParaCodigo));
                            }
                        }
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Numero:
                        if (item.EsArreglo)
                        {
                            for (int i = 1; i <= item.Valor; i++)
                            {
                                strBldr.AppendLine(string.Format("{0}[{1}] := 0;", item.NombreParaCodigo,i));
                            }
                        }
                        else
                        {
                            if (!item.EsConstante)
                            {
                                strBldr.AppendLine(string.Format("{0} := 0;", item.NombreParaCodigo));
                            }
                        }
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Booleano:
                        if (item.EsArreglo)
                        {
                            for (int i = 1; i <= item.Valor; i++)
                            {
                                strBldr.AppendLine(string.Format("{0}[{1}] := true;", item.NombreParaCodigo,i));
                            }
                        }
                        else
                        {
                            if (!item.EsConstante)
                            {
                                strBldr.AppendLine(string.Format("{0} := true;", item.NombreParaCodigo));
                            }
                        }
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Ninguno:
                        break;
                    default:
                        break;
                }

            }

            return strBldr.ToString();
        }

        internal static string DefinirVariablesAuxiliares(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos)
        {
            StringBuilder strBldr = new StringBuilder();

            List<CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos> listaVars = tablaSimbolos.ObtenerAuxilairesParaCodIntermedio();

            foreach (CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos item in listaVars)
            {

                switch (item.TipoDato)
                {
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Texto:
                       
                        if (!item.EsConstante)
                        {
                            strBldr.AppendLine(string.Format("var {0} : string;", item.Nombre));
                        }
                        
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Numero:
                       
                        if (!item.EsConstante)
                        {
                            //strBldr.AppendLine(string.Format("var {0} : integer;", item.Nombre));
                            strBldr.AppendLine(string.Format("var {0} : real;", item.Nombre));
                        }
                        
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Booleano:
                      
                        
                        if (!item.EsConstante)
                        {
                            strBldr.AppendLine(string.Format("var {0} : boolean;", item.Nombre));
                        }
                        
                        break;
                    case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Ninguno:
                        break;
                    default:
                        break;
                }

            }

            return strBldr.ToString();
        }


        internal static string CrearArchivoDeResultados()
        {
            return string.Format("CrearArchivoResultados('{0}'); ",archivoTemporalEstaEjecucion);
        }

        internal static string CrearProcedimientoResultadoCorrectoEnArchivo()
        {
            return string.Format("ColocarResultadoCorrectoEnArch('{0}'); ", archivoTemporalEstaEjecucion);
        }

        internal static string CrearProcedimientoResultadoIncorrectoEnArchivo()
        {
            return string.Format("ColocarResultadoIncorrectoEnArch('{0}'); ", archivoTemporalEstaEjecucion);
        }

        internal static string CrearErrorEnArch(string tipoError, string descError)
        {
            return string.Format("AgregarErrorArchivoResultados('{0}','{1}','{2}',{3}); ", 
                archivoTemporalEstaEjecucion,tipoError,descError,variableContadoraLineas);
        }

        internal static string CrearErrorEnArchConVariable(string tipoError, string variable)
        {
            return string.Format("AgregarErrorArchivoResultados('{0}','{1}',{2},{3}); ",
                archivoTemporalEstaEjecucion, tipoError, variable, variableContadoraLineas);
        }
        
        internal static string ArmarProcedimientoMarcarEntradaEnArchivo(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos)
        {
            StringBuilder strBldrEncabezado = new StringBuilder();
            StringBuilder strBldrLlamado = new StringBuilder();
            StringBuilder strBldrTotal = new StringBuilder();

            string parametros = string.Empty;

            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerVariablesDelProcPrincipal())
            {
                if (!item.EsConstante)
                {
                    string tipo = ObtenerTipoParaParametroPascal(item.EsArreglo, item.TipoDato);

                    strBldrEncabezado.AppendFormat("{0} : {1};", item.Nombre, tipo);
                    strBldrLlamado.AppendFormat("{0},", item.NombreParaCodigo);
                }
            }

            llamarCrearEntradaEnArchResultado = string.Format("FrameworkProgramArProgramAr0000001InsertarEntradaEnArchResultados({0});",strBldrLlamado.ToString().TrimEnd(','));

            strBldrTotal.Append(" procedure FrameworkProgramArProgramAr0000001InsertarEntradaEnArchResultados( ").Append(strBldrEncabezado.ToString().TrimEnd(';')).Append(" ) ;").AppendLine();
            strBldrTotal.Append(" begin ").AppendLine();
            strBldrTotal.AppendFormat("  CrearNuevaEntradaParcial('{0}', {1}); ", archivoTemporalEstaEjecucion, variableContadoraLineas).AppendLine();

            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerVariablesDelProcPrincipal())
            {
                if (!item.EsConstante)
                {
                    if (item.EsArreglo)
                    {

                        strBldrTotal.AppendFormat("  CrearNuevoArregloEnEntradaEnLinea('{0}', {1},'{2}','{4}', {2},{3}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.Nombre, item.Valor, item.NombreContextoLocal).AppendLine();

                    }
                    else
                    {
                        strBldrTotal.AppendFormat("  CrearNuevaVariableEnEntradaEnLinea('{0}', {1},'{2}','{3}',{2}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.Nombre, item.NombreContextoLocal).AppendLine();
                    }
                }
            }

            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerVariablesGlobales())
            {
                if (!item.EsConstante)
                {
                    if (item.EsArreglo)
                    {

                        strBldrTotal.AppendFormat("  CrearNuevoArregloEnEntradaEnLinea('{0}', {1},'{2}','{5}',{3},{4}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.Nombre, item.NombreParaCodigo, item.Valor, item.Contexto.ToString()).AppendLine();

                    }
                    else
                    {
                        strBldrTotal.AppendFormat("  CrearNuevaVariableEnEntradaEnLinea('{0}', {1},'{2}','{4}',{3}); ",
                                    archivoTemporalEstaEjecucion, variableContadoraLineas, item.Nombre, item.NombreParaCodigo, item.Contexto.ToString()).AppendLine();
                    }
                }
            }

            strBldrTotal.Append(" end; ").AppendLine();


            return strBldrTotal.ToString();
        }

        internal static string ArmarLlamadaResFinalEnArchivo(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos)
        {
            StringBuilder strBldrLlamado = new StringBuilder();
            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerParametrosDelProcSalida())
            {
                if (!item.EsConstante)
                {
                    string tipo = ObtenerTipoParaParametroPascal(item.EsArreglo, item.TipoDato);

                    strBldrLlamado.AppendFormat("{0},", item.NombreParaCodigo);
                }
            }

            return string.Format("FrameworkProgramArProgramAr0000001InsertarResFinalEnArchResultados({0});", strBldrLlamado.ToString().TrimEnd(','));
        }

        internal static string ArmarProcedimientoResFinalEnArchivo(Semantico.TablaDeSimbolos.TablaSimbolos tablaSimbolos)
        {
            StringBuilder strBldrEncabezado = new StringBuilder();
            StringBuilder strBldrTotal = new StringBuilder();

            string parametros = string.Empty;

            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerParametrosDelProcSalida())
            {
                if (!item.EsConstante)
                {
                    string tipo = ObtenerTipoParaParametroPascal(item.EsArreglo, item.TipoDato);

                    strBldrEncabezado.AppendFormat("{0} : {1};", item.Nombre, tipo);
                }
            }

            strBldrTotal.Append(" procedure FrameworkProgramArProgramAr0000001InsertarResFinalEnArchResultados( ").Append(strBldrEncabezado.ToString().TrimEnd(';')).Append(" ) ;").AppendLine();
            strBldrTotal.Append(" begin ").AppendLine();

            foreach (Semantico.TablaDeSimbolos.NodoTablaSimbolos item in tablaSimbolos.ObtenerParametrosDelProcSalida())
            {
                if (!item.EsConstante)
                {
                    if (item.EsArreglo)
                    {

                        strBldrTotal.AppendFormat("  CrearNuevoArregloEnResultado('{0}', '{1}','{3}',{1},{2}); ",
                                    archivoTemporalEstaEjecucion, item.Nombre, item.Valor, item.NombreContextoLocal).AppendLine();

                    }
                    else
                    {
                        strBldrTotal.AppendFormat("  CrearNuevaVariableEnResultado('{0}', '{1}','{2}',{1}); ",
                                    archivoTemporalEstaEjecucion, item.Nombre, item.NombreContextoLocal).AppendLine();
                    }
                }
            }

            strBldrTotal.Append(" end; ").AppendLine();


            return strBldrTotal.ToString();
        }

        private static string ObtenerTipoParaParametroPascal(bool esArreglo, Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato tipoDeDato)
        {
            string retorno = string.Empty;
            switch (tipoDeDato)
            {
                case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Texto:
                    if (esArreglo)
                    {
                        retorno = "array of string";
                    }
                    else
                    {
                        retorno = "string";
                    }
                    break;
                case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Numero:
                    if (esArreglo)
                    {
                        //retorno = "array of integer";
                        retorno = "array of real";
                    }
                    else
                    {
                        retorno = "real";
                    }
                    break;
                case CompiladorGargar.Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoDeDato.Booleano:
                    if (esArreglo)
                    {
                        retorno = "array of boolean";
                    }
                    else
                    {
                        retorno = "boolean";
                    }
                    break;
                
            }

            return retorno;
        }



       
    }
}
