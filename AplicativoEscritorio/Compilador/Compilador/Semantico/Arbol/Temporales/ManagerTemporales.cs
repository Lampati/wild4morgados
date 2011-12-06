using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compilador.Semantico.Arbol.Temporales
{
    public sealed class ManagerTemporales
    {

        static ManagerTemporales instance=null;
        static readonly object padlock = new object();

        

        public ManagerTemporales()
        {
            listaTemporales = new List<Temporal>();
        }

        public List<Temporal> listaTemporales;

        public static ManagerTemporales Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance==null)
                    {
                        instance = new ManagerTemporales();
                    }
                    return instance;
                }
            }
        }
        
        public Temporal CrearNuevoTemporal(string nombreProc, string nodoCreador)
        {
            Temporal temp = new Temporal();
            temp.Nombre = new StringBuilder("temp").Append(nombreProc).Append(nodoCreador).Append(this.listaTemporales.Count.ToString()).ToString();
            temp.NombreProc = nombreProc;
            temp.NodoCreador = nodoCreador;
            

            listaTemporales.Add(temp);


            return temp;
        }

        /*
        
         public Temporal CrearNuevoTemporal(string nombreProc, string nodoCreador)
        {
            if (!this.ExisteTemporal(new StringBuilder("temp").Append(nombreProc).Append(nodoCreador).ToString()))
            {
                Temporal temp = new Temporal();
                if ( nodoCreador.ToUpper().Equals("MULTS"))
                {
                    temp.Nombre = new StringBuilder("temp").Append(nombreProc).Append(nodoCreador).ToString();                   
                }
                else
                {
                    temp.Nombre = new StringBuilder("temp").Append(nombreProc).Append(nodoCreador).Append(this.listaTemporales.Count.ToString()).ToString();
                }
                temp.NombreProc = nombreProc;
                temp.NodoCreador = nodoCreador;

                listaTemporales.Add(temp);

                return temp;
            }
            else
            {
                return this.ObtenerTemporal(new StringBuilder("temp").Append(nombreProc).Append(nodoCreador).ToString());
            }
        }

        

        private bool ExisteTemporal(string nombre)
        {
           return this.listaTemporales.Exists(

                delegate(Temporal temp)
                {
                    return (nombre.Equals(temp.Nombre));

                }

           );
        }

        private Temporal ObtenerTemporal(string nombre)
        {
            return this.listaTemporales.Find(

                delegate(Temporal temp)
                {
                    return (nombre.Equals(temp.Nombre));

                }

           );
        }
      
        */
        public Temporal CrearNuevoTemporal(string nombreProc, string nodoCreador,string valor)
        {
            Temporal temp = new Temporal();
            temp.Nombre = new StringBuilder("temp").Append(nombreProc).Append(nodoCreador).Append(this.listaTemporales.Count.ToString()).ToString();
            temp.NombreProc = nombreProc;
            temp.NodoCreador = nodoCreador;
            temp.Valor = valor;


            listaTemporales.Add(temp);


            return temp;
        }

        public int CantidadTemporalesParaProc(string nombreProc)
        {
            List<Temporal> temporales = this.listaTemporales.FindAll(

                delegate(Temporal temp)
                {
                    return (temp.NombreProc.Equals(nombreProc));

                }

                );

            return (temporales != null) ? temporales.Count : 0;
        }

        
    }
}
