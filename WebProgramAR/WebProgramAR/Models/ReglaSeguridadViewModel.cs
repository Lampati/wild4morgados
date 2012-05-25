using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebProgramAR.Sitio.Models
{
    public class ReglaSeguridadViewModel
    {

        public int ReglaId { get; set; }

        [Display(Name = "Entidad")]
        [Required(ErrorMessage = "Requerido")]
        public int TablaId { get; set; }

        [Display(Name = "Campo")]
        [Required(ErrorMessage = "Requerido")]
        public int ColumnaId { get; set; }

        [Display(Name = "Comparación")]
        [Required(ErrorMessage = "Requerido")]
        public int ComparadorId { get; set; }

        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }

        [Display(Name = "Tipo de Usuario")]
        public int TipoUsuarioId { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Requerido")]
        public string Valor { get; set; }

        [Display(Name = "Habilitada")]
        public bool Activa { get; set; }

        public string Tipo { get; set; }
    }
}