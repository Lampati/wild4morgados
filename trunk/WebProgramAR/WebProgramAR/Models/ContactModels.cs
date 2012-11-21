using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;

namespace WebProgramAR.Sitio.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Descripcion")]
        public string Descripcion{ get; set; }
    }
   
}
