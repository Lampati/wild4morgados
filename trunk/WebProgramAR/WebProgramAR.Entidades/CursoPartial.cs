using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WebProgramAR.Entidades
{
    [MetadataType(typeof(CursoValidation))]
    public partial class Curso : EntidadProgramARBase
    {
    }



    public class CursoValidation
    {
        [Display(Name="Nombre")]
        [Required( ErrorMessage="Requerido")]
        [StringLength(50, ErrorMessage = "El nombre de curso debe tener como maximo 50 caracteres")]
        public string Nombre { get; set; }
    }
}