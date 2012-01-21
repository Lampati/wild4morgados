using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;

namespace WebProgramAR.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener como minimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmacion son distintas.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
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
