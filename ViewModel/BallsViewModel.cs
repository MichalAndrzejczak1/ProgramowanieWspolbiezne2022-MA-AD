using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;


namespace ViewModel
{
    public class BallsViewModel : INotifyPropertyChanged
    {
        public BallModel Board { get; set; }


        public BallsViewModel()
        {

        }

        public ICommand AddBalls { protected get; set; }
        public ICommand AddBall { protected get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
