using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.Enums;
using ModoGrafico.ViewModels;
using InterfazTextoGrafico;
using InterfazTextoGrafico.Enums;
using System.Collections.ObjectModel;

namespace ModoGrafico.Interfaces
{

    public interface IPropiedadesContexto
    {      

        string Nombre { get; set; }
        TipoDato TipoRetorno { get; set; }
        string Retorno { get; set; }
        ObservableCollection<ParametroViewModel> Parametros { get; set; }
    }
}
