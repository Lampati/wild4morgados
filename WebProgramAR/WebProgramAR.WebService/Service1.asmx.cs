using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebProgramAR.DataAccess;

namespace WebProgramAR.WebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
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
        public string EjerciciosXCurso(int cursoId, string cursosLocales)
        {
            return String.Empty;
        }

        [WebMethod]
        public int EjerciciosXCursoCount(int cursoId, string cursosLocales)
        {
            return 0;
        }

        [WebMethod]
        public string EjerciciosXProfesor(int profesorId, string cursosLocales)
        {
            return String.Empty;
        }

        [WebMethod]
        public int EjerciciosXProfesorCount(int profesorId, string cursosLocales)
        {
            return 0;
        }
    }
}