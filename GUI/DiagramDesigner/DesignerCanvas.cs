using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using System.Text.RegularExpressions;

namespace DiagramDesigner
{
    public partial class DesignerCanvas : Canvas
    {
        private Point? rubberbandSelectionStartPoint = null;
        private List<object> objetosIntersecados;

        private SelectionService selectionService;
        internal SelectionService SelectionService
        {
            get
            {
                if (selectionService == null)
                    selectionService = new SelectionService(this);

                return selectionService;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                // in case that this click is the start of a 
                // drag operation we cache the start point
                this.rubberbandSelectionStartPoint = new Point?(e.GetPosition(this));

                // if you click directly on the canvas all 
                // selected items are 'de-selected'
                SelectionService.ClearSelection();
                Focus();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                this.rubberbandSelectionStartPoint = null;

            // ... but if mouse button is pressed and start
            // point value is set we do have one
            if (this.rubberbandSelectionStartPoint.HasValue)
            {
                // create rubberband adorner
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    RubberbandAdorner adorner = new RubberbandAdorner(this, rubberbandSelectionStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }
            }
            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {            
            base.OnDrop(e);
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
            if (dragObject != null && !String.IsNullOrEmpty(dragObject.Xaml))
            {
                DesignerItem newItem = null;
                Object content = XamlReader.Load(XmlReader.Create(new StringReader(dragObject.Xaml)));

                Regex rg = new Regex("<Path ToolTip=\"([A-Za-z]+)\"");
                Match mc = rg.Match(dragObject.Xaml, 0, 50);
                string tp = String.Empty;
                if (mc.Success)
                    tp = mc.Groups[mc.Groups.Count - 1].Value;

                if (content != null)
                {
                    newItem = new DesignerItem();
                    newItem.AsignarTipoElemento(tp);
                    newItem.Content = content;

                    Point position = e.GetPosition(this);

                    if (dragObject.DesiredSize.HasValue)
                    {
                        Size desiredSize = dragObject.DesiredSize.Value;
                        newItem.Width = desiredSize.Width;
                        newItem.Height = desiredSize.Height;

                        DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2));
                        DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2));
                    }
                    else
                    {
                        DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X));
                        DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y));
                    }

                    Canvas.SetZIndex(newItem, this.Children.Count);
                    this.Children.Add(newItem);                    
                    SetConnectorDecoratorTemplate(newItem);

                    //update selection
                    this.SelectionService.SelectItem(newItem);
                    newItem.Focus();

                    this.ElementoActual = newItem;

                    double escala = ((System.Windows.Media.ScaleTransform)(this.LayoutTransform)).ScaleX;

                    double x = DesignerCanvas.GetLeft(newItem) * escala;
                    double y = DesignerCanvas.GetTop(newItem) * escala;
                    System.Windows.Rect r = new System.Windows.Rect(new Point(x+(20 * escala), y+(20 * escala)),
                        new Size(newItem.Width * escala, newItem.Height * escala));

                    this.objetosIntersecados = new List<object>();
                    System.Windows.Media.RectangleGeometry eGeo = new System.Windows.Media.RectangleGeometry(r);
                    System.Windows.Media.VisualTreeHelper.HitTest(((ScrollViewer)this.Parent), null, new System.Windows.Media.HitTestResultCallback(HitTestCallback),
                        new System.Windows.Media.GeometryHitTestParameters(eGeo));                          
                }

                e.Handled = true;

                //MessageBox.Show("Conexiones Intersecadas: " + objetosIntersecados.Count.ToString());
                if (objetosIntersecados.Count > 0)
                    ReasignarConexion((Connection)objetosIntersecados[0]);
            }
        }

        private DesignerItem ElementoActual;

        private System.Windows.Media.HitTestResultBehavior HitTestCallback(System.Windows.Media.HitTestResult htrResult)
        {
            System.Windows.Media.IntersectionDetail idDetail = ((System.Windows.Media.GeometryHitTestResult)htrResult).IntersectionDetail;
            switch (idDetail)
            {
                case System.Windows.Media.IntersectionDetail.FullyContains:
                    return System.Windows.Media.HitTestResultBehavior.Continue;
                case System.Windows.Media.IntersectionDetail.Intersects:
                    object obj = ((System.Windows.FrameworkElement)(((System.Windows.Media.GeometryHitTestResult)(htrResult)).VisualHit)).DataContext;
                    if (obj != null)
                        if (obj is Connection)
                            if (!this.objetosIntersecados.Contains(obj))
                                this.objetosIntersecados.Add(obj);
                    return System.Windows.Media.HitTestResultBehavior.Continue;
                case System.Windows.Media.IntersectionDetail.FullyInside:
                    return System.Windows.Media.HitTestResultBehavior.Continue;
                default:
                    return System.Windows.Media.HitTestResultBehavior.Stop;
            }
        }

        private void ReasignarConexion(Connection conn)
        {
            DesignerItem origen = conn.Source.ParentDesignerItem;
            DesignerItem destino = conn.Sink.ParentDesignerItem;
            Connector c = new Connector();
            c.ParentDesignerItem = this.ElementoActual;
            c.Position = new Point(DesignerCanvas.GetLeft(this.ElementoActual), DesignerCanvas.GetTop(this.ElementoActual));
            c.Orientation = ConnectorOrientation.Left;

            conn.Sink = c;
        }
        
        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();

            foreach (UIElement element in this.InternalChildren)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                //measure desired size for each child
                element.Measure(constraint);

                Size desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }
            // add margin 
            size.Width += 10;
            size.Height += 10;
            return size;
        }

        private void SetConnectorDecoratorTemplate(DesignerItem item)
        {
            if (item.ApplyTemplate() && item.Content is UIElement)
            {
                ControlTemplate template = DesignerItem.GetConnectorDecoratorTemplate(item.Content as UIElement);
                Control decorator = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
                if (decorator != null && template != null)
                    decorator.Template = template;
            }
        }
    }
}
