using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgramAR.WebService.EntidadesDTO
{
    public class CursoDTO
    {
        #region Atributos
        private int id;
        private string nombre;
        #endregion

        #region Propiedades
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        #endregion

        #region Constructores
        public CursoDTO() { }

        public CursoDTO(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }
        #endregion
    }
}