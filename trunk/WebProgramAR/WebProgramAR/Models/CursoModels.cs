using System.ComponentModel.DataAnnotations;
using WebProgramAR.Globales;

namespace WebProgramAR.Models
{

    public class CursoModel
    {
        [Required]
        [Display(Name = "Nombre")]
        [RegularExpression(Globals.MATCH_NORMAL_STRING_EXTENDED_PATTERN, ErrorMessage = "Formato de curso incorrecto")]
        public string Nombre { get; set; }

    }
   
}
