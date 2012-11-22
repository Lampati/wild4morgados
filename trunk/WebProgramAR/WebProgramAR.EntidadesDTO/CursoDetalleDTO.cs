using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgramAR.EntidadesDTO
{
    public class CursoDetalleDTO
    {
        #region Atributos
        private int cursoId;
        private List<EjercicioDetalleDTO> ejercicios;
        #endregion

        #region Propiedades
        public int CursoId
        {
            get { return this.cursoId; }
            set { this.cursoId = value; }
        }

        public List<EjercicioDetalleDTO> Ejercicios
        {
            get { return this.ejercicios; }
            set { this.ejercicios = value; }
        }
        #endregion

        #region Constructores
        public CursoDetalleDTO() { }

        public CursoDetalleDTO(int cursoId)
        {
            this.cursoId = cursoId;
            this.ejercicios = new List<EjercicioDetalleDTO>();

            Random r = new Random();
            for (int i = 0; i < r.Next(0, 11); i++)
                this.ejercicios.Add(new EjercicioDetalleDTO(i + 1));
        }
        #endregion

        #region Metodos
        public static CursoDetalleDTO Proxy(object o)
        {
            CursoDetalleDTO cd = new CursoDetalleDTO();
            cd.cursoId = (int)o.GetType().GetProperty("CursoId").GetValue(o, null);

            object ob = o.GetType().GetProperty("Ejercicios").GetValue(o, null);
            if (!Object.Equals(ob, null))
                cd.ejercicios = (List<EjercicioDetalleDTO>)ob;

            return cd;
        }
        #endregion
    }
}