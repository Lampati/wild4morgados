using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.Enums;
using InterfazTextoGrafico;
using System.Collections.ObjectModel;

namespace ModoGrafico.EventArgsClasses
{
    public class ActualizarParametrosEventArgs
    {
        public ObservableCollection<ParametroViewModel> Parametros { get; set; }

        public ActualizarParametrosEventArgs()
        {
        }
    }
}
