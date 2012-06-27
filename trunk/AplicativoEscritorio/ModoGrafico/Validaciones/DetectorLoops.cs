using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ModoGrafico.Validaciones
{
    internal class DetectorLoops
    {
        private Dictionary<string, List<string>> llamadas;

        internal DetectorLoops()
        {
            this.llamadas = new Dictionary<string, List<string>>();
        }

        internal void AgregarLlamada(string invocador)
        {
            invocador = invocador.ToLower();
            if (!this.llamadas.ContainsKey(invocador))
                this.llamadas.Add(invocador, new List<string>());
        }

        internal void AgregarLlamada(string invocador, string llamada)
        {
            invocador = invocador.ToLower();
            llamada = llamada.ToLower();

            if (!this.llamadas.ContainsKey(invocador))
                this.llamadas.Add(invocador, new List<string>());

            List<string> lista = this.llamadas[invocador];
            if (!lista.Contains(llamada))
                lista.Add(llamada);

            foreach (string key in this.llamadas.Keys)
            {
                List<string> cadenaInvocaciones = new List<string>();
                this.ValidarLoopsRecursivo(new List<string>(this.llamadas[key]), key, cadenaInvocaciones, String.Empty);
            }
        }

        private void ValidarLoopsRecursivo(List<string> lista, string invocador, List<string> cadenaInvocaciones, string llamador)
        {
            if (lista.Count.Equals(0))
                return;

            foreach (string llam in lista)
            {
                if (!cadenaInvocaciones.Contains(llam))
                    cadenaInvocaciones.Add(llam); 
                else
                    throw new Exception(String.Format("Se ha detectado una llamada circular en {0}", llam));

                if (this.llamadas.ContainsKey(llam))  
                    if (cadenaInvocaciones.Contains(invocador))
                        throw new Exception(String.Format("Se ha detectado una llamada circular en {0} ({1})", llamador, invocador));
                    else
                        this.ValidarLoopsRecursivo(this.llamadas[llam], invocador, cadenaInvocaciones, llam);
            }
        }            
    }
}
