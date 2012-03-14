using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    internal abstract class TipoBase
    {
        protected List<Terminal> listaLineaEntera = new List<Terminal>();

        public List<Terminal> ListaLineaEntera
        {
            get
            {
                return listaLineaEntera;
            }            
        }



        public void Validar()
        {

        }


    }
}
