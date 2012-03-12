using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class TipoUsuarioNegocio
    {
        public static TipoUsuario GetTipoUsuarioById(int id)
        {
            return TipoUsuarioDA.GetTipoUsuarioById(id);
        }

       
        public static IEnumerable<TipoUsuario> GetTiposUsuario()
        {
            return TipoUsuarioDA.GetTiposUsuario(true);
        }

        public static IEnumerable<TipoUsuario> GetTiposUsuarioSinGuest()
        {
            return TipoUsuarioDA.GetTiposUsuario(false);
        }


        public static TipoUsuario GetTipoUsuarioByName(string p)
        {
            return TipoUsuarioDA.GetTipoUsuarioByName(p);
        }
    }
}
