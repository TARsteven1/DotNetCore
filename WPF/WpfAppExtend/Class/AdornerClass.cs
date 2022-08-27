using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//必要引用
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfAppExtend
{
    //装饰器
    public class AdornerClass : Adorner
    {
        public AdornerClass(UIElement adornedElement)
    : base(adornedElement)
        {

        }
        // A common way to implement an adorner's rendering behavior is to override the OnRender
        // method, which is called by the layout system as part of a rendering pass.
        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);

            //// Some arbitrary drawing implements.
            //SolidColorBrush renderBrush = new SolidColorBrush(Colors.Green);
            //renderBrush.Opacity = 0.2;
            //Pen renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
            //double renderRadius = 5.0;

            //// Draw a circle at each corner.
            //drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
            //drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
            //drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
            //drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Red), 1.0);

            // Draw a circle at each corner.
            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopLeft.X - 3, adornedElementRect.TopLeft.Y - 3), new Point(adornedElementRect.TopLeft.X + 5, adornedElementRect.TopLeft.Y - 3));
            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopLeft.X - 3, adornedElementRect.TopLeft.Y - 3), new Point(adornedElementRect.TopLeft.X - 3, adornedElementRect.TopLeft.Y + 5));

            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopRight.X + 3, adornedElementRect.TopRight.Y - 3), new Point(adornedElementRect.TopRight.X - 5, adornedElementRect.TopRight.Y - 3));
            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.TopRight.X + 3, adornedElementRect.TopRight.Y - 3), new Point(adornedElementRect.TopRight.X + 3, adornedElementRect.TopRight.Y + 5));

            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomLeft.X - 3, adornedElementRect.BottomLeft.Y + 3), new Point(adornedElementRect.BottomLeft.X + 5, adornedElementRect.BottomLeft.Y + 3));
            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomLeft.X - 3, adornedElementRect.BottomLeft.Y + 3), new Point(adornedElementRect.BottomLeft.X - 3, adornedElementRect.BottomLeft.Y - 5));

            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomRight.X + 3, adornedElementRect.BottomRight.Y + 3), new Point(adornedElementRect.BottomRight.X - 5, adornedElementRect.BottomRight.Y + 3));
            drawingContext.DrawLine(renderPen, new Point(adornedElementRect.BottomRight.X + 3, adornedElementRect.BottomRight.Y + 3), new Point(adornedElementRect.BottomRight.X + 3, adornedElementRect.BottomRight.Y - 5));

        }
    }

}
