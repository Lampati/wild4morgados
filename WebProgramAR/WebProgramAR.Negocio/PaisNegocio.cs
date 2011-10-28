﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class PaisNegocio
    {
        public static Pais GetPaisById(string id)
        {
            return PaisDA.GetPaisById(id);
        }

        public static void Alta(Pais Pais)
        {
            PaisDA.Alta(Pais);
        }

        public static void Modificar(Pais Pais)
        {
            PaisDA.Modificar(Pais);
        }

        public static void Eliminar(string id)
        {
            PaisDA.Eliminar(id);
        }

        public static int ContarCantidad(string idPais, string apellido)
        {
            return PaisDA.ContarCantidad(idPais, apellido);
        }

        public static IEnumerable<Pais> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string idPais, string apellido)
        {
            return PaisDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, idPais, apellido);
        }

      
    }
}
