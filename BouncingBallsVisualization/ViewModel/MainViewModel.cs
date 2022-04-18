using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BouncingBalls.Data;

namespace BouncingBalls.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {

        }

        public MainViewModel(ModelLayer model = default(ModelLayer))
        {
            MyModel = model ?? new ModelLayer();
        }

        private ModelLayer MyModel { get; set; }


        public ICommand AddBalls { protected get; set; }
        public ICommand AddBall { protected get; set; }
    }
}
