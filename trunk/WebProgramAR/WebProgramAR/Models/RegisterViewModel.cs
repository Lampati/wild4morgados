using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using WebProgramAR.Entidades;

namespace WebProgramAR.Sitio.Models
{
    public class RegisterViewModel
    {
        public Usuario Usuario { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "El Maximo permitido es 20 caracteres")]
        public string Contrasena { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Contrasena", ErrorMessage = "Contraseña y confirmacion son distintas.")]
        public string ConfirmarContrasena { get; set; }

    }
   
}
