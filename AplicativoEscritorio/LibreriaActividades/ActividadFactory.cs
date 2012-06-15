using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfazTextoGrafico;

namespace LibreriaActividades
{
    public static class ActividadFactory
    {
        public static ActividadBase CrearActividad(ActividadViewModelBase x)
        {
            ActividadBase retorno = null;

            if (x != null)
            {

                Type tipo = x.GetType();

                if (tipo == typeof(SiViewModel))
                {
                    retorno = new Si();
                }
                else if (tipo == typeof(MientrasViewModel))
                {
                    retorno = new Mientras();
                }
                else if (tipo == typeof(SecuenciaViewModel))
                {
                    retorno = new Secuencia();
                }

                if (retorno != null)
                {
                    retorno.AsignarDatos(x);
                }
            }

            return retorno;
        }
    }
}
