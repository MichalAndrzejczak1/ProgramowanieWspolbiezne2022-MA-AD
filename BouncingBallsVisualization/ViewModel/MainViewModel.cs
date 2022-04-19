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
        public MainViewModel()
        {
            MyModel = new ModelLayer(770, 500);
            AddBall = new RelayCommand(MyModel.CreateBall);
            AddBalls = new RelayCommand(MyModel.CreateBalls);
        }

        public RelayCommand AddBalls { protected get; set; }
        public RelayCommand AddBall { protected get; set; }

        public void SetCanvas(Canvas canvas)
        {
            MyModel.canvas = canvas;
        }

        #region Private stuff
        public ModelLayer MyModel { get; set; }

        #endregion
    }
}
