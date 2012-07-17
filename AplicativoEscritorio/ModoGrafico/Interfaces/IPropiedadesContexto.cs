using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.Enums;
using ModoGrafico.ViewModels;
using InterfazTextoGrafico;
using InterfazTextoGrafico.Enums;

namespace ModoGrafico.Interfaces
{

    public interface IPropiedadesContexto
    {      

        string Nombre { get; set; }
        TipoDato TipoRetorno { get; set; }
        string Retorno { get; set; }
        List<ParametroViewModel> Parametros { get; set; }
    }
}
