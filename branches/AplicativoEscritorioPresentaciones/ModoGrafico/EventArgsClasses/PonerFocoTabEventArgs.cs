using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModoGrafico.Enums;
using ModoGrafico.ViewModels;

namespace ModoGrafico.EventArgsClasses
{
    public class PonerFocoTabEventArgs
    {
        

        private Tab tab;
        public Tab Tab
        {
            get
            {
                return tab;
            }
        }


        public PonerFocoTabEventArgs(Tab t)
        {
            tab = t;
        }
    }
}
