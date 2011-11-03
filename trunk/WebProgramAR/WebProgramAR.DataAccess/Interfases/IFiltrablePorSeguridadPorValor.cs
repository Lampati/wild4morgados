using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;

namespace WebProgramAR.DataAccess.Interfases
{
    interface IFiltrablePorSeguridadPorValor
    {
        List<Entidades.EntidadProgramARBase> Filtrar(List<EntidadProgramARBase> lista, Usuario user, TipoUsuario tipo);
    }
}
