using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using BouncingBalls.Data;

namespace BouncingBalls.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public Canvas canvas;

        public MainViewModel()
        {
            MyModel = new ModelLayer();
            AddBall = new RelayCommand(CreateBall);
            AddBalls = new RelayCommand(CreateBalls);
            StartingBalls = 1;
        }

        public RelayCommand AddBalls { protected get; set; }
        public RelayCommand AddBall { protected get; set; }

        #region Private stuff
        private ModelLayer MyModel { get; set; }
        public int StartingBalls { get; set; }


        private void CreateBall()
        {
            Random r = new Random();
            int ray = r.Next(10, 25);
            Ellipse newEllipse = new Ellipse { Width = ray * 2, Height = ray * 2, Fill = Brushes.Brown, StrokeThickness = 3, Stroke = Brushes.Black };

            double x = newEllipse.ActualWidth + r.NextDouble() * (canvas.ActualWidth - newEllipse.ActualWidth) * 0.9;
            double y = newEllipse.ActualHeight + r.NextDouble() * (canvas.ActualHeight - newEllipse.ActualHeight) * 0.9;

            Canvas.SetLeft(newEllipse, x);
            Canvas.SetTop(newEllipse, y);
            Console.WriteLine(x);
            Console.WriteLine(y);

            canvas.Children.Add(newEllipse);
        }

        private void CreateBalls()
        {
            for (int i = 0; i < StartingBalls; i++)
                CreateBall();
        }
        #endregion
    }
}
