using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ragnarok.EventArgsClasses
{
    public class ModificarPropiedadesEjercicioEventArgs
    {
        public string Enunciado { get; set; }
        public string SolucionTexto { get; set; }
        public short? NivelEjercicio { get; set; }

        public ModificarPropiedadesEjercicioEventArgs()
        {
            Enunciado = null;
            SolucionTexto = null;
            NivelEjercicio = null;
        }
    }
}
