using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using WebProgramAR.Globales;
using System.Web.Mvc;

namespace WebProgramAR.Entidades
{
    
    [MetadataType(typeof(UsuarioValidation))]
    public partial class Usuario : EntidadProgramARBase
    {

        public string ConfirmarContrasena { get; set; }
    }



    public class UsuarioValidation
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "El nombre debe tener como maximo 20 caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "El apellido debe tener como maximo 20 caracteres")]
        public string Apellido { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Requerido")]     
        [RegularExpression(Globals.MATCH_EMAIL_PATTERN, ErrorMessage = "El formato del email ingresado es incorrecto")]
        public string Email { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "Requerido")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "El nombre de usuario debe tener como maximo 20 caracteres")]
        public string UsuarioNombre { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "La contraseña debe tener como maximo 20 caracteres")]
        public string Contrasena { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Contrasena", ErrorMessage = "La nueva contraseña y la confirmacion son distintas.")]
        public string ConfirmarContrasena { get; set; }

        
        [Display(Name = "Pais")]
        [Required(ErrorMessage = "Requerido")]
        public string PaisId { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Requerido")]
        public string ProvinciaId { get; set; }

        [Display(Name = "Localidad")]
        [Required(ErrorMessage = "Requerido")]
        public string LocalidadId { get; set; }

    }
}
