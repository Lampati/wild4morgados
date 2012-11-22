using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AplicativoEscritorio.DataAccess.Entidades;
using System.IO;
using Utilidades.XML;
using AplicativoEscritorio.DataAccess.Excepciones;
using WebProgramAR.EntidadesDTO;

namespace Sincronizacion
{
    public class Servicio
    {
        #region Atributos
        private Proxy.ProxyDinamico proxy;
        private object barraProgreso;
        private object labelInfo;
        private List<string> wsdl;
        Random r;
        private bool simularRespuesta = false;
        private string directorio;
        #endregion

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
            if (Object.Equals(this.proxy, null) || !this.wsdl.Contains(this.proxy.UrlWsdl))
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
                        catch (InvalidOperationException)
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
            Eventos.Handler.InvocandoMetodoEventFire("Solicitando ejercicios para descarga... (" + nombre + ")", this.LabelInfo);
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
                string ids = this.ListadoIds;
                object o = this.InvocarMetodo("EjerciciosGlobales", new object[] { ids });
                if (!Object.Equals(o, null))
                    this.GuardarEjercicios(o.ToString());
                else
                    return false;

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
                if (!Object.Equals(o, null) && !String.IsNullOrEmpty(o.ToString()))
                    return (int)o;
            }
            return 0;
        }

        public bool EjerciciosPorCurso(int cursoId)
        {
            if (this.Conectar())
            {
                string ids = this.ListadoIds;
                object o = this.InvocarMetodo("EjerciciosXCurso", new object[] { ids, cursoId });
                if (!Object.Equals(o, null) && !String.IsNullOrEmpty(o.ToString()))
                    this.GuardarEjercicios(o.ToString());
                else
                    return false;

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
                if (!Object.Equals(o, null) && !String.IsNullOrEmpty(o.ToString()))
                    return (int)o;
            }
            return 0;
        }

        public bool EjerciciosPorId(int ejercicioId)
        {
            if (this.Conectar())
            {
                string ids = this.ListadoIds;
                object o = this.InvocarMetodo("EjerciciosXEjercicioId", new object[] { ids, ejercicioId });
                if (!Object.Equals(o, null) && !String.IsNullOrEmpty(o.ToString()))
                    this.GuardarEjercicios(o.ToString());
                else
                    return false;

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
                if (!Object.Equals(o, null) && !String.IsNullOrEmpty(o.ToString()))
                    return (int)o;
            }
            return 0;
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        public int Cursos(Nullable<int> cursoId, string nombre, string creador)
        {
            if (this.Conectar())
            {
                //Este object que devuelve en realidad es un List<WebProgramAR.WebService.EntidadesDTO.CursoDTO>
                object o = this.InvocarMetodo("Cursos", new object[] { (cursoId.HasValue) ? cursoId.Value.ToString() : String.Empty, nombre, creador });
                CursoDTO cursos = CursoDTO.Proxy(o);
                //Podría devolver la cantidad de cursos que trajo, no se si serviria para algo...
                return 0;
            }

            return 0;
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        public int Ejercicios(Nullable<int> ejercicioId, string usuario, string nombre, Nullable<int> nivel)
        {
            if (this.Conectar())
            {
                //Este object que devuelve en realidad es un List<WebProgramAR.WebService.EntidadesDTO.EjercicioDTO>
                object o = this.InvocarMetodo("Ejercicios", new object[] { (ejercicioId.HasValue) ? ejercicioId.Value.ToString() : String.Empty, 
                    usuario, nombre, (nivel.HasValue) ? nivel.Value.ToString() : String.Empty });
                EjercicioDTO ejercicios = EjercicioDTO.Proxy(o);
                //Idem Cursos, podría devolver la cantidad de ejercicios que trajo...
                return 0;
            }

            return 0;
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        public void CursoDetalle(int cursoId)
        {
            //QUE TIPO DE DATO DEBERIA DEVOLVER ESTE METODO?
            if (this.Conectar())
            {
                //Este object que devuelve en realidad es un List<WebProgramAR.WebService.EntidadesDTO.CursoDetalleDTO>
                object o = this.InvocarMetodo("CursoDetalle", new object[] { cursoId });
                CursoDetalleDTO cursoDetalle = CursoDetalleDTO.Proxy(o);
            }
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        public void EjercicioDetalle(int ejercicioId)
        {
            //QUE TIPO DE DATO DEBERIA DEVOLVER ESTE METODO?
            if (this.Conectar())
            {
                //Este object que devuelve en realidad es un List<WebProgramAR.WebService.EntidadesDTO.EjercicioDetalleDTO>
                object o = this.InvocarMetodo("EjercicioDetalle", new object[] { ejercicioId });
                EjercicioDetalleDTO ejercicioDetalle = EjercicioDetalleDTO.Proxy(o);
            }
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
                    catch (ArgumentOutOfRangeException) { errorApertura = true; }
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
                    catch (NullReferenceException) { }
                }
                Eventos.Handler.FinalizadoEventFire("Finalizada la descarga de ejercicios!", this.LabelInfo);
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
