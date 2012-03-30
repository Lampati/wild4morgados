using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AplicativoEscritorio.DataAccess.Entidades;
using System.IO;
using Utilidades.XML;
using AplicativoEscritorio.DataAccess.Excepciones;

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

        public void EjerciciosGlobales()
        {
            string ids = this.ListadoIds;
            object o = proxy.InvocarMetodo("EjerciciosGlobales", new object[] { ids });
            if (!Object.Equals(o, null))
                this.GuardarEjercicios(o.ToString());
        }

        public int EjerciciosGlobalesCount()
        {
            object o = proxy.InvocarMetodo("EjerciciosGlobalesCount");
            if (!Object.Equals(o, null))
                return (int)o;

            return 0;
        }

        private string ListadoIds
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (string archivo in Directory.GetFiles(@"D:\Acustico", "*.gej"))
                {
                    Ejercicio ej = new Ejercicio();
                    bool errorApertura = false;
                    try
                    {
                        ej.Abrir(new FileInfo(archivo));
                    }
                    catch (ExcepcionCriptografia) { errorApertura = true; }
                    if (!errorApertura && ej.TieneId)
                    {
                        sb.Append(ej.EjercicioId.ToString());
                        sb.Append(",");
                    }
                }

                if (sb.Length > 0)
                    sb = sb.Remove(sb.Length - 1, 1); //Sacamos la última ","

                return sb.ToString();
            }
        }

        private void GuardarEjercicios(string respuestaWS)
        {
            //Si la respuesta del WS es vacía no hay nada que hacer...
            if (String.IsNullOrEmpty(respuestaWS))
                return;

            //La respuesta del WS es el XML de cada ejercicio separado por una ",". Además viene encriptado.
            string[] ejerciciosEncriptadosStr = respuestaWS.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string ejercicioEncriptadoStr in ejerciciosEncriptadosStr)
            {
                Ejercicio ej = new Ejercicio();
                ej.Abrir(ejercicioEncriptadoStr);
                ej.Guardar(Path.Combine(@"D:\Acustico", ej.EjercicioId.ToString() + ".gej"));
            }
        }
    }
}
