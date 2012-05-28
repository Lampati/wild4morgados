using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace LibreriaActividades
{
    public class ComboBoxVectorItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            eTipoVector valor = (eTipoVector)value;
            switch (valor)
            {
                case eTipoVector.Booleano:
                    return "Booleano";
                case eTipoVector.Numero:
                    return "Numero";
                case eTipoVector.Texto:
                    return "Texto";
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
                        return eTipoVector.Booleano;
                    case "Numero":
                        return eTipoVector.Numero;
                    case "Texto":
                        return eTipoVector.Texto;
                }
            }
            return null;
        }
    }
}
