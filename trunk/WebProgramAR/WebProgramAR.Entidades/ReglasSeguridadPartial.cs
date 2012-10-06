using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WebProgramAR.Entidades
{
    [MetadataType(typeof(ReglasSeguridadValidation))]
    public partial class ReglasSeguridad
    {
    }



    public class ReglasSeguridadValidation
    {
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

        [Display(Name="Valor")]
        [Required( ErrorMessage="Requerido")]
        public string Valor { get; set; }
        //public bool IsValorBool { get { return bool.Parse(Valor); } }

        [Display(Name = "Habilitada")]
        public bool Activa { get; set; }
    }
}