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
        private object barraProgreso;
        private object labelInfo;
        private List<string> wsdl;
        Random r;
        private bool simularRespuesta = false;
        private string respuestaSimulada = "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";
        private string directorio;

        public Servicio()
            : this(new List<string>())
        {
        }

        public Servicio(string wsdl)
            : this(new List<string>() { wsdl })
        {
        }

        public Servicio(List<string> wsdls)
        {
            this.wsdl = wsdls;
            r = new Random();
        }

        public List<string> Urls
        {
            set { this.wsdl = value; }
        }

        public bool Conectar()
        {
            if (Object.Equals(this.proxy, null))
            {
                if (Object.Equals(this.wsdl, null) || this.wsdl.Count.Equals(0))
                    Eventos.Handler.ErrorConexionEventFire("No se ha establecido servidor para descargar ejercicios.", this.LabelInfo);
                else
                {
                    foreach (string str in this.wsdl)
                    {
                        try
                        {
                            Eventos.Handler.ConectadoEventFire(str, this.LabelInfo);
                            this.proxy = new Proxy.ProxyDinamico(str);
                            Eventos.Handler.ConectadoEventFire(str, this.LabelInfo);
                            return true;
                        }
                        catch (NotSupportedException)
                        {
                            Eventos.Handler.ErrorConexionEventFire("Error de conexión. Error al conectar a " + str, this.LabelInfo);
                        }
                        catch (UriFormatException)
                        {
                            Eventos.Handler.ErrorConexionEventFire("Error de conexión. Error al conectar a " + str, this.LabelInfo);
                        }
                        catch (System.Net.WebException)
                        {
                            Eventos.Handler.ErrorConexionEventFire("Error al conectar a " + str, this.LabelInfo);
                        }
                    }
                }
                return false;
            }
            return true;
        }

        public object BarraProgreso
        {
            get { return this.barraProgreso; }
            set { this.barraProgreso = value; }
        }

        public object LabelInfo
        {
            get { return this.labelInfo; }
            set { this.labelInfo = value; }
        }

        private object InvocarMetodo(string nombre, object[] parametros)
        {
            Eventos.Handler.InvocandoMetodoEventFire("Invocando metodo " + nombre + " ...", this.LabelInfo);
            try
            {
                return this.proxy.InvocarMetodo(nombre, parametros);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is System.Web.Services.Protocols.SoapException)
                {
                    Eventos.Handler.ErrorConexionEventFire("Error al conectar con el servicio para invocar el metodo " + nombre, this.LabelInfo);
                    this.proxy = null;
                }
            }
            return null;
        }

        public bool EjerciciosGlobales()
        {
            if (this.Conectar())
            {
                if (this.simularRespuesta)
                    this.GuardarEjercicios(this.respuestaSimulada);
                else
                {
                    string ids = this.ListadoIds;
                    object o = this.InvocarMetodo("EjerciciosGlobales", new object[] { ids });
                    if (!Object.Equals(o, null))
                        this.GuardarEjercicios(o.ToString());
                    else
                        return false;
                }

                return true;
            }

            return false;
        }

        public int EjerciciosGlobalesCount()
        {
            if (this.Conectar())
            {
                string ids = this.ListadoIds;
                object o = this.InvocarMetodo("EjerciciosGlobalesCount", new object[] { ids });
                if (!Object.Equals(o, null))
                    return (int)o;
            }
            return 0;
        }

        public bool EjerciciosPorCurso(int cursoId)
        {
            if (this.Conectar())
            {
                if (this.simularRespuesta)
                    this.GuardarEjercicios(this.respuestaSimulada);
                else
                {
                    string ids = this.ListadoIds;
                    object o = this.InvocarMetodo("EjerciciosXCurso", new object[] { ids, cursoId });
                    if (!Object.Equals(o, null))
                        this.GuardarEjercicios(o.ToString());
                    else
                        return false;
                }

                return true;
            }

            return false;
        }

        public int EjerciciosPorCursoCount(int cursoId)
        {
            if (this.Conectar())
            {
                string ids = this.ListadoIds;
                object o = this.InvocarMetodo("EjerciciosXCursoCount", new object[] { ids, cursoId });
                if (!Object.Equals(o, null))
                    return (int)o;
            }
            return 0;
        }

        public bool EjerciciosPorId(int ejercicioId)
        {
            if (this.Conectar())
            {
                if (this.simularRespuesta)
                    this.GuardarEjercicios(this.respuestaSimulada);
                else
                {
                    string ids = this.ListadoIds;
                    object o = this.InvocarMetodo("EjerciciosXEjercicioId", new object[] { ids, ejercicioId });
                    if (!Object.Equals(o, null))
                        this.GuardarEjercicios(o.ToString());
                    else
                        return false;
                }

                return true;
            }

            return false;
        }

        public int EjerciciosPorIdCount(int ejercicioId)
        {
            if (this.Conectar())
            {
                string ids = this.ListadoIds;
                object o = this.InvocarMetodo("EjerciciosXEjercicioIdCount", new object[] { ids, ejercicioId });
                if (!Object.Equals(o, null))
                    return (int)o;
            }
            return 0;
        }

        public string Directorio
        {
            get { return this.directorio; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.directorio = value;
                    if (!Directory.Exists(this.directorio))
                        Directory.CreateDirectory(this.directorio);
                }
            }
        }

        private string ListadoIds
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (string archivo in Directory.GetFiles(this.Directorio, "*.gej"))
                {
                    Ejercicio ej = new Ejercicio();
                    bool errorApertura = false;
                    try
                    {
                        ej.Abrir(new FileInfo(archivo));
                    }
                    catch (ExcepcionCriptografia) { errorApertura = true; }
                    catch (ExcepcionHashNoConcuerda) { errorApertura = true; }
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
            //La respuesta del WS es el XML de cada ejercicio separado por una ",". Además viene encriptado.
            string[] ejerciciosEncriptadosStr = respuestaWS.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int cant = 0;
            if (ejerciciosEncriptadosStr.Length.Equals(0))
                Eventos.Handler.GuardarEjercicioEventFire(this.BarraProgreso, this.LabelInfo, 0, 0);
            else
            {
                foreach (string ejercicioEncriptadoStr in ejerciciosEncriptadosStr)
                {
                    Ejercicio ej = new Ejercicio();
                    try
                    {
                        ej.Abrir(ejercicioEncriptadoStr);
                        ej.Guardar(Path.Combine(this.Directorio, ej.EjercicioId.ToString() + ".gej"));
                        cant++;
                        Eventos.Handler.GuardarEjercicioEventFire(this.BarraProgreso, this.LabelInfo, cant, ejerciciosEncriptadosStr.Length);
                        System.Threading.Thread.Sleep(r.Next(100, 500));
                    }
                    catch (ExcepcionHashNoConcuerda) { }
                }
                Eventos.Handler.FinalizadoEventFire("Finalizada la descarga de ejercicios!", this.LabelInfo);
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
