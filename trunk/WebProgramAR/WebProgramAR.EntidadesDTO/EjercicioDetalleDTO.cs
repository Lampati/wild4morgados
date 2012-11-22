using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgramAR.EntidadesDTO
{
    public class EjercicioDetalleDTO
    {
        #region Atributos
        private int ejercicioId;
        private string nombre;
        private string usuario;
        private string enunciado;
        private string solucionGarGar;
        private string solucionTexto;
        private bool global;
        private DateTime fechaAlta;

        private static Random r = new Random();
        #endregion

        #region Propiedades
        public int EjercicioId
        {
            get { return this.ejercicioId; }
            set { this.ejercicioId = value; }
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

        public string Enunciado
        {
            get { return this.enunciado; }
            set { this.enunciado = value; }
        }

        public string SolucionGarGar
        {
            get { return this.solucionGarGar; }
            set { this.solucionGarGar = value; }
        }

        public string SolucionTexto
        {
            get { return this.solucionTexto; }
            set { this.solucionTexto = value; }
        }

        public bool Global
        {
            get { return this.global; }
            set { this.global = value; }
        }

        public DateTime FechaAlta
        {
            get { return this.fechaAlta; }
            set { this.fechaAlta = value; }
        }
        #endregion

        #region Constructores
        public EjercicioDetalleDTO() { }

        public EjercicioDetalleDTO(int ejercicioId)
        {
            this.ejercicioId = ejercicioId;
            this.nombre = String.Format("Nombre del Ejercicio {0}", this.ejercicioId);
            this.usuario = String.Format("Creador del Ejercicio {0}", this.ejercicioId); ;
            this.enunciado = String.Format("Enunciado del Ejercicio {0}", this.ejercicioId);
            this.solucionGarGar = String.Format("Solucion GarGar del Ejercicio {0}", this.ejercicioId);
            this.solucionTexto = String.Format("Solucion Texto del Ejercicio {0}", this.ejercicioId);
            this.global = r.NextDouble() < 0.5;
            this.fechaAlta = DateTime.Now.AddDays(-r.Next(1, 7));
        }
        #endregion

        #region Metodos
        public static EjercicioDetalleDTO Proxy(object o)
        {
            EjercicioDetalleDTO ed = new EjercicioDetalleDTO();
            ed.ejercicioId = (int)o.GetType().GetField("EjercicioId").GetValue(o);

            object ob = o.GetType().GetField("Nombre").GetValue(o);
            if (!Object.Equals(ob, null))
                ed.nombre = ob.ToString();

            ob = o.GetType().GetField("Usuario").GetValue(o);
            if (!Object.Equals(ob, null))
                ed.usuario = ob.ToString();

            ob = o.GetType().GetField("Enunciado").GetValue(o);
            if (!Object.Equals(ob, null))
                ed.enunciado = ob.ToString();

            ob = o.GetType().GetField("SolucionGarGar").GetValue(o);
            if (!Object.Equals(ob, null))
                ed.solucionGarGar = ob.ToString();

            ob = o.GetType().GetField("SolucionTexto").GetValue(o);
            if (!Object.Equals(ob, null))
                ed.solucionTexto = ob.ToString();

            ed.global = (bool)o.GetType().GetField("Global").GetValue(o);
            ed.fechaAlta = (DateTime)o.GetType().GetField("FechaAlta").GetValue(o);
            return ed;
        }
        #endregion
    }
}