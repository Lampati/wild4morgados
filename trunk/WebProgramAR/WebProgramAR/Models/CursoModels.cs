using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;

namespace WebProgramAR.Models
{

    public class CursoModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

    }
   
}
