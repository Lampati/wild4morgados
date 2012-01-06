using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProgramAR.Entidades;


namespace PMI.CETools.Sitio.Models
{
    public class CursoGrillaModel : ModelGrillaBase
    {
        public IEnumerable<Curso> Cursos { get; set; }
    }
}
