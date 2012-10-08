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
    public partial class Usuario : EntidadProgramARBase, ICloneable
    {
        public string Email { get; set; }


        #region ICloneable Members

        public object Clone()
        {
            Usuario copia = new Usuario();
            copia.UsuarioId = this.UsuarioId;
            copia.UsuarioNombre = this.UsuarioNombre;
            copia.TipoUsuarioId = this.TipoUsuarioId;
            copia.Apellido = this.Apellido;
            copia.Nombre = this.Nombre;
            copia.Email = this.Email;
            copia.LocalidadId = this.LocalidadId;
            copia.PaisId = this.PaisId;
            copia.ProvinciaId = this.ProvinciaId;
            copia.FechaAlta = this.FechaAlta;

            if (this.TipoUsuario != null)
            {
                copia.TipoUsuario = new TipoUsuario();
                copia.TipoUsuario.TipoUsuarioId = this.TipoUsuario.TipoUsuarioId;
                copia.TipoUsuario.Descripcion = this.TipoUsuario.Descripcion;
            }

            if (this.Pais != null)
            {
                copia.Pais = new Pais();
                copia.Pais.PaisId = this.Pais.PaisId;
                copia.Pais.Descripcion = this.Pais.Descripcion;
            }

            if (this.Provincia != null)
            {
                copia.Provincia = new Provincia();
                copia.Provincia.ProvinciaId = this.Provincia.ProvinciaId;
                copia.Provincia.Descripcion = this.Provincia.Descripcion;
            }

            if (this.Localidad != null)
            {
                copia.Localidad = new Localidad();
                copia.Localidad.LocalidadId = this.Localidad.LocalidadId;
                copia.Localidad.Descripcion = this.Localidad.Descripcion;
            }

            return copia;
        }

        #endregion
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
        [RegularExpression("^((?!-1).*)$", ErrorMessage = "Requerido")]
        public string PaisId { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Requerido")]
        [RegularExpression("^((?!-1).*)$", ErrorMessage = "Requerido")]
        public string ProvinciaId { get; set; }

        [Display(Name = "Localidad")]
        [Required(ErrorMessage = "Requerido")]
        [RegularExpression("^((?!-1).*)$", ErrorMessage = "Requerido")]
        public string LocalidadId { get; set; }

    }
}
