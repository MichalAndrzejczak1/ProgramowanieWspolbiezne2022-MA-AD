using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using BouncingBalls.Data;
using BouncingBalls.Logic;

namespace BouncingBallsVisualization.Model
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
        /// <summary>
        /// Płótno na którym są rysowane kule.
        /// </summary>
        public Canvas Canvas { get => canvas; set => canvas = value;  }

        /// <summary>
        /// Płotno na którym są rysowane elipsy.
        /// </summary>
        private Canvas canvas;
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="api">API logiki.</param>
        public ModelLayer( LogicAbstractApi api = null)
        {
            logicApi = api ?? LogicAbstractApi.CreateLayer();
            logicApi.PropertyChanged += UpdateElipsesCords;
            ellipses = new List<Ellipse>();
            Canvas = new Canvas();
            Canvas.Width = logicApi.GetBoardWidth();
            Canvas.Height = logicApi.GetBoardHeight();
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
            if (logicApi.IsRunning())
            {
                logicApi.Stop();
                logicApi.Start();
            }
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
        private void UpdateElipsesCords(object sender, PropertyChangedEventArgs args)
        {
            MovingBall ball = (MovingBall)sender;
            Canvas.SetLeft(ellipses[ball.Id], ball.X);
            Canvas.SetTop(ellipses[ball.Id], ball.Y);
                //Trace.WriteLine("Works 2!");
        }

        /// <summary>
        /// API logiki.
        /// </summary>
        private readonly LogicAbstractApi logicApi;
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
        private bool stopIsEndabled;
        #endregion Private stuff
    }
}
