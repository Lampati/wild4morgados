using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Semantico.Arbol.Nodos;
using Compilador.Sintactico.Gramatica;
using System.Windows.Forms;
using Compilador.Semantico.RecorredorArbol;
using Compilador.Semantico.TablaDeSimbolos;

namespace Compilador.Semantico.Arbol
{
    class ArbolSemantico
    {

        public const string ERROR_NATURAL_MENOR_CERO = "'Se intento convertir un entero menor a 0 en natural. El programa abortara.'";
        public const string ERROR_ARREGLO_FUERA_LIMITES = "'Se intento acceder a un indice del arreglo fuera del intervalo definido. El programa abortara.'";
        public const string ERROR_DIVISION_POR_CERO = "'Se intento dividir por cero. El programa abortara.'";
        
        

        public event Compilador.ErrorCompiladorDelegate errorCompilacion;


        private NodoArbolSemantico nodoRaiz;
        private NodoArbolSemantico nodoActual;

        public ArbolSemantico(NoTerminal nt)
        {
            nodoRaiz = new NodoStart(null, nt);
            nodoRaiz.CrearTablaSimbolos();
            nodoActual = nodoRaiz;
        }

        public void AgregarHijosNodoActual(Produccion prod)
        {
            this.nodoActual.AgregarHijos(prod);
            this.nodoActual = this.nodoActual.ObtenerPrimerHijo(); 
        }



        public void CalcularAtributos(Terminal t)
        {
            bool continuar = true;
            while (this.nodoActual != null && continuar)
            {
                if (this.nodoActual.CalculadoAtributosHijos)
                {
                    NodoArbolSemantico nodo = this.nodoActual;
                    try
                    {
                        nodo = this.nodoActual.CalcularAtributos(t);                    
                        nodo.ChequearAtributos(t);
                    }
                    catch(ErrorSemanticoException ex)
                    {
                        this.MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, false));
                        nodo = nodo.SalvarAtributosParaContinuar();
                    }

                    if (this.nodoActual != this.nodoRaiz)
                    {
                        this.nodoActual = nodo.PadreNodo;
                        this.nodoActual.ActualizarAtributos(nodo);
                        this.nodoActual = this.nodoActual.ProximoNodoACalcular();
                    }
                    else
                    {
                        continuar = false;
                    }
                }
                else
                {
                    continuar = false;
                }

            }

            
        }

        internal void CalcularMemoriaGlobal()
        {
            StringBuilder strBldr = new StringBuilder();

            //errores
            strBldr.Append("ErrorNaturalMenorCero").Append(" ");
            strBldr.Append("dw ").Append(ERROR_NATURAL_MENOR_CERO).AppendLine();
            strBldr.Append("ArregloFueraLimites").Append(" ");
            strBldr.Append("dw ").Append(ERROR_ARREGLO_FUERA_LIMITES).AppendLine();
            strBldr.Append("DivisionPorCero").Append(" ");
            strBldr.Append("dw ").Append(ERROR_DIVISION_POR_CERO).AppendLine();


            

            strBldr.Append("divisorAuxiliar").Append(" ");
            strBldr.Append("dw ?").AppendLine();

            foreach (NodoTablaSimbolos nodo in this.nodoRaiz.TablaSimbolos.ListaNodos)
            {
                if (nodo.TipoEntrada != NodoTablaSimbolos.TipoDeEntrada.Funcion && nodo.TipoEntrada != NodoTablaSimbolos.TipoDeEntrada.Procedimiento)
                {
                    switch (nodo.TipoDato)
                    {
                        case NodoTablaSimbolos.TipoDeDato.Natural:
                        case NodoTablaSimbolos.TipoDeDato.Entero:
                            if (nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Temporal)
                            {
                                strBldr.Append(nodo.Nombre).Append(" ");
                                strBldr.Append("dw").Append(" ");
                                strBldr.Append((nodo.Valor != int.MinValue) ? nodo.Valor.ToString() : "?").AppendLine();
                            }
                            else
                            {
                                strBldr.Append(nodo.NombreContextoLocal).Append(nodo.Nombre).Append(" ");
                                //strBldr.Append(nodo.Tamanio / 2).Append(" ");
                                strBldr.Append("dw ");

                                if (nodo.Valor == int.MinValue)
                                {
                                    strBldr.Append("?");
                                    for (int i = 2; i < nodo.Tamanio / 2; i++)
                                    {
                                        strBldr.Append(", ?");
                                    }
                                    strBldr.AppendLine();
                                }
                                else
                                {
                                    strBldr.Append(nodo.Valor.ToString()).AppendLine();
                                }
                            }
                            break;

                        case NodoTablaSimbolos.TipoDeDato.String:

                            strBldr.Append(nodo.Nombre).Append(" ");
                            strBldr.Append("dw").Append(" ");
                            strBldr.Append("'").Append(nodo.ValorString).Append("'").AppendLine();

                            break;
                    }
                }
            }

            ((NodoStart)nodoRaiz).MemoriaGlobal = strBldr.ToString();
           
        }


        public void CalcularExpresiones()
        {
            PilaRecorredor pilaRecorredor = new PilaRecorredor();
            pilaRecorredor.InsertarElemento(new NodoPilaRecorredor(this.nodoRaiz));

            NodoArbolSemantico nodoActual = this.nodoRaiz;

            while (!pilaRecorredor.esVacia())
            {
                NodoPilaRecorredor nodoRecorredorActual = pilaRecorredor.ObtenerTope();

                if (nodoRecorredorActual.Actual < nodoRecorredorActual.Nodo.ObtenerCantidadHijos())
                {
                    NodoPilaRecorredor p = new NodoPilaRecorredor(nodoRecorredorActual.Nodo.ObtenerHijo(nodoRecorredorActual.Actual));

                    p.Nodo.CalcularExpresiones();

                    pilaRecorredor.InsertarElemento(p);                   
                    
                    nodoRecorredorActual.Actual++;

                }
                else
                {                   
                    if (!pilaRecorredor.esVacia())
                    {
                        pilaRecorredor.DescartarTope();
                    }
                }
            }
        }
      

        public string CalcularCodigo()
        {
            PilaRecorredor pilaRecorredor = new PilaRecorredor();
            pilaRecorredor.InsertarElemento(new NodoPilaRecorredor(this.nodoRaiz));

            NodoArbolSemantico nodoActual = this.nodoRaiz;

            while (!pilaRecorredor.esVacia())
            {

                NodoPilaRecorredor nodoRecorredorActual = pilaRecorredor.ObtenerTope();

                if (nodoRecorredorActual.Actual < nodoRecorredorActual.Nodo.ObtenerCantidadHijos())
                {

                    pilaRecorredor.InsertarElemento(new NodoPilaRecorredor(nodoRecorredorActual.Nodo.ObtenerHijo(nodoRecorredorActual.Actual)));
                    nodoRecorredorActual.Actual++;

                }
                else
                {
                    nodoRecorredorActual.Nodo.CalcularCodigo();

                    if (!pilaRecorredor.esVacia())
                    {
                        pilaRecorredor.DescartarTope();
                    }
                }

            }

            return this.nodoRaiz.Codigo;
        }



        internal TreeNode DibujarArbol()
        {

            PilaRecorredor pilaRecorredor = new PilaRecorredor();
            pilaRecorredor.InsertarElemento(new NodoPilaRecorredor(this.nodoRaiz));

            TreeNode treeNode = new TreeNode(this.nodoRaiz.ToString());
            ColaIndices colaIndices = new ColaIndices();
            

            NodoArbolSemantico nodoActual = this.nodoRaiz;

            TreeNode treeNodeActual = treeNode;

            while (!pilaRecorredor.esVacia())
            {
                treeNodeActual = treeNode;
                NodoPilaRecorredor nodoRecorredorActual = pilaRecorredor.ObtenerTope();

                if (nodoRecorredorActual.Actual < nodoRecorredorActual.Nodo.ObtenerCantidadHijos())
                {                                      
                    int x = 0;

                    while (x < colaIndices.Count)
                    {
                        int indice = colaIndices.ObtenerIndice(x);

                        if (indice >= 0)
                        {
                            treeNodeActual = treeNodeActual.Nodes[indice];
                        }
                        x++;
                    }

                    //string texto = nodoRecorredorActual.Nodo.ObtenerHijo(nodoRecorredorActual.Actual).TextoParaImprimirArbol;
                    string texto = nodoRecorredorActual.Nodo.ObtenerHijo(nodoRecorredorActual.Actual).Dibujar();

                    if (!texto.Equals(string.Empty))
                    {
                        //treeNodeActual.Nodes.Add(new TreeNode(texto));
                        //colaIndices.InsertarIndice(nodoRecorredorActual.Actual);

                        //Mantengo el indice al nodo actual
                        colaIndices.InsertarIndice(treeNodeActual.Nodes.Count);
                        treeNodeActual.Nodes.Add(new TreeNode(texto));
                        
                    }
                    else
                    {
                        //Inserto esto como si fuera un dummy
                        colaIndices.InsertarIndice(-1);
                    }
                    

                    pilaRecorredor.InsertarElemento(new NodoPilaRecorredor(nodoRecorredorActual.Nodo.ObtenerHijo(nodoRecorredorActual.Actual)));
                    nodoRecorredorActual.Actual++;
                                        
                }
                else
                {
                    if (!colaIndices.esVacia())
                    {
                        colaIndices.EliminarUltimoIndice();
                    }

                    if (!pilaRecorredor.esVacia())
                    {
                        pilaRecorredor.DescartarTope();
                    }
                }

            }

            return treeNode;
            
            //return this.nodoRaiz.RecorrerArbolYDibujar(nodoStart);

        }

        public void MostrarError(ErrorCompiladorEventArgs e)
        {
            if (errorCompilacion != null)
            {
                errorCompilacion(e.Tipo, e.Descripcion, e.Fila, e.Columna, e.PararAnalisis);
            }

        }

        
    }
}
