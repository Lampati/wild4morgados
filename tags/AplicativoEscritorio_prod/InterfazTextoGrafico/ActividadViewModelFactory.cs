using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public static class ActividadViewModelFactory
    {

        public static ActividadViewModelBase CrearActividadViewModel(string nombreTipo)
        {
            ActividadViewModelBase retorno = null;

            if (!string.IsNullOrEmpty(nombreTipo))
            {
                if (nombreTipo.ToUpper().Equals("SiViewModel".ToUpper()))
                {
                    retorno = new SiViewModel();
                }
                else if (nombreTipo.ToUpper().Equals("MientrasViewModel".ToUpper()))
                {
                    retorno = new MientrasViewModel();
                }
                else if (nombreTipo.ToUpper().Equals("DeclaracionConstanteViewModel".ToUpper()))
                {
                    retorno = new DeclaracionConstanteViewModel();
                }
                else if (nombreTipo.ToUpper().Equals("DeclaracionVariableViewModel".ToUpper()))
                {
                    retorno = new DeclaracionVariableViewModel();
                }
                else if (nombreTipo.ToUpper().Equals("DeclaracionArregloViewModel".ToUpper()))
                {
                    retorno = new DeclaracionArregloViewModel();
                }
                else if (nombreTipo.ToUpper().Equals("AsignacionViewModel".ToUpper()))
                {
                    retorno = new AsignacionViewModel();
                }
                else if (nombreTipo.ToUpper().Equals("SecuenciaViewModel".ToUpper()))
                {
                    retorno = new SecuenciaViewModel();
                }
                else if (nombreTipo.ToUpper().Equals("DeclaracionConstanteViewModel".ToUpper()))
                {
                    retorno = new DeclaracionConstanteViewModel();
                }
                else if (nombreTipo.ToUpper().Equals("LeerViewModel".ToUpper()))
                {
                    retorno = new LeerViewModel();
                }
                else if (nombreTipo.ToUpper().Equals("MostrarViewModel".ToUpper()))
                {
                    retorno = new MostrarViewModel();
                }
                else if (nombreTipo.ToUpper().Equals("LlamarProcedimientoViewModel".ToUpper()))
                {
                    retorno = new LlamarProcedimientoViewModel();
                }

            }

            return retorno;

        }
    }
}
