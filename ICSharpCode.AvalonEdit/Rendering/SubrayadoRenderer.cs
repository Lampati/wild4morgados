using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace ICSharpCode.AvalonEdit.Rendering
{
    public class SubrayadoRenderer : IBackgroundRenderer
    {
        private TextEditor _editor;
        private List<int> lineas;

        public void AgregarLinea(int linea)
        {
            if (this.lineas == null)
                this.lineas = new List<int>();

            if (!lineas.Contains(linea))
                lineas.Add(linea);
        }

        public void ResetearLineas()
        {
            if (this.lineas == null)
                return;

            if (this.lineas.Count > 0)
                _editor.TextArea.TextView.Redraw();

            this.lineas = new List<int>();            
        }

        public SubrayadoRenderer(TextEditor editor)
        {
            _editor = editor;
        }

        public KnownLayer Layer
        {
            get { return KnownLayer.Background; }
        }

        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (_editor.Document == null || this.lineas == null)
                return;

            textView.EnsureVisualLines();
            foreach (int linea in new List<int>(this.lineas))
            {
                ICSharpCode.AvalonEdit.Document.ISegment currentLine;
                double x;
                try
                {
                    currentLine = _editor.Document.GetLineByNumber(linea);
                    string st = _editor.Document.GetText(currentLine);
                    x = DeterminarStartX(st);
                }
                catch (ArgumentOutOfRangeException) { this.lineas.Remove(linea); continue; }
                foreach (var rect in BackgroundGeometryBuilder.GetRectsForSegment(textView, currentLine))
                {
                    drawingContext.DrawLine(new System.Windows.Media.Pen(
                        new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)), 2),
                        new Point(x, rect.BottomLeft.Y), new Point(rect.BottomRight.X, rect.BottomRight.Y));
                }
            }
        }

        private double DeterminarStartX(string cadena)
        {
            int tabs = cadena.Count(a => a.Equals('\t'));
            int i = 0;
            int cant = 0;
            while (i < cadena.Length)
            {
                if (cadena[i] == ' ')
                {
                    i++;
                    cant++;
                }
                else
                    if (cadena[i] == '\t')
                        i++;
                    else
                        break;
            }

            return cant * 3.30 + tabs * 22.04;
        }
    }
}
