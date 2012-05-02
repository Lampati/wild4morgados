using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProgramAR.Entidades;
using System.ComponentModel.DataAnnotations;


namespace WebProgramAR.Sitio.Models
{
    public class EjercicioUploadModel
    {
        [Required]
        public string FileUpload { get; set; }   
     
        public bool Error { get; set; }
        public string MensajeError { get; set; }
    }

    
}
