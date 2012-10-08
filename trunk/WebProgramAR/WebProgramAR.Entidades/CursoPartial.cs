using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WebProgramAR.Entidades
{
    [MetadataType(typeof(CursoValidation))]
    public partial class Curso : EntidadProgramARBase, ICloneable
    {

        

        #region ICloneable Members

        public object Clone()
        {
            Curso copia = new Curso();

            copia.CursoId = this.CursoId;
            copia.Nombre = this.Nombre;

            if (this.Usuario != null)
            {
                copia.Usuario = this.Usuario.Clone() as Usuario;              
            }

            if (this.Ejercicios != null)
            {
                copia.Ejercicios = new List<Ejercicio>();

                foreach (var item in this.Ejercicios.ToList())
                {
                    Ejercicio ej = item.Clone() as Ejercicio;               


                    copia.Ejercicios.Add(ej);
                }
            }

            return copia;
        }

        #endregion
    }



    public class CursoValidation
    {
        [Display(Name="Nombre")]
        [Required( ErrorMessage="Requerido")]
        [StringLength(50, ErrorMessage = "El nombre de curso debe tener como maximo 50 caracteres")]
        public string Nombre { get; set; }
    }
}