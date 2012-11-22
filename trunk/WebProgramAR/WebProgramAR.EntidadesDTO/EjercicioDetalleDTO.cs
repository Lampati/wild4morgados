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
            ed.ejercicioId = (int)o.GetType().GetProperty("EjercicioId").GetValue(o, null);

            object ob = o.GetType().GetProperty("Enunciado").GetValue(o, null);
            if (!Object.Equals(ob, null))
                ed.enunciado = ob.ToString();

            ob = o.GetType().GetProperty("SolucionGarGar").GetValue(o, null);
            if (!Object.Equals(ob, null))
                ed.solucionGarGar = ob.ToString();

            ob = o.GetType().GetProperty("SolucionTexto").GetValue(o, null);
            if (!Object.Equals(ob, null))
                ed.solucionTexto = ob.ToString();

            ed.global = (bool)o.GetType().GetProperty("Global").GetValue(o, null);
            ed.fechaAlta = (DateTime)o.GetType().GetProperty("FechaAlta").GetValue(o, null);
            return ed;
        }
        #endregion
    }
}