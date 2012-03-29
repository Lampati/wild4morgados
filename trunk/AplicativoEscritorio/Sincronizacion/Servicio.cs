using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AplicativoEscritorio.DataAccess.Entidades;
using System.IO;
using Utilidades.XML;

namespace Sincronizacion
{
    public class Servicio
    {
        private Proxy.ProxyDinamico proxy;

        public Servicio()
        {
            //Este constructor es de debug, esta información va a salir del archivo de configuración de Fedex
            this.proxy = new Proxy.ProxyDinamico("http://localhost:1889/Service1.asmx?wsdl");    
        }

        public Servicio(string wsdl)
        {
            this.proxy = new Proxy.ProxyDinamico(wsdl);    
        }

        public string EjerciciosGlobales()
        {
            string hashes = this.ListadoHashGlobales;
            object o = proxy.InvocarMetodo("EjerciciosGlobales");
            if (!Object.Equals(o, null))
                return o.ToString();

            return null;
        }

        public int EjerciciosGlobalesCount()
        {
            object o = proxy.InvocarMetodo("EjerciciosGlobalesCount");
            if (!Object.Equals(o, null))
                return (int)o;

            return 0;
        }

        private string ListadoHashGlobales
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (string archivo in Directory.GetFiles(@"D:\Acustico", "*.gej"))
                {
                    Ejercicio ej = new Ejercicio();
                    ej.Abrir(archivo);
                    //Falta acá preguntar si es global!!
                    sb.Append(ej.Hash);
                    sb.Append(",");
                }
                if (sb.Length > 0)
                    sb = sb.Remove(sb.Length - 1, 1); //Sacamos la última ","

                return sb.ToString();
            }
        }
    }
}
