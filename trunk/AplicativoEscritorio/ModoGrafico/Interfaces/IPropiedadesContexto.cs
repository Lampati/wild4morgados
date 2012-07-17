using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.Enums;
using ModoGrafico.ViewModels;

namespace ModoGrafico.Interfaces
{

    public interface IPropiedadesContexto
    {      

        string Nombre { get; set; }
        TipoDato TipoRetorno { get; set; }
        string Retorno { get; set; }
        List<Parametro> Parametros { get; set; }
    }
}
