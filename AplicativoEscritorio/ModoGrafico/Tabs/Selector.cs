using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using ModoGrafico.ViewModels;

namespace ModoGrafico.Tabs
{
    public class Selector : StyleSelector
    {
        public Style SinEstilo { get; set; }
        public Style SoloPropiedadesEstilo { get; set; }
        public Style ConEstilo { get; set; }

        public override Style SelectStyle(object item,
          DependencyObject container)
        {
            Tab tab = item as Tab;

            if (tab != null)
            {
                if (tab.Header != null)
                {
                    string path = tab.Header.ToString().Trim();
                    //string path = item.GetType().GetProperty("Header").GetValue(item, null).ToString().Trim();
                    if (path == "+" || path == "PROC +" || path == "FUNC +" || path == "PRINCIPAL" || path == "CONSTANTES" || path == "VARIABLES" )
                    {
                        return SinEstilo;
                    }
                    else if (path == "SALIDA")
                    {
                        return SoloPropiedadesEstilo;
                    }
                }
            }
            return ConEstilo;
            
        }
    }
}
