using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

using System.Configuration;
using WebProgramAR.Entidades;

namespace WebProgramAR.Sitio.Models
{
    public class ModerarEjercicioModel
    {
        public Ejercicio Ejercicio { get; set; }
        public string MensajeModeracion { get; set; }
        public bool Aceptado { get; set; }
    }
    

   
}