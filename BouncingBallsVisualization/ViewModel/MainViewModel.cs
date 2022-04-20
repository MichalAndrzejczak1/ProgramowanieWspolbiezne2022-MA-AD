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
        public int StartingBalls { get => MyModel.StartingBalls; set => MyModel.StartingBalls = value; }
        public bool OKIsEnabled { get => MyModel.OKIsEnabled; set { MyModel.OKIsEnabled = value; RaisePropertyChanged(); } }
        public bool NewBallIsEndabled { get => MyModel.NewBallIsEndabled; set { MyModel.NewBallIsEndabled = value; RaisePropertyChanged(); } }
        public bool StartIsEndabled { get => MyModel.StartIsEndabled; set {MyModel.StartIsEndabled = value; RaisePropertyChanged(); } }
        public bool StopIsEndabled { get => MyModel.StopIsEndabled; set {MyModel.StopIsEndabled = value; RaisePropertyChanged(); } }

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


        public RelayCommand AddBalls { protected get; set; }
        public RelayCommand AddBall { protected get; set; }
        public RelayCommand StartMovement { protected get; set; }
        public RelayCommand StopMovement { protected get; set; }

        public void SetCanvas(Canvas canvas)
        {
            MyModel.canvas = canvas;
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


        #region Private stuff
        private ModelLayer MyModel { get; set; }

        private void CreateBalls()
        {
            MyModel.CreateBalls();
            OKIsEnabled = false;
            NewBallIsEndabled = true;
        }
        #endregion
    }
}
