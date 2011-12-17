using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Semantico.TablaDeSimbolos;
using System.Diagnostics;
using Compilador.Auxiliares;

namespace Compilador.Semantico.TablaDeSimbolos
{
    class TablaSimbolos
    {
        public const int TAMANIOENTERO = 2;
        

        private List<NodoTablaSimbolos> listaNodos;
        public List<NodoTablaSimbolos> ListaNodos
        {
            get { return listaNodos; }
        }


        public TablaSimbolos()
        {
            this.listaNodos = new List<NodoTablaSimbolos>();
        }

        public TablaSimbolos(TablaSimbolos tabla)
        {
            this.listaNodos = new List<NodoTablaSimbolos>(tabla.listaNodos);
        }

        #region Temporales

        public void AgregarTemporal(string nombre, NodoTablaSimbolos.TipoDeDato tdato)
        {
            if (!this.ExisteTemporal(nombre, tdato))
            {
                int des = this.ObtenerDesplazamientoParaContexto(string.Empty);

                this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Temporal, tdato, int.MinValue, false, NodoTablaSimbolos.TipoContexto.Global, string.Empty, TAMANIOENTERO, des));
            }
        }

        public void AgregarTemporal(string nombre, NodoTablaSimbolos.TipoDeDato tdato, string valor)
        {
            if (!this.ExisteTemporal(nombre, tdato))
            {
                int des = this.ObtenerDesplazamientoParaContexto(string.Empty);

                this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Temporal, tdato, valor, false, NodoTablaSimbolos.TipoContexto.Global, string.Empty, valor.Length, des));
            }
        }

        public bool ExisteTemporal(string nombre, NodoTablaSimbolos.TipoDeDato tdato)
        {
            return this.listaNodos.Exists(

               delegate(NodoTablaSimbolos _nodo)
               {
                   return (_nodo.Nombre.Equals(nombre) &&
                       (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Temporal) &&
                       (_nodo.TipoDato == tdato)
                       );
               }
               );
        }

        public string ObtenerStringTemporal(string nombre)
        {
            NodoTablaSimbolos nodo = this.listaNodos.Find(

              delegate(NodoTablaSimbolos _nodo)
              {
                  return (_nodo.Nombre.Equals(nombre) &&
                      (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Temporal) &&
                      (_nodo.TipoDato == NodoTablaSimbolos.TipoDeDato.String)
                      );
              }
              );

            return nodo.ValorString;
        }

        public NodoTablaSimbolos.TipoDeDato ObtenerTipoTemporal(string nombre)
        {
            NodoTablaSimbolos nodo = this.listaNodos.Find(

              delegate(NodoTablaSimbolos _nodo)
              {
                  return (_nodo.Nombre.Equals(nombre) &&
                      (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Temporal) 
                      );
              }
              );

            return nodo.TipoDato;
        }

        public int ObtenerValorTemporal(string nombre)
        {
            NodoTablaSimbolos nodo = this.listaNodos.Find(

              delegate(NodoTablaSimbolos _nodo)
              {
                  return (_nodo.Nombre.Equals(nombre) &&
                      (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Temporal) &&
                      (_nodo.TipoDato == NodoTablaSimbolos.TipoDeDato.Numero || _nodo.TipoDato == NodoTablaSimbolos.TipoDeDato.Numero)
                      );
              }
              );

            return nodo.Valor;
        }


        #endregion

        #region Manejo Variables

        public void AgregarParametroDeProc(string nombre, NodoTablaSimbolos.TipoDeDato tdato, NodoTablaSimbolos.TipoContexto contexto, string nombreProc)
        {
            int des = this.ObtenerDesplazamientoParaContexto(nombreProc);

            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Parametro, tdato, int.MinValue, false, contexto, nombreProc, TAMANIOENTERO, des));
        }

        public void AgregarVariable(string nombre, NodoTablaSimbolos.TipoDeDato tdato, bool esConstante, NodoTablaSimbolos.TipoContexto contexto, string nombreProc)
        {
            int des = this.ObtenerDesplazamientoParaContexto(nombreProc);

            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Variable, tdato, int.MinValue, esConstante, contexto, nombreProc, TAMANIOENTERO, des));
        }

        public void AgregarVariable(string nombre, NodoTablaSimbolos.TipoDeDato tdato, bool esConstante, NodoTablaSimbolos.TipoContexto contexto, string nombreProc, Int32 val)
        {
            int des = this.ObtenerDesplazamientoParaContexto(nombreProc);

            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Variable, tdato, val, esConstante, contexto, nombreProc, TAMANIOENTERO, des));
        }
        
        public bool ExisteVariable(string nombre)
        {
            return this.listaNodos.Exists(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.Nombre.Equals(nombre) &&
                        ((_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable) || (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro)) 
                        //&& (_nodo.EsArreglo == false) 
                        );
                }
                );
        }
        
        public bool ExisteVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            return this.listaNodos.Exists(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.Nombre.Equals(nombre) &&
                        ((_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable) || (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro)) &&
                        ((_nodo.Contexto == contexto) && 
                        (_nodo.NombreContextoLocal == nombreContexto))
                        );
                }
                );
        }

         

        private NodoTablaSimbolos ObtenerVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo;
            
            nodo = this.listaNodos.Find(

                delegate(NodoTablaSimbolos _nodo)
                {
                    //flanzani 23/10/2010
                    //Faltaba agregar el nombre del contexto local, pq sino puede traer de cualquiera.
                    return (
                            _nodo.Contexto == NodoTablaSimbolos.TipoContexto.Local &&                            
                            _nodo.Nombre.Equals(nombre) &&
                            ((_nodo.NombreContextoLocal == nombreContexto &&
                            (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable)) ||                          
                            (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro))
                    
                    );
                }
                );

            if (nodo == null)
            {
                nodo = this.listaNodos.Find(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.Contexto == NodoTablaSimbolos.TipoContexto.Global &&
                        _nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable && _nodo.Nombre.Equals(nombre));
                }
                );
            }

            Debug.Assert(nodo != null, new StringBuilder("La variable ").Append(nombre).Append(" no estaba declarada.").ToString());

            return nodo;
        }

        public int ObtenerValorVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre, contexto, nombreContexto);
            return nodo.Valor;           
        }

        public NodoTablaSimbolos.TipoDeDato ObtenerTipoVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre,contexto,nombreContexto);
            return nodo.TipoDato;
        }

        public NodoTablaSimbolos.TipoDeDato ObtenerTipoVariableUsandoNombreEntero(string nombre)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre);
            return nodo.TipoDato;
        }

        private NodoTablaSimbolos ObtenerVariable(string nombre)
        {
            return this.listaNodos.Find(

                delegate(NodoTablaSimbolos _nodo)
                {
                    //flanzani 23/10/2010
                    //Faltaba agregar el nombre del contexto local, pq sino puede traer de cualquiera.
                    return (
                        nombre.Equals(_nodo.NombreContextoLocal + _nodo.Nombre) &&                            
                        (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable) 
                    );
                }
                );
        }

        public NodoTablaSimbolos.TipoContexto ObtenerContextoVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre, contexto, nombreContexto);
            return nodo.Contexto;
        }

        public string ObtenerNombreContextoVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre, contexto, nombreContexto);
            return nodo.NombreContextoLocal;
        }

        public bool EsModificableValorVarible(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre,contexto,nombreContexto);
            return !nodo.EsConstante;
        }

        public void ModificarValorVarible(string nombre, int val, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre, contexto, nombreContexto);
            nodo.Valor = val;
        }

        public void EliminarVariablesContextoLocal(string nombreContexto)
        {
            this.listaNodos.RemoveAll(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.Contexto == NodoTablaSimbolos.TipoContexto.Local) &&
                        (_nodo.NombreContextoLocal == nombreContexto);
                }
                );
        }

        #endregion


        #region Manejo Arreglos

        public void AgregarArreglo(string nombre, NodoTablaSimbolos.TipoDeDato tdato, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto, int indice, bool esConst)
        {

            //flanzani Codigo viejo
            //Antes creaba muchas direcciones, una por cada 
            //for (int i = 0; i < indice; i++)
            //{
            //    this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Variable,
            //        tdato, int.MinValue, true, i, esConst, NodoTablaSimbolos.TipoContexto.Global,string.Empty));
            //}

            int des = this.ObtenerDesplazamientoParaContexto(EnumUtils.stringValueOf(NodoTablaSimbolos.TipoContexto.Global));

            this.listaNodos.Add(
                
                    new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Variable,
                   tdato, int.MinValue, true, indice, esConst, contexto,
                   nombreContexto,
                   indice*TAMANIOENTERO,des)
                   
                   );
        }

        internal void AgregarArregloParametroDeProc(string nombre, NodoTablaSimbolos.TipoDeDato tipoDeDato, NodoTablaSimbolos.TipoContexto tipoContexto, string nombreProc)
        {
            int des = this.ObtenerDesplazamientoParaContexto(nombreProc);

            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Parametro, tipoDeDato, int.MinValue, true, int.MinValue, false, tipoContexto, nombreProc, TAMANIOENTERO, des));
        }

        private int ObtenerDesplazamientoParaContexto(string p)
        {
            /*
            NodoTablaSimbolos nodo = this.listaNodos.FindLast(
                delegate(NodoTablaSimbolos nd) 
                    { return 
                        (nd.NombreContextoLocal.Equals(p) &&
                         (nd.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro 
                          || nd.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable)
                        
                        ); }
                    
                    );
            
            
            if (nodo == null)
            {
                return 0;
            }
            else
            {
                return nodo.Desplazamiento + nodo.Tamanio;
            }
             * */

            NodoTablaSimbolos nodo = this.listaNodos.FindLast(
                delegate(NodoTablaSimbolos nd)
                {
                    return
                      (
                       (nd.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro
                        || nd.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable
                        || nd.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Temporal
                        )

                      );
                }

                    );

            if (nodo == null)
            {
                return 0;
            }
            else
            {
                return nodo.Desplazamiento + nodo.Tamanio;
            }
        }


        private NodoTablaSimbolos ObtenerPosicionArreglo(string nombre,int posicion)
        {
            NodoTablaSimbolos nodo;

            nodo = this.listaNodos.Find(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (
                         (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable || _nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro) && 
                         _nodo.EsArreglo == true &&
                         _nodo.Indice == posicion &&
                         _nodo.Nombre.Equals(nombre));
                }
                );

            //Debug.Assert(nodo != null, new StringBuilder("La variable ").Append(nombre).Append(" no estaba declarada.").ToString());

            return nodo;
        }

        public bool ExisteArreglo(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            return this.listaNodos.Exists(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return ((_nodo.Nombre.Equals(nombre)) &&
                        (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable || _nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro) &&
                        (_nodo.EsArreglo == true) &&
                        (_nodo.Contexto == contexto) &&
                        (_nodo.NombreContextoLocal == nombreContexto) 
                        
                        );
                }
                );
        }

        public NodoTablaSimbolos ObtenerArreglo(string nombre)
        {
            return this.listaNodos.Find(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return ((nombre.Equals(_nodo.NombreContextoLocal+_nodo.Nombre)) &&
                        (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable) &&
                        (_nodo.EsArreglo == true));
                }
                );
        }    

      
        public NodoTablaSimbolos.TipoDeDato ObtenerTipoArreglo(string nombre)
        {
            NodoTablaSimbolos nodo = this.ObtenerPosicionArreglo(nombre, 0);
            return nodo.TipoDato;
            
            //return this.ObtenerTipoVariable(nombre,NodoTablaSimbolos.TipoContexto.Global,string.Empty);            
        }

        public int ObtenerTamanioArreglo(string nombre)
        {
            NodoTablaSimbolos nodo = this.ObtenerArreglo(nombre);

            return nodo.Tamanio / TAMANIOENTERO;

            //return this.ObtenerTipoVariable(nombre,NodoTablaSimbolos.TipoContexto.Global,string.Empty);            
        }

        public void ModificarValorPosicionArreglo(string nombre, int pos,int val)
        {
            NodoTablaSimbolos nodo = this.ObtenerPosicionArreglo(nombre,pos);
            nodo.Valor = val;
        }

        #endregion


        #region Manejo Procedimientos/Funciones

        public void AgregarProcedimiento(string nombre, List<NodoTablaSimbolos.TipoDeDato> firma, int cantTemps)
        {
            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Procedimiento,
                NodoTablaSimbolos.TipoDeDato.Ninguno, firma,cantTemps));
        }

        public void AgregarFuncion(string nombre, List<NodoTablaSimbolos.TipoDeDato> firma, 
            NodoTablaSimbolos.TipoDeDato tdato, int cantTemps)
        {
            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Funcion, tdato, firma,cantTemps));
        }

        public bool ExisteProcedimiento(string nombre)
        {
            return this.listaNodos.Exists(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.Nombre.Equals(nombre) && (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Procedimiento));
                }
                );
        }

        public bool ExisteFuncion(string nombre)
        {
            return this.listaNodos.Exists(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.Nombre.Equals(nombre) && (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Funcion));
                }
                );
        }

        private NodoTablaSimbolos ObtenerRutina(string nombre, NodoTablaSimbolos.TipoDeEntrada tipo)
        {
            NodoTablaSimbolos nodo;

            nodo = this.listaNodos.Find(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.TipoEntrada == tipo &&
                         _nodo.Nombre.Equals(nombre));
                }
                );

            Debug.Assert(nodo != null, new StringBuilder("La variable ").Append(nombre).Append(" no estaba declarada.").ToString());

            return nodo;
        }

        public List<NodoTablaSimbolos.TipoDeDato> ObtenerFirma(string nombre, NodoTablaSimbolos.TipoDeEntrada tipo)
        {
            NodoTablaSimbolos nodo = this.ObtenerRutina(nombre, tipo);

            return nodo.Firma;
        }

        public List<string> ObtenerParametros(string nombre)
        {
            List<NodoTablaSimbolos> nodos = this.listaNodos.FindAll(
                delegate(NodoTablaSimbolos _nodo) 
                {
                    return (_nodo.NombreContextoLocal.Equals(nombre) && (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro));
                }                   
            );

            List<string> retorno = new List<string>();

            foreach (NodoTablaSimbolos nodo in nodos)
            {
                retorno.Add(nodo.Nombre);
            }

            return retorno;
        }

        public NodoTablaSimbolos.TipoDeDato ObtenerTipoFuncion(string nombre)
        {
            NodoTablaSimbolos nodo = this.ObtenerRutina(nombre, NodoTablaSimbolos.TipoDeEntrada.Funcion);

            return nodo.TipoDato;
        }

        public NodoTablaSimbolos.TipoDeDato ObtenerTipoProcedimiento(string nombre)
        {
            NodoTablaSimbolos nodo = this.ObtenerRutina(nombre, NodoTablaSimbolos.TipoDeEntrada.Procedimiento);

            return nodo.TipoDato;
        }


        #endregion






        internal bool EsParametroDeEsteProc(string nombre, NodoTablaSimbolos.TipoContexto tipoContexto, string nombreCont)
        {
            return this.listaNodos.Exists(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.Nombre.Equals(nombre) &&
                        ( (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro)
                        && _nodo.Contexto == tipoContexto
                        && _nodo.NombreContextoLocal == nombreCont)
                        //&& (_nodo.EsArreglo == false) 
                        );
                }
                );
        }
    }
}
