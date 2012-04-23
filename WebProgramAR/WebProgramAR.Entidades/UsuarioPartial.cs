﻿using System;
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
        public string Email { get; set; }

    }



    public class UsuarioValidation
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Apellido { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Requerido")]     
        [RegularExpression(Globals.MATCH_EMAIL_PATTERN, ErrorMessage = "Formato de email incorrecto")]
        public string Email { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "Requerido")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string UsuarioNombre { get; set; }

     

        
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