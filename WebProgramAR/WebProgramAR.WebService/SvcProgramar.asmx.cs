using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebProgramAR.DataAccess;
using WebProgramAR.EntidadesDTO;

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
        public List<CursoDTO> Cursos(string ejerciciosLocales, string sId, string nombre, string creador)
        {
            int id = -1; //Si vino cargado este parámetro lo intentamos castear
            if (!String.IsNullOrEmpty(sId))
                if (!int.TryParse(sId, out id))
                    return null; //Si no se puede castear devolvemos una lista vacía, dar Exception por SOAP es un garron

            List<int> ids = this.Ids(ejerciciosLocales);
            List<CursoDTO> c = new List<CursoDTO>();
            foreach (Entidades.Curso curso in CursoDA.GetCursos(id, nombre, creador))
            {
                bool loTieneLocal = true;
                foreach (Entidades.Ejercicio ej in curso.Ejercicios)
                    if (!ids.Contains(ej.EjercicioId))
                    {
                        loTieneLocal = false;
                        break;
                    }

                CursoDTO cursoDTO = CursoDTO.DesdeEntidad(curso);
                cursoDTO.LoTieneLocal = loTieneLocal;
                c.Add(cursoDTO);
            }

            return c;
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        [WebMethod]
        public List<EjercicioDTO> Ejercicios(string ejerciciosLocales, string sId, string usuario, string nombre, string sNivel)
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
            List<int> ids = this.Ids(ejerciciosLocales);
            foreach (Entidades.Ejercicio ejercicio in EjercicioDA.GetEjercicios(id, usuario, nombre, nivel))
            {
                EjercicioDTO ejDTO = EjercicioDTO.DesdeEntidad(ejercicio);
                ejDTO.LoTieneLocal = ids.Contains(ejercicio.EjercicioId);
                e.Add(ejDTO);
            }

            return e;
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        [WebMethod]
        public CursoDetalleDTO CursoDetalle(string ejerciciosLocales, int cursoId)
        {
            Entidades.Curso curso = CursoDA.GetCursoById(cursoId);
            return CursoDetalleDTO.DesdeEntidad(curso, this.Ids(ejerciciosLocales));
        }

        //IDC_WEB_2: EHT (21/11/2012) -> Metodo agregado por cambio solicitado por Tomassini, A.
        [WebMethod]
        public EjercicioDetalleDTO EjercicioDetalle(string ejerciciosLocales, int ejercicioId)
        {
            Entidades.Ejercicio ejercicio = EjercicioDA.GetEjercicioById(ejercicioId);
            return EjercicioDetalleDTO.DesdeEntidad(ejercicio, this.Ids(ejerciciosLocales));
        }
    }
}