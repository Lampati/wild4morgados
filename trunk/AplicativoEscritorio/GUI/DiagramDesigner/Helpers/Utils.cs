using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Windows.Controls.Ribbon;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ragnarok.Helpers
{

    class Utils
    {
        public static void DisplayTemplateControl(DependencyObject element)
        {
            DisplayRecursivelyTemplateControl(element, 0);
        }

        protected static void DisplayRecursivelyTemplateControl(DependencyObject element, int level)
        {
            // get the type of the element
            string objType = element.GetType().ToString().Split('.').Last();

            // display indentation spaces
            for (int i = 0; i < level; i++)
            {
                Debug.Write("\t");
            }
            // display starting tag
            Debug.WriteLine(string.Format("<{0}>", objType));

            // visit children recursively
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                DisplayRecursivelyTemplateControl(VisualTreeHelper.GetChild(element, i), level + 1);
            }

            // display indentation spaces
            for (int i = 0; i < level; i++)
            {
                Debug.Write("\t");
            }
            // display ending tag
            Debug.WriteLine(string.Format("</{0}>", objType));
        }

        public static double GetHorizontalSpace(Thickness thickness)
        {
            return thickness.Left + thickness.Right;
        }

        /// <summary>
        /// Tries to find the first occurence of the specified type (T) inside the specified element's template. Returns null if no control of that type was found.
        /// </summary>
        /// <typeparam name="T">The type of the inner control you are looking for</typeparam>
        /// <param name="element">The dependency object in which template you want to search</param>
        /// <param name="includeCurrentElement">A boolean value indicating that the current element (actually starting element) should be included or not in the search</param>
        /// <returns></returns>
        public static T FindTemplateControl<T>(DependencyObject element, bool includeCurrentElement) where T : DependencyObject
        {
            // if the current element is of type T and we want to include the current element in the search
            // then we found out inner control
            if (element is T && includeCurrentElement)
                return (T)element;

            // otherwise, look recursively in the inner children of the element
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                // we exclude only the starting element from the search, all children are included
                var result = FindTemplateControl<T>(VisualTreeHelper.GetChild(element, i), true);
                if (result != null)
                    return result;
            }

            // if no element was found, return null
            return null;
        }

        public static BitmapImage GetImage(string relativeUri)
        {
            BitmapImage bmp = new BitmapImage();
            try
            {
                bmp.BeginInit();
                bmp.UriSource = new Uri(relativeUri, UriKind.Relative);
                bmp.EndInit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Exception:{0} ... StackTrace:{1}", ex.Message, ex.StackTrace));
            }
            return bmp;
        }

    }
}

