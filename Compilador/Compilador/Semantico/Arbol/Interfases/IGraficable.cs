using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfazTextoGrafico;

namespace CompiladorGargar.Semantico.Arbol.Interfases
{
    interface IGraficable
    {
        ActividadViewModelBase ActividadViewModel { get; set; }
    }
}
