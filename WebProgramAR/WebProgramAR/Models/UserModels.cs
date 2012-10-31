using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using WebProgramAR.Globales;

namespace WebProgramAR.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "Usuario")]
        [RegularExpression(Globals.MATCH_NORMAL_STRING_EXTENDED_PATTERN, ErrorMessage = "Formato de nombre de usuario incorrecto")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Debe tener como minimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [RegularExpression(Globals.MATCH_NORMAL_STRING_PATTERN, ErrorMessage = "Formato de nombre de contraseña incorrecto")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmacion son distintas.")]
        [RegularExpression(Globals.MATCH_NORMAL_STRING_PATTERN, ErrorMessage = "Formato de nombre de contraseña incorrecto")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [RegularExpression(Globals.MATCH_NORMAL_STRING_EXTENDED_PATTERN, ErrorMessage = "Formato de nombre de nombre incorrecto")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        [RegularExpression(Globals.MATCH_NORMAL_STRING_EXTENDED_PATTERN, ErrorMessage = "Formato de nombre de apellido incorrecto")]
        public string Apellido { get; set; }

        [Required]
        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.Date)]
        public string FechaNac { get; set; }

        [Required]
        [Display(Name = "Pais")]
        public string Pais{get;set;}
        
        
        [Required]
        [Display(Name = "Provincia")]
        public string Provincia{get;set;}
        
        [Required]
        [Display(Name = "Localidad")]
        public string Localidad { get; set; }

        [Required]
        [Display(Name = "Tipo Usuario")]
        public int TipoUsuario{ get; set; }
    }
   
}
