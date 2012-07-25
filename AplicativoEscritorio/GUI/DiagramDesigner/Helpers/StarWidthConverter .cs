using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;

namespace DiagramDesigner.Helpers
{
    public class StarWidthConverter : IValueConverter
    {
        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    ListView listview = value as ListView;
        //    double width;

        //    //if (listview.Width == Double.NaN)
        //    //{
        //    //    width = ((DiagramDesigner.BarraMensajes)((System.Windows.Controls.Grid)listview.Parent).Parent).ActualWidth;
        //    //}
        //    //else
        //    //{            
        //    //    width = listview.Width;
        //    //}

        //    width = ((DiagramDesigner.UserControls.Mensajes.BarraMensajes)((System.Windows.Controls.Grid)listview.Parent).Parent).ActualWidth;
            
        //    GridView gv = listview.View as GridView;
        //    for (int i = 0; i < gv.Columns.Count-1; i++)
        //    {
        //            width -= gv.Columns[i].Width;
        //    }
        //    //return width - 15;// this is to take care of margin/padding
        //    return width ;
        //}

        public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            ListView listView = value as ListView;
            GridView grdView = listView.View as GridView;
            int minWidth = 0;
            bool widthIsPercentage = parameter != null && !int.TryParse(parameter.ToString(), out minWidth);
            if (widthIsPercentage)
            {
                string widthParam = parameter.ToString();
                double percentage = double.Parse(widthParam.Substring(0, widthParam.Length - 1));
                return listView.ActualWidth * percentage;
            }
            else
            {
                double total = 0;
                for (int i = 0; i < grdView.Columns.Count - 1; i++)
                {
                    total += grdView.Columns[i].ActualWidth;
                }
                double remainingWidth = listView.ActualWidth - total;
                if (remainingWidth > minWidth)
                { // fill the remaining width in the ListView
                    return remainingWidth;
                }
                else
                { // fill remaining space with MinWidth
                    return minWidth;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
