using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebProgramAR.DataAccess;
using WebProgramAR.WebService.EntidadesDTO;

namespace WebProgramAR.WebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://program-ar.com.ar/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SvcProgramar : System.Web.Services.WebService
    {
        private List<int> Ids(string idsConcatenados)
        {
            List<int> ids = new List<int>();
            if (!String.IsNullOrEmpty(idsConcatenados))
            {
                string[] s = idsConcatenados.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                int aux = 0;
                foreach (string id in s)
                {
                    if (int.TryParse(id, out aux))
                        ids.Add(aux);
                }
            }
            return ids;
        }

        [WebMethod]
        public string EjerciciosGlobales(string cursosLocales)
        {
            return EjercicioDA.GetEjercicioByGlobal(this.Ids(cursosLocales));
        }

        [WebMethod]
        public int EjerciciosGlobalesCount(string cursosLocales)
        {
            return EjercicioDA.GetEjercicioByGlobalCount(this.Ids(cursosLocales));
        }

        [WebMethod]
        public string EjerciciosXCurso(string cursosLocales, int cursoId)
        {
            return EjercicioDA.GetEjercicioByCurso(this.Ids(cursosLocales), cursoId);
        }

        [WebMethod]
        public int EjerciciosXCursoCount(string cursosLocales, int cursoId)
        {
            return EjercicioDA.GetEjercicioByCursoCount(this.Ids(cursosLocales), cursoId);
        }

        [WebMethod]
        public string EjerciciosXEjercicioId(string cursosLocales, int ejercicioId)
        {
            return EjercicioDA.GetEjercicioById(this.Ids(cursosLocales), ejercicioId);
        }

        [WebMethod]
        public int EjerciciosXEjercicioIdCount(string cursosLocales, int ejercicioId)
        {
            return EjercicioDA.GetEjercicioByIdCount(this.Ids(cursosLocales), ejercicioId);
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        [WebMethod]
        public List<CursoDTO> Cursos(string sId, string nombre, string creador)
        {
            int id = -1; //Si vino cargado este parámetro lo intentamos castear
            if (!String.IsNullOrEmpty(sId))
                if (!int.TryParse(sId, out id))
                    return null; //Si no se puede castear devolvemos una lista vacía, dar Exception por SOAP es un garron

            List<CursoDTO> c = new List<CursoDTO>();
            for (int i = 0; i < 10; i++)
                c.Add(new CursoDTO(i+1, String.Format("Curso {0}", i)));
            return c;
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        [WebMethod]
        public List<EjercicioDTO> Ejercicios(string sId, string usuario, string nombre, string sNivel)
        {
            int id = -1;
            if (!String.IsNullOrEmpty(sId))
                if (!int.TryParse(sId, out id))
                    return null;

            int nivel = -1;
            if (!String.IsNullOrEmpty(sNivel))
                if (!int.TryParse(sNivel, out nivel))
                    return null;

            List<EjercicioDTO> e = new List<EjercicioDTO>();
            for (int i = 0; i < 10; i++)
                e.Add(new EjercicioDTO(i+1));
            return e;
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        [WebMethod]
        public CursoDetalleDTO CursoDetalle(int cursoId)
        {
            return new CursoDetalleDTO(cursoId);
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        [WebMethod]
        public EjercicioDetalleDTO EjercicioDetalle(int ejercicioId)
        {
            return new EjercicioDetalleDTO(ejercicioId);
        }
    }
}