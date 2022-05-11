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
    /// <summary>
    /// Warstwa modelu dla Widoku.
    /// </summary>
    public class ModelLayer
    {
        /// <summary>
        /// Liczba początkowych kul.
        /// </summary>
        public int StartingBalls { get => startingBalls; set => startingBalls = value; }
        /// <summary>
        /// Stan aktywności przycisku OK.
        /// </summary>
        public bool OkIsEnabled { get => okIsEnabled; set => okIsEnabled = value; }
        /// <summary>
        /// Stan aktywności przycisku New Ball.
        /// </summary>
        public bool NewBallIsEndabled { get => newBallIsEndabled; set => newBallIsEndabled = value; }
        /// <summary>
        /// Stan aktywności przycisku Start.
        /// </summary>
        public bool StartIsEndabled { get => startIsEndabled; set => startIsEndabled = value; }
        /// <summary>
        /// Stan aktywności przycisku Stop.
        /// </summary>
        public bool StopIsEndabled { get => stopIsEndabled; set => stopIsEndabled = value; }
        public Canvas Canvas { get => canvas; set => canvas = value;  }

        /// <summary>
        /// Płotno na którym są rysowane elipsy.
        /// </summary>
        private Canvas canvas;
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="width">Szerokość obszaru po którym poruszają się kule.</param>
        /// <param name="height">Wysokość obszaru po którym poruszają się kule.</param>
        /// <param name="api">API logiki.</param>
        public ModelLayer(int width, int height, LogicAbstractApi api = null)
        {
            logicApi = api ?? LogicAbstractApi.CreateLayer(width, height);
            logicApi.CordinatesChanged += (sender, args) => UpdateElipsesCords();
            ellipses = new List<Ellipse>();
            Canvas = new Canvas();
            Canvas.Width = 770;
            Canvas.Height = 500;
            Canvas.Background = new SolidColorBrush(Color.FromRgb(36, 156, 82));
        }
        /// <summary>
        /// Tworzy nową kulę.
        /// </summary>
        public void CreateBall()
        {
            int ballNumer = logicApi.Add();
            double radius = LogicAbstractApi.GetBallRadius(logicApi.Get(ballNumer));
            double x = logicApi.GetX(ballNumer);
            double y = logicApi.GetY(ballNumer);

            Ellipse newEllipse = new Ellipse { Width = radius * 2, Height = radius * 2, Fill = Brushes.Brown, StrokeThickness = 3, Stroke = Brushes.Black };
            ellipses.Add(newEllipse);

            Canvas.SetLeft(newEllipse, x);
            Canvas.SetTop(newEllipse, y);

            Canvas.Children.Add(newEllipse);
        }
        /// <summary>
        /// Tworzy kilka kul na raz.
        /// </summary>
        public void CreateBalls()
        {
            for (int i = 0; i < startingBalls; i++)
                CreateBall();
        }
        /// <summary>
        /// Uruchamia ruch kul.
        /// </summary>
        public void Start() => logicApi.Start();
        /// <summary>
        /// Wstrzymuje ruch kul.
        /// </summary>
        public void Stop() => logicApi.Stop();


        #region Private stuff
        /// <summary>
        /// Aktualizuje położenie elips na podstawie danych w warstwy logiki.
        /// </summary>
        private void UpdateElipsesCords()
        {
            for (int i = 0; i < logicApi.Count(); i++)
            {
                Canvas.SetLeft(ellipses[i], logicApi.Get(i).X);
                Canvas.SetTop(ellipses[i], logicApi.Get(i).Y);
            }
            //Trace.WriteLine("Works 2!");
        }

        /// <summary>
        /// API logiki.
        /// </summary>
        private readonly LogicAbstractApi logicApi = default(LogicAbstractApi);
        /// <summary>
        /// Lista elips na płótnie.
        /// </summary>
        private readonly List<Ellipse> ellipses;
        /// <summary>
        /// Liczba początkowych kul.
        /// </summary>
        private int startingBalls = 1;
        /// <summary>
        /// Aktywność przycisku OK.
        /// </summary>
        private bool okIsEnabled = true;
        /// <summary>
        /// Aktywność przycisku New Ball.
        /// </summary>
        private bool newBallIsEndabled = true;
        /// <summary>
        /// Aktywność przycisku Start.
        /// </summary>
        private bool startIsEndabled = true;
        /// <summary>
        /// Aktywność przycisku Stop.
        /// </summary>
        private bool stopIsEndabled = false;
        #endregion Private stuff
    }
}
