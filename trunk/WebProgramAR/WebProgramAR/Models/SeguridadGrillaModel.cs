using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProgramAR.Entidades;


namespace WebProgramAR.Sitio.Models
{
    public class SeguridadGrillaModel : ModelGrillaBase
    {
        public IEnumerable<ReglasSeguridad> ReglasSeguridad { get; set; }
    }
}
