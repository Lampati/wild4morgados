using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using System.Diagnostics;
using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Semantico.TablaDeSimbolos
{
    class TablaSimbolos
    {
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

      

        #region Manejo Variables

        public void AgregarParametroDeProc(string nombre, NodoTablaSimbolos.TipoDeDato tdato, NodoTablaSimbolos.TipoContexto contexto, string nombreProc)
        {

            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Parametro, tdato,  false, contexto, nombreProc));
        }

        public void AgregarVariable(string nombre, NodoTablaSimbolos.TipoDeDato tdato, bool esConstante, NodoTablaSimbolos.TipoContexto contexto, string nombreProc)
        {

            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Variable, tdato, esConstante, contexto, nombreProc));
        }

        
        
        //public bool ExisteVariable(string nombre)
        //{
        //    return this.listaNodos.Exists(

        //        delegate(NodoTablaSimbolos _nodo)
        //        {
        //            return (_nodo.Nombre.Equals(nombre) &&
        //                ((_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable) || (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro)) 
        //                //&& (_nodo.EsArreglo == false) 
        //                );
        //        }
        //        );
        //}

        
        
        //public bool ExisteVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        //{
        //    return this.listaNodos.Exists(

        //        delegate(NodoTablaSimbolos _nodo)
        //        {
        //            return (_nodo.Nombre.Equals(nombre) &&
        //                ((_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable) || (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro)) &&
        //                ((_nodo.Contexto == contexto) && 
        //                (_nodo.NombreContextoLocal == nombreContexto))
        //                );
        //        }
        //        );
        //}

        public bool ExisteVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            return ObtenerVariable(nombre, contexto, nombreContexto) != null;
        }

        public bool ExisteVariableEnEsteContexto(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = ObtenerVariable(nombre, contexto, nombreContexto);            

            return (nodo != null && nodo.Contexto == contexto && nodo.NombreContextoLocal == nombreContexto);
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
                            _nodo.EsArreglo == false &&
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
                    return (_nodo.EsArreglo == false &&
                        _nodo.Contexto == NodoTablaSimbolos.TipoContexto.Global &&
                        _nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable && 
                        _nodo.Nombre.Equals(nombre));
                }
                );
            }

            //Debug.Assert(nodo != null, new StringBuilder("La variable ").Append(nombre).Append(" no estaba declarada.").ToString());

            return nodo;
        }       

        public NodoTablaSimbolos.TipoDeDato ObtenerTipoVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre,contexto,nombreContexto);
            return nodo.TipoDato;
        }

       

        //public NodoTablaSimbolos.TipoContexto ObtenerContextoVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        //{
        //    NodoTablaSimbolos nodo = this.ObtenerVariable(nombre, contexto, nombreContexto);
        //    return nodo.Contexto;
        //}

        //public string ObtenerNombreContextoVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        //{
        //    NodoTablaSimbolos nodo = this.ObtenerVariable(nombre, contexto, nombreContexto);
        //    return nodo.NombreContextoLocal;
        //}

        public bool EsModificableValorVarible(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre,contexto,nombreContexto);
            return !nodo.EsConstante;
        }

                

        #endregion


        #region Manejo Arreglos

        public void AgregarArreglo(string nombre, NodoTablaSimbolos.TipoDeDato tdato, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto, int indice, bool esConst)
        {     
            
            this.listaNodos.Add(
                
                    new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Variable,
                   tdato,  true,  esConst, contexto,
                   nombreContexto )
                   
                   );
        }

        internal void AgregarArregloParametroDeProc(string nombre, NodoTablaSimbolos.TipoDeDato tipoDeDato, NodoTablaSimbolos.TipoContexto tipoContexto, string nombreProc)
        {

            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Parametro, tipoDeDato,  true,  false, tipoContexto, nombreProc));
        }

       

        

        //public bool ExisteArreglo(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        //{
        //    return this.listaNodos.Exists(

        //        delegate(NodoTablaSimbolos _nodo)
        //        {
        //            return ((_nodo.Nombre.Equals(nombre)) &&
        //                (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable || _nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro) &&
        //                (_nodo.EsArreglo == true) &&
        //                (_nodo.Contexto == contexto) &&
        //                (_nodo.NombreContextoLocal == nombreContexto) 
                        
        //                );
        //        }
        //        );
        //}

        public bool ExisteArreglo(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            return ObtenerArreglo(nombre, contexto, nombreContexto) != null;

        }

        public bool ExisteArregloEnEsteContexto(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = ObtenerArreglo(nombre, contexto, nombreContexto);

            return (nodo != null && nodo.Contexto == contexto && nodo.NombreContextoLocal == nombreContexto);
        }

        private NodoTablaSimbolos ObtenerArreglo(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
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
                            _nodo.EsArreglo &&
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
                    return _nodo.EsArreglo &&
                        (_nodo.Contexto == NodoTablaSimbolos.TipoContexto.Global &&
                        _nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable && _nodo.Nombre.Equals(nombre));
                }
                );
            }

            //Debug.Assert(nodo != null, new StringBuilder("La variable ").Append(nombre).Append(" no estaba declarada.").ToString());

            return nodo;
        }


        public NodoTablaSimbolos.TipoDeDato ObtenerTipoArreglo(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerArreglo(nombre, contexto, nombreContexto);
            return nodo.TipoDato;
            
            //return this.ObtenerTipoVariable(nombre,NodoTablaSimbolos.TipoContexto.Global,string.Empty);            
        }

       

       
        #endregion


        #region Manejo Procedimientos/Funciones

        public void AgregarProcedimiento(string nombre, List<FirmaProc> firma)
        {
            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Procedimiento,
                NodoTablaSimbolos.TipoDeDato.Ninguno, firma));
        }

        public void AgregarFuncion(string nombre, List<FirmaProc> firma, 
            NodoTablaSimbolos.TipoDeDato tdato)
        {
            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Funcion, tdato, firma));
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

        public List<FirmaProc> ObtenerFirma(string nombre, NodoTablaSimbolos.TipoDeEntrada tipo)
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

            NodoTablaSimbolos nodo = ObtenerVariable(nombre, tipoContexto, nombreCont);

            return nodo != null 
                && nodo.Nombre.Equals(nombre) 
                && nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro
                && nodo.Contexto == tipoContexto
                && nodo.NombreContextoLocal == nombreCont;
            
        }
    }
}
