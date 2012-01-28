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

    public class ChangePasswordModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "El Maximo permitido es 20 caracteres")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "El Maximo permitido es 20 caracteres")]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("NewPassword", ErrorMessage = "Contraseña y confirmacion son distintas.")]
        public string ConfirmPassword { get; set; }

        public bool EsResetPassword { get; set; }
        public string UserName { get; set; }
    }

    public class RedirectionModel
    {
        public string ControllerRedirect { get; set; }
        public string ActionRedirect { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

      

        public bool isAuthenticated { get; set; }
    }

    public class RecoverPasswordModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Requerido")]
        [RegularExpression(Globals.MATCH_EMAIL_PATTERN, ErrorMessage = "Formato de email incorrecto")]
        public string Email { get; set; }
    }

   
    public static class SimpleSessionPersister
    {
        static string UserNameSessionVar = "username";
        public static string UserName
        {
            get
            {
                if (HttpContext.Current == null) return string.Empty;
                var sessionvar = HttpContext.Current.Session[UserNameSessionVar];
                if (sessionvar != null)
                    return sessionvar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session[UserNameSessionVar] = value;
            }
        }

    }
}
