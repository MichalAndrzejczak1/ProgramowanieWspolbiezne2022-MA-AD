using BouncingBalls.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BouncingBalls.Data
{
    public class ModelLayer
    {
        public int StartingBalls { get => startingBalls; set => startingBalls = value; }
        public bool OKIsEnabled { get => okIsEnabled; set => okIsEnabled = value; }
        public bool NewBallIsEndabled { get => newBallIsEndabled; set => newBallIsEndabled = value; }
        public bool StartIsEndabled { get => startIsEndabled; set => startIsEndabled = value; }
        public bool StopIsEndabled { get => stopIsEndabled; set => stopIsEndabled = value; }

        public Canvas canvas;

        public ModelLayer(int width, int height, LogicAbstractAPI api = null)
        {
            data = api ?? LogicAbstractAPI.CreateLayer(width, height);
            data.CordinatesChanged += (sender, args) => UpdateElipsesCords();
            ellipses = new List<Ellipse>();
        }

        public void CreateBall()
        {
            MovingObject ball = data.Create();
            data.Add(ball);
            double radius = LogicAbstractAPI.GetBallRadius(ball);

            Ellipse newEllipse = new Ellipse { Width = radius * 2, Height = radius * 2, Fill = Brushes.Brown, StrokeThickness = 3, Stroke = Brushes.Black };
            ellipses.Add(newEllipse);

            Canvas.SetLeft(newEllipse, ball.X);
            Canvas.SetTop(newEllipse, ball.Y);

            canvas.Children.Add(newEllipse);
            //data.Update(1000);
        }
        public void CreateBalls()
        {
            for (int i = 0; i < startingBalls; i++)
                CreateBall();
        }

        public void Start() => data.Start();
        public void Stop() => data.Stop();


        #region Private stuff
        private void UpdateElipsesCords()
        {
            for (int i = 0; i < data.Count(); i++)
            {
                Canvas.SetLeft(ellipses[i], data.Get(i).X);
                Canvas.SetTop(ellipses[i], data.Get(i).Y);
            }
            Trace.WriteLine("Works 2!");
        }

        private void Update() => data.Update(1000);

        private readonly LogicAbstractAPI data = default(LogicAbstractAPI);
        private readonly List<Ellipse> ellipses;
        private int startingBalls = 1;
        private bool okIsEnabled = true;
        private bool newBallIsEndabled = true;
        private bool startIsEndabled = true;
        private bool stopIsEndabled = false;
        #endregion Private stuff
    }
}
