using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Reflection;
using System.Diagnostics;
using System.Linq.Expressions;
using WebProgramAR.DataAccess.Seguridad.Helpers;

namespace WebProgramAR.DataAccess.Seguridad
{
    public static class SeguridadXValorManager
    {

        public static List<T> Filtrar<T>(List<T> lista, string tabla, Usuario usuarioLogueado, out float timestamp) where T : EntidadProgramARBase
        {
            long timestampNormal = Stopwatch.GetTimestamp();

            List<T> retorno = Filtrar<T>(lista, tabla, usuarioLogueado);

            float timestampNormalRes = ((float)(Stopwatch.GetTimestamp() - timestampNormal)) / ((float)Stopwatch.Frequency);

            long timestampDelegate = Stopwatch.GetTimestamp();

            List<T> retorno2 = FiltrarOptmizado<T>(lista, tabla, usuarioLogueado);

            float timestampDelegateRes = ((float)(Stopwatch.GetTimestamp() - timestampDelegate)) / ((float)Stopwatch.Frequency);

            long timestampFastPrpertyAccessor = Stopwatch.GetTimestamp();

            List<T> retorno3 = FiltrarFastPropertyAccessor<T>(lista, tabla, usuarioLogueado);

            float timestampFastPrpertyAccessorRes = ((float)(Stopwatch.GetTimestamp() - timestampFastPrpertyAccessor)) / ((float)Stopwatch.Frequency);

            timestamp = timestampDelegateRes;

            return retorno;

        }


        public static List<T> Filtrar<T>(List<T> lista, string tabla, Usuario usuarioLogueado) where T : EntidadProgramARBase
        {
            //http://msmvps.com/blogs/jon_skeet/archive/2008/08/09/making-reflection-fly-and-exploring-delegates.aspx


            List<T> retorno = new List<T>();

            int? userId = null;
            int? tipoUserId = null;

            if (usuarioLogueado != null)
            {
                userId = usuarioLogueado.UsuarioId;
                tipoUserId = usuarioLogueado.TipoUsuarioId;
            }

            List<ReglasSeguridad> reglas = ReglasSeguridadDA.GetReglasByTablaByUsuarioByTipoUsuario(tabla, userId, tipoUserId).ToList();



            if (lista.Count > 0 && reglas.Count > 0)
            {
                PropertyInfo[] props = lista[0].GetType().GetProperties();


                foreach (var entidad in lista)
                {
                    bool filtroAplicadosHastaAhora = false;

                    foreach (ReglasSeguridad regla in reglas)
                    {
                        bool resFiltro = false;                        

                        PropertyInfo prop = props.Single(x => x.Name == regla.Columna.Nombre); //aca va la columna
                        object obj = prop.GetValue(entidad, null);

                        switch (regla.Columna.Tipo.Nombre.ToUpper()) //Aca va el tipo de la col
                        {
                            case ConstantesSeguridad.TIPO_STRING:
                                resFiltro = Comparar((string)obj, regla.Comparador.Nombre, regla.Valor);
                                break;
                            case ConstantesSeguridad.TIPO_INT:
                                resFiltro = Comparar((int)obj, regla.Comparador.Nombre, Convert.ToInt32(regla.Valor));
                                break;
                            case ConstantesSeguridad.TIPO_BOOL:
                                resFiltro = Comparar((bool)obj, regla.Comparador.Nombre, Convert.ToBoolean(regla.Valor));
                                break;
                            case ConstantesSeguridad.TIPO_DATETIME:
                                resFiltro = Comparar((DateTime)obj, regla.Comparador.Nombre, Convert.ToDateTime(regla.Valor));
                                break;
                        }


                        filtroAplicadosHastaAhora |= resFiltro;

                        if (filtroAplicadosHastaAhora)
                        {
                            break;
                        }
                    }

                    if (!filtroAplicadosHastaAhora)
                    {
                        retorno.Add(entidad);
                    }
                }

            }
            else
            {
                retorno = lista;
            }

            return retorno;
        }

        public static List<T> FiltrarOptmizado<T>(List<T> lista, string tabla, Usuario usuarioLogueado) where T : EntidadProgramARBase
        {
            //http://msmvps.com/blogs/jon_skeet/archive/2008/08/09/making-reflection-fly-and-exploring-delegates.aspx

            //http://www.codeproject.com/Articles/14560/Fast-Dynamic-Property-Field-Accessors

            List<T> retorno = new List<T>();

            int? userId = null;
            int? tipoUserId = null;

            if (usuarioLogueado != null)
            {
                userId = usuarioLogueado.UsuarioId;
                tipoUserId = usuarioLogueado.TipoUsuarioId;
            }

            List<ReglasSeguridad> reglas = ReglasSeguridadDA.GetReglasByTablaByUsuarioByTipoUsuario(tabla, userId, tipoUserId).ToList();

           

            if (lista.Count > 0 && reglas.Count > 0)
            {
                PropertyInfo[] props = lista[0].GetType().GetProperties();

                Dictionary<int, Delegate> dictio = new Dictionary<int, Delegate>();

                foreach (ReglasSeguridad regla in reglas)
                {
                    PropertyInfo prop = props.Single(x => x.Name == regla.Columna.Nombre);

                    Type getterType = Expression.GetFuncType(prop.DeclaringType, prop.PropertyType);
                    Delegate deleg= Delegate.CreateDelegate(getterType, null, prop.GetGetMethod());

                    dictio.Add(regla.ReglaId, deleg);
                }

                foreach (var entidad in lista)
                {
                    bool filtroAplicadosHastaAhora = false;

                    foreach (ReglasSeguridad regla in reglas)
                    {
                        bool resFiltro = false;

                        object obj = dictio[regla.ReglaId].DynamicInvoke(entidad);

                        //PropertyInfo prop = props.Single(x => x.Name == regla.Columna.Nombre); //aca va la columna
                        //object obj = prop.GetValue(entidad, null);

                        switch (regla.Columna.Tipo.Nombre.ToUpper()) //Aca va el tipo de la col
                        {
                            case ConstantesSeguridad.TIPO_STRING:
                                resFiltro = Comparar((string)obj, regla.Comparador.Nombre, regla.Valor);
                                break;
                            case ConstantesSeguridad.TIPO_INT:
                                resFiltro = Comparar((int)obj, regla.Comparador.Nombre, Convert.ToInt32(regla.Valor));
                                break;
                            case ConstantesSeguridad.TIPO_BOOL:
                                resFiltro = Comparar((bool)obj, regla.Comparador.Nombre, Convert.ToBoolean(regla.Valor));
                                break;
                            case ConstantesSeguridad.TIPO_DATETIME:
                                resFiltro = Comparar((DateTime)obj, regla.Comparador.Nombre, Convert.ToDateTime(regla.Valor));
                                break;
                        }


                        filtroAplicadosHastaAhora |= resFiltro;

                        if (filtroAplicadosHastaAhora)
                        {
                            break;
                        }
                    }

                    if (!filtroAplicadosHastaAhora)
                    {
                        retorno.Add(entidad);
                    }
                }
               
            }
            else
            {
                retorno = lista;
            }

            return retorno;
        }

        public static List<T> FiltrarFastPropertyAccessor<T>(List<T> lista, string tabla, Usuario usuarioLogueado) where T : EntidadProgramARBase
        {
            //http://msmvps.com/blogs/jon_skeet/archive/2008/08/09/making-reflection-fly-and-exploring-delegates.aspx

            //http://www.codeproject.com/Articles/14560/Fast-Dynamic-Property-Field-Accessors

            List<T> retorno = new List<T>();

            int? userId = null;
            int? tipoUserId = null;

            if (usuarioLogueado != null)
            {
                userId = usuarioLogueado.UsuarioId;
                tipoUserId = usuarioLogueado.TipoUsuarioId;
            }

            List<ReglasSeguridad> reglas = ReglasSeguridadDA.GetReglasByTablaByUsuarioByTipoUsuario(tabla, userId, tipoUserId).ToList();



            if (lista.Count > 0 && reglas.Count > 0)
            {
                PropertyInfo[] props = lista[0].GetType().GetProperties();

                Dictionary<int, Delegate> dictio = new Dictionary<int, Delegate>();

                
                

                foreach (ReglasSeguridad regla in reglas)
                {
                    PropertyInfo prop = props.Single(x => x.Name == regla.Columna.Nombre);

                    Type tipoProp = prop.PropertyType;

                    Delegate deleg = null;

                    if (tipoProp == typeof(int))
                    {
                        deleg = TypeUtility<T>.GetMemberGetDelegate<Int32>(regla.Columna.Nombre);
                    }
                    else if (tipoProp == typeof(string))
                    {
                        deleg = TypeUtility<T>.GetMemberGetDelegate<string>(regla.Columna.Nombre);
                    }
                    else if (tipoProp == typeof(DateTime))
                    {
                        deleg = TypeUtility<T>.GetMemberGetDelegate<DateTime>(regla.Columna.Nombre);
                    }
                    else if (tipoProp == typeof(bool))
                    {
                        deleg = TypeUtility<T>.GetMemberGetDelegate<bool>(regla.Columna.Nombre);
                    }



                   

                    dictio.Add(regla.ReglaId, deleg);
                }

                foreach (var entidad in lista)
                {
                    bool filtroAplicadosHastaAhora = false;

                    foreach (ReglasSeguridad regla in reglas)
                    {
                        bool resFiltro = false;

                        object obj;

                        switch (regla.Columna.Tipo.Nombre)
                        {

                            case ConstantesSeguridad.TIPO_INT:
                                obj = ((TypeUtility<T>.MemberGetDelegate<Int32>)dictio[regla.ReglaId])(entidad);
                                break;
                            case ConstantesSeguridad.TIPO_STRING:
                                obj = ((TypeUtility<T>.MemberGetDelegate<string>)dictio[regla.ReglaId])(entidad);
                                break;
                            case ConstantesSeguridad.TIPO_DATETIME:
                                obj = ((TypeUtility<T>.MemberGetDelegate<DateTime>)dictio[regla.ReglaId])(entidad);
                                break;
                            case ConstantesSeguridad.TIPO_BOOL:
                                obj = ((TypeUtility<T>.MemberGetDelegate<bool>)dictio[regla.ReglaId])(entidad);
                                break;
                            default:
                                obj = null;
                                break;
                        }

                        

                        //PropertyInfo prop = props.Single(x => x.Name == regla.Columna.Nombre); //aca va la columna
                        //object obj = prop.GetValue(entidad, null);

                        switch (regla.Columna.Tipo.Nombre.ToUpper()) //Aca va el tipo de la col
                        {
                            case ConstantesSeguridad.TIPO_STRING:
                                resFiltro = Comparar((string)obj, regla.Comparador.Nombre, regla.Valor);
                                break;
                            case ConstantesSeguridad.TIPO_INT:
                                resFiltro = Comparar((int)obj, regla.Comparador.Nombre, Convert.ToInt32(regla.Valor));
                                break;
                            case ConstantesSeguridad.TIPO_BOOL:
                                resFiltro = Comparar((bool)obj, regla.Comparador.Nombre, Convert.ToBoolean(regla.Valor));
                                break;
                            case ConstantesSeguridad.TIPO_DATETIME:
                                resFiltro = Comparar((DateTime)obj, regla.Comparador.Nombre, Convert.ToDateTime(regla.Valor));
                                break;
                        }


                        filtroAplicadosHastaAhora |= resFiltro;

                        if (filtroAplicadosHastaAhora)
                        {
                            break;
                        }
                    }

                    if (!filtroAplicadosHastaAhora)
                    {
                        retorno.Add(entidad);
                    }
                }

            }
            else
            {
                retorno = lista;
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
            return false;
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
