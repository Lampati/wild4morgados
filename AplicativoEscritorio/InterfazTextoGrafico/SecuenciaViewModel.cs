using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModoGrafico.Datos
{
    public class SecuenciaViewModel : ActividadViewModelBase
    {
        public List<ActividadViewModelBase> ListaActividades { get; set; }

        public SecuenciaViewModel()
        {
            ListaActividades = new List<ActividadViewModelBase>();
        }
    }
}
