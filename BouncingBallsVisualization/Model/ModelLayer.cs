using BouncingBalls.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BouncingBalls.Data
{
    public class ModelLayer
    {
        public int StartingBalls { get; set; }
        public Canvas canvas;

        public ModelLayer(int width, int height, LogicAbstractAPI bussinesLayer = null)
        {
            data = bussinesLayer ?? LogicAbstractAPI.CreateLayer(width, height);
            StartingBalls = 1;
            elipses = new List<Ellipse>();
        }

        public void CreateBall()
        {
            Ball ball = (Ball)data.Create();
            data.Add(ball);

            Ellipse newEllipse = new Ellipse { Width = ball.Radius * 2, Height = ball.Radius * 2, Fill = Brushes.Brown, StrokeThickness = 3, Stroke = Brushes.Black };
            elipses.Add(newEllipse);

            Canvas.SetLeft(newEllipse, ball.X);
            Canvas.SetTop(newEllipse, ball.Y);

            canvas.Children.Add(newEllipse);
        }
        public void CreateBalls()
        {
            for (int i = 0; i < StartingBalls; i++)
                CreateBall();
        }

        private readonly LogicAbstractAPI data = default(LogicAbstractAPI);
        private readonly List<Ellipse> elipses;
    }
}
