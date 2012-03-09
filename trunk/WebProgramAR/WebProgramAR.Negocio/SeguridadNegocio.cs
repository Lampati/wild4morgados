using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class SeguridadNegocio
    {
        public static ReglasSeguridad GetReglaSeguridadById(int id)
        {
            return ReglasSeguridadDA.GetReglaSeguridadById(id);
        }

        public static void Alta(ReglasSeguridad regla)
        {
            ReglasSeguridadDA.Alta(regla);
        }

        public static void Modificar(ReglasSeguridad regla)
        {
            ReglasSeguridadDA.Modificar(regla);
        }

        public static void Eliminar(int id)
        {
            ReglasSeguridadDA.Eliminar(id);
        }

        public static int ContarCantidad(int tablaId, int columnaId, int comparadorId, int? usuarioId, int? tipoUsuarioId,bool? activa)
        {
            return ReglasSeguridadDA.ContarCantidad(tablaId, columnaId, comparadorId, usuarioId, tipoUsuarioId, activa);
        }

        public static IEnumerable<ReglasSeguridad> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int tablaId, int columnaId, int comparadorId, int? usuarioId, int? tipoUsuarioId, bool? activa)
        {
            return ReglasSeguridadDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, tablaId, columnaId, comparadorId, usuarioId, tipoUsuarioId, activa);
        }

        public static IEnumerable<Tabla> GetTablas()
        {
            return TablaDA.GetTablas();
        }

        public static IEnumerable<Columna> GetColumnasByTabla(int tablaId)
        {
            return ColumnaDA.GetColumnasByTabla(tablaId);
        }

        public static IEnumerable<Comparador> GetComparadorByColumna(int colId)
        {
            Columna col = ColumnaDA.GetColumnaById(colId);

            return ComparadorDA.GetComparadoresByTipo(col.TipoId);
        }
    }
}
