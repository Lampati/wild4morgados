using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WebProgramAR.Entidades
{
    [MetadataType(typeof(EjercicioValidation))]
    public partial class Ejercicio : EntidadProgramARBase, ICloneable
    {
        public string XmlDelEjercicio { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            Ejercicio ej = new Ejercicio();
            ej.EjercicioId = this.EjercicioId;
            ej.Enunciado = this.Enunciado;
            ej.SolucionGarGar = this.SolucionGarGar;
            ej.SolucionTexto = this.SolucionTexto;
            ej.Nombre = this.Nombre;
            ej.NivelEjercicio = this.NivelEjercicio;
            ej.Global = this.Global;
            ej.EstadoEjercicioId = this.EstadoEjercicioId;
            ej.FechaAlta = this.FechaAlta;
            ej.XML = this.XML;
            ej.XmlDelEjercicio = this.XmlDelEjercicio;
            ej.UsuarioId = this.UsuarioId;
            ej.Global = this.Global;

            if (this.EstadoEjercicio != null)
            {
                ej.EstadoEjercicio = new EstadoEjercicio();
                ej.EstadoEjercicio.EstadoEjercicioId = ej.EstadoEjercicio.EstadoEjercicioId;
                ej.EstadoEjercicio.Descripcion = ej.EstadoEjercicio.Descripcion;
            }

            if (this.Usuario != null)
            {
                ej.Usuario = this.Usuario.Clone() as Usuario;
            }

            return ej;
        }

        #endregion
    }



    public class EjercicioValidation
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(50, ErrorMessage = "El nombre de ejercicio debe tener como maximo 50 caracteres")]
        public string Nombre { get; set; }
        
        [Display(Name = "Enunciado")]
        [Required(ErrorMessage = "Requerido")]
        public string Enunciado { get; set; }

        [Display(Name = "Solucion")]
        [Required(ErrorMessage = "Requerido")]
        public string SolucionTexto { get; set; }

        [Display(Name = "Nivel")]
        [Required(ErrorMessage = "Requerido")]
        public int NivelEjercicio { get; set; }
    }
}
