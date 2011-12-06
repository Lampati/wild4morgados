using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compilador.Semantico.Arbol.Labels
{
    public sealed class ManagerLabels
    {

        static ManagerLabels instance = null;
        static readonly object padlock = new object();

        public List<CodeLabel> listaLabels;

        public ManagerLabels()
        {
            listaLabels = new List<CodeLabel>();
        }

        public static ManagerLabels Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ManagerLabels();
                    }
                    return instance;
                }
            }
        }

        public CodeLabel CrearNuevoLabel(string tipo)
        {
            CodeLabel lab = new CodeLabel(new StringBuilder("label").Append(tipo).Append(this.listaLabels.Count.ToString()).ToString());

            listaLabels.Add(lab);

            return lab;
        }

    }
}
