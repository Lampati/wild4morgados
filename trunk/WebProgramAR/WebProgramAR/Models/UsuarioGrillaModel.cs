using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProgramAR.Entidades;


namespace WebProgramAR.Sitio.Models
{
    public class UsuarioGrillaModel : ModelGrillaBase
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int TipoUsuario { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}
