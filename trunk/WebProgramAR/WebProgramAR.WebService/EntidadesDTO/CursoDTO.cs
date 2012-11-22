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
        private string creador;
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

        public string Creador
        {
            get { return this.creador; }
            set { this.creador = value; }
        }
        #endregion

        #region Constructores
        public CursoDTO() { }

        public CursoDTO(int id, string nombre, string creador)
        {
            this.id = id;
            this.nombre = nombre;
            this.creador = creador;
        }
        #endregion
    }
}