using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using BouncingBalls.Data;

namespace BouncingBalls.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Liczba początkowych kul.
        /// </summary>
        public int StartingBalls { get => MyModel.StartingBalls; set => MyModel.StartingBalls = value; }
        /// <summary>
        /// Stan aktywności przycisku OK.
        /// </summary>
        public bool OKIsEnabled { get => MyModel.OKIsEnabled; set { MyModel.OKIsEnabled = value; RaisePropertyChanged(); } }
        /// <summary>
        /// Stan aktywności przycisku New Ball.
        /// </summary>
        public bool NewBallIsEndabled { get => MyModel.NewBallIsEndabled; set { MyModel.NewBallIsEndabled = value; RaisePropertyChanged(); } }
        /// <summary>
        /// Stan aktywności przycisku Start.
        /// </summary>
        public bool StartIsEndabled { get => MyModel.StartIsEndabled; set {MyModel.StartIsEndabled = value; RaisePropertyChanged(); } }
        /// <summary>
        /// Stan aktywności przycisku Stop.
        /// </summary>
        public bool StopIsEndabled { get => MyModel.StopIsEndabled; set {MyModel.StopIsEndabled = value; RaisePropertyChanged(); } }

        public Canvas Canvas { get => MyModel.Canvas; set { MyModel.Canvas = value; RaisePropertyChanged(); }  }
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public MainViewModel()
        {
            MyModel = new ModelLayer(770, 500);
            AddBall = new RelayCommand(MyModel.CreateBall);
            AddBalls = new RelayCommand(CreateBalls);
            StartMovement = new RelayCommand(Start);
            StopMovement = new RelayCommand(Stop);

            StartIsEndabled = true;
            StopIsEndabled = false;
            NewBallIsEndabled = false;
            OKIsEnabled = true;
        }

        /// <summary>
        /// Polecenie dodawania startowych kul.
        /// </summary>
        public RelayCommand AddBalls { protected get; set; }
        /// <summary>
        /// Polecenie dodawania jednej kuli.
        /// </summary>
        public RelayCommand AddBall { protected get; set; }
        /// <summary>
        /// Polecenie rozpoczęcia ruchu kul.
        /// </summary>
        public RelayCommand StartMovement { protected get; set; }
        /// <summary>
        /// Polecenie zatrzymania ruchu kul.
        /// </summary>
        public RelayCommand StopMovement { protected get; set; }

        /// <summary>
        /// Ustala obecne płótno.
        /// </summary>
        /// <param name="canvas"></param>
        public void SetCanvas(Canvas canvas)
        {
            MyModel.Canvas = canvas;
        }

        public void Start()
        {
            MyModel.Start();
            StartIsEndabled = false;
            StopIsEndabled = true;
        }
        public void Stop()
        {
            MyModel.Stop();
            StartIsEndabled = true;
            StopIsEndabled = false;
        }

        public void Pause()
        {
            MyModel.Stop();
            StartIsEndabled = true;
            StopIsEndabled = false;
        }


        #region Private stuff
        private ModelLayer MyModel { get; set; }

        private void CreateBalls()
        {
            MyModel.CreateBalls();
            OKIsEnabled = false;
            NewBallIsEndabled = true;
        }
        #endregion Private stuff
    }
}
