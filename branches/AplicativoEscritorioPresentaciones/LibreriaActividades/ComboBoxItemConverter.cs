using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Activities.Presentation.Model;
using System.Activities;
using System.Activities.Expressions;
using Microsoft.VisualBasic.Activities;

namespace LibreriaActividades
{
    public class ComboBoxItemConverter :  IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            eTipoVariable valor = (eTipoVariable)value;
            switch (valor)
            {
                case eTipoVariable.Booleano:
                    return "Booleano";
                case eTipoVariable.Numero:
                    return "Numero";
                case eTipoVariable.Texto:
                    return "Texto";
                case eTipoVariable.Vector:
                    return "Vector";
            }
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string valor = value as string;
            if (valor != null)
            {
                switch (valor)
                {
                    case "Booleano":
                        return eTipoVariable.Booleano;
                    case "Numero":
                        return eTipoVariable.Numero;
                    case "Texto":
                        return eTipoVariable.Texto;
                    case "Vector":
                        return eTipoVariable.Vector;
                }
            }
            return null;
        }
    }
}
