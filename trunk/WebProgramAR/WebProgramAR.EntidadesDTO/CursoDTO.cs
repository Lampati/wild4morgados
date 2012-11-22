using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgramAR.EntidadesDTO
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

        #region Metodos
        public static CursoDTO Proxy(object o)
        {
            CursoDTO c = new CursoDTO();
            c.id = (int)o.GetType().GetProperty("Id").GetValue(o, null);

            object ob = o.GetType().GetProperty("Nombre").GetValue(o, null);
            if (!Object.Equals(ob, null))
                c.nombre = ob.ToString();

            ob = o.GetType().GetProperty("Creador").GetValue(o, null);
            if (!Object.Equals(ob, null))
                c.creador = ob.ToString();

            return c;
        }
        #endregion
    }
}