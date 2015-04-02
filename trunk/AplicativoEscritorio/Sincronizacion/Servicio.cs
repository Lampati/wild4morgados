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
        public List<CursoDTO> Cursos(Nullable<int> cursoId, string nombre, string creador)
        {
            if (this.Conectar())
            {
                string ids = this.ListadoIds;
                List<CursoDTO> cursos = new List<CursoDTO>();
                //Este object que devuelve en realidad es un List<WebProgramAR.EntidadesDTO.CursoDTO>
                object[] o = this.InvocarMetodo("Cursos", new object[] { ids, (cursoId.HasValue) ? cursoId.Value.ToString() : String.Empty, nombre, creador }) as object[];
                if (Object.Equals(o, null))
                    return null;

                foreach (object ob in o)
                    cursos.Add(CursoDTO.Proxy(ob));

                return cursos;
            }

            return null;
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        public List<EjercicioDTO> Ejercicios(Nullable<int> ejercicioId, string usuario, string nombre, Nullable<int> nivel)
        {
            if (this.Conectar())
            {
                string ids = this.ListadoIds;
                List<EjercicioDTO> ejercicios = new List<EjercicioDTO>();
                //Este object que devuelve en realidad es un List<WebProgramAR.EntidadesDTO.EjercicioDTO>
                object[] o = this.InvocarMetodo("Ejercicios", new object[] { ids, (ejercicioId.HasValue) ? ejercicioId.Value.ToString() : String.Empty, 
                    usuario, nombre, (nivel.HasValue) ? nivel.Value.ToString() : String.Empty }) as object[];
                if (Object.Equals(o, null))
                    return null;

                foreach (object ob in o)
                    ejercicios.Add(EjercicioDTO.Proxy(ob));

                return ejercicios;
            }

            return null;
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        public CursoDetalleDTO CursoDetalle(int cursoId)
        {
            if (this.Conectar())
            {
                string ids = this.ListadoIds;
                //Este object que devuelve en realidad es un List<WebProgramAR.EntidadesDTO.CursoDetalleDTO>
                object o = this.InvocarMetodo("CursoDetalle", new object[] { ids, cursoId });
                if (Object.Equals(o, null))
                    return null;

                CursoDetalleDTO cursoDetalle = CursoDetalleDTO.Proxy(o);
                return cursoDetalle;
            }

            return null;
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        public EjercicioDetalleDTO EjercicioDetalle(int ejercicioId)
        {
            if (this.Conectar())
            {
                string ids = this.ListadoIds;
                //Este object que devuelve en realidad es un List<WebProgramAR.EntidadesDTO.EjercicioDetalleDTO>
                object o = this.InvocarMetodo("EjercicioDetalle", new object[] { ids, ejercicioId });
                if (Object.Equals(o, null))
                    return null;

                EjercicioDetalleDTO ejercicioDetalle = EjercicioDetalleDTO.Proxy(o);
                return ejercicioDetalle;
            }

            return null;
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

        public string ListadoIds
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
                    string[] ejercicioConNombre = ejercicioEncriptadoStr.Split(new string[] { "!#!" }, StringSplitOptions.RemoveEmptyEntries);
                    Ejercicio ej = new Ejercicio();
                    try
                    {
                        ej.Abrir(ejercicioConNombre[0]);
                        ej.Guardar(Path.Combine(this.Directorio, String.Format("{0}_{1}.{2}", ej.EjercicioId, NombreSinCaracteresInvalidos(ref ejercicioConNombre[1]), "gej")));
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

        private string NombreSinCaracteresInvalidos(ref string nombre)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                if (nombre.Contains(c))
                    nombre = nombre.Replace(c.ToString(), String.Empty);

            return nombre;
        }
    }
}
