using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.UserControls.Entorno
{
    public class CambiarTamanioEventArgs
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public CambiarTamanioEventArgs(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }
}
