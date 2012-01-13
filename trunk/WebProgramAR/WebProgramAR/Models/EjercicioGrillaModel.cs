using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProgramAR.Entidades;


namespace PMI.CETools.Sitio.Models
{
    public class EjercicioGrillaModel : ModelGrillaBase
    {
        public IEnumerable<Ejercicio> Ejercicios { get; set; }
    }
}
