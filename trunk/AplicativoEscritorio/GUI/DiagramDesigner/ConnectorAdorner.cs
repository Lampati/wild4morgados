using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System;

namespace Ragnarok
{
    public class ConnectorAdorner : Adorner
    {
        private PathGeometry pathGeometry;
        private DesignerCanvas designerCanvas;
        private Connector sourceConnector;
        private Pen drawingPen;

        private DesignerItem hitDesignerItem;
        private DesignerItem HitDesignerItem
        {
            get { return hitDesignerItem; }
            set
            {
                if (hitDesignerItem != value)
                {
                    if (hitDesignerItem != null)
                        hitDesignerItem.IsDragConnectionOver = false;

                    hitDesignerItem = value;

                    if (hitDesignerItem != null)
                        hitDesignerItem.IsDragConnectionOver = true;
                }
            }
        }

        private Connector hitConnector;
        private Connector HitConnector
        {
            get { return hitConnector; }
            set
            {
                if (hitConnector != value)
                {
                    hitConnector = value;
                }
            }
        }

        public ConnectorAdorner(DesignerCanvas designer, Connector sourceConnector)
            : base(designer)
        {
            this.designerCanvas = designer;
            this.sourceConnector = sourceConnector;
            drawingPen = new Pen(Brushes.LightSlateGray, 1);
            drawingPen.LineJoin = PenLineJoin.Round;
            this.Cursor = Cursors.Cross;
        }

        private bool ValidarConexiones(Connector source)
        {
            bool ok = true;
            if (source.ParentDesignerItem.Connections.Count > 0)
            {
                if (source.ParentDesignerItem.TipoElemento != Enums.TipoElemento.Condicional)
                {
                    MessageBox.Show("Un elemento no puede tener más de 1 conexión");
                    ok = false;
                }
                else
                {
                    if (source.ParentDesignerItem.Connections.Count > 1)
                    {
                        MessageBox.Show("Un elemento condicional no puede tener más de 2 conexiones");
                        ok = false;
                    }
                }
            }

            return ok;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            try
            {
                if (HitConnector != null)
                {
                    Connector sourceConnector = this.sourceConnector;
                    Connector sinkConnector = this.HitConnector;
                    if (ValidarConexiones(sourceConnector))
                    {
                        Connection newConnection = new Connection(sourceConnector, sinkConnector);

                        Canvas.SetZIndex(newConnection, designerCanvas.Children.Count);
                        this.designerCanvas.Children.Add(newConnection);
                    }

                }
                if (HitDesignerItem != null)
                {
                    this.HitDesignerItem.IsDragConnectionOver = false;
                }

                if (this.IsMouseCaptured) this.ReleaseMouseCapture();

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.designerCanvas);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured) this.CaptureMouse();
                HitTesting(e.GetPosition(this));
                this.pathGeometry = GetPathGeometry(e.GetPosition(this));
                this.InvalidateVisual();
            }
            else
            {
                if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawGeometry(null, drawingPen, this.pathGeometry);

            // without a background the OnMouseMove event would not be fired
            // Alternative: implement a Canvas as a child of this adorner, like
            // the ConnectionAdorner does.
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));
        }

        private PathGeometry GetPathGeometry(Point position)
        {
            PathGeometry geometry = new PathGeometry();

            ConnectorOrientation targetOrientation;
            if (HitConnector != null)
                targetOrientation = HitConnector.Orientation;
            else
                targetOrientation = ConnectorOrientation.None;

            List<Point> pathPoints = PathFinder.GetConnectionLine(sourceConnector.GetInfo(), position, targetOrientation);

            if (pathPoints.Count > 0)
            {
                PathFigure figure = new PathFigure();
                figure.StartPoint = pathPoints[0];
                pathPoints.Remove(pathPoints[0]);
                figure.Segments.Add(new PolyLineSegment(pathPoints, true));
                geometry.Figures.Add(figure);
            }

            return geometry;
        }

        private void HitTesting(Point hitPoint)
        {
            bool hitConnectorFlag = false;

            DependencyObject hitObject = designerCanvas.InputHitTest(hitPoint) as DependencyObject;
            while (hitObject != null &&
                   hitObject != sourceConnector.ParentDesignerItem &&
                   hitObject.GetType() != typeof(DesignerCanvas))
            {
                if (hitObject is Connector)
                {
                    HitConnector = hitObject as Connector;
                    hitConnectorFlag = true;
                }

                if (hitObject is DesignerItem)
                {
                    HitDesignerItem = hitObject as DesignerItem;
                    if (!hitConnectorFlag)
                        HitConnector = null;
                    return;
                }
                hitObject = VisualTreeHelper.GetParent(hitObject);
            }

            HitConnector = null;
            HitDesignerItem = null;
        }
    }
}
