using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ragnarok.EjercicioBrowser.EventArgsClasses
{

    // flanzani 22/11/2012
    // IDC_APP_9
    // Repositorio de ejercicios
    // Creado de clase para manejo de mensajes entre los user control y la ventana
    public class MensajeEstadoEventArgs : EventArgs
    {
        public string Mensaje { get; set; }

        //null para nada
        //false para error
        //true para satisfactorio
        public bool? Resultado { get; set; }
    }
}
