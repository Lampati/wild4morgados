using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace FormattedTabControl
{
    public class Selector : StyleSelector
    {
        public Style SinEstilo { get; set; }
        public Style ConEstilo { get; set; }

        public override Style SelectStyle(object item,
          DependencyObject container)
        {
            string path = item.GetType().GetProperty("Header").GetValue(item, null).ToString().Trim();
            if (path == "+" || path == "PRINCIPAL" || path == "CONSTANTES" || path == "VARIABLES")
                return SinEstilo;
            return ConEstilo;
            
        }
    }
}
