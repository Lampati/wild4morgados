using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProgramAR.Entidades;


namespace WebProgramAR.Sitio.Models
{
    public class EjercicioGrillaModel : ModelGrillaBase
    {
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public IEnumerable<Ejercicio> Ejercicios { get; set; }
    }

    public class ListEjercicioGrillaModel
    {
        public List<EjercicioGrillaModel> ListEjerciciosGrillaModel { get; set; }
        public int cursoId { get; set; }
    }
}
