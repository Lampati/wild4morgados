using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProgramAR.Entidades;


namespace PMI.CETools.Sitio.Models
{
    public class UsuarioGrillaModel : ModelGrillaBase
    {
        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}
