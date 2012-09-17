using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WebProgramAR.Entidades
{
    [MetadataType(typeof(EjercicioValidation))]
    public partial class Ejercicio : EntidadProgramARBase
    {
         
    }



    public class EjercicioValidation
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(50, ErrorMessage = "El nombre de ejercicio debe tener como maximo 50 caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Enunciado")]
        [Required(ErrorMessage = "Requerido")]
        public string Enunciado { get; set; }

        [Display(Name = "Solucion")]
        [Required(ErrorMessage = "Requerido")]
        public string SolucionTexto { get; set; }

        [Display(Name = "Nivel")]
        [Required(ErrorMessage = "Requerido")]
        public int NivelEjercicio { get; set; }
    }
}
