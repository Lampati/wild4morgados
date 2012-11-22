using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgramAR.EntidadesDTO
{
    public class EjercicioDTO
    {
        #region Atributos
        private int id;
        private string nombre;
        private string usuario;
        private int nivel;

        private static Random r = new Random();
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

        public string Usuario
        {
            get { return this.usuario; }
            set { this.usuario = value; }
        }

        public int Nivel
        {
            get { return this.nivel; }
            set { this.nivel = value; }
        }
        #endregion

        #region Constructores
        public EjercicioDTO() { }

        public EjercicioDTO(int id)
        {
            this.id = id;
            this.nombre = String.Format("Ejercicio {0}", this.id);
            this.usuario = String.Format("Usuario del Ejercicio {0}", this.id);
            this.nivel = r.Next(0, 11);
        }
        #endregion

        #region Metodos
        public static EjercicioDTO Proxy(object o)
        {
            EjercicioDTO e = new EjercicioDTO();
            e.id = (int)o.GetType().GetField("Id").GetValue(o);

            object ob = o.GetType().GetField("Nombre").GetValue(o);
            if (!Object.Equals(ob, null))
                e.nombre = ob.ToString();

            ob = o.GetType().GetField("Usuario").GetValue(o);
            if (!Object.Equals(ob, null))
                e.usuario = ob.ToString();
            e.nivel = (int)o.GetType().GetField("Nivel").GetValue(o);
            return e;
        }
        #endregion
    }
}