﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;

namespace WebProgramAR.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener como minimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmacion son distintas.")]
        public string ConfirmPassword { get; set; }
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

        [Display(Name = "Recordarme?")]
        public bool RememberMe { get; set; }

        public bool isAuthenticated { get; set; }
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