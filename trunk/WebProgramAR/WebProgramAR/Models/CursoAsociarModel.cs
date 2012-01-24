using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using WebProgramAR.Entidades;

namespace WebProgramAR.Models
{

    public class CursoAsociarModel
    {

        public Curso Curso { get; set; }
        public int[] EjerciciosId { get; set; }

    }
   
}
