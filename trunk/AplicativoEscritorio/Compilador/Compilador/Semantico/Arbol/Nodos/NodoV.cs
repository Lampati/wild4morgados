using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using CompiladorGargar.Auxiliares;
using InterfazTextoGrafico;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoV : NodoArbolSemantico
    {
        public NodoV(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            List<Variable> variables = this.hijosNodo[0].VariablesACrear;
            NodoTablaSimbolos.TipoDeDato tipo = this.hijosNodo[2].TipoDato;
            this.RangoArregloSinPrefijo = this.hijosNodo[2].RangoArregloSinPrefijo;

            StringBuilder textoParaArbol = new StringBuilder();

            if (DeclaracionesPermitidas == TipoDeclaracionesPermitidas.Variables)
            {
                List<string> nombresVariables = new List<string>();

                if (variables.Count > 0)
                {

                    foreach (Variable v in variables)
                    {
                        if (!this.hijosNodo[2].EsArreglo)
                        //if (!v.EsArreglo)
                        {
                            if (!this.TablaSimbolos.ExisteVariableEnEsteContexto(v.Lexema, this.ContextoActual, this.NombreContextoLocal))
                            {
                                this.TablaSimbolos.AgregarVariable(v.Lexema, tipo, this.EsConstante, this.ContextoActual, this.NombreContextoLocal);
                                textoParaArbol.Append("Declaracion de variable ").Append(v.Lexema).Append(" ").Append(EnumUtils.stringValueOf(this.ContextoActual));
                                textoParaArbol.Append(" de tipo ").Append(EnumUtils.stringValueOf(tipo));

                                nombresVariables.Add(v.Lexema);
                            }
                            else
                            {
                                throw new ErrorSemanticoException(new StringBuilder("La variable ").Append(v.Lexema).Append(" ya existia en ese contexto").ToString());
                            }
                        }
                        else
                        {

                            if (!this.TablaSimbolos.ExisteArregloEnEsteContexto(v.Lexema, this.ContextoActual, this.NombreContextoLocal))
                            {


                                bool res = this.TablaSimbolos.AgregarArreglo(v.Lexema, tipo, this.ContextoActual, this.NombreContextoLocal, this.RangoArregloSinPrefijo, false);

                                if (!res)
                                {
                                    throw new ErrorSemanticoException(new StringBuilder("El tope de un arreglo no puede ser decimal").ToString());
                                }

                                nombresVariables.Add(v.Lexema);
                            }
                            else
                            {
                                throw new ErrorSemanticoException(new StringBuilder("El arreglo ").Append(v.Lexema).Append(" ya existia").ToString());
                            }
                        }
                    }

                    if (!this.hijosNodo[2].EsArreglo)
                    {
                        AgregarVariableViewModel(string.Join(",", nombresVariables.ToArray()), tipo);
                    }
                    else
                    {
                        AgregarArregloViewModel(string.Join(",", nombresVariables.ToArray()), tipo, this.RangoArregloSinPrefijo);
                    }
                }
            }
            else
            {
                throw new ErrorSemanticoException(new StringBuilder("No se permiten declarar variables aqui. Las variables deben ser creadas en el contexto global al principio del programa o en la zona de declaraciones de un procedimiento o funcion").ToString());
            }

            return this;
        }

        private void AgregarArregloViewModel(string nombre, NodoTablaSimbolos.TipoDeDato tipo, string tope)
        {
            DeclaracionArregloViewModel act = new DeclaracionArregloViewModel();

            act.Nombre = nombre;
            act.Tope = tope;

            switch (tipo)
            {
                case NodoTablaSimbolos.TipoDeDato.Texto:
                    act.Tipo = InterfazTextoGrafico.Enums.TipoDato.Texto;
                    break;
                case NodoTablaSimbolos.TipoDeDato.Numero:
                    act.Tipo = InterfazTextoGrafico.Enums.TipoDato.Numero;
                    break;
                case NodoTablaSimbolos.TipoDeDato.Booleano:
                    act.Tipo = InterfazTextoGrafico.Enums.TipoDato.Booleano;
                    break;
            }

            this.ActividadViewModel = act;
        }

        private void AgregarVariableViewModel(string nombre, NodoTablaSimbolos.TipoDeDato tipo)
        {
            DeclaracionVariableViewModel act = new DeclaracionVariableViewModel();

            act.Nombre = nombre;

            switch (tipo)
            {
                case NodoTablaSimbolos.TipoDeDato.Texto:
                    act.Tipo = InterfazTextoGrafico.Enums.TipoDato.Texto;
                    break;
                case NodoTablaSimbolos.TipoDeDato.Numero:
                    act.Tipo = InterfazTextoGrafico.Enums.TipoDato.Numero;
                    break;
                case NodoTablaSimbolos.TipoDeDato.Booleano:
                    act.Tipo = InterfazTextoGrafico.Enums.TipoDato.Booleano;
                    break;
            }

            this.ActividadViewModel = act;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();


            strBldr.Append(this.hijosNodo[0].Codigo);
            strBldr.Append(" ");
            strBldr.Append(":");
            strBldr.Append(" ");
            strBldr.Append(this.hijosNodo[2].Codigo);
            

            this.Codigo = strBldr.ToString();
        }
    }
}
