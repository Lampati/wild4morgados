using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProgramAR.Entidades;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace WebProgramAR.Sitio.Models
{
    public class EjercicioModel
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(50, ErrorMessage = "El nombre de ejercicio debe tener como maximo 50 caracteres")]
        public string Nombre { get; set; }

        [AllowHtml]
        [Display(Name = "Enunciado")]
        [Required(ErrorMessage = "Requerido")]
        public string Enunciadooo { get; set; }

        [AllowHtml]
        [Display(Name = "Solucion")]
        [Required(ErrorMessage = "Requerido")]
        public string SolucionTexto { get; set; }

        [AllowHtml]
        [Display(Name = "Solucion GarGar")]
        [Required(ErrorMessage = "Requerido")]
        public string SolucionGarGar { get; set; }

        [Display(Name = "Nivel")]
        [Required(ErrorMessage = "Requerido")]
        public int NivelEjercicio { get; set; }

        [AllowHtml]
        [Display(Name = "Solucion GarGar")]
        public string XmlDelEjercicio { get; set; }

        public bool Global { get; set; }
    }


}
