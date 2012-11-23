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
        private bool loTieneLocal;
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

        public bool LoTieneLocal
        {
            get { return this.loTieneLocal; }
            set { this.loTieneLocal = value; }
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
            c.id = (int)o.GetType().GetField("Id").GetValue(o);

            object ob = o.GetType().GetField("Nombre").GetValue(o);
            if (!Object.Equals(ob, null))
                c.nombre = ob.ToString();

            ob = o.GetType().GetField("Creador").GetValue(o);
            if (!Object.Equals(ob, null))
                c.creador = ob.ToString();

            c.loTieneLocal = (bool)o.GetType().GetField("LoTieneLocal").GetValue(o);
            return c;
        }

        public static CursoDTO DesdeEntidad(Entidades.Curso curso)
        {
            CursoDTO c = new CursoDTO();
            c.id = curso.CursoId;
            c.nombre = curso.Nombre;
            c.creador = curso.Usuario.UsuarioNombre;
            return c;
        }
        #endregion
    }
}