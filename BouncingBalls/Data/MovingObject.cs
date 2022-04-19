using System;
using System.Collections.Generic;
using System.Text;

namespace BouncingBalls.Data
{
    public abstract class MovingObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double SpeedX { get; set; }
        public double SpeedY { get; set; }

        public abstract void Move(double timeDelta);
    }
}
