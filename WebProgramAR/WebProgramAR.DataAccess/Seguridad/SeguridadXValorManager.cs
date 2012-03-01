using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Reflection;

namespace WebProgramAR.DataAccess.Seguridad
{
    public static class SeguridadXValorManager
    {

        public static List<EntidadProgramARBase> Filtrar(List<EntidadProgramARBase> lista, string tabla, int? userId, int? tipoUserId)
        {
            List<EntidadProgramARBase> retorno = new List<EntidadProgramARBase>();

            List<ReglasSeguridad> reglas = ReglasSeguridadDA.GetReglasByTablaByUsuarioByTipoUsuario(tabla, userId, tipoUserId).ToList();

            if (lista.Count > 0)
            {
                PropertyInfo[] props = lista[0].GetType().GetProperties();

                foreach (var entidad in lista)
                {
                    bool resGlobalTodasReglas = true;

                    foreach (ReglasSeguridad regla in reglas)
                    {
                        PropertyInfo prop = props.Single(x => x.Name == ""); //aca va la columna

                        bool resLocal;

                        switch (regla.Tabla.Nombre) //Aca va el tipo de la col
                        {
                            default:
                                object obj = prop.GetValue(entidad, null);
                                resLocal = Comparar((string)obj, regla.Comparador.Nombre, regla.Valor);
                                break;
                        }                        

                        resGlobalTodasReglas &= resLocal;

                        if (!resGlobalTodasReglas)
                        {
                            break;
                        }
                    }

                    if (resGlobalTodasReglas)
                    {
                        retorno.Add(entidad);
                    }
                }
            }

            return retorno;
        }

        private static bool Comparar(string valEntidad, string comp, string valRef)
        {
            switch (comp)
            { 
                case ConstantesSeguridad.IGUAL_NOCASESENSITIVE:
                    return string.Compare(valEntidad,valRef,true) == 0;
                case ConstantesSeguridad.DISTINTO_NOCASESENSITIVE:
                    return string.Compare(valEntidad,valRef,true) != 0;
                case ConstantesSeguridad.EMPIEZA_CON_NOCASESENSITIVE:
                    return valEntidad.ToUpper().StartsWith(valRef.ToUpper());
                case ConstantesSeguridad.TERMINA_CON_NOCASESENSITIVE:
                    return valEntidad.ToUpper().EndsWith(valRef.ToUpper());
                default:
                    break;
            }
            return true;
        }

        private static bool Comparar(int valEntidad, string comp, int valRef)
        {
            switch (comp)
            {
                case ConstantesSeguridad.MAYOR_IGUAL:
                    return valEntidad >= valRef;
                case ConstantesSeguridad.MENOR_IGUAL:
                    return valEntidad <= valRef;
                case ConstantesSeguridad.MAYOR:
                    return valEntidad > valRef;
                case ConstantesSeguridad.MENOR:
                    return valEntidad < valRef;
                case ConstantesSeguridad.IGUAL:
                    return valEntidad == valRef;
                case ConstantesSeguridad.DISTINTO:
                    return valEntidad != valRef;
                default:
                    break;
            }
            return true;
        }
    }
}
