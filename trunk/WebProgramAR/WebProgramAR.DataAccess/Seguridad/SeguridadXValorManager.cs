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
                        PropertyInfo prop = props.Single(x => x.Name == regla.Columna.Nombre); //aca va la columna

                        bool resLocal = false;

                        object obj = prop.GetValue(entidad, null);

                        switch (regla.Columna.Tipo.Nombre.ToUpper()) //Aca va el tipo de la col
                        {
                            case ConstantesSeguridad.TIPO_STRING:
                                resLocal = Comparar((string)obj, regla.Comparador.Nombre, regla.Valor);
                                break;
                            case ConstantesSeguridad.TIPO_INT:
                                resLocal = Comparar((int)obj, regla.Comparador.Nombre, Convert.ToInt32(regla.Valor));
                                break;
                            case ConstantesSeguridad.TIPO_BOOL:
                                resLocal = Comparar((bool)obj, regla.Comparador.Nombre, Convert.ToBoolean(regla.Valor));
                                break;
                            case ConstantesSeguridad.TIPO_DATETIME:
                                resLocal = Comparar((DateTime)obj, regla.Comparador.Nombre, Convert.ToDateTime(regla.Valor));
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
                case ConstantesSeguridad.CONTIENE_NOCASESENSITIVE:
                    return valEntidad.ToUpper().Contains(valRef.ToUpper());
                case ConstantesSeguridad.MENOR:
                    return valEntidad.ToUpper().CompareTo(valRef.ToUpper()) < 0;
                case ConstantesSeguridad.MAYOR:
                    return valEntidad.ToUpper().CompareTo(valRef.ToUpper()) > 0;
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

        private static bool Comparar(bool valEntidad, string comp, bool valRef)
        {
            switch (comp)
            {                
                case ConstantesSeguridad.IGUAL:
                    return valEntidad == valRef;
                case ConstantesSeguridad.DISTINTO:
                    return valEntidad != valRef;
                default:
                    break;
            }
            return true;
        }

        private static bool Comparar(DateTime valEntidad, string comp, DateTime valRef)
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
